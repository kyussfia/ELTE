#include <iostream>
#include <fstream>
#include <vector>
#include <cstdlib>
#include "read_file.h"


using namespace std;

namespace file {
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
            //exit(1);
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
}
