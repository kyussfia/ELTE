#ifndef READ_H_INCLUDED
#define READ_H_INCLUDED
#include <string>
#include "read.cpp"
using namespace std;
//K�sz�tette: Mikus M�rk
//T�rgy: Programoz�s II. beadand�
//neptun-k�d: CM6TSV
//email: kyussfia@gmail.com
// *************************************
//Seg�df�ggv�nyek import�l�sa headerben


bool all(int k);

int ReadInt(const string &msg, const string &errormsg, bool cond(int) =  all);
int ReadNat(const string &msg, const string &errormsg, bool cond(int) =  all);


#endif // READ_H_INCLUDED
