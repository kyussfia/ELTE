%option noyywrap c++ yylineno

%{
#include <iostream>
#include <stdlib.h>
#include "Parser.h"
%}

%%

int         return Parser::INT;
main        return Parser::MAIN;
unsigned    return Parser::UNSIGNED; 
bool        return Parser::BOOL;
if          return Parser::IF;
else        return Parser::ELSE;
while       return Parser::WHILE;
cout        return Parser::STR_OUT;
cin         return Parser::STR_IN;
";"         return Parser::ENDL;
">>"        return Parser::CIN;
"<<"        return Parser::COUT;
"="         return Parser::EQV;
"=="        return Parser::EQUAL;
"<"         return Parser::LESS;
">"         return Parser::GRT;
"+"         return Parser::PLS;
"-"         return Parser::MIN;
"*"         return Parser::MUL;
"/"         return Parser::DIV; 
"%"         return Parser::MOD;
"&&"        return Parser::AND;
"||"        return Parser::OR;
!           return Parser::NEG;
"{"         return Parser::OPENBRAC;
"}"         return Parser::CLOSEBRAC;
"("         return Parser::OPENPAR;
")"         return Parser::CLOSEPAR;

[0-9]+ 		return Parser::NUM;
true        return Parser::TRUE;
false       return Parser::FALSE;

"//".*\n    // nem kell semmit csinalni

[a-zA-Z_]+[a-zA-Z_0-9]*    return Parser::ID;

[ \t\n]+   	// feher szokozok: semmi teendo

. 			{
			    std::cerr << "Line " << lineno() << ": Lexical error. Unexpected character: " << YYText() << std::endl;
				exit(1);
			}

%%
