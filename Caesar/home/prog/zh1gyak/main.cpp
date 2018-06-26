#include <iostream>

using namespace std;

int main()
{
    // N napig felirták hogy hány busz, hány autó, hány teherautó és hány motor ment át.

int n;
cout << "napok száma: "; cin >> n;
int autok[n]; int motor[n]; int teher[n]; int busz[n];

//autok
cout << "autók száma" <<endl;
for (int i=1; i<=n; i++)
{
    cout << i << ". nap: " << endl;
    cin >> autok[i];
};
//motorok
cout << "motorok száma" <<endl;
for (int i=1; i<=n; i++)
{
    cout << i << ". nap: " << endl;
    cin >> motor[i];
};
//teherautók
cout << "teherautók száma" <<endl;
for (int i=1; i<=n; i++)
{
    cout << i << ". nap: " << endl;
    cin >> teher[i];
};
//buszok
cout << "buszok száma" <<endl;
for (int i=1; i<=n; i++)
{
    cout << i << ". nap: " << endl;
    cin >> busz[i];
};


//1. volt e olyan nap amikor az összes áthaladás  db alatt volt.
cout << "1. volt e nap amikor összesen több mint 100 jármû haladt át?";

int m=100; bool volte = false;
for (int i=1; i<=n; i++)
{
    if (autok[i]+motor[i]+teher[i]+busz[i]>m)
    {
        volte = true;
    };
};
if (volte == true)
{
    cout << "volt." << endl;
} else cout <<  "nem volt." << endl;

//2. melyik nap haladt át a legkevesebb motor?
cout << "2. Melyik nap haladt ût a legkevesebb motor?" <<endl;

int min=motor[1]; //elõször feltételezzük h elsõ nap haladt át a legkevesebb motor. most mért ne mert max tényleg azt szegény program futotta csak le mind az i esetet
int minnap=1;
for (int i=1; i<=n; i++)
{
    if (min>motor[i])
    {
        min=motor[i];
        minnap=i;
    };
};
cout << minnap <<". nap" <<endl;

//mindjárt vége
// 3. hány nap vt több teher mint busz + motor?
cout << "3. Hány nap haladt át több teherautó mint busz és motor összesen?" << endl;

int nap=0;
for (int i=1; i<=n; i++)
{
    if (motor[i]+busz[i]<teher[i])
    {
        nap++;
    };
};
cout << nap << " ilyen nap volt" << endl;

//mester vagyok
//4.
cout << "4. volt e egymás után 3 olyan nap, hogy több autó haladt át mint busz és teherautó összesen?" << endl;

bool pah=false;
int a[n];
for (int i=1; i<=n; i++)
{
    a[i]=busz[i]+teher[i];
};
for (int i=1; i<=n; i++)
{
    if (a[i]<autok[i])
    {
        if (a[i+1]<autok[i+1])
        {
            if (a[i+2]<autok[i+2])
            {
                pah=true;
               // cout << i << endl; //csak kivi vagyok mi jön ki ebből
            };
        };
    };
};
if (pah==true)
{
    cout << "volt," <<endl;

} else cout << "nem volt." << endl;

//5. volt e olyan nap hogy valamiből nem volt forgalom
cout << "5. volt e olyan nap amikor az egyik típusú járműből nem ment át egy sem az úton" << endl;

//most kicsit leröviditem a progi lefutási idejét mert hát ost mine nézze végig szegény az összes járműtípust ha már tudja hogy az egyikből volt hogy nem ment át egy sem
//azaz mostmégsem
bool nee=false;
for (int i=1; i<=n; i++)
{
    if (autok [i]=0) { nee=ture; };
};
if (nee=false)
{
    for (int i=1; i<=n; i++)
{
    if (motor[i]=0) { nee=ture; };
};
if (nee=false)
{
    for (int i=1; i<=n; i++)
{
    if (busz[i]=0) { nee=ture; };
};
if (nee=false)
{
    for (int i=1; i<=n; i++)
{
    if (teher[i]=0) { nee=ture; };
};
}
if (nee==true)
{
    cout << "volt" << endl;
} else cout << "nem volt." << endl;
    return 0;
}
