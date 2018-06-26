#include <iostream>

using namespace std;

int main()
{ //1. feladat

    cout << "1. feladat, gondolj 1 számra: ";
    int buzi; cin >> buzi;
    if (buzi>=0) cout << buzi << endl;
    else cout << "buzi vagy" << endl;
//2. feladat
    cout << " 2. feladat, kreditátlag add meg a jegyxkredit összeget: ";
    int osszeg; cin >> osszeg;
    if (osszeg<0) cout << "te meleg";
    else cout << (osszeg/30) <<endl;
    //3. feladat
    cout <<"3. feladat, licit. licit also határa 42. add meg a licitet: ";
    int licitalso; licitalso=42; int licit;
    cin >> licit ;
    if (licit<licitalso) cout << "nemjo, buzi" << endl;
    else cout << licit <<endl;
    //4. feladat
    cout << "4. feladat, munkavállalás" << endl << "add meg az alsó árhatárt:";
    int also; cin >> also; cout <<"add meg a felső árhatárt" <<endl;
    int felso; cin >> felso;
    if (felso<also) cout << "az alsó árhatár meghaladja a felsőt" << endl;
    else cout << also << "-tól" << felso << "-ig" << endl;
    //5. feladat

    return 0;
}
