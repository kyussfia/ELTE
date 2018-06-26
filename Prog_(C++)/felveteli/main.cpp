//
// Feladat: A "felvi.be" fájlban adottak a következõ adatok: a ponthatár, a felvételizõk száma, majd
//          soronként a felvételizõ neve (20 karakteren) és elért pontszáma.
//	        Válogassuk ketté a felvetteket és az elutasítottakat. Az eredményt a képernyőre írjuk ki.

#include <iostream>
#include <fstream>
#include <string>
#include <cstdlib>

using namespace std;

// Egy felvételizõ neve és pontszáma egy "ember" típusú rekordba fog kerülni.
struct ember
{
    string nev;
    int pont;
};

// ponthatár, db, (név(20), pontszám(5))
void beolvas(int &h, int &n, ember felvi[]);

void feldolg(int h, int n, ember felvi[], int &db1, ember y[], int &db2, ember z[]);

void kiir(int n, ember x[]);

int main()
{

// változók deklarálása
int h,n, db1,db2;
ember felvi[100],y[100],z[100];

setlocale(LC_ALL,"Hun");

   beolvas(h,n,felvi);

// szétválogat
   feldolg(h,n,felvi,db1,y,db2,z);


// felvettek
   cout <<endl<< "Felvettek: " << endl;
   kiir(db1,y);

//elutasitottak

   cout <<endl<< "Elutasítottak: " <<endl;
   kiir(db2,z);


    return 0;
}

void beolvas(int &h, int &n, ember felvi[])
{
ifstream be("felvi.be");
if (be.fail())
{ cout << "Nem nyitható meg az adatfájl!";
  exit(1);
};

string sor;
   getline(be,sor,'\n');
   h=atoi(sor.c_str());  //ponthatár
   getline(be,sor,'\n');
   n=atoi(sor.c_str());  //felvetelizok szama
   cout << "Ponthatár: " << h << endl;
   cout << "Felvételizõk száma: " << n << endl;
   cout << "Felvételi eredmények: " << endl;

   for (int i=0;i<n;++i)
   {
       getline(be,sor,'\n');  // olvasása
       felvi[i].nev=sor.substr(0,20);
       felvi[i].pont=atoi(sor.substr(20,5).c_str());
       cout << i+1 << ". " << felvi[i].nev << " "<<felvi[i].pont << " pont" <<endl;
   }
    be.close();
}

// Szétválogatás
void feldolg(int h, int n, ember felvi[], int &db1, ember y[], int &db2, ember z[])
{
    db1=-1;
    db2=-1;

    for (int i=0;i<n;i++)
    {
        if(felvi[i].pont>=h)
        {
            db1++;
            y[db1]=felvi[i];
        }
        else
        {
            db2++;
            z[db2]=felvi[i];
        }

    }
    db1=db1++;
    db2=db2++;
    // Ezt kell megírni.

}

void kiir(int n, ember x[])
{
    for(int i=0;i<n;i++)
    {
        cout<<"A felvettek nevsora: \n";
        cout<<i+1<<". felvetelt nyert szemely: "<<x[i].nev<<" "<<x[i].pont<<endl;
    }
    // Ezt kell megírni.
}

