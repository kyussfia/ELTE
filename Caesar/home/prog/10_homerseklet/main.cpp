//
//
//
//
//10 Feladat:
//
//Reggeli hőmérséklet értékelése, átszámítása Fahrenheit-re. F = C * 9/5 + 32
//Olvassuk be a hőmérsékletet - a Földön eddig előfordult leghidegebb és leg-
//melegebb hőmérsékletek a kódban adottak (-89,58). Hibás beolvasás esetén
//jelezzük a hibát és lépjünk ki a programból. Helyes érték esetén adjuk meg
//és írjuk is ki, hogy fagypont alatti vagy feletti-e a hőmérséklet és
//számoljuk azt át Fahrenheitre!
//
//Specifikáció:
//
//Be: hom:Egész
//[Konstans minH,maxH:Egész(-89,58)]
//Ki: fagyott:Logikai, homF:Egész
//Ef: hom eleme [minH...maxH]
//Uf: fagyott=hom<=0 és homF=hom*9/5+32
//
//Teendõk:
//TODO Specifikáció

#include <iostream>
#include <stdlib.h>

using namespace std;

int main() {

    // Hõmérséklet 10
    //Változók deklarálása
    int hom, homF;
    bool fagyott;
    const int minH = -89, maxH = 58;

    //Hőmérséklet bekérése, és változóba mentés
    cout <<"Homerseklet megadasa: ";
    cin >>hom; cin.get();

    //Hőmérséklet realitásának megnézése
    if (!((minH<=hom) && (hom<=maxH))) {
        //Hibás hőmérsékletnél, tény közlése
        cout <<"Hibas homerseklet!" << endl;
        //Billentyűnyomásra vár
        cin.get();

        //Programból való kilépés
        exit(0);
    }

    //Fagyott TRUE ha feltétel teljesül
    fagyott = hom<=0;
    //Hőmérséklet Fahrenheit-ba átváltás
    homF = hom*9/5 + 32;

    //Hőmérséklet kiiratás
    cout << hom << endl;
    //Hőmérséklet vizsgálata fagypont szempontból
    cout <<"A homerseklet fagypont ";

    if (fagyott) {
        cout <<"alatt ";
    } else {
        cout <<"felett ";
    }

    cout << "van!"<<endl;
    //Hőmérséklet megjelenítése FahrenHeit-ban
    cout <<"A homerseklet Fahrenheit-ban: " << homF << endl;
    //Billentyűnyomásra várás
    cin.get();

    //Visszatérési érték
    return 0;

}
