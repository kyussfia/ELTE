/* Osztott rendszerek specifikacioja es implementacioja */
// Feladat: asszociativ fuggveny kiszamitasara vonatkozo
// tetelre valo visszavezetes, matrix szorzas
// Keszitette: Ivan Gergo
// EHA: IVGRAAI.ELTE
// e-mail cim: ivgraai@gmail.com
// Gyakorlat vezeto neve: Tejfel Mate

#include <iostream>
//#include <vector>
#include <pvm3.h>

using namespace std;

int main(int argc, char* argv[], char* envp[])
{
    int* tids;
    int numtask = pvm_siblings(&tids);
    
    int index = 0;
    while(index < numtask && tids[index] != pvm_mytid())
    {
	++index;
    }
    
    pvm_recv(pvm_parent(), 1);
    int i, j;
    pvm_upkint(&i, 1, 1); pvm_upkint(&j, 1, 1); // kiolvassuk a sorok es oszlopok szamat
    
    /*vector<int> data;
    data.resize(i);
    for(int k = 0; k < i; ++k) // fogadjuk az index. matrixot
    {
	data[k].resize(j);
	for(int l = 0; l < j; ++l)
	{
	    pvm_upkint(&data[k][l], 1, 1);
	}
    }*/
    
    int** data;
    data = new int*[i];
    for(int k = 0; k < i; ++k) // fogadjuk az index. input matrixot
    {
	data[k] = new int[j];
	pvm_upkint(data[k], j, 1);
    }
    
    // INNENTOL megvan a "gs(index, k(index))"!
    int t = 1;
    
    while(index + t < numtask || index - t >= 0)
    {
	if(index + t < numtask)
	{
	    pvm_initsend(PvmDataDefault);
	    pvm_pkint(&i, 1, 1); pvm_pkint(&j, 1, 1); // elkuldjuk a matrixunkat
	    for(int k = 0; k < i; ++k)
	    {
		pvm_pkint(data[k], j, 1);
	    }
	    
	    pvm_send(tids[index + t], 1);
	}
	if(index - t >= 0)
	{
	    pvm_recv(tids[index - t], 1);
	    int k, l;
	    pvm_upkint(&k, 1, 1); pvm_upkint(&l, 1, 1);
	    int other[k][l];
	    for(int m = 0; m < k; ++m)
	    {
		pvm_upkint(other[m], l, 1);
	    }
	    
	    // uj ertek szamitasa (_new-ba)
	    int** _new;
	    _new = new int*[k];
	    for(int m = 0; m < k; ++m)
	    {
		_new[m] = new int[j];
		for(int n = 0; n < j; ++n)
		{
		    _new[m][n] = 0;
		}
	    }
	    for(int m = 0; m < k; ++m)
	    {
		for(int n = 0; n < j; ++n)
		{
		    for(int o = 0; o < l/* || o < i*/; ++o)
		    {
			_new[m][n] += other[m][o] * data[o][n]; // VIGYAZAT: a matrix szorzas nem kommutativ!
		    }
		}
	    }
	    // uj ertek visszamasolasa a data-ba!
	    for(int m = 0; m < i; ++m)
	    {
		delete[] data[m];
	    }
	    delete[] data;
	    i = k; data = _new;
	}
	
	t *= 2;
    }
    
    pvm_initsend(PvmDataDefault);
    pvm_pkint(&i, 1, 1); pvm_pkint(&j, 1, 1); // elkuldjuk a matrixunkat a szulonek, ha vegeztunk
    for(int k = 0; k < i; ++k)
    {
	pvm_pkint(data[k], j, 1);
    }
    pvm_send(pvm_parent(), 1);
    
    for(int m = 0; m < i; ++m)
    {
	delete[] data[m];
    }
    delete[] data;
    pvm_exit();
    
    return 0;
}