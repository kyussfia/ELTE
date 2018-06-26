#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
#include<pvm3.h>

using namespace std;




int main(){

        ifstream infile;
        infile.open("input.txt", ios::in);
        if(infile.fail()){
		cout<<"A file nem megnyithato!"<<endl;
		pvm_exit();
		return 1;
		}
        int temp;
        int n;
        int m;
        vector<int> bin;
        vector<int> eredmeny;
        infile >> n >> m;
        while (infile >> temp )
        {
            bin.push_back(temp);
        }
        cout << endl;
        infile.close();
       
	int tids[n];
	int started = pvm_spawn("slave", 0, PvmTaskDefault, 0, n, tids);
	if(started <  n){
		cout<<"Nem sikerult az osszes gyereket elinditani!"<<endl;
		for(int i = 0;i<started;i++){
			pvm_kill(tids[i]);
		}
		pvm_exit();
	}


	for(int i = 0;i<n;i++){
		pvm_initsend(PvmDataDefault);
		pvm_pkint(&n, 1, 1);
		pvm_pkint(&m, 1, 1);
		int seged = i;
		pvm_pkint(&seged,1,1);
		seged=bin.size();
		pvm_pkint(&seged,1,1);
		pvm_pkint(&bin[0],bin.size(),1);
		pvm_send(tids[i],1);
	}




	for(int i = 0;i<n;i++){
		pvm_recv(tids[i], -1);
		int szam;
		pvm_upkint(&szam, 1, 1);
		eredmeny.push_back(szam);
	}


	ofstream myfile;
 	myfile.open ("output.txt");

 	for (int i = 0; i < n; ++i)
 	{
 		if(eredmeny[i] != 0)
 		{
 		myfile<<eredmeny[i]<<endl;
 		}
 	}
	myfile.close();
	pvm_exit();
	return 0;
}


