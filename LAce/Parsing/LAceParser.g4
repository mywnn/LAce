parser grammar LAceParser;

expr
	: body?
	;
	
body
    : stmt NL body  # Cons
	| stmt NL?     	# Ret
    ;
stmt
    : bindN;

// operation precedence (low -> high)
bindN
	: bind (BIND NL INDENT body DEDENT)?;
bind
	: bind BIND oprtN | oprtN;
    
oprtN
	: oprt (OPRT NL INDENT body DEDENT)?;
oprt
	: oprt OPRT compN | compN;
       
compN
	: comp (COMP NL INDENT body DEDENT)?;
comp
	: comp COMP app | app;
     
app
	: app APP oftype | oftype;
     
oftype
	: oftype OFTYPE addsubN | addsubN;
 
addsubN
	: addsub (ADDSUB NL INDENT body DEDENT)?;
addsub
	: addsub ADDSUB muldivN | muldivN;
    
muldivN
	: muldiv (MULDIV NL INDENT body DEDENT)?;
muldiv
	: muldiv MULDIV term | term;
    
term
    : OPEN body CLOSE
    | TYPE | TVAR | INT | VVAR
    ;