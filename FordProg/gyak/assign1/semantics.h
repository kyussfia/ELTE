#include <iostream>
#include <string>
#include <map>
#include <sstream>

enum typee{boolean, natural};

struct var_data{
	int decl_row;
	typee var_type;
	
	var_data(){};
	var_data(int dec, typee var) : decl_row(dec), var_type(var) {}
};