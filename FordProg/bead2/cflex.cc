//Mikus Márk - CM6TSV
#include <iostream>
#include <fstream>
#include <string>
#include <cstdlib>
#include "Parser.h"
#include <FlexLexer.h>

using namespace std;

void input_handler( ifstream& in, int argc, char* argv[] );


int Parser::lex()
{
    int ret = fl->yylex();
    d_loc__.first_line = fl->lineno();
    return ret;
}

int main( int argc, char* argv[] )
{
    ifstream in;
    input_handler( in, argc, argv );
    //yyFlexLexer fl(&in, &cout);
    //fl.yylex();
    Parser pars(in);
    pars.parse();
    return 0;
}


void input_handler( ifstream& in, int argc, char* argv[] )
{
    if( argc < 2 )
    {
        cerr << "You should define inputfile in command line parameter." << endl;
        exit(1);
    }
    in.open( argv[1] );
    if( !in )
    {
        cerr << "The " << argv[1] << " file can't be opened." << endl;
        exit(1);
    }
}