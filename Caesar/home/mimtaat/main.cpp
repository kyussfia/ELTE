#include <iostream>
#include <stdlib.h>
#include <time.h>
//Mikus Márk - Mimtaat.elte



//12. Barkóba: Készítsünk programot, amiben ki kell találni egy 1 és 100 közötti egész számot! A
//felhasználó addig tippelhet, amíg ki nem találja a program által "gondolt" számot.

using namespace std;

int main()
{


    int t;  //tippek
    int n;  //gondolt
    srand(time(NULL));


    string proba;

   // cout << "Gondolok:";
    n=rand()%100+1;
cout <<"Gondoltam egy szamra 1 es 100 kozott.Találd ki!" << endl;


        do{

        cout << "Tippelj egy szamot:" << endl;
        cin>>t;
        }
        while(t<1 || t>100);




        bool cucc=false;

    do {
        if(t==n)
        {
            cout << "Gratulálok" << endl;
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
            cout << "Gratulálok" << endl;
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


