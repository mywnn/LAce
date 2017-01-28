lexer grammar LAcer;

tokens { INDENT, DEDENT }

NL
    : (' '* '\r'? '\n' ' '*)+;
BIND
    : (' '* '=' ' '*);
COMP
    : (' '* ('>>' | '<<') ' '*);
OPTR
    : (' '* [@<>|=!][@<>|=!]+ ' '*);
OFTYPE
    : (' '* ':' ' '*);
ADDSUB
    : (' '* [+-] ' '*);
MULDIV
    : (' '* [*/] ' '*);
APP
    : ' '+;
TYPE
    : 'int'
    | 'unit'
    ;
TVAR
    : '\'' [a-zA-Z][a-zA-Z0-9_-]*;
VVAR
    : [a-zA-Z][a-zA-Z0-9_-]*;
INT
    : [0-9]+;
OPEN
    : '(';
CLOSE
    : ')';
