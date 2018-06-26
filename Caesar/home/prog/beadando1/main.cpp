#include <iostream>

using namespace std;

int main()
{
    int n; int m;        //n: települések száma, m: napok száma
    int a[n][m];     //az adott településen az adott napra várt legmagasabb hõmérsékletet tartalmazza
    cout << "Adja meg a települések számát!" << endl;
    cin >> n;
    cout << "Adja meg a napok számát!" << endl;
    cin >> m;
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
    return 0;
}
