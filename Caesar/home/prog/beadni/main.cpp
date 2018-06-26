#include <iostream>

using namespace std;
/*15. A meteorol�giai int�zet az orsz�g N telep�l�s�re adott M napos id�j�r�s el�rejelz�st, az
adott telep�l�sen az adott napra v�rt legmagasabb h�m�rs�kletet. K�sz�ts programot, amely
megadja azokat a telep�l�seket, ahol a h�m�rs�klet egyik napr�l a k�vetkez�re legal�bb K fokot
v�ltozik!*/

int main()
{
    int n;
    int m;    //n: telep�l�sek sz�ma,m: napok sz�ma;
    do
    {
     cout<<"Add meg a telep�l�sek sz�mm�t:"<<endl;
     cin>>n;
    }
    while(n<=0);
    do
    {
        cout<<"Add meg a napok sz�m�t:"<<endl;
        cin>>m;
    }
    while(m<=0);                                    //Adatok beolvas�sa

    int a [n][m];
    cout<<"Aja meg a legnagyobb v�rt h�m�rs�kletet(adott napra,adott telep�l�sre):"<<endl;

        for (int i=1; i<=n; i++)
    {
        cout << i << ". telep�l�s" << endl;
        for (int j=1; j<=m; j++)
        {
            cout << j << ". nap: " << endl;
            cin >> a[i][j];
        };
    };                                              //n*m m�trix l�trehoz�sa

    int k;
    cout<<"Add meg k-t:"<<endl;
    cin >> k;
    cout<<"Ezeken a telep�l�seken,t�rt�nt k fok� h�m�rs�kletv�ltoz�s(ha nincs adat,�rtelemszer�en nem volt ilyen telep�l�s sem):"<<endl;

    for (int i=1;i<=n;i++)
    {
        for(int j=1;j<m;j++)
        {
            if(a[i][j+1]-a[i][j]>=k)
            {
              cout<<i<<".telep�l�s"<<endl;
              j=m;
            }
            else
            {
                if(a[i][j]-a[i][j+1]>=k)
                {
                    cout<<i<<".telep�l�s"<<endl;
                    j=m;
                };
            };
        };
    };
cout<<"Nyomj meg egy billenyt�t a kil�p�shez!"<<endl;
return 0;

}
