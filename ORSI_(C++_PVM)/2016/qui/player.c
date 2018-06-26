////////////////////////////////////////////////////////////////////////
//
//	Osztott rendszerek specifikációja és implementációja (2014-2015-1)
//	Első beadandó feladat
//
//	Készítette Fejes Bence (AQTNP5)
//
////////////////////////////////////////////////////////////////////////

#include <stdio.h>
#include <stdlib.h>
#include "pvm3.h"

int main(int argc, char *argv[])
{
	int range;
	int guess;
	int tid;
	int ptid;
	int winner;
	
	if (argc < 2) return 1;
	range = atoi(argv[1]);

	tid = pvm_mytid();
	ptid = pvm_parent();

	srand(tid);
	
	do
	{
		// tippelés -`range` és `range` között
		guess = (rand() % (2 * range + 1)) - range;
		
		// tipp elküldése a taszkazonosítóval együtt
		pvm_initsend(PvmDataDefault);
		pvm_pkint(&tid, 1, 1);
		pvm_pkint(&guess, 1, 1);
		pvm_send(ptid, 1);
		
		// várakozás a győztes taszkazonosítójára
		pvm_recv(ptid, 1);
		pvm_upkint(&winner, 1, 1);
	}
	while (winner < 0);
	
	pvm_exit();
	return 0;
}
