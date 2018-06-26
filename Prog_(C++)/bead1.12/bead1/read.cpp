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
 * Eldönti egy egész számról, hogy nagyobb avgy egyenlő-e mint 0.
 *
 * @param  int  k  A bemeneti egész szám
 * @return bool -  A visszatérési érték  True = valid érték
 **/
bool validValue(int k)
{
    return 0<=k;
}

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
        hiba = n==0 && str !="0" || !cond(n);
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

/**
 * Felméretezi a vektort majd feltölti nevekkel (string).
 *
 * @param int            n    A vektor mérete/ nevek száma
 * @param string         str  Az olvasásnál kiírandó üzenet
 * @param vector(string) v    A feltölteni kívánt vektor
 **/
void readNames(int n, const string &str, vector<string> &v)
{
    v.resize(n);
    for(int i=0; i<n; i++)
    {
        cout << i+1 << "." << str << "neve: ";
        cin >> v[i];
    }
}

/**
 * Felméretezi majd feltölt egy mátrixot a validValue() által érvényesnek elfogadott értékekkel.
 *
 * @param vector(string)      telep  A települések neveit tartalmazó tömb
 * @param vector(string)      madar  A madárfajok neveit tartalmazó tömb
 * @param vector<vector(int)> a      Az értékekkel feltölteni kívánt mátrix
 *
 **/
void readMatrixValues(const vector<string> &telep, const vector<string> &madar, vector<vector<int> > &a)
{
    a.resize((int)telep.size());
    for(int i=0; i<(int)telep.size(); ++i)
    {
        a[i].resize((int)madar.size());
        cout << telep[i] << " eredmenyei\n";
        for(int j=0; j<(int)madar.size(); ++j)
        {
            cout << "\t" << madar[j] << ": ";
            a[i][j] = ReadInt("","0 vagy nagyobb termeszetes szam kell!", validValue);
        }
    }
}

