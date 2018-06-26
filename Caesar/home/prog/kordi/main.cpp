#include <iostream>

using namespace std;

int main()
{
    int x;
    cout << "Add meg egy pont elsõ kooordinátáját:" << endl;
    cin>>x;
    int y;
    cout<<"Add meg ezen pont második koordinátáját:" <<endl;
    cin>>y;
    if(x>0)
    {
        if(y>0)
        {
            cout<<"Az elsõ síknegyedben van";

        }else{cout<<"A negyedik síknegyedben van";}
    }else{ if (x<0)
        {
            if(y>0)
            {
                cout<<"A második síknegyedben van";
            }else{
                cout<<"A harmadik síknegyedben van";
                }
        }else{cout<<"A pont a tengelyen van";}
    }
    return 0;
}
