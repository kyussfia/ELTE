// hazi_3.cpp : Defines the entry point for the console application.
//
#include <iostream>
#include <string>
#include <conio.h>
using namespace std;

void getNum(int &n); //integer szám bekérése, csak 1-9 es közé eshet, természetes szám.
void getNum(long double &n); //valós szám bekérése.
//-----------------------------------------------
void main()
{

	int n = 0;
	long double x = 0;
	getNum( x ); //valós szám bekérése
	getNum( n ); //természetes szám bekérése
	long double d = 0;
		d = x;
		for(int i = 1; i < n; i++)
		{
			//muvelet elvégzése
			d = x * (double)d; 
		}

	cout << "\nErteke = ";
	cout << d;

	getch();
}//void main()
//--------------------------------
void getNum(int &n)
{
	bool ell = false;
	string str;
	do
	{	
		cout << "Termeszetes tipus hossza := ";
		cin >> str;
		for(int h = 0; h < (signed)str.length(); h++)
		{
			if(str[ h ] >= '1' && str[ h ] <= '9')
			{
				//hmm
				ell = true;
			}
			else
			{
				cout << "hiba! tartalmaz mas karaktert is!" << endl;
				ell = false;	
				break;
			}
		}
	}while( ell == false);
	n = atoi( str.c_str() );
}//void getNum(string str, int n)
//-------------------------------------------
void getNum(long double &n)
{
	bool ell = false;
	string str;
	do
	{	
		cout << "Valos tipus hossza := ";
		cin >> str;
		int dot = 0;
		for(int h = 0; h < (signed)str.length(); h++)
		{
			if((	str[ h ] >= '0' && str[ h ] <= '9') || 
				((	str[ h ] == '.' && dot <= 1) || (str[ h ] == '-' && h == 0)))
			{
				//hmm
				ell = true;
				if(str[ h ] == '.')
				{
					dot++;
				}
			}
			else
			{
				cout << "hiba! tartalmaz mas karaktert is!" << endl;
				ell = false;
				break;
			}
		}
	}while( ell == false);
	n = atof( str.c_str() );
}//void getNum(string str, int n)