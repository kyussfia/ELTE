//
//
//
//
//09 Feladat:
//
//Miquel, a perui diák bukása. Peruban 1-től 20-ig osztályoznak, de 1-től 10-ig sajnos
//bukásnak számít az elért eredmény. Olvassuk be az osztályzatot és azonnal jelezzük, ha
//az nem felel meg osztályzatnak, ekkor lépjünk ki a porgramból. Helyes osztályzat
//beolvasása után írjuk ki azt és a bukás tényét, ha az elért osztályzat nem megfelelő!
//
//Specifikáció:
//
//Be: Osztalyzat:Egész
//Ki: Osztalyzat:Egész
//    Bukas:Logikai
//Ef: Osztalyzat eleme [1..20]
//Uf: Osztalyzat'=Osztalyzat
//    Bukas=Osztalyzat<=10
//
//Teendõk:
//TODO Specifikáció

#include <iostream>

using namespace std;

int main() {

    //Perui diak bukasa 09
    //Változók deklarálása
    int Osztalyzat;
    bool Bukas;

    //Osztályzat bekérés, és változóba mentés
    cout <<"Osztalyzat: ";
    cin >>Osztalyzat; cin.get();

    //Bukás tényének rögzítése, bukás TRUE, ha feltétel teljesül
    Bukas = Osztalyzat<=10;

    //Osztályzat vizsgálata, megfelelő érték lehet csak
    if ((Osztalyzat>=1) && (Osztalyzat<=20)) {
        //Osztályzat kiiratás
        cout << Osztalyzat << endl;
        //Ha bukás, akkor ennek tényének megjelenítése
        if (Bukas) cout <<"Sajnos ebbol a tantargybol bukasra all!" <<endl;
    } else {
        //Nem megfelelő osztályzatnál, tény közlése
        cout <<"Nem megfelelo osztalyzat!" <<endl;
    }
    //Billentyűnyomásra várás
    cin.get();

    //Visszatérési érték
    return 0;

}
