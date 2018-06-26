#ifndef SEMANTICS_H
#define SEMANTICS_H

#include <iostream>
#include <string>
#include <map>
#include <sstream>

enum tipus { Egesz, Logikai };

struct valtozo_leiro
{
	int def_sora;
	tipus vtip;
	valtozo_leiro(){};
	valtozo_leiro( int s = 0, tipus t = Egesz )
		: def_sora(s), vtip(t)
	{}
};

struct kifejezes_leiro
{
	int sor;
	tipus ktip;
	kifejezes_leiro( int s, tipus t )
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
