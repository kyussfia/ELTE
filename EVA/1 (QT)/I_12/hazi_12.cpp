
#include <iostream>
#include <string>
#include <conio.h>
using namespace std;

int getNum(bool end);	//Ellen�rz�m, hogy csak sz�mot �rjon be a flsz. megadhatom, hogy tobbkaraktert, vagy csak 1et vihet fel! Ha t�bb sz�mjegy is lehet, akkor 0-t megengedem, ha csak 1 sz�mjegy, akkor 0 nincs megengedve!
int StackFull(int tmp); //9 + 1 t�lcsordul.. ilyenkor -1 a visszat�r�si �rt�ke
int checkNum(string tmp); //csak sz�m lehet! itt v�gzem el a string vizsg�lat�t
void putResult(int tmb[], int tmbCount);//eredm�ny ki�rat�s, tmp t�bb elemu tomb, �s a m�rete -> sizeof al si lehetne sz�molni a tanultak alapj�n, de.. �gy a nyer�.
//-----------------------------------------------
int main()
{
	cout << "Adja meg a tomb hosszat (1 <= n )"<< endl;
	int n = getNum(true);
	int* tmb = new int[ n ];

	for(int k = 0; k < n; k++)
	{
		cout << "Adja meg a helyiertekeket := ";
		tmb[ k ] = getNum(false);
	}
	putResult(tmb, n);
	int lastValid = 0;
	int ell = 0;
	for(int h = 1; h <= n; h++)
	{
		ell = StackFull(tmb[ h - 1 ] + 1);
		if(ell != -1)
		{
			lastValid = h;
		}
	}
	if(lastValid != 0)
	{
		tmb[ lastValid - 1 ] = tmb[ lastValid - 1 ] + 1;
		for(int null = lastValid; null < n; null++)
		{
			tmb[ null ] = 0;
		}
		putResult(tmb, n);
	}
	else
	{
		cout << "game over!" << endl; //a tulcsordult elem
	}
getch();
return 0;
}//int main()
//---------------------------------
void putResult(int tmb[], int tmbCount)
{
	cout << "\nTomb erteke = ";
	for(int z = 0; z < tmbCount; z++)
	{
		cout << tmb[ z ]; //a tulcsordult elem
	}
}//void putResult(int tmb[], int tmbCount)
//---------------------------------
int StackFull(int tmp)
{
	if(tmp > 9 )
	{
		//cout << "Tul nagy a megadott: " << tmp << " ertek!" << endl; //a tulcsordult elem
		return -1;
	}
	else
	{
		return tmp;
	}
}//int StackFull(unsigned int tmp)
//---------------------------------
int checkNum(string tmp)
{
	for(int h = 0; h < (signed)tmp.length(); h++)
	{
		if(tmp[ h ] >= '0' && tmp[ h ] <= '9')
		{
			//hmm
		}
		else
		{
			cout << "hiba! tartalmaz mas erteket is!" << endl;
			return -1;
		}
	}
	int n = atoi(tmp.c_str());
return n;
}//int checkNum(string tmp)
//--------------------------------
int getNum(bool end)
{
	string str;
	int n = 0, ell = 1;
	do
	{	
		if(end == true)
		{
			cout << "Tomb hossza = ";
		}
		cin >> str;
		if(end == true)
		{
			n = checkNum( str );
			ell = 1;
		}
		else if(end == false)
		{
			if( 1 < str.length())
			{
				cout << "Kerem adja meg a szamot (ugyeljen, hogy 1 <= n �s n <= 9!)" << endl;
				n = -1;
			}
			else
			{
				n = checkNum( str );
				ell = 0;
			}
		}
	}while( !(ell <= n));
return n;
}//int getNum()

