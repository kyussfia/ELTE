//K�sz�tette: Mikus M�rk
//T�rgy: Programoz�s II. beadand�
//neptun-k�d: CM6TSV
//email: kyussfia@gmail.com
// *************************************
// Seg�df�ggv�nyek import�l�sa headerben


#ifndef _READ_
#define _READ_

#include <string>
using namespace std;

bool all(int k);

int ReadInt(const string &msg, const string &errormsg, bool cond(int) =  all);
int ReadNat(const string &msg, const string &errormsg, bool cond(int) =  all);

#endif
