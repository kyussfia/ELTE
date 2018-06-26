#include <iostream>

using namespace std;
/*15. A meteorológiai intézet az ország N településére adott M napos idõjárás elõrejelzést, az
adott településen az adott napra várt legmagasabb hõmérsékletet. Készíts programot, amely
megadja azokat a településeket, ahol a hõmérséklet egyik napról a következõre legalább K fokot
változik!*/

int main()
{
    int n;
    int m;    //n: települések száma,m: napok száma;
    do
    {
     cout<<"Add meg a települések számmát:"<<endl;
     cin>>n;
    }
    while(n<=0);
    do
    {
        cout<<"Add meg a napok számát:"<<endl;
        cin>>m;
    }
    while(m<=0);                                    //Adatok beolvasása

    int a [n][m];
    cout<<"Aja meg a legnagyobb várt hõmérsékletet(adott napra,adott településre):"<<endl;

        for (int i=1; i<=n; i++)
    {
        cout << i << ". település" << endl;
        for (int j=1; j<=m; j++)
        {
            cout << j << ". nap: " << endl;
            cin >> a[i][j];
        };
    };                                              //n*m mátrix létrehozása

    int k;
    cout<<"Add meg k-t:"<<endl;
    cin >> k;
    cout<<"Ezeken a településeken,történt k fokú hõmérsékletváltozás(ha nincs adat,értelemszerûen nem volt ilyen település sem):"<<endl;

    for (int i=1;i<=n;i++)
    {
        for(int j=1;j<m;j++)
        {
            if(a[i][j+1]-a[i][j]>=k)
            {
              cout<<i<<".település"<<endl;
              j=m;
            }
            else
            {
                if(a[i][j]-a[i][j+1]>=k)
                {
                    cout<<i<<".település"<<endl;
                    j=m;
                };
            };
        };
    };
cout<<"Nyomj meg egy billenytût a kilépéshez!"<<endl;
return 0;

}
