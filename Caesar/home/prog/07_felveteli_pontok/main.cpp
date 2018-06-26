//
//
//
//
//07 Feladat:
//
//Felvételi pontszámok kiszámítása. Olvassa be a két tárgy százalékos eredményét. Azonnal
//jelezze, ha a beolvasott érték nem lehet vizsgaeredmény és lépjen ki a program. Jó
//értékek esetén számítsa ki a duplázott pontot és írja ki az eredményt!
//(OsszesPont=(Pont1+Pont2)*2)
//
//Specifikáció:
//
//Be: pont1:Egész
//    pont2:Egész
//Ki: osszeg: Egész
//Ef: pont1,pont2 eleme [0..100]
//Uf: osszeg=(pont1+pont2)*2
//
//Teendõk:
//TODO Specifikáció
//
//Fejlesztések:
//Felhasználói gépelések kiszűrése, Ha első adatnál téveszt, akkor ne írja ki hogy Pont2:
//hibásan bevitt adat, pont2 még nem is volt bekérve

#include <iostream>
#include <stdlib.h>

using namespace std;

int main() {

    //Felvételi pontszámok kiszámítása 07
    //Változók deklarálása
    int pont1, pont2;
    int osszeg;

    //Pont1 érték bekérése, változóba tárolás
    cout <<"Pont1: ";
    cin >>pont1; cin.get();

    //Pont1 értékének vizsgálata
    if (!((pont1>=0) && (pont1<=100) )) {
        //Nem jó értéknél, kiírása a hibának
        cout <<"Nem jo vizsgaeredmeny!";
        //Billentyűnyomásra vár
        cin.get();

        //Kilépés a programból
        exit(0);
    }

    //Pont2 érték bekérése, változóba tárolás
    cout <<"Pont2: ";
    cin >> pont2; cin.get();

    //Pont2 értékének vizsgálata
    if (!((pont2>=0) && (pont2<=100) )) {
        //Nem jó értéknél, kiírása a hibának
        cout <<"Nem jo vizsgaeredmeny!";
        //Billentyűnyomásra vár
        cin.get();

        //Kilépés a programból
        exit(0);
    }

    //Dupla pontszám kiszámítása és kiírása
    osszeg = (pont1 + pont2)*2;
    cout <<"A duplazott pont erteke: " << osszeg << endl;
    //Billentyűnyomásra vár
    cin.get();

    //Visszatérési érték
    return 0;

}
