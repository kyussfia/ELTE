#include <iostream>
#include <stdlib.h>
#include <time.h>
//Mikus M�rk - Mimtaat.elte



//12. Bark�ba: K�sz�ts�nk programot, amiben ki kell tal�lni egy 1 �s 100 k�z�tti eg�sz sz�mot! A
//felhaszn�l� addig tippelhet, am�g ki nem tal�lja a program �ltal "gondolt" sz�mot.

using namespace std;

int main()
{


    int t;  //tippek
    int n;  //gondolt
    srand(time(NULL));


    string proba;

   // cout << "Gondolok:";
    n=rand()%100+1;
cout <<"Gondoltam egy szamra 1 es 100 kozott.Tal�ld ki!" << endl;


        do{

        cout << "Tippelj egy szamot:" << endl;
        cin>>t;
        }
        while(t<1 || t>100);




        bool cucc=false;

    do {
        if(t==n)
        {
            cout << "Gratul�lok" << endl;
            cucc=true;
        }

    else
        {
            if(t>n)
        {
            cout << "Kisebbet!" << endl;
        };
            if(t<n)
        {
            cout <<"Nagyobbat" << endl;
        };
cout << "Akarsz meg probalkozni? (igen/nem)" <<endl;
        cin >> proba;
            if(proba=="igen")
            {
                cout << "Kerek egy tippet!" << endl;
                cin >> t;
            }else
            {
                cout << "Kar!Pedig a szam a(z) "  << n <<"volt. :)" <<endl;
                cucc=true;
            };
       };
    }while (cucc==false);









        /*while(t<1 && t>100);
        {

        cout << "Tippelj egy szamot:" << endl;
        cin>>t;
        };
    if(t==n)
        {
            cout << "Gratul�lok" << endl;
        }
    else
        {
            if(t>n)
        {
            cout << "Kisebbet!" << endl;
        };
            if(t<n)
        {
            cout <<"Nagyobbat" << endl;
        };

       };*/
return 0;
}


