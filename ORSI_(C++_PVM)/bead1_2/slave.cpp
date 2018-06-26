#include <iostream>
#include<pvm3.h>
#include <vector>

using namespace std;

int main(){


	int n;
	int m;
	int myIndex;
	int eredmeny=0;
	int len;
	int s = 1;


//fogadás

	pvm_recv(pvm_parent(), 1);
	pvm_upkint(&n,1,1);
	pvm_upkint(&m,1,1);
	pvm_upkint(&myIndex,1,1);
	pvm_upkint(&len,1,1);
	int arr[len];
	pvm_upkint(arr,len,1);
	vector<int> bin(arr, arr + sizeof arr / sizeof arr[0]);


	for (int i = myIndex; i < len; i+=n)
	{
		eredmeny=eredmeny+ bin[i]*s;
		s= s*2;
	}
	pvm_initsend(PvmDataDefault);
	pvm_pkint(&eredmeny,1,1);
	pvm_send(pvm_parent(),1);

	
	pvm_exit();
	return 0;
}

