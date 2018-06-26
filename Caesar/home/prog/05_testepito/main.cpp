//
//
//
//
//05 Feladat:
//
//Egy testépítő versenyen a 30 versenyző között, két jó barát is indul (két egész cseréje).
//A versenyzőket a pontozáshoz sorszámokkal azonosítják (1-30). Egyikük "szerencseszáma"
//éppen a másik indulási sorszáma - megegyeznek és cserélnek. Olvassuk be a két számot a
//billentyűzetről (ellenőrizzük), végezzük el a cserét, majd az eredményt írjuk ki a kon-
//zolablakba!
//
//Specifikáció:
//
//Be: Szerencse:Egész
//    Indulasi_Szam:Egész
//Ki: Szerencse:Egész
//    Indulasi_Szam:Egész
//Ef: Szerencse, Indulasi_Szam eleme [1..30]
//Uf: Szerencse = Indulasi_Szam
//    Indulasi_Szam = Szerencse
//
//Teendõk:
//TODO Specifikáció

#include <iostream>

using namespace std;

int main() {

    //Testépítő verseny (két egész cseréje) 05
    //Változók deklarálása
    int Szerencse, Indulasi_Szam, Seged;

    //Adatok bekérése, változóba tételük
    cout <<"1. versenyzo (szerencseszam): ";
    cin>>Szerencse;
    cout <<"2. versenyzo (indulasi sorszam): ";
    cin>>Indulasi_Szam; cin.get();

    //Adatok ellenőrzése
    if ((Szerencse!=Indulasi_Szam) && ((Szerencse>=1) && (Szerencse<=30))
            && ((Indulasi_Szam>=1) && (Indulasi_Szam<=30)) ) {
        //Két szám kicserélése
        Seged = Szerencse;
        Szerencse = Indulasi_Szam;
        Indulasi_Szam = Seged;

        //Csere utáni kiiratás
        cout <<"\nCsere utan: \n";
        cout <<"1. versenyzo sorszama: " <<Szerencse<<endl;
        cout <<"2. versenyzo sorszama: " <<Indulasi_Szam<<endl;
    } else {
        //Hibás adatnál levő kiiratás
        cout <<"Hibasan bevitt adat!"<<endl;
    }
    //Billentyűnyomásra várás
    cin.get();

    //Visszatérési érték
    return 0;

}
