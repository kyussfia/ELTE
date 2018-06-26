  	/* Osztott rendszerek specifikacioja es implementacioja */
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <map>
#include <cstdlib>
#include <pvm3.h>
#include <string>

using namespace std;

int main(int argc, char* argv[])
{
    ifstream infile; //input file 

    infile.open("input.txt", ios::in);


    if (infile.fail()) {
		cout << "Nem tudtam megnyitni a fajlt olvasasra!" << endl;
		exit(1);
    }
    
    int m = -1; //gyerek
    int n = -1; //sor
    infile >> n >> m;

    if (m < 1 || n < 1) {
    	cout << "n:" << n<< " m:" << m << endl;
    	cout << "Pozitiv egeszek kellenek a fajl elejen!" << endl;
    	exit(2);
    }

    //read data
    multimap<string, int> votes; //votes[game] = minute
    multimap<string, int> result; // vote[game] = minute; vote[game] = minute

    for (int i = 0; i < n; ++i) {
    	string user = "";
		//do nothing with username
    	string name = "";
    	int time = 0;

    	infile >> user >> name >> time;

    	votes.insert(pair<string, int>(name, time));
    }
    

    //children processes
    int* tids = new int[m];

	int started = pvm_spawn("child", 0, PvmTaskDefault, 0, m, tids);

	if (started <  m) {
		cout<<"Nem sikerult az osszes gyereket elinditani!"<<endl;
		for(int i = 0;i<started;i++){
			pvm_kill(tids[i]);
		}
		pvm_perror("spawn");
		exit(3);
	}

	int counter_m = 0;  //imitiate  tid[m] element

	for (multimap<string,int>::iterator it = votes.begin(); it != votes.end(); it = votes.upper_bound(it->first), ++counter_m) {

	    multimap<string,int>::iterator itlow;
	    multimap<string,int>::iterator itup; //range os key

	    multimap<string,int>::iterator innerIter; //for inner loop

	    string currentKey =  it->first;

	    itlow = votes.lower_bound(currentKey);
	    itup = votes.upper_bound(currentKey);


	    //prepare sending message
	    pvm_initsend(PvmDataDefault);

	    //send how many integers will be sent
	    int value = votes.count(currentKey);
	    pvm_pkint(&value,1,1);

	    //iterate elements of key
	    for (innerIter=itlow; innerIter!=itup; ++innerIter) {
	    	int minutes = innerIter->second;

	    	// set value to send
	    	pvm_pkint(&minutes, 1, 1);
	    }

	    //sending
	    pvm_send(tids[counter_m], 1);
	}

	counter_m = 0;

	//recieve message
	for (multimap<string,int>::iterator it = votes.begin(); it != votes.end(); it = votes.upper_bound(it->first), ++counter_m) {
		string currentKey =  it->first;

		pvm_recv(tids[counter_m], -1);

		int sumTime;
		int avgTime;

		pvm_upkint(&sumTime, 1, 1);
		pvm_upkint(&avgTime, 1, 1);

		result.insert(pair<string, int>(currentKey, sumTime));
		result.insert(pair<string, int>(currentKey, avgTime));
	}

	//output  to file
	ofstream myfile;
 	myfile.open("output.txt");

 	//iterate through games
 	string currentKey = "";
 	for (multimap<string,int>::iterator it = result.begin(); it != result.end(); ++it) {
 		
	    if (currentKey != it->first) { //new key
	    	if (it != result.begin()) {
	    		myfile<<endl;
	    	}
	    	currentKey = it->first;

	    	myfile<<it->first<<" ";	//name
	    	myfile<<it->second<<" ";
	    } else {
	    	myfile<<it->second;
	    }
 	}
	myfile.close();

	delete[] tids;
    pvm_exit();

	return 0;
}