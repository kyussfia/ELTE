  	/* Osztott rendszerek specifikacioja es implementacioja */
#include <iostream>
#include <fstream>
#include <sstream>
#include <cstdlib>
#include <pvm3.h>

#include <string>

using namespace std;

int main(int argc, char* argv[])
{
    ifstream infile; //input file 

    infile.open("input_1.txt", ios::in);


    if (infile.fail()) {
		cout << "Nem tudtam megnyitni a fajlt olvasasra!" << endl;
		exit(1);
    }
    
    int n = -1; //sor
    infile >> n;

    if (n < 1) {
    	cout << "n:" << n << endl;
    	cout << "Pozitiv egesz kell a fajl elejen!" << endl;
    	exit(2);
    }

    //read data
    string names[n];
    string result[n];

    for (int i = 0; i < n; ++i) {
    	string tmp;
    	infile >> tmp;
    	names[i] = tmp;
    }

    int* tids = new int[1];
    int started = pvm_spawn("child", 0, PvmTaskDefault, 0, 1, tids);
	//int started = pvm_spawn("child", 0, PvmTaskHost + PvmHostCompl, "atlasz", 1, tids);

	if (started <  1) {
		cout<<"Nem sikerult a gyereket elinditani!"<<endl;
		pvm_kill(tids[0]);
		pvm_perror("spawn");
		exit(3);
	}

	//prepare sending message
	pvm_initsend(PvmDataDefault);

    //send    //starter (helper index)
    int begin = 0;
    pvm_pkint(&begin, 1, 1);
    int end = n - 1;
    pvm_pkint(&end, 1, 1);

    // & data
    for(int i = 0; i < n; ++i) {
        int len = names[i].length();
        pvm_pkint(&len, 1, 1);
        pvm_pkstr((char*)names[i].c_str());
    }

    //sending
    pvm_send(tids[0], 1);
    /*************************************/
	//receive
    pvm_recv(tids[0], 1);
    
    for(int i = 0; i < n; ++i) {
        int len;
        pvm_upkint(&len, 1, 1);
        char *buf = new char[len+1];
        pvm_upkstr(buf);
        result[i] = buf;
        delete buf;
    }
    pvm_exit();

	//output  to file
	ofstream myfile;
 	myfile.open("output.txt");

 	//iterate through names
 	for (int i = 0; i < n; ++i) {
 		cout << result[i] << endl;
 		myfile << result[i];

 		if (i < n-1) {
 			cout << endl;
 			myfile << endl;
 		}

 	}
	myfile.close();
    delete[] tids;

	return 0;
}