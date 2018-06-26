#include <iostream>

using namespace std;

//Egy betegnek N napon keresztül mérték a vérnyomását és pulzusát. (minden mérés alkalmával 3 adatot jegyeztek fel: systolés vérnyomás /20-300 közti egész/, diasztolés vérnyomást /20-160 közti egész/ és a pulzusát /30-250 közti egész/)
//Határozd meg, hogy
//1.)     Volt-e olyan nap, amikor a vérnyomás adatok alapján súlyos emelkedett állapotban volt a beteg? (systolés:160 felett vagy diastolés:100 felett)
//2.)     Melyik napon volt a legalacsonyabb a beteg pulzusa?
//3.)     Hány olyan nap volt, amikor normálérték alatt volt a vérnyomás, de emelkedett volt a pulzus? (normál vérnyomás systolés:140 alatt és diastolés: 90 alatt; emelkedett pulzus: 100 feletti)
//4.)     Állapítsd meg a beteg átlag pulzusát a rögzített idõszakban!
//5.)     Ha volt olyan egymást követõ 3 nap, amikor a pulzus 100 alatt volt, akkor ezen napokban volt-e a vérnyomás valamelyik összetevõje normál határérték felett? (normál értékek a 3-as részfeladatban vannak megadva)

int bekeres(int ah, int fh)
{
 int pah;
 do
 {
     cin >> pah; cout << endl;
 }   while (pah<ah || pah >fh);
 return pah;
}

int main()
{

    int n;
    do
    {
        cout << "adja meg a napok számát!" << endl;
        cin >> n;
    } while (n<0);
    int svn[n];
    int dvn[n];
    int pulz[n];
    cout << "Systolés vérnyomás: " << endl;
    for (int i=1; i<=n; i++)
    {
      cout << i << ". nap: " << endl;
      svn[i]=bekeres(20,300);
    };
    cout << "Diasztolés vérnyomás: " << endl;
    for (int i=1; i<=n; i++)
    {
      cout << i << ". nap: " << endl;
      dvn[i]=bekeres(20,160);
    };
    cout << "Pulzus: " << endl;
    for (int i=1; i<=n; i++)
    {
      cout << i << ". nap: " << endl;
      pulz[i]=bekeres(30,250);
    };

 //1.)     Volt-e olyan nap, amikor a vérnyomás adatok alapján súlyos emelkedett állapotban volt a beteg? (systolés:160 felett vagy diastolés:100 felett)

    bool volte=false;
    int mikor;
    for (int i=1; i<=n; i++)
    {
        if (svn[i]>160 || dvn[i]>100)
        {
            mikor=i;
            volte=true;
            i=n+1;
        };
    };
    if (volte==true)
    {
        cout << "volt olyan nap, amikor a beteg súlyos emelkedett állapotban volt, elõször a " << mikor << ". napon" << endl;
    } else
    {
        cout << "nem volt olyan nap, amikor súlyos emelkedett állapotban volt a beteg." << endl;
    }
//2.)     Melyik napon volt a legalacsonyabb a beteg pulzusa?

    mikor=1;
    int min=250;
    for (int i=1; i<=n; i++)
    {
        if (pulz[i]<min)
        {
            min=pulz[i];
            mikor=i;
        };
    };
    cout << "A beteg pulzusa a " << mikor << ". napon volt a legalacsonyabb" << endl;

//3.)     Hány olyan nap volt, amikor normálérték alatt volt a vérnyomás, de emelkedett volt a pulzus? (normál vérnyomás systolés:140 alatt és diastolés: 90 alatt; emelkedett pulzus: 100 feletti)

    int nap=0;
    for (int i=1; i<=n; i++)
    {
        if (svn[i]<=140 && dvn[i] <=90 && pulz[i]>100)
        {
            nap++;
        };
    };

    return 0;
}
