#include <iostream>
#include <cstdlib>
#include <string>
#include <pvm3.h>

using namespace std;

void merge(string *a, int lbegin, int lend, int rbegin, int rend);
void mergeSort(string* input, int u, int v);

int main(int argc, char* argv[])
{
    int parentId = pvm_parent();
    pvm_recv(parentId, 1);

    int u;
    pvm_upkint(&u, 1, 1);

    int v;
    pvm_upkint(&v, 1, 1);

    string input[v+1];

    for(int i = u; i < v+1; ++i) {
        int len;
        pvm_upkint(&len, 1, 1);
        char *buf = new char[len+1];
        pvm_upkstr(buf);
        input[i] = buf;
        delete buf;
    }

    mergeSort(input, u, v);

    pvm_initsend(PvmDataDefault);

    for(int i = u; i < v+1; ++i) {
        int len = input[i].length();
        pvm_pkint(&len, 1, 1);
        pvm_pkstr((char*)input[i].c_str());
    }

    pvm_send(parentId,1);
	
    pvm_exit();

    return 0;
}

void mergeSort(string* input, int u, int v)
{
    if (u < v) {
        int k = (u+v)/2;

        int* tids = new int[2];
        int started = pvm_spawn("child", 0, PvmTaskDefault, 0, 2, tids);
        //int started = pvm_spawn("child", 0, PvmTaskHost + PvmHostCompl, "atlasz", 2, tids);

        if (started <  2) {
            cout<<"Nem sikerult a 2 gyereket elinditani!"<<endl;
            for(int i = 0;i<started;i++){
                pvm_kill(tids[i]);
            }
            pvm_perror("spawn");
            exit(3);
        }
        //first child
        //prepare sending message
        pvm_initsend(PvmDataDefault);

        int l_u = u;
        pvm_pkint(&l_u, 1, 1);
        int  l_v = k;
        pvm_pkint(&l_v, 1, 1);

        for(int i = u; i <= k; ++i) {
            int len = input[i].length();
            pvm_pkint(&len, 1, 1);
            pvm_pkstr((char*)input[i].c_str());
			//cout << "bal send: "<<input[i] <<endl;
        }

        pvm_send(tids[0], 1);

        //second child
        pvm_initsend(PvmDataDefault);

        int r_u = k + 1;
        pvm_pkint(&r_u, 1, 1);
        int r_v = v;
        pvm_pkint(&r_v, 1, 1);
		
        for(int i = k+1; i <= v; ++i) {
            int len = input[i].length();
            pvm_pkint(&len, 1, 1);
			//cout << "jobb send: "<<input[i] <<endl;
            pvm_pkstr((char*)input[i].c_str());
        }

        pvm_send(tids[1], 1);

        //receive first
        pvm_recv(tids[0], 1);

        for(int i = u; i <= k; ++i) {
            int len;
            pvm_upkint(&len, 1, 1);
            char *buf = new char[len+1];
            pvm_upkstr(buf);
            input[i] = buf;
			//cout <<"bal receive: "<< input[i] << endl;
            delete buf;
        }

        //receive second
        pvm_recv(tids[1], 1);
		//cout << " k+: "<<r_u<<" r_v: "<<r_v<<endl; 

        for(int i = k+1, j=0; i <= v; ++i, ++j) {
            int len;
            pvm_upkint(&len, 1, 1);
            char *buf = new char[len+1];
            pvm_upkstr(buf);
            input[i] = buf;
			//cout <<"jobb receive: "<< input[i] << endl;
            delete buf;
        }

        merge(input, u, k, k+1, v);
    }

}

void merge(string *a, int lbegin, int lend, int rbegin, int rend)
{
    string tmp[sizeof(a)/sizeof(*a)];
    int pa = lbegin, pb = rbegin, pt = lbegin;
    while(pa <= lend && pb <= rend){
        if(a[pa] < a[pb]){
            tmp[pt++] = a[pa++];
        }else{
            tmp[pt++] = a[pb++];
        }
    }
//    if(pa > lend){
        while(pb <= rend){  //left sub array exhausted
            tmp[pt++] = a[pb++];
        }
//    }else{
        while(pa <= lend){  //right sub array exhausted
            tmp[pt++] = a[pa++];
        }
//    }
    
    //write sorted element in array a
    for(pt = lbegin; pt <= rend; pt++){
        a[pt] = tmp[pt];
		//cout<<"tmp "<<tmp[pt]<<endl;
    }
}

/*void merge(string* a, int lb, int le, int rb, int re, string* out)
{
	
	//string tmp[re-lb+1];
	//int size = sizeof(a)/sizeof(*a);
	//string tmp[size];
	int db = -1;
	int i = lb;
	int j = rb;

	
	while (i <= le && j <= re)
	{
		db = db+1;
		if (a[i] < a[j]) {
			out[db] = a[i];
			++i;
		}
		if (a[i] == a[j]) {
			out[db] = a[i];
			++i;
			++j;
		}
		if (a[i] > a[j])
		{
			out[db] = a[j];
			++j;
		}
	}
	
	
	
	while (i <= le) {
		db = db+1;
		out[db] = a[i];
		++i;
	}
	while (j <= re) {
		db = db+1;
		out[db] = a[j];
		++j;
	}
	
	//merge:
	for(i = lb; i <= re; ++i) {
		cout << out[i]<< endl;
		a[i] = out[i];
	}
}*/