%baseclass-preinclude  "semantics.h"
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
%token <szoveg> ID
%type <tip> kifejezes;

%left OR
%left AND
%left NEG
%left EQUAL
%left LESS GRT
%left PLS MIN
%left MUL DIV MOD

%union
{
    std::string *szoveg;
    //tipus* tip;

    //kifejezes_leiro *kif;
    //utasitas_leiro *utasitas;
    //%type <kif> kifejezes
	//%type <utasitas> ertekadas
	//%type <utasitas> be
	//%type <utasitas> ki
	//%type <utasitas> elagazas
	//%type <utasitas> ciklus
}


%%

start:
    szignatura OPENBRAC deklaraciok torzs CLOSEBRAC
    {
        //std::cout << "start -> INT MAIN OPENPAR CLOSEPAR OPENBRAC deklaraciok torzs CLOSEBRAC" << std::endl;
    }
;

szignatura:
    INT MAIN OPENPAR CLOSEPAR
    {
        //std::cout << "szignatura -> INT MAIN OPENPAR CLOSEPAR" << std::endl;
    }
;

deklaraciok:
    // ures
    {
        //std::cout << "deklaraciok -> epszilon" << std::endl;
    }
|
    deklaracio deklaraciok
    {
        //std::cout << "deklaraciok -> deklaracio deklaraciok" << std::endl;
    }
;

deklaracio:
    UNSIGNED ID ENDL
    {
        //std::cout << "deklaracio -> UNSIGNED ID ENDL" << std::endl;
        if( szimb_tabla.count(*$2) > 0 )
        {
            std::stringstream ss;
            ss << "Ujradeklaralt valtozo: " << *$2 << ".\n"
            << "Korabbi deklaracio sora: " << szimbolumtabla[*$2].decl_row << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            szimb_tabla[*$2] = valtozo_leiro(d_loc__.first_line,Egesz);
        }
        delete $2;
    }
|
    BOOL ID ENDL
    {
        //std::cout << "deklaracio -> BOOL ID ENDL" << std::endl;
        if( szimb_tabla.count(*$2) > 0 )
        {
            std::stringstream ss;
            ss << "Ujradeklaralt valtozo: " << *$2 << ".\n"
            << "Korabbi deklaracio sora: " << szimbolumtabla[*$2].decl_row << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            szimb_tabla[*$2] = valtozo_leiro(d_loc__.first_line,Logikai);
        }
        delete $2;
    }
;

torzs:
    utasitas
    {
        //std::cout << "torzs -> utasitas" << std::endl;
    }
|
    utasitas torzs
    {
        //std::cout << "torzs -> utasitas torzs" << std::endl;
    }
;

utasitas:
    ertekadas
    {
        //std::cout << "utasitas -> ertekadas" << std::endl;
    }
|
    be
    {
        //std::cout << "utasitas -> be" << std::endl;
    }
|
    ki
    {
        //std::cout << "utasitas -> ki" << std::endl;
    }
|
    elagazas
    {
        //std::cout << "utasitas -> elagazas" << std::endl;
    }
|
    ciklus
    {
        //std::cout << "utasitas -> ciklus" << std::endl;
    }
;

ertekadas:
    ID EQV kifejezes ENDL
    {
        //std::cout << "ertekadas -> ID EQV kifejezes ENDL" << std::endl;
        if( szimb_tabla.count( *$1 ) == 0 )
        {
            std::stringstream ss;
            ss << d_loc__.first_line << ": A(z) '" << *$1 << "' valtozo nincs deklaralva." << std::endl;
            error( ss.str().c_str() );
        }
        else if( szimb_tabla[*$1].vtip != *$3)// $3->ktip )
        {
            std::stringstream ss;
            ss << d_loc__.first_line << ": Az ertekadas jobb- es baloldalan kulonbozo tipusu kifejezesek allnak." << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            $$ = new utasitas_leiro( d_loc__.first_line );
        }
        delete $1;
        delete $3;
    }
;

be:
    STR_IN CIN ID ENDL
    {
        //std::cout << "be -> STR_IN CIN ID ENDL" << std::endl;
        if( szimb_tabla.count( *$3 ) == 0 )
        {
        	std::stringstream ss;
            ss << d_loc__.first_line << ": A(z) '" << *$3 << "' valtozo nincs deklaralva." << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            $$ = new utasitas_leiro( d_loc__.first_line );
        }
        delete $3;
    }
;

ki:
    STR_OUT COUT kifejezes ENDL
    {
        //std::cout << "ki -> STR_OUT COUT kifejezes ENDL" << std::endl;
        $$ = new utasitas_leiro( d_loc__.first_line );
        delete $3;
    }
;

elagazas:
    IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC
    {
        //std::cout << "elagazas -> IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC" << std::endl;
        if( $3->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << d_loc__.first_line << ": Nem logikai tipusu az elagazas feltetele." << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            $$ = new utasitas_leiro( $3->sor );
        }
        delete $3;
    }
|
    IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC ELSE OPENBRAC torzs CLOSEBRAC
    {
        //std::cout << "elagazas -> IF OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC ELSE OPENBRAC torzs CLOSEBRAC" << std::endl;
        if( $3->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << d_loc__.first_line << ": Nem logikai tipusu az elagazas feltetele." << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            $$ = new utasitas_leiro( $3->sor );
        }
        delete $3;
    }
;

