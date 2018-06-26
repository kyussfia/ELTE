#include <iostream>

using namespace std;

void VektorBekerdezes(int m,int v[],string s)




int main()
{
    cout<<s<<endl;
    for(int i=1;i<m;++)
    {
        cout<<i<<endl;
        cin>>v[i];
    }
}
int PozMin(int n;int v[])
{
    int minert=99999;
    for(int i=0;i<n;i++)
    {
        if(v[i]> && v[i]<minert)
        {
            minert=v[i];
        }
    }
    return=minert;

}

int PozitivakMinimuma(int n,int v1[],int v2[])
{
    int m1,m2;
    m1=PozMin(n,v1);
    m2=PozMin(n,v2);
    if(m1<m2)
    {
        return m1;
    }
    else
    {
        return m2;
    }

}
int LokMinKereses(int xx,int vx[])
{
    int i=0
    while((i<<xx-1) && (vx[i-1]<=vx[i]) || (vx[i]>=vx[i+1]))
    {
        i++;
    }
    if(i<xx-1)
    {
        return i;
    }
    else
    {
        return -1;
    }
}
float PozAtlaga(int n, int k[])
{
    float s=0;
    for(int i=0;i<n;i++)
    {
        s=s+k[i];
    };
    return s/n;
}

string Ment30nalkisebbel(int m,int t[])
{
    int i=0;
    while(i<m && (t[i]=0) || (t[i]>30))
    {
        i++;
    }
    if(i<m)
    {
        return "ment";
    }

    else
    {
        return "nem ment";
    }

}
int MikorLassitott(int n,t[])
{
    i++;
}
return i;

void KivalogatGyorshajtas (int n, int t[],int &eredmdb,int kiv[])
{
    for(int i=0;i<n;i++)
    {
        if(t[i]>50)
        {
            kiv[eredmdb]=t[i];
            eredmdb++;
        }

    }
}

void TombKiiras(int db, int ve[]);
{
    if(db=0)

    {
        cout<<"Nincs elem!"<<endl;

    }
    else
    {


    for(int i=0;i<db;i++)
    {
        cout<<i<<".:"<<ve[i]<<endl;
    }
    }
}

int main()
{
    int n;
    cout<<"n:"<<endl;
    cin>>n;

    int k1[n],k2[n];
    /*for(int i=0;i<n;i++)
    {
    cout<<i<<"."<<endl;
    cout<<"k1:";
    cin>>k1[i];
    cout<<"k2[i]";
    }
    */

VektroBekerdezes(n,k1,"k1");
VektorBekerdezes(n,k2,"k2");



//b) Minimumkiválasztás
cout<<PozitivakMiknimuma(n,k1,k2)<<endl;

//C) Keresés
cout<<LokMinKereses(n,k1)<<endl;

//d) Sorozat
float a1,a2;
a1=PozAtlaga(n,k1);
a2=PozAtlaga(n,k2);
if(a1<a2)
{
    cout<<"k1 kisebb"<<endl;

}
else
{

}

if (PozAtlaga(n,k2)<PozAtlaga(n,k2))

//e) eldöntés

cout<<"k1"<<Ment30nalkisebbel(n,k1)<<endl;
cout<<"k2"<<Ment30nalkisebbel(n,k2)<<endl;


//f) kivalasztas

cout<<"k2 a"<<MikorLassitott(n,k2)<<". percben lassított"<<endl;


//g) kivalogatas

int kiv[n*2];
int kivdb=0;

KivalogatGyorshajtas(n,k1,kivdb,kiv);
KivalogatGyorshajtas(n,k2,kivdb,kiv);
Tombkiiras(kivdb,kiv);

}
