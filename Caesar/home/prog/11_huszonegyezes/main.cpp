//
//
//
//
//11 Feladat:
//
//Huszonegyezes. A játékot többen játszhatják, a cél az, hogy minél jobban meg-
//közelítse alulról a 21-et (ha 21-nél több, akkor nem nyerhet, befuccsolt) - a
//lapok értékei a rajta levő számok (7-10), az alsóé: 2, a felső: 3, a király: 4
//és az ász: 11. Kezdetben a játékosok 2 lapot kapnak, amihez kérhetnek még. A
//kódban adott, hogy mennyi a kézben tartott 2 lap értéke. Olvassa be a kért lap
//értékét, ha nem létezik ilyen értékű lap, akkor jelezze a hibát és lépjen ki a
//programból! Helyes adat esetén írja ki az eredményt és azt is, ha befuccsolt!
//
//Specifikáció:
//
//Be: KertLap: Egész
//   [Konstans KezbenTartott1, KezbenTartott2]
//Ki: LapokErteke: Egész
//    Befuccsolt:Logikai
//Ef: Kertlap eleme [2,3,4,7...10,11]
//Uf: LapokErteke = KezbenTartott1 + KezbenTartott2 + KertLap
//    Befuccsolt = 21<LapokErteke
//
//Teendõk:
//TODO Specifikáció
//
//Fejlesztések:
//Több játékosra való írás, véletlenszerű kártyakiosztás (tömbök), de játék
//struktúrája maradna

#include <iostream>

using namespace std;

int main() {

    //Huszonegyezés 11
    //Változók deklarálása
    int KertLap, LapokErteke;
    const int KezbenTartott1 = 11, KezbenTartott2 = 8;
    bool Befuccsolt;

    //Kért lap értékének beolvasása, változóba mentés
    cout <<"Kert lap erteke: ";
    cin>>KertLap; cin.get();

    //Kért lap értékének vizsgálata
    switch (KertLap) {
    case 7:
    case 8:
    case 9:
    case 10:
    case 2:
    case 3:
    case 4:
    case 11:
        //Összes lap értékének meghatározása, ami kezünkben van
        LapokErteke = KertLap+KezbenTartott1+KezbenTartott2;
        //Befuccsolás tényének vizsgálata, TRUE ha 21 felett van össz érték
        Befuccsolt = LapokErteke>21;

        //Lapok értékének kiiratása
        cout<<"Lapok erteke: "<<LapokErteke<<endl;
        //Ha 21-nél több az érték, akkor ennek megjelenítése
        if (Befuccsolt) cout<<"Befuccsolt! Sajnos most NEM nyert!";
        break;
    default:
        //Érvénytelen Lapot kért
        cout <<"Ilyen erteku lap nem letezik!" <<endl;
        break;
    }
    //Billentyűnyomásra vár
    cin.get();

    //Visszatérési érték
    return 0;

}
