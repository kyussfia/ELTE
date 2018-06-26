//Próba Zh
//Készítette: Nagy Sára
#include <iostream>
#include <cstdlib>

using namespace std;

// Globális tömbök
const int meret=100;
int letszam[meret];
int szavazott[meret];

//Használt függvények
void beolv(int &n, int t1[], int t2[]); //két tömb együttes beolbvasása
float f(int i); //százalékszámítás

int main()
{


    int n,i;
    int s1,s2;
    bool ervenyes;
    int max, maxsz;
    string valasz;

    //Beolvasás
    do{
        system("cls");
    beolv(n,letszam,szavazott);

    //Adatok visszaírása

        //.......



        s1=0;
        s2=0;
        for(int i=0;i<n;i++)
        {
            s1=s1+letszam[i];
            s2=s2+szavazott[i];
        };

    ervenyes=s2>((s1)/2);

    cout<<"A szavazasra jogosultak szama:"<<s1<<endl;
    cout<<"Osszesen ennyien szavatak:"<<s2<<endl;

    if(ervenyes)
    {
        cout<<"A szavazas ervenyes volt!"<<endl;

    }else
    {
        cout<<"Ervenytelen szavas"<<endl;

    };


        //Legaktívabb szavazókör.

            // .......
    max=0;
    maxsz=f(0);
    for(int i=1;i<n;i++)
    {
        if(f(i)>maxsz)
        {
            max=i;
            maxsz=f(i);
        }
        else{}
    }

    cout<<"Uj szavazas? (igen/nem)"<<endl;
    cin>>valasz;

    }while(valasz=="igen");
    return 0;
}


void beolv(int &n, int t1[], int t2[])
{
    do{
    cout << "Add meg a szavazokorok szamat:";
    cin>>n;}
    while(n<1 || n>100);
    for(int i=0;i<n;i++)
    {
        do{cout <<"A(z) " <<i+1<<" szavazokorhoz tartozo dolgozok letszama:"<<endl;
        cin>>t1[i];}
        while(t1[i]<=0 || t1[i]>100);
        do{
        cout <<"A(z) " <<i+1<<" szvazokorben a leadott szavazatok szama:"<<endl;
        cin>>t2[i];}
        while(t1[i]<=t2[i]);
    }
    return;
}

float f(int i)
{
    return ( float(szavazott[i])/float(letszam[i]))*100.0;
}
