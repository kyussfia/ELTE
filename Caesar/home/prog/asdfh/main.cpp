#include <iostream>

using namespace std;

int main()
{
  int a;
  cout<<"a:";
  cin>>a;
  if (a<0)
  {
     cout<<"-1";
  }else
  {
    if(a>0)
    {
        cout<<"+1";}
    else{cout<<"0";}
  }
}
