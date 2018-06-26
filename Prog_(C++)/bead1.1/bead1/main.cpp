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

bool reRunning();
bool validValue(int k);
bool menuNumber(int s);
/**A main gondoskodik a beolvasás, a kiértékelés, kiíratás részek aktivizálásáról.
 * Eldönti, hogy mi legyen a beolvasás forrása,Feltölti a bemenõ változókat,
 * Ezután meghívja a feladatot megoldó függvényt, majd kiértékeli az eredményt.
 *
 * A mainben keletkező/deklarált adatok:
 * 		- vector<vector<int> > t 	- A mérési eredményeket tartalmazó mátrix
 *		- vector<string> town		  A települések neveit tartalmazó vektor
 * 		- vector<string> bird		  A madárfajok neveit tartalmazó vektro
 * @param int  argc   A paraméteres inditás egy változója
 * @param char *agv[] A paraméteres indítás egy változója
 *
 **/
int main(int argc, char *argv[])
{
    bool re;
    do{
    //Képernyő törlése:
    //system("CLS");
	//Deklaráció:
    vector<vector<int> > t;   //t mátrix amiben a mért értékeket tároljuk
    vector<string>      town; // town - települések neveinek vektora
    vector<string>      bird; // bird - a madárfajok neveinek vektora
    int n,m; 				  //n - a town mérete; m - a bird mérete

    // Beolvasás
    int s = 0;
    string fajlnev;
    if (argc<=1) {
        cout << "Valassza ki az adatbevitel modjat!\n";
        cout << "1 - adatok beolvasasa billentyuzetrol\n";
        cout << "2 - adatok beolvasasa file-bol\n";
        cout << "3 - kilepes\n";
        s = ReadInt("Adatbevitel modja: ","1, 2 vagy 3 lehet", menuNumber);
    }
	//Menü
    switch (s) {
        case 0:
            fajlnev = argv[1];
            Fajlbol_olvas(fajlnev, town, bird, t);
            break;
        case 1:
            n = ReadNat("Hany telepules van? ", "Termeszetes szam kell!");
            cout << "Adja meg a telepulesek neveit: " << endl;
            readNames(n, "telepules ", town);
            cout << endl;

            m = ReadNat("Hany madarfaj van? ","Termeszetes szamot kerek!");
            cout << "Adja meg a madarfajok neveit: "<< endl;
            readNames(m, "madarfaj ", bird);
            cout << endl;

            cout << "Adja meg az mert elofordulasi mutatokat!"<< endl;
            readMatrixValues(town, bird, t);
            cout << endl;
            break;
        case 2:
            cout << "Adja meg a file nevet: ";
            cin >> fajlnev;
            Fajlbol_olvas(fajlnev, town, bird, t);
            break;
        case 3:
            exit(0);
    }
    // Kiértékelés
    cout << "Osszesen " << countBirds(t) <<" telepulesen, fordult elo mindegyik madarfaj \n";
    cout<<endl;
    re = reRunning();
    }while(re);
    return 0;
}


bool reRunning() {
    cout << "Ujra futtatja a programot? (I/N) \n";
    bool a;
    string str;
    cin >> str;
    if( str.c_str() == "N" || str.c_str() == "n") {
        a = false;
    } else {
        a = true;
    }
    return a;
}

/**
 * Megszámlálja egy mátrixban, hogy hány allAppeared tulajdonságú sor van benne.
 *
 * @param  vector(vector<int>) t  Az mért értékeket tartalmazó mátrix
 * @return int				   c  A számlálás eredménye
 **/
int countBirds(const vector<vector<int> > &t) {
    int c = 0;
    for( int i = 0; i<(int)t.size(); i++) {
        if(allAppeared(t[i])){
            c += 1;
        }
    }
    return c;
}


/**
 * Optimista lineáris keresés, amely egy vectorban (egész), megvizsgálja,
 * hogy mindegyik értéke 0-nál nagyobb-e.
 *
 * @param  vector<int>    telep  A értékeket tartalmazó mátrix egy sora(a településenkénti eredmények)
 * @return bool			  l      A számlálás eredménye
 **/
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
        cout << telep[i] << " eredmenyei\n";
        for(int j=0; j<(int)madar.size(); ++j)
        {
            cout << "\t" << madar[j] << ": ";
            a[i][j] = ReadInt("","0 vagy nagyobb termeszetes szam kell!", validValue);
        }
    }
}

/**
 * A fájlból olvasás modulja: beolvassa a feltételezhetőleg helyesen kitöltött file-ból az adatokat.
 * 		(lásd: dokumentáció)
 *
 * @param const string        fajlnev    A megnyitandó file neve
 * @param vector<string>      telep      A települések neveit tartalmazó tömb
 * @param vector<string>      madar      A madárfajok neveit tartalmazó tömb
 * @param vector<vector(int)> t          Az értékekkel feltölteni kívánt mátrix
 *
 **/
void Fajlbol_olvas(const string &fajlnev, vector<string> &telep, vector<string> &madar, vector<vector<int> > &t)
{
    ifstream f(fajlnev.c_str());
    if(f.fail()){
        cout << "Hibas filename!\n";
        exit(1);
    }

    int n, m;
    f >> n >> m;
    string str;
    getline(f, str, '\n');

    cout << "\nTelepulesek:\n";
    telep.resize(n);
    for(int i=0; i<n; ++i)
    {
        getline(f, telep[i], '\n');
        cout << telep[i] << endl;
    }

    cout << "\nMadarfajok:\n";
    madar.resize(m);
    for(int j=0; j<m; ++j)
    {
        f >> madar[j];
        cout << madar[j] << endl;
    }

    cout << "\nElofordulasok:\n";
    t.resize(n);
    for(int i=0; i<(int)telep.size(); ++i)
    {
        t[i].resize(m);
        cout << telep[i] << " madarfajainak szama\n";
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
