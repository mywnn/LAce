using Antlr4.Runtime;
using System.Collections.Generic;

namespace LAce.Parsing
{
    partial class LAcer
    {
        Queue<IToken> pendingTokens = new Queue<IToken>();
        Stack<int> openedIndentSizes = new Stack<int>();

        public override IToken NextToken()
        {
            if (pendingTokens.Count > 0)
                return pendingTokens.Dequeue();

            var token = base.NextToken();

            // If a newline presents, find the new indentation to see whether the current block should be closed
            // or create a new block
            if (Vocabulary.GetSymbolicName(token.Type) == "NL" || token.Type < 0)
            {
                // The NL token has a space tail which can be used to dertermine the indentation of the new line.
                var indentSize = token.Type > 0 ? token.Text.Length - (token.Text.LastIndexOf('\n') + 1) : 0;
                var lastIndentSize = openedIndentSizes.Count > 0 ? openedIndentSizes.Peek() : 0;

                // If the new identation is larger than the last, start a new block.
                if (indentSize > lastIndentSize)
                {
                    // push a new block opening, nested in the last one.
                    openedIndentSizes.Push(indentSize);
                    pendingTokens.Enqueue(CreateToken("INDENT"));
                }
                // If it is less, close some of the current blocks.
                else if (indentSize < lastIndentSize)
                {
                    openedIndentSizes.Pop();
                    // close all current blocks
                    var currentToken = token;

                    // return a block closing. 
                    token = CreateToken("DEDENT");

                    // if many blocks need to be closed, keep the closing tokens to the queue.
                    while (indentSize < (lastIndentSize = openedIndentSizes.Count > 0 ? openedIndentSizes.Peek() : 0))
                    {
                        openedIndentSizes.Pop();
                        pendingTokens.Enqueue(CreateToken("NL"));
                        pendingTokens.Enqueue(CreateToken("DEDENT"));
                    }
                    // after all closing tag all returned, send the current token.
                    pendingTokens.Enqueue(currentToken);
                }
            }
            
            return 
                token.Type > 0 || pendingTokens.Count == 0
                ? token
                // when the current token is EOF, and the queue is not yet empty.
                // flush all tokens in the queue
                : pendingTokens.Dequeue();
        }

        IToken CreateToken(string tokenName) => TokenFactory.Create(GetTokenType(tokenName), "");
    }
}
