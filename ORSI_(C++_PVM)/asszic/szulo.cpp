/* Osztott rendszerek specifikacioja es implementacioja */
// Feladat: asszociativ fuggveny kiszamitasara vonatkozo
// tetelre valo visszavezetes, matrix szorzas
// Keszitette: Ivan Gergo
// EHA: IVGRAAI.ELTE
// e-mail cim: ivgraai@gmail.com
// Gyakorlat vezeto neve: Tejfel Mate

#include <iostream>
#include <fstream>
#include <vector>
#include <cstdlib>
#include <pvm3.h>

using namespace std;

int main(int argc, char* argv[], char* envp[])
{
    if(argc != 2)
    {
	cout << "Hasznalat: " << argv[0] << " <bemeneti-fajl>!" << endl;
	return 1;
    }
    
    ifstream infile;
    infile.clear();
    infile.open(argv[1]);
    if(infile.fail())
    {
	cout << "Nem tudtam megnyitni a fajlt olvasasra!" << endl;
	exit(1);
    }
    
    int db = -1;
    infile >> db;
    if(db < 1 || db > 12) // hany blade van?
    {
	cout << "Helytelen darabszam talalhato a fajl elejen!" << endl;
	exit(2);
    }
    vector<vector<vector<int> > > input;
    input.resize(db);
    for(int i = 0; i < db; ++i)
    {
	int k = -1, l = -1;
	infile >> k >> l;
	input[i].resize(k);
	for(int m = 0; m < k; ++m)
	{
	    input[i][m].resize(l);
	    for(int n = 0; n < l; ++n)
	    {
		infile >> input[i][m][n];
	    }
	}
    }
    
    int* tids = new int[db];
    if(pvm_spawn("gyerek", 0, PvmTaskDefault, 0, db, tids) < db)
    {
	pvm_perror("spawn");
	exit(3);
    }
    
    for(int i = 0; i < db; ++i)
    {
	pvm_initsend(PvmDataDefault);
	int m = (int)input[i].size(), n = (int)input[i][0].size();
	pvm_pkint(&m, 1, 1); pvm_pkint(&n, 1, 1);
	for(int k = 0; k < (int)input[i].size(); ++k)
	{
	    for(int l = 0; l < (int)input[i][k].size(); ++l)
	    {
		pvm_pkint(&input[i][k][l], 1, 1);
	    }
	}
	pvm_send(tids[i], 1);
    }
    
    for(int i = 0; i < db; ++i)
    {
	cout << endl << i + 1 << ". matrix:" << endl;
	pvm_recv(tids[i], 1);
	int m, n;
	pvm_upkint(&m, 1, 1); pvm_upkint(&n, 1, 1);
	int data;
	for(int k = 0; k < m; ++k)
	{
	    for(int l = 0; l < n; ++l)
	    {
		pvm_upkint(&data, 1, 1);
		cout << data << ' ';
	    }
	    cout << endl;
	}
    }
    
    //delete[] tids;
    pvm_exit();
    
    return 0;
}