%baseclass-preinclude "semantics.h"
%lsp-needed

%token NATURAL;
%token BOOLEAN;
%token TRUE;
%token FALSE;
%token NUMBER;
%token <szoveg> IDENT;
%token ASSIGN;
%type <tipus> expr;

%union
{
  std::string *szoveg;
  typee* tipus;
}

%%

start:
    declarations assignments
;

declarations:
    // ures
|
    declaration declarations
;

declaration:
    NATURAL IDENT 
	{
		//std::cout << *$2 << std::endl;
		if( szimbolumtabla.count(*$2) > 0 )
		{
			std::stringstream ss;
			ss << "Ujradeklaralt valtozo: " << *$2 << ".\n"
			<< "Korabbi deklaracio sora: " << szimbolumtabla[*$2].decl_row << std::endl;
			error( ss.str().c_str() );
		}
		szimbolumtabla[*$2] = var_data( d_loc__.first_line, natural );
		delete $2;
	}
|
    BOOLEAN IDENT
	{
		//std::cout << *$2 << std::endl;
		if( szimbolumtabla.count(*$2) > 0 )
		{
			std::stringstream ss;
			ss << "Ujradeklaralt valtozo: " << *$2 << ".\n"
			<< "Korabbi deklaracio sora: " << szimbolumtabla[*$2].decl_row << std::endl;
			error( ss.str().c_str() );
		}
		szimbolumtabla[*$2] = var_data( d_loc__.first_line, boolean );
		delete $2;
	}
;

assignments:
    // ures
|
    assignment assignments
;

assignment:
    IDENT ASSIGN expr
	{
		if( szimbolumtabla.find(*$1) == szimbolumtabla.end())
		{ //not found
			std::stringstream ss;
			ss << "Nem deklaralt valtozo: " << *$1 << ".\n" << std::endl;
			error( ss.str().c_str() );
		}
		if( szimbolumtabla[*$1].var_type != *$3)
		{
		  error( "Tipushibas ertekadas.\n" );
		}
		delete $1;
		delete $3;
	}
;

expr:
    IDENT
	{
		if( szimbolumtabla.find(*$1) == szimbolumtabla.end())
		{ //not found
			std::stringstream ss;
			ss << "Nem deklaralt valtozo: " << *$1 << ".\n" << std::endl;
			error( ss.str().c_str() );
		}
		$$ = new typee(szimbolumtabla[*$1].var_type);
		delete $1;
	}
|
    NUMBER
	{
		$$ = new typee(natural);
	}
|
    TRUE
	{
		$$ = new typee(boolean);
	}
|
    FALSE
	{
		$$ = new typee(boolean);
	}
;
