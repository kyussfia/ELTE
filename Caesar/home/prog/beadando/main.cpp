#include <iostream>

using namespace std;

/*
Programozási alapismeretek, beadandó feladat: Idõjárás jelentés
Fekete Andrea Márta
Feladatszám: 5
Kurzuskód: m1c2pn2/5
Gyakorlatvezetõ: Daiki Tennó

5. A meteorológiai intézet az ország N településére adott M napos idõjárás elõrejelzést, az
adott településen az adott napra várt legmagasabb hõmérsékletet. Készíts programot, amely
megadja azt a települést, ahol az elõrejelzés szerint egyik napról a másikra a lehetõ legnagyobb
a változás!
*/

int main()
{
    int n; int m;        //n: települések száma, m: napok száma
    do {
    cout << "Adja meg a települések számát!" << endl;
    cin >> n; } while (n<=0);   //beolvasás
    do {
    cout << "Adja meg a napok számát!" << endl;
    cin >> m; } while (m<=0);   //beolvasás
    int a[n][m];     //az adott településen az adott napra várt legmagasabb hõmérsékletet tartalmazza

    //beolvasás
    cout << "Adja meg a várt legmagasabb hõmérsékletet" << endl;
    for (int i=1; i<=n; i++)
    {
        cout << i << ". település" << endl;
        for (int j=1; j<=m; j++)
        {
            cout << j << ". nap: " << endl;
            cin >> a[i][j];
        };
    };
    // feladat megoldása
    int max=0;  //maximális hőmérsékletkülönbség
    int telepules=0;    //max értékéhez tartozó település
    for (int i=1;i<=n;i++)
    {
        for (int j=1;j<m;j++)
        {
            if (a[i][j+1]-a[i][j]>max)
            {
                max=a[i][j+1]-a[i][j];
                telepules=i;

            } else
            {
                if (a[i][j]-a[i][j+1]>max)
                {
                    max=a[i][j]-a[i][j+1];
                    telepules=i;
                };
            };
        };
    };
    if (telepules<0)
    {
        cout << "Egyik napról a másikra a lehető legnagyobb változás a(z) " << telepules << ". településen volt, " << max << " fok." << endl;
    } else
    {
        cout << "Minden településen 0 fok volt a változás." << endl;
    }

    cout << "Nyomjon meg egy gombot a kilépéshez..." << endl;

    return 0;
}
