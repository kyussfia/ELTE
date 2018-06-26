%option noyywrap c++ yylineno

%{
#include <iostream>
#include <cstdlib>
//Mikus Márk - CM6TSV
void elem_handler(const char* type, const char* match);

%}

%%

int															{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}

main														{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}

true														{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}


false														{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}


if															{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}


else														{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}


while														{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}


cout														{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}


cin															{
																//kulcsszo
																elem_handler("Kulcsszo", YYText());
															}

unsigned													{
																// kulcsszo tipusok
																elem_handler("Kulcsszo/tipus", YYText());
															}

bool														{
																//kulcsszo
																elem_handler("Kulcsszo/tipus", YYText());
															}


"//".*														{
																//komment
																elem_handler("Komment", YYText());
															}

"("															{
																//nyitózárójel
																elem_handler("Nyito zarojel", YYText());
															}

")"															{
																//csukozárójel
																elem_handler("Csuko zarojel", YYText());
															}

"{"															{
																//szög nyitózárójel
																elem_handler("Szogletes Nyito zarojel", YYText());
															}

"}"															{
																//szög csukozárójel
																elem_handler("Szogletes Csuko zarojel", YYText());
															}

";"															{
																//sorvége
																elem_handler("Utasitas vege operator", YYText());
															}

"!"															{
																//sorvége
																elem_handler("Negalas operator", YYText());
															}	

"%"															{
																//sorvége
																elem_handler("Modulo operator", YYText());
															}

"="															{
																//sorvége
																elem_handler("Ertekadas operator", YYText());
															}		

"+"															{
																//sorvége
																elem_handler("Osszeadas operator", YYText());
															}	

"-"															{
																//sorvége
																elem_handler("Kivonas operator", YYText());
															}		

"/"															{
																//sorvége
																elem_handler("Osztas operator", YYText());
															}	

"*"															{
																//sorvége
																elem_handler("Szorzas operator", YYText());
															}	

"<"															{
																//sorvége
																elem_handler("Kisebb operator", YYText());
															}	

">"															{
																//sorvége
																elem_handler("Nagyobb operator", YYText());
															}	

">>"														{
																//sorvége
																elem_handler("Beolvasas operator", YYText());
															}	

"<<"														{
																//sorvége
																elem_handler("Kiiras operator", YYText());
															}	

"=="														{
																//sorvége
																elem_handler("Egyenlosegvizsgalat operator", YYText());
															}	

"&&"														{
																//sorvége
																elem_handler("Logikai es operator", YYText());
															}	

"||"														{
																//sorvége
																elem_handler("Logikai vagy operator", YYText());
															}																																																																												

[a-zA-Z_]+[a-zA-Z_0-9]*										{
																//azonosító
																elem_handler("Azonosito", YYText());
															}

[0-9]+														{
																//szam
																elem_handler("Szam", YYText());
															}																

[ \t\n]+													{
																//szoköz, sortörés, tab
																//elem_handler("Whitespace", YYText());
															}

.															{
												                std::cerr << "Line " << lineno() << ": Lexical error. Unexpected character: " << YYText() << std::endl;
												                exit(1);
												            }

%%

void elem_handler(const char* type, const char* match)
{
	std::cout << type << ": " << match << std::endl;
}