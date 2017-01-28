using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LAce.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAce
{
    class Program
    {


        const string codes =
@"let x: int =           
    
   
    let y: int =
   
        20 + 30

let z: int = 

    let a =
        x + 9 + 20
    
    let b = a + 30

    b + 90

";

        static void Main(string[] args)
        {
            //var codeStream = new AntlrInputStream(codes);
            //var lexer = new LAcer(codeStream);

            //foreach (var token in Seq(lexer.NextToken).TakeWhile(x => x.Type >= 0))
            //{
            //    var type = lexer.Vocabulary.GetSymbolicName(token.Type);
            //    Console.WriteLine($"{lexer.Vocabulary.GetDisplayName(token.Type)}\t {(type != "NL" ? token.Text : "")}");
            //}

            //Console.ReadLine();

            var inputs = Seq(Console.ReadLine).TakeWhile(x => x != ":q");

            foreach (var input in inputs)
            {
                var codeStream = new AntlrInputStream(input);
                var lexer = new LAcer(codeStream);

                foreach (var token in Seq(lexer.NextToken).TakeWhile(x => x.Type >= 0))
                {
                    var type = lexer.Vocabulary.GetSymbolicName(token.Type);
                    Console.WriteLine($"{lexer.Vocabulary.GetDisplayName(token.Type)}\t {(type != "NL" ? token.Text : "")}");
                }

                lexer.Reset();

                var parser = new LAceParser(new CommonTokenStream(lexer));
                Console.WriteLine(parser.expr().ToStringTree());
                //var walker = new ParseTreeWalker();

                //walker.Walk(new Listener(), expr.GetChild(0));
            }
        }

        public static IEnumerable<T> Seq<T>(Func<T> generator)
        {
            while (true)
            {
                yield return generator();
            }
        }
    }
}
