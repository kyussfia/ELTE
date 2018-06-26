//Készítette: Mikus Márk
//Tárgy: Programozás II. beadandó
//neptun-kód: CM6TSV
//email: kyussfia@gmail.com
// *************************************
// Segédfüggvények importálása headerben


#ifndef _READ_
#define _READ_

#include <string>
using namespace std;

bool all(int k);

int ReadInt(const string &msg, const string &errormsg, bool cond(int) =  all);
int ReadNat(const string &msg, const string &errormsg, bool cond(int) =  all);

#endif
