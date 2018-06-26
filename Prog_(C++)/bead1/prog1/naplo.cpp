#include <iostream>
#include <cstdlib>
#include <fstream>
#include <locale>
#include <string>
#include <vector>
#include "read.h"

using namespace std;

void readNames(int n, const string &str, vector<string> &v);
void readMatrixValues(const vector<string> &telep, const vector<string> &madar, vector<vector<int> > &t);

void Fajlbol_olvas(const string &fajlnev, vector<string> &telep, vector<string> &madar, vector<vector<int> > &t);

int countBirds(const vector<vector<int> > &t);
bool allAppeared(const vector<int> &telep);

bool validValue(int k);
bool menuNumber(int s);
//Feladat: 	A fõprogram gondoskodik a beolvasás, a kiértékelés, kiíratás részek aktivizálásáról.
//Bemenõ adatok:vector<vector<int> > naplo 	- osztályzatokat tartalmazó mátrix
//		        vector<string> tanulo 		- tanulók neveit tartalmazó tömb
//		        vector<string> targy		- tárgyak neveit tartalmazó tömb
//Kimenõ adatok:igen/nem válasz
//Tevékenység:	Eldönti, hogy mi legyen a beolvasás forrása:
//    parancssorban megadott szöveges állomány neve (ez az alapértelmezett, ha megadtuk)
//    vagy menübõl választhatunk a billentyûzet illetve szöveges állomány között.
//    Feltölti a bemenõ változókat: billentyûzetrõl beolvassa a tanulók számát,
//    majd a neveiket, ugyanazt teszi a tantárgyakkal, és beolvassa az egyes osztályzatokat.
//    Ezután meghívja a feladatot megoldó mindenkinek_ket_otos() függvényt.
//    Ennek eredményétõl függõen pozitív vagy negatív választ ír a standard kimenetre.
int main(int argc, char *argv[])

{

    setlocale(LC_ALL,"Hun"); //magyar nyelvre átállítás
    vector<vector<int> > t;  //t mátrix amiben a mért értékeket tároljuk
    vector<string>      town; // town - települések neveinek vektora
    vector<string>      bird; // bird - a madárfajok neveinek vektora
    int n,m; //n - a town mérete; m - a bird mérete

    // Beolvasás
    int s = 0;
    string fajlnev;
    if (argc<=1) {
        cout << "Válassza ki az adatbevitel módját!\n";
        cout << "1 - adatok beolvasása billentyûzetrõl\n";
        cout << "2 - adatok beolvasása fájlból\n";
        cout << "3 - kilépés\n";
        s = ReadInt("Adatbevitel módja: ","1, 2 vagy 3 lehet", menuNumber);
    }
    switch (s) {
        case 0:
            fajlnev = argv[1];
            Fajlbol_olvas(fajlnev, town, bird, t);
            break;
        case 1:
            n = ReadNat("Hány település van? ", "Természetes szám kell!");
            cout << "Adja meg a települések neveit: " << endl;
            readNames(n, "település ", town);
            cout << endl;

            m = ReadNat("Hány madárfaj van? ","Természetes számot kérek!");
            cout << "Adja meg a madárfajok neveit: "<< endl;
            readNames(m, "madárfaj ", bird);
            cout << endl;

            cout << "Adja meg az mért előfordulási mutatókat!"<< endl;
            readMatrixValues(town, bird, t);
            cout << endl;
            break;
        case 2:
            cout << "Add meg a fájl nevét: ";
            cin >> fajlnev;
            Fajlbol_olvas(fajlnev, town, bird, t);
            break;
        case 3:
            exit(0);
    }

    // Kiértékelés
    cout << "Összesen " << countBirds(t) <<" településen, fordult elő mindegyik madárfaj \n";
    cout<<endl;
    return 0;
}

int countBirds(const vector<vector<int> > &t) {
    int c = 0;
    for( int i = 0; i<(int)t.size(); i++) {
        if(allAppeared(t[i])){
            c += 1;
        }
    }
    return c;
}



bool allAppeared(const vector<int> &telep) {
    bool l = true;
    for( int j = 0; l && j<(int)telep.size(); j++) {
        l = telep[j] > 0;
    }
    return l;
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
        cout << telep[i] << " eredményei\n";
        for(int j=0; j<(int)madar.size(); ++j)
        {
            cout << "\t" << madar[j] << ": ";
            a[i][j] = ReadInt("","0 vagy nagyobb természetes szám kell!", validValue);
        }
    }
}

//Feladat: 	Tanuló és tantárgy neveket olvas egy szöveges állományból,
//          majd ugyanonnan 1 és 5 közötti számokkal feltölti a naplót.
//Bemenõ adatok:string fajlnev 		        - inputfájl neve
//Kimenõ adatok:vector<string> tanulo		- tanulok neveit tartalmazó tömb
//              vector<string> targy		- tárgyak neveit tartalmazó tömb
//              vector<vector<int> > naplo 	- osztályzatokat tartalmazó mátrix
//Tevékenység:	Megfelelõ méretûre (n és m) állítja be a tanulo és a targy tömböt,
//		majd feltölti az értékeit sztringekkel. A tanuló neve tartalmazhat szóközt is.
//      Megfelelõ méretûre (n×m) állítja be a naplo mátrixot,
//		majd feltölti az értékeit 1 és 5 közötti számokkal.
//		A beolvasott adatok a konzolablakban is megjelennek.
void Fajlbol_olvas(const string &fajlnev, vector<string> &telep, vector<string> &madar, vector<vector<int> > &t)
{
    ifstream f(fajlnev.c_str());
    if(f.fail()){
        cout << "Hibás fájlnév!\n";
        exit(1);
    }

    int n, m;
    f >> n >> m;
    string str;
    getline(f, str, '\n');

    cout << "\nTelepülések:\n";
    telep.resize(n);
    for(int i=0; i<n; ++i)
    {
        getline(f, telep[i], '\n');
        cout << telep[i] << endl;
    }

    cout << "\nMadárfajok:\n";
    madar.resize(m);
    for(int j=0; j<m; ++j)
    {
        f >> madar[j];
        cout << madar[j] << endl;
    }

    cout << "\nElőfordulások:\n";
    t.resize(n);
    for(int i=0; i<(int)telep.size(); ++i)
    {
        t[i].resize(m);
        cout << telep[i] << " madárfajainak száma\n";
        for(int j=0; j<(int)madar.size(); ++j)
        {
            f >> t[i][j];
            cout << "\t" << madar[j] << ": " << t[i][j];
        }
        cout << endl;
    }
}


/**
 * Validálja a menü-nél a beolvasott menü sorszámát,hogy csak az adott menüpontok közül lehesssen választani.
 *
 * @param  int  s  A választott(beolvasott szám)
 * @return bool -  A függvény visszatérérési értéke: True = valid menüpont
 **/
bool menuNumber(int s)
{
    return s>=1 && s<=3;
}
