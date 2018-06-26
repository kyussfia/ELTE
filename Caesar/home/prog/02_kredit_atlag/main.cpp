//
//
//
//
//02 Feladat:
//
//Számítsa ki a kreditátlagot! Olvassa be a jegyek és kreditek szorzatának összegét!
//Ez az érték csak pozitív lehet, rossz érték esetén jelezze a hibát és lépjen ki a
//programból. Helyes érték beolvasása után számolja ki a kreditátlagot és írja ki a
//képernyőre!
//
//Specifikáció:
//
//Be: SzorzatOsszeg:Egész
//Ki: KreditAtlag:Valós
//Ef: 0<SzorzatOsszeg
//Uf: KreditAtlag = Valós(SzorzatOsszeg) / 30
//
//Teendõk:
//TODO Specifikáció
//
//Fejlesztések:
//Felhasználói gépelések kiszűrése, "g"-t ne fogadja el számnak

#include <iostream>
#include <stdlib.h>

using namespace std;

int main() {

    //Kreditatlag 02
    //Változók deklarálása
    int SzorzatOsszeg;
    float KreditAtlag;

    //Adat bekérése, változóba tétele
    cout <<"Jegyek es kreditek szorzatanak osszege: ";
    cin >>SzorzatOsszeg; cin.get();

    //Adat ellenőrzése
    if (SzorzatOsszeg<=0) {
        //Hibás értéknél, képernyőre kiírás
        cout <<"Hibas ertek!"<<endl;
        //Billentyűnyomásra várás
        cin.get();

        //Kilépés a programból
        exit(0);
    }

    //Átlag kiszámítása, kiiratása
    KreditAtlag = float(SzorzatOsszeg) / 30;
    cout <<"A kreditatlag: " << KreditAtlag << endl;
    //Billentyűnyomásra várás
    cin.get();

    //Visszatérési érték
    return 0;

}
