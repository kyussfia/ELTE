#include <iostream>

using namespace std;
/*N r�sztvev� s�r-/F�ty�l�s-fogyaszt�s�t. Feljegyezt�k a kor�t (18..37) �s
alkohol-�rz�kenys�g�t (A�=azt az alkohol mennyis�get [10..200; ml-ben], amely ut�n
�szrevehet� ked�lyv�ltoz�son ment �t). Az ezzel kapcsolatos feladatok megold�s�hoz kell egy programot �rni!*/


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

/*a) Legfeljebb h�ny poh�r (=3 dl) s�rt ihatnak a megfigyel�sben
r�sztvev�k (k�l�n-k�l�n), ha tudjuk, hogy a felk�n�lt Z�ld s�r alkoholfoka 5% (t�rfogatsz�zal�k)?*/

//3 dl=300 ml
//300*0.05=15

int h;

    for(int i=1;i<=n;i++)
    {
        h=AE[i]/15;
        cout << h <<" ";
    };

cout<<endl;

/*b) Az adatok alapj�n kijelenthet�-e, hogy a 28 �v alattiak ��tlagosan�
 jobban b�rj�k a s�rt, mint a 27 felettiek? Azaz a 28 alattiak �tlagos �rz�kenys�ge
  magasabb, mint a 27 felettiek�. �rja ki a k�t �tlagot (28 alatti, 27 feletti sorrendben;
  eg�szre csonk�tva) �s az �tlagok pontos �rt�ke alapj�n hozott v�gk�vetkeztet�st sz�vegesen.
  Ha va-lamely �korcsoport� �res lenne, akkor a hozz�tartoz� �tlag legyen 0. A sz�veg �IGAZ�, �HAMIS�,
  ill. �res csoport eset�n �ELDONTHETETLEN� lehet. A 3 v�laszelemet egy-egy sz�k�zzel v�lassza el!*/

  double ossz1=0;      //27 felettiek �sszege
  double ossz2=0;    //28 alatti �sszege
  int a1=0;           //27 felettiek sz�ma
  int a2=0;            //28 alattiak sz�ma

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
      cout<<"ELD�NTHETETLEN";
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
