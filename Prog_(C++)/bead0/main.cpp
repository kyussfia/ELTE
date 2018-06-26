/*
Készítette:Mikus Márk
Neptun-kód: CM6TSV
Csoport: 3.
Gyak.vez: Sike Sándor
Dátum: 2014.02.18.
email: kyussfia@gmail.com
*/
/* Feladat:

5. Keressük egy valós számokat tartalmazó tömbnek azt az indexét, amelyre teljesül, hogy v[k]=(v[k-
1]+ v[k+1])/2.

// A = x : |R ^ n , s : |N, l : |L
// Ef = x = x '
// Ug = Ef , s = search(i=1..n-1) x[i]=( x[i-1]+ x[i+1] ) / 2

Tétel: Lineáris Keresés
*/
#include <iostream>
#include <stdio.h>
#include <string>
#include <fstream>
#include <vector>

using namespace std;

int main()
{
    char a; //rerunning
    bool isExist = false;   //van e ilyen elem
    int index;  //elem indexe
    vector<double> t;  //tömb
    int n; //tömb mérete

    do //program running
    {
        /*
        Tömb méretének inicializálása
        */
        cout<<"Add meg a tomb meretet! (Termeszetes szam kell, 0 vagy nagyobb) \n";
        cin >> n;

        if(n==0) {  //üres tömb
            cout << "A tomb ures \n";
        } else {
            t.resize(n);

            /*
                Tömb értékeinek inicializálása
            */
            for(int i=0; i<(int)t.size(); i++){
                    cout<<"Add meg kerlek az "<<i+1<<". erteket:  ";
                    cin >> t[i];
            }
            /*
                Keresés
            */
            for (int i=1; i<((int)t.size() - 1) && !isExist; i++) {
                isExist = t[i] ==( t[i-1]+ t[i+1] ) / 2;
                index = i;
            }
            /*
                Eredmény közlése
            */
            if(isExist){
                cout << " \n Talaltam ilyen elemet: \n Az elem erteke: "<<t[index];
                cout << "\n Az elem indexe: " <<index+1<<endl;
            } else {
                cout << "Nicsen az adott tulajdonsagu elembol a tombben. \n";
            }
        }
        /*
            Újrafuttatás
        */
        cout << "Ujra futtatod a programot? (i/n) \n";
        cin >> a;
    }while( (a =='i') || (a=='I'));

    return 0;
}
