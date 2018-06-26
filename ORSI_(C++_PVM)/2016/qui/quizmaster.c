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
	int max_rounds;
	int solution;
	int *tids;
	int winner;
	int rounds;
	char **init_args;
	
	if (argc < 3)
	{
		printf("[Hiba]: Helyes használat: spawn -> quizmaster [tippelési tartomány] [maximum körök száma]\n");
		return 1;
	}
	
	range = atoi(argv[1]);
	max_rounds = atoi(argv[2]);
	
	if (range <= 0)
	{
		printf("[Hiba]: A tippelési tartománynak pozitív egész számnak kell lennie!\n");
		return 1;
	}

	if (max_rounds < 0)
	{
		printf("[Hiba]: A körök maximális számának nemnegatív egész számnak kell lennie!\n");
		return 1;
	}

	// véletlenszám kiválasztása -`range` és `range` között
	srand(pvm_mytid());
	solution = (rand() % (2 * range + 1)) - range;
	
	printf("[Játékmester]: Gondoltam egy számra -%d és %d között: %d\n", range, range, solution);

	// itt gyakorlatilag ez történik: init_args = { "range", NULL }
	init_args = malloc(2 * sizeof(char*));
	init_args[0] = malloc(10 * sizeof(char));
	init_args[1] = NULL;
	sprintf(init_args[0], "%d", range);
	
	// `range` darab folyamat létrehozása, a `range` értékének átadásával
	// a `tids` fogja tárolni a gyerektaszkok azonosítóit
	tids = malloc(range * sizeof(int));
	if (pvm_spawn("player", init_args, PvmTaskDefault, 0, range, tids) < range) return 1;
	
	winner = (-1);
	rounds = 0;

	while (winner < 0 && rounds < max_rounds)
	{
		int guessed_tasks;
		int i;

		guessed_tasks = 0;
		++rounds;
		
		printf("[Játékmester]: Indul a(z) %d/%d kör!\n", rounds, max_rounds);

		while (guessed_tasks < range && winner < 0)
		{
			int guess;
			int tid;
			
			// várakozás (bárkitől) egy tippre
			pvm_recv(-1, -1);
			pvm_upkint(&tid, 1, 1);
			pvm_upkint(&guess, 1, 1);
			
			printf("[Játékmester]: Tipp t%x játékostól: %d\n", tid, guess);
			
			if (guess == solution) winner = tid;
			++guessed_tasks;
		}
		
		// győztes taszkazonosító szétküldése
		pvm_initsend(PvmDataDefault);
		pvm_pkint(&winner, 1, 1);
		for (i = 0; i < range; ++i) pvm_send(tids[i], 1);
	}

	if (winner < 0)
	{
		int i;
		
		printf("[Játékmester]: A játék nyertes nélkül ért véget.\n");

		// mivel a gyerekfolyamatok akkor terminálnak, ha született
		// valamilyen győztes, ezért ilyenkor a játékmester lesz a
		// győztes
		winner = pvm_mytid();

		// győztes taszkazonosító szétküldése
		pvm_initsend(PvmDataDefault);
		pvm_pkint(&winner, 1, 1);
		for (i = 0; i < range; ++i) pvm_send(tids[i], 1);
	}
	else
		printf("[Játékmester]: A játékot t%x nyerte. t%x örül.\n", winner, winner);

	pvm_exit();
	return 0;
}
