#ifndef SEMANTICS_H
#define SEMANTICS_H

#include <iostream>
#include <string>
#include <map>
#include <sstream>

enum type { Egesz, Logikai };

struct var_data {
	int def_sora;
	type tipus;

	var_data(){};
	var_data(int s, type t) : def_sora(s), tipus(t) {}
};

struct kifejezes_leiro
{
	int sor;
	type ktip;
	kifejezes_leiro( int s, type t )
		: sor(s), ktip(t)
    {}
};

struct utasitas_leiro
{
	int sor;
	utasitas_leiro( int s )
		: sor(s)
    {}
};
#endif //SEMANTICS_H
