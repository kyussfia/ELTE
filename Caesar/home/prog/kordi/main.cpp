#include <iostream>

using namespace std;

int main()
{
    int x;
    cout << "Add meg egy pont els� kooordin�t�j�t:" << endl;
    cin>>x;
    int y;
    cout<<"Add meg ezen pont m�sodik koordin�t�j�t:" <<endl;
    cin>>y;
    if(x>0)
    {
        if(y>0)
        {
            cout<<"Az els� s�knegyedben van";

        }else{cout<<"A negyedik s�knegyedben van";}
    }else{ if (x<0)
        {
            if(y>0)
            {
                cout<<"A m�sodik s�knegyedben van";
            }else{
                cout<<"A harmadik s�knegyedben van";
                }
        }else{cout<<"A pont a tengelyen van";}
    }
    return 0;
}
