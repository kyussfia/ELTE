%baseclass-preinclude <iostream>
%lsp-needed

%token INT
%token MAIN
%token OPENBRAC
%token CLOSEBRAC
%token UNSIGNED 
%token BOOL
%token IF
%token ELSE
%token WHILE
%token CIN
%token COUT
%token STR_OUT
%token STR_IN
%token ENDL
%token EQV
%token OPENPAR
%token CLOSEPAR
%token NUM
%token TRUE
%token FALSE
%token ID

%left OR
%left AND
%left NEG
%left EQUAL
%left LESS GRT
%left PLS MIN
%left MUL DIV MOD

%%

start:
    szignatura OPENBRAC deklaraciok torzs CLOSEBRAC
    {
        std::cout << "start -> INT MAIN OPENPAR CLOSEPAR OPENBRAC deklaraciok torzs CLOSEBRAC" << std::endl;
    }
;

szignatura:
    INT MAIN OPENPAR CLOSEPAR
    {
        std::cout << "szignatura -> INT MAIN OPENPAR CLOSEPAR" << std::endl;
    }
;

deklaraciok:
    // ures
    {
        std::cout << "deklaraciok -> epszilon" << std::endl;
    }
|
    deklaracio deklaraciok
    {
        std::cout << "deklaraciok -> deklaracio deklaraciok" << std::endl;
    }
;

deklaracio:
    UNSIGNED ID ENDL
    {
        std::cout << "deklaracio -> UNSIGNED ID ENDL" << std::endl;
    }
|
    BOOL ID ENDL
    {
        std::cout << "deklaracio -> BOOL ID ENDL" << std::endl;
    }
;

torzs:
    utasitas
    {
        std::cout << "torzs -> utasitas" << std::endl;
    }
|
    utasitas torzs
    {
        std::cout << "torzs -> utasitas torzs" << std::endl;
    }
;

utasitas:
    ertekadas
    {
        std::cout << "utasitas -> ertekadas" << std::endl;
    }
|
    be
    {
        std::cout << "utasitas -> be" << std::endl;
    }
|
    ki
    {
        std::cout << "utasitas -> ki" << std::endl;
    }
|
    elagazas
    {
        std::cout << "utasitas -> elagazas" << std::endl;
    }
|
    ciklus
    {
        std::cout << "utasitas -> ciklus" << std::endl;
    }
;

ertekadas:
    ID EQV kifejezes ENDL
    {
        std::cout << "ertekadas -> ID EQV kifejezes ENDL" << std::endl;
    }
;

be:
    STR_IN CIN ID ENDL
    {
        std::cout << "be -> STR_IN CIN ID ENDL" << std::endl;
    }
;

ki:
    STR_OUT COUT kifejezes ENDL
    {
        std::cout << "ki -> STR_OUT COUT kifejezes ENDL" << std::endl;
    }
;

elagazas:
    IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC
    {
        std::cout << "elagazas -> IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC" << std::endl;
    }
|
    IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC ELSE OPENBRAC torzs CLOSEBRAC
    {
        std::cout << "elagazas -> IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC ELSE OPENBRAC torzs CLOSEBRAC" << std::endl;
    }
;

ciklus:
    WHILE OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC
    {
        std::cout << "ciklus -> WHILE OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC" << std::endl;
    }
;

kifejezes:
    NUM
    {
        std::cout << "kifejezes -> NUM" << std::endl;
    }
|
    TRUE
    {
        std::cout << "kifejezes -> TRUE" << std::endl;
    }
|
    FALSE
    {
        std::cout << "kifejezes -> FALSE" << std::endl;
    }
|
    ID
    {
        std::cout << "kifejezes -> ID" << std::endl;
    }
|
    kifejezes PLS kifejezes
    {
        std::cout << "kifejezes -> kifejezes PLS kifejezes" << std::endl;
    }
|
    kifejezes MIN kifejezes
    {
        std::cout << "kifejezes -> kifejezes MIN kifejezes" << std::endl;
    }
|
    kifejezes MUL kifejezes
    {
        std::cout << "kifejezes -> kifejezes MUL kifejezes" << std::endl;
    }
|
    kifejezes DIV kifejezes
    {
        std::cout << "kifejezes -> kifejezes DIV kifejezes" << std::endl;
    }
|
    kifejezes MOD kifejezes
    {
        std::cout << "kifejezes -> kifejezes MOD kifejezes" << std::endl;
    }
|
    kifejezes LESS kifejezes
    {
        std::cout << "kifejezes -> kifejezes LESS kifejezes" << std::endl;
    }
|
    kifejezes GRT kifejezes
    {
        std::cout << "kifejezes -> kifejezes GRT kifejezes" << std::endl;
    }
|
    kifejezes EQUAL kifejezes
    {
        std::cout << "kifejezes -> kifejezes EQUAL kifejezes" << std::endl;
    }
|
    kifejezes AND kifejezes
    {
        std::cout << "kifejezes -> kifejezes AND kifejezes" << std::endl;
    }
|
    kifejezes OR kifejezes
    {
        std::cout << "kifejezes -> kifejezes OR kifejezes" << std::endl;
    }
|
    NEG kifejezes
    {
        std::cout << "kifejezes -> NEG kifejezes" << std::endl;
    }
|
    OPENPAR kifejezes CLOSEPAR
    {
        std::cout << "kifejezes -> OPENPAR kifejezes CLOSEPAR" << std::endl;
    }
;