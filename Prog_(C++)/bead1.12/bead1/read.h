#ifndef READ_H_INCLUDED
#define READ_H_INCLUDED
#include <string>
#include "read.cpp"
using namespace std;
//Készítette: Mikus Márk
//Tárgy: Programozás II. beadandó
//neptun-kód: CM6TSV
//email: kyussfia@gmail.com
// *************************************
//Segédfüggvények importálása headerben


bool all(int k);

int ReadInt(const string &msg, const string &errormsg, bool cond(int) =  all);
int ReadNat(const string &msg, const string &errormsg, bool cond(int) =  all);


#endif // READ_H_INCLUDED