ciklus:
    WHILE OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC
    {
        //std::cout << "ciklus -> WHILE OPENPAR kifejezes CLOSEPAR OPENBRAC torzs CLOSEBRAC" << std::endl;
        if( $3->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << d_loc__.first_line << ": Nem logikai tipusu a ciklus feltetele." << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            $$ = new utasitas_leiro( $3->sor );
        }
        delete $3;
    }
;

kifejezes:
    NUM
    {
        //std::cout << "kifejezes -> NUM" << std::endl;
        $$ = new kifejezes_leiro( d_loc__.first_line, Egesz );
        delete $1;
    }
|
    TRUE
    {
        //std::cout << "kifejezes -> TRUE" << std::endl;
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
    }
|
    FALSE
    {
        //std::cout << "kifejezes -> FALSE" << std::endl;
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
    }
|
    ID
    {
        //std::cout << "kifejezes -> ID" << std::endl;
        if( szimb_tabla.count( *$1 ) == 0 )
        {
        	std::stringstream ss;
            ss << d_loc__.first_line << ": A(z) '" << *$1 << "' valtozo nincs deklaralva." << std::endl;
            error( ss.str().c_str() );
        }
        else
        {
            valtozo_leiro vl = szimb_tabla[*$1];
            $$ = new kifejezes_leiro( vl.def_sora, vl.vtip );
            delete $1;
        }
    }
|
    kifejezes PLS kifejezes
    {
        //std::cout << "kifejezes -> kifejezes PLS kifejezes" << std::endl;
        if( $1->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $1->sor << ": A '+' operator baloldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $3->sor << ": A '+' operator jobboldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Egesz );
        delete $1;
        delete $3;
    }
|
    kifejezes MIN kifejezes
    {
        //std::cout << "kifejezes -> kifejezes MIN kifejezes" << std::endl;
        if( $1->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $1->sor << ": A '-' operator baloldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $3->sor << ": A '-' operator jobboldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Egesz );
        delete $1;
        delete $3;
    }
|
    kifejezes MUL kifejezes
    {
        //std::cout << "kifejezes -> kifejezes MUL kifejezes" << std::endl;
        if( $1->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $1->sor << ": A '*' operator baloldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $3->sor << ": A '*' operator jobboldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Egesz );
        delete $1;
        delete $3;
    }
|
    kifejezes DIV kifejezes
    {
        //std::cout << "kifejezes -> kifejezes DIV kifejezes" << std::endl;
        if( $1->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $1->sor << ": A 'div' operator baloldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $3->sor << ": A 'div' operator jobboldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Egesz );
        delete $1;
        delete $3;
    }
|
    kifejezes MOD kifejezes
    {
        //std::cout << "kifejezes -> kifejezes MOD kifejezes" << std::endl;
        if( $1->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $1->sor << ": A 'mod' operator baloldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $3->sor << ": A 'mod' operator jobboldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Egesz );
        delete $1;
        delete $3;
    }
|
    kifejezes LESS kifejezes
    {
        //std::cout << "kifejezes -> kifejezes LESS kifejezes" << std::endl;
        if( $1->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $1->sor << ": A '<' operator baloldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $3->sor << ": A '<' operator jobboldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
        delete $1;
        delete $3;
    }
|
    kifejezes GRT kifejezes
    {
        //std::cout << "kifejezes -> kifejezes GRT kifejezes" << std::endl;
        if( $1->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $1->sor << ": A '>' operator baloldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Egesz )
        {
        	std::stringstream ss;
            ss << $3->sor << ": A '>' operator jobboldalan nem egesz tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
        delete $1;
        delete $3;
    }
|
    kifejezes EQUAL kifejezes
    {
        //std::cout << "kifejezes -> kifejezes EQUAL kifejezes" << std::endl;
        if( $1->ktip != $3->ktip )
        {
        	std::stringstream ss;
            ss << $1->sor << ": Az '=' operator jobb- es baloldalan kulonbozo tipusu kifejezesek allnak." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
        delete $1;
        delete $3;
    }
|
    kifejezes AND kifejezes
    {
        //std::cout << "kifejezes -> kifejezes AND kifejezes" << std::endl;
        if( $1->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << $1->sor << ": Az 'and' operator baloldalan nem logikai tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << $3->sor << ": Az 'and' operator jobboldalan nem logikai tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
        delete $1;
        delete $3;
    }
|
    kifejezes OR kifejezes
    {
        //std::cout << "kifejezes -> kifejezes OR kifejezes" << std::endl;
        if( $1->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << $1->sor << ": Az 'or' operator baloldalan nem logikai tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        if( $3->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << $3->sor << ": Az 'or' operator jobboldalan nem logikai tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
        delete $1;
        delete $3;
    }
|
    NEG kifejezes
    {
        //std::cout << "kifejezes -> NEG kifejezes" << std::endl;
        if( $2->ktip != Logikai )
        {
        	std::stringstream ss;
            ss << $2->sor << ": A 'NEM' operator utan nem logikai tipusu kifejezes all." << std::endl;
            error( ss.str().c_str() );
        }
        $$ = new kifejezes_leiro( d_loc__.first_line, Logikai );
        delete $2;
    }
|
    OPENPAR kifejezes CLOSEPAR
    {
        //std::cout << "kifejezes -> OPENPAR kifejezes CLOSEPAR" << std::endl;
        $$ = $2;
    }
;