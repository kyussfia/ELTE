//K�sz�tette: Mikus M�rk
//T�rgy: Programoz�s II. beadand�
//neptun-k�d: CM6TSV
//email: kyussfia@gmail.com
// *************************************
//Seg�df�ggv�nyek beolvas�s�hoz

#include "read.h" //header
#include <iostream>
#include <stdlib.h>

using namespace std;

/**
 * a cond() f�ggv�ny el�fordul�sakor haszn�lt alap�rtelmezett f�ggv�ny, ami a konstans TRUE-t adja vissza.
 *
 * @param  int  k     Az bemeneti eg�sz sz�m
 * @return bool TRUE (konstans true)
 **/
bool all(int k) {return true;}

/**
 * Beolvas egy , a felt�telt (cond()) kiel�g�t� sz�mot.
 * Hib�k: n=0 �s a beolvasott string nem a 0 vagy a felt�tel hamissal t�r vissza.
 *
 * @param  string msg      A ki�rand� megjegyz�s/�zenet a beolvas�shoz
 * @param  string errormsg A ki�rand� hiba�zenet, (sikertelen beolvas�s eset�n)
 * @return int    n        A sikeresen beolvasott eg�sz sz�m
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
 * Beolvas egy , a felt�telt (cond()) kiel�g�t� sz�mot.
 * Hib�k: n=0 �s a beolvasott sting nem a 0 vagy n<0 vagy a felt�tel hamissal t�r vissza
 *
 * @param  string msg      A ki�rand� megjegyz�s/�zenet a beolvas�shoz
 * @param  string errormsg A ki�rand� hiba�zenet, (sikertelen beolvas�s eset�n)
 * @return int    n        A sikeresen beolvasott eg�sz sz�m
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
