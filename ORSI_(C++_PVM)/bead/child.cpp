#include <iostream>
#include <cstdlib>
#include <pvm3.h>

using namespace std;

int main(int argc, char* argv[])
{
    int sumTime = 0;
    int avgTime = 0;

	int howManyVotes;
    
    pvm_recv(pvm_parent(), 1);

    //first number is how many votes
    pvm_upkint(&howManyVotes, 1, 1);

    int minutes[howManyVotes];
    pvm_upkint(minutes, howManyVotes, 1); //array of times

    for (int i = 0; i < howManyVotes; ++i) {
    	sumTime += minutes[i];
    }

    if (howManyVotes < 1) {
		pvm_perror("[Hiba]: Inkonzisztens adatok.");
		exit(5);
    }

    avgTime = sumTime / howManyVotes;

    pvm_initsend(PvmDataDefault);
	pvm_pkint(&sumTime,1,1);
	pvm_pkint(&avgTime,1,1);
	pvm_send(pvm_parent(),1);

	pvm_exit();
	return 0;
}