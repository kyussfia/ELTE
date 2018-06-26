%option noyywrap c++ yylineno

%{
#include <iostream>
#include <cstdlib>
#include "Parser.h"

//Mikus Márk - CM6TSV
%}

%%

int															{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::INT;
															}

main														{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::MAIN;
															}

true														{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::TRUE;
															}


false														{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::FALSE;
															}


if															{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::HA;
															}


else														{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::KULONBEN;
															}


while														{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::WHILE;
															}


cout														{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::COUT;
															}


cin															{
																//kulcsszo
																//elem_handler("Kulcsszo", YYText());
																return Parser::CIN;
															}

unsigned													{
																// kulcsszo tipusok
																//elem_handler("Kulcsszo/tipus", YYText());
																return Parser::UNSIGNED;
															}

bool														{
																//kulcsszo
																//elem_handler("Kulcsszo/tipus", YYText());
																return Parser::BOOL;
															}


"//".*														{
																//komment
																//elem_handler("Komment", YYText());
																//return Parser::COMMENT; //Nem kell semmit csinálni
															}

"("															{
																//nyitózárójel
																//elem_handler("Nyito zarojel", YYText());
																return Parser::OPEN_PAR;
															}

")"															{
																//csukozárójel
																//elem_handler("Csuko zarojel", YYText());
																return Parser::CLOSE_PAR;
															}

"{"															{
																//szög nyitózárójel
																//elem_handler("Szogletes Nyito zarojel", YYText());
																return Parser::OPEN_BRAC;
															}

"}"															{
																//szög csukozárójel
																//elem_handler("Szogletes Csuko zarojel", YYText());
																return Parser::CLOSE_BRAC;
															}

";"															{
																//sorvége
																//elem_handler("Utasitas vege operator", YYText());
																return Parser::ENDL;
															}

"!"															{
																//sorvége
																//elem_handler("Negalas operator", YYText());
																return Parser::NEG;
															}	

"%"															{
																//sorvége
																//elem_handler("Modulo operator", YYText());
																return Parser::MOD;
															}

"="															{
																//sorvége
																//elem_handler("Ertekadas operator", YYText());
																return Parser::EQV;
															}		

"+"															{
																//sorvége
																//elem_handler("Osszeadas operator", YYText());
																return Parser::PLS;
															}	

"-"															{
																//sorvége
																//elem_handler("Kivonas operator", YYText());
																return Parser::MIN;
															}		

"/"															{
																//sorvége
																//elem_handler("Osztas operator", YYText());
																return Parser::DIV;
															}	

"*"															{
																//sorvége
																//elem_handler("Szorzas operator", YYText());
																return Parser::MUL;
															}	

"<"															{
																//sorvége
																//elem_handler("Kisebb operator", YYText());
																return Parser::LESS;
															}	

">"															{
																//sorvége
																//elem_handler("Nagyobb operator", YYText());
																return Parser::GRT;
															}	

">>"														{
																//sorvége
																//elem_handler("Beolvasas operator", YYText());
																return Parser::STR_IN;
															}	

"<<"														{
																//sorvége
																//elem_handler("Kiiras operator", YYText());
																return Parser::STR_OUT;
															}	

"=="														{
																//sorvége
																//elem_handler("Egyenlosegvizsgalat operator", YYText());
																return Parser::EQUAL;
															}	

"&&"														{
																//sorvége
																//elem_handler("Logikai es operator", YYText());
																return Parser::ES;
															}	

"||"														{
																//sorvége
																//elem_handler("Logikai vagy operator", YYText());
																return Parser::OR;
															}																																																																												

[a-zA-Z_]+[a-zA-Z_0-9]*										{
																//azonosító
																//elem_handler("Azonosito", YYText());
																return Parser::ID;
															}

[0-9]+														{
																//szam
																//elem_handler("Szam", YYText());
																return Parser::NUM;
															}																

[ \t\n]+													{
																//szoköz, sortörés, tab
																//elem_handler("Whitespace", YYText());
																//return Parser::WS;
															}

.															{
												                std::cerr << "Line " << lineno() << ": Lexical error. Unexpected character: " << YYText() << std::endl;
												                exit(1);
												            }

%%
}