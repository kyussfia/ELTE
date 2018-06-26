#include <iostream>

using namespace std;

int main()
{
    int n;
    int ft,eu;
    cin>>n;

    int arf[n+1];

    cin>>ft;
    cin>>eu;

    for(int i=0;i<n+1;i++)
    {
        cin>>arf[i];
    }
    //a feladat
    int db=0;
    for(int i=1;i<n;i++)
    {
        if ((arf[i-1]>arf[i]) && (arf[i]<arf[i+1]) ||
         ((arf[i-1]<arf[i]) && (arf[i]>arf[i+1])))  //lok minimum nézd meg talpon!!!!
        {
            db++;
        }
    }
    cout<<db<<endl;
    //b feladat
    int aft[n+1],aeu[n+1];
    aft[0]=ft;
    aeu[0]=eu;
    for(int i=1;i<n+1;i++)
    {
        if(arf[i]<arf[i-1])  //erõsödik->mindent euroba
        {
            aeu[i]=aeu[i-1]+aft[i-1]*100/arf[i];
            aft[i]=0;
        };
        if(arf[i]>arf[i-1])
        {
            aft[i]=aft[i-1]+aeu[i-1]*arf[i]/100;
            aeu[i]=0;
        };
    }

    cout<<aft[n]<<" "<<aeu[n]<<endl;
    //c feladat
    int s=0;
    for (int i=0;i<n+1;i++)
    {
        s=s+arf[i];
    }
    cout<<s/(n+1)<<endl;
    //d feladat
    int kdb=0;
    int ks[n+1];
    for(int i=1;i<n+1;i++)
    {
        if(ft*1.1<aft[i])
        {
            ks[kdb]=i;
            kdb++;
        };
    } //kiválogatas megvan
    cout<<kdb;
    for (int i=0;i<kdb;i++)
    {
        cout<<" "<<ks[i];
    }
    cout<<endl;
    //e feladat
    int i=0;
    while(i<n && arf[i]*1.1>=arf[i+1])
     {
         i++;
     };
     if(i<n)
     {
         cout<<"HAMIS";
     }
     else
     {
         cout<<"IGAZ";
     }
    cout<<endl;
}
