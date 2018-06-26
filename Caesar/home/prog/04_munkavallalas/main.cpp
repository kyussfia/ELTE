//
//
//
//
//04 Feladat:
//
//Muunkavállalás. Olvassa be annak az árintervallumnak az alsó és felső értékét, amelyben
//elvállal egy munkát. Jelezze, ha a felsőérték kisebb az alsónál és lépjen ki a program-
//ból, különben írja ki a képernyőre az árintervallumot!
//
//Specifikáció:
//
//Be: Min, Max:Egész
//Ki: Min, Max:Egész
//Ef: 0<=Min, Max
//    Min<=Max
//Uf: Min' = Min
//    Max' = Max
//
//Teendõk:
//TODO Specifikáció
//
//Fejlesztések:
//Felhasználói gépelések kiszűrése, ha második értéknek betűt irok be, akkor azt ne fogadja el
//és ha elsőt rontjuk, akkor ne írja ki maximum árat, majd hogy hibásan bevitt adat, mert
//be sem kértük még

#include <iostream>

using namespace std;

int main() {

    //Munkavállalás 04
    //Változók deklarálás
    int Min, Max;

    //Adatok bekérése, változóba tétele
    cout <<"Ird be a minimum arat: ";
    cin >>Min;
    cout <<"Ird be a maximum arat: ";
    cin >>Max; cin.get();

    //Adatok helyességének ellenőrzése, kiiratás ha nem jó érték
    if (Min>Max) {
        //Nem jó érték bevitelekor hiba kiírás
        cout <<"Nem jo a bevitt adat!" << endl;
    } else {
        //Jó adatnál intervallum kiírás
        cout <<"A munkat " << Min << " - " << Max << " intervallumban vallalod" << endl;
    }
    //Billentyűnyomásra várás
    cin.get();

    //Visszatérési érték
    return 0;

}
