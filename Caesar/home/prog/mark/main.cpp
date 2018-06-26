#include <iostream>

using namespace std;
/*N résztvevõ sör-/Fütyülõs-fogyasztását. Feljegyeztük a korát (18..37) és
alkohol-érzékenységét (AÉ=azt az alkohol mennyiséget [10..200; ml-ben], amely után
észrevehetõ kedélyváltozáson ment át). Az ezzel kapcsolatos feladatok megoldásához kell egy programot írni!*/


int main()
{
   int n;
  do
  {

   cout<<"Add meg az emberek szamat:"<<endl;
   cin>>n;
  }
  while(n<=0);

  int k[n];
  for(int i=1;i<=n;i++)
  {
    do
    {
    cout<<i<<". ember kora?(egesz szammal megadva)"<<endl;
    cin>>k[i];
    }
    while(18>k[i] || 37<k[i]);
  }
    double AE[n];

for(int i=1;i<=n;i++)

    {
        do
        {
        cout<<i<<". ember alkoholmennyisege?"<<endl;
        cin>>AE[i];
        }
        while(10>AE[i] || AE[i]>200);

    }

/*a) Legfeljebb hány pohár (=3 dl) sört ihatnak a megfigyelésben
résztvevõk (külön-külön), ha tudjuk, hogy a felkínált Zöld sör alkoholfoka 5% (térfogatszázalék)?*/

//3 dl=300 ml
//300*0.05=15

int h;

    for(int i=1;i<=n;i++)
    {
        h=AE[i]/15;
        cout << h <<" ";
    };

cout<<endl;

/*b) Az adatok alapján kijelenthetõ-e, hogy a 28 év alattiak „átlagosan”
 jobban bírják a sört, mint a 27 felettiek? Azaz a 28 alattiak átlagos érzékenysége
  magasabb, mint a 27 felettieké. Írja ki a két átlagot (28 alatti, 27 feletti sorrendben;
  egészre csonkítva) és az átlagok pontos értéke alapján hozott végkövetkeztetést szövegesen.
  Ha va-lamely „korcsoport” üres lenne, akkor a hozzátartozó átlag legyen 0. A szöveg „IGAZ”, „HAMIS”,
  ill. üres csoport esetén „ELDONTHETETLEN” lehet. A 3 válaszelemet egy-egy szóközzel válassza el!*/

  double ossz1=0;      //27 felettiek összege
  double ossz2=0;    //28 alatti összege
  int a1=0;           //27 felettiek száma
  int a2=0;            //28 alattiak száma

  for (int i=1;i<=n;i++)
  {
      if(k[i]>27)
      {
        ossz1=ossz1+AE[i];
        a1++;
      };
      if(k[i]<28)
      {
          ossz2=ossz2+AE[i];
          a2++;
        };

  };
   cout<<ossz1/a1<<" "<<ossz2<<" ";
  if(ossz1==0 || ossz2==0)
  {
      cout<<"ELDÖNTHETETLEN";
  }
  else
  {
      if((ossz1/a1)<(ossz2/a2))
      {
          cout<<"IGAZ";
      }
      else
      {
          cout<<"HAMIS";
      };
  };
  cout<<endl;






   }
