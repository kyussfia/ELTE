//Készítette: Mikus Márk
//Tárgy: Programozás II. beadandó
//neptun-kód: CM6TSV
//email: kyussfia@gmail.com
// *************************************
//Segédfüggvények beolvasásához

#include "read.h" //header
#include <iostream>
#include <stdlib.h>

using namespace std;

/**
 * a cond() függvény elõfordulásakor használt alapértelmezett függvény, ami a konstans TRUE-t adja vissza.
 *
 * @param  int  k     Az bemeneti egész szám
 * @return bool TRUE (konstans true)
 **/
bool all(int k) {return true;}

/**
 * Beolvas egy , a feltételt (cond()) kielégítõ számot.
 * Hibák: n=0 és a beolvasott string nem a 0 vagy a feltétel hamissal tér vissza.
 *
 * @param  string msg      A kiírandó megjegyzés/üzenet a beolvasáshoz
 * @param  string errormsg A kiírandó hibaüzenet, (sikertelen beolvasás esetén)
 * @return int    n        A sikeresen beolvasott egész szám
 **/
int ReadInt(const string &msg, const string &errormsg, bool cond(int) )
{
    int n;
    bool hiba;
    do{
        cout << msg;
        string str;
        cin >> str;
        n = atoi(str.c_str());
        hiba = n==0 && str!="0" || !cond(n);
       if(hiba) cout<< errormsg<< endl;
    }while(hiba);
    return n;
}

/**
 * Beolvas egy , a feltételt (cond()) kielégítõ számot.
 * Hibák: n=0 és a beolvasott sting nem a 0 vagy n<0 vagy a feltétel hamissal tér vissza
 *
 * @param  string msg      A kiírandó megjegyzés/üzenet a beolvasáshoz
 * @param  string errormsg A kiírandó hibaüzenet, (sikertelen beolvasás esetén)
 * @return int    n        A sikeresen beolvasott egész szám
 **/
int ReadNat(const string &msg, const string &errormsg, bool cond(int) )
{
    int n;
    bool hiba;
    do{
        cout << msg;
        string str;
        cin >> str;
        n = atoi(str.c_str());
        hiba = n==0 && str!="0" || n<0 || !cond(n);
       if(hiba) cout<< errormsg<< endl;
    }while(hiba);
    return n;
}
