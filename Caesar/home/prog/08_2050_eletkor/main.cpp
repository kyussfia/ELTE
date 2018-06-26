//
//
//
//
//08 Feladat:
//
//Hány éves lesz 2050-ben? Olvassa be az illető nevét és életkorát, az aktuális dátum
//ismert a programban (konstansként). Azonnal jelezze a hibát, ha az életkor nem reális
//és lépjen ki a programból. Helyes adatok esetén számítsa ki a 2050-ben várható életkort
//és írja ki a képernyőre!
//
//Specifikáció:
//
//Be: nev:Szöveg
//    eletkor:Egész
//   [Konstans datum:Egész]
//Ki: varhatoEletkor:Egész
//Ef: eletkor<=100
//Uf: varhatoEletkor= 2050-datum+eletkor
//
//Teendõk:
//TODO Specifikáció
//
//Fejlesztések:
//Kiiratása annak, ha 2050-t valószínűleg már nem éli meg, névnél számot ne engedjünk meg

#include <iostream>

using namespace std;

int main() {

    //2050-ben hany eves 08
    //Adatok deklarálása
    string nev;
    int eletkor, varhatoEletkor;
    const int datum = 2012;

    //Név és életkor bekérése, változóba tétele
    cout <<"Nev: "; getline(cin,nev);
    cout <<"Eletkor: "; cin>>eletkor; cin.get();

    //Életkor nem reális létének vizsgálata
    if (eletkor>100) {
        //Szürreális életkornál hiba printelése
        cout <<"Hibas eletkor!";
    } else {
        //Ha hihető életkor, akkor várható életkor kiszámolás és kiiratás
        varhatoEletkor = 2050-datum+eletkor;
        cout <<"2050-ben a varhato eletkor " << varhatoEletkor << " lesz!" << endl;
    }
    //Billentyűnyomásra vár
    cin.get();

    //Visszatérési érték
    return 0;

}
