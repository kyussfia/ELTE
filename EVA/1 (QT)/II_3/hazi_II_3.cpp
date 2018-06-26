// hazi_II_3.cpp : Defines the entry point for the console application.
// Határozzuk meg egy egész számokat tartalmazó vektor legkisebb, k-val osztható értékét!
#include <fstream>
#include <string>
#include <conio.h>
#include <stdio.h>
#include <iostream>
using namespace std;

int getNum(bool end);		//Ellenörzöm, hogy csak számot írjon be a flsz. megadhatom, hogy tobbkaraktert, vagy csak 1et vihet fel! Ha több számjegy is lehet, akkor 0-t megengedem, ha csak 1 számjegy, akkor 0 nincs megengedve!
int checkNum(string tmp, char kezd);	//csak szám lehet! itt végzem el a string vizsgálatát
int* getTMB(int &n, bool file, bool klav, string filename);
bool checkTMB(int v[], int n, int act);
int main(int argc, char* argv[])
{
    string con;
    do{
		string filename;
		bool file = false, klav = false;
		if(argv[ 1 ] != "" && argc >= 2)
		{
			cout << "A fajl neve := ";
			filename = argv[ 1 ];
			cout << argv[ 1 ] << endl;
			file = true;
		}
		else
		{
			do
			{
				string str;
				cout << "Tomb feltoltese billenyuzetrol, \nakarja? ( I/N ) := ";
				cin >> str;
				char * copySTR = _strdup( str.c_str() );
				str = _strupr( copySTR );
				if(str == "I")
				{
					klav = true;
				}
				else if(str == "N")
				{
					file = true;
				}

			}while(file == false && klav == false);
		}
		int n = 0;
		int* v = getTMB(n, file, klav, filename );// a vector feltoltese
		//Kiírjuk a tömb elemeit a szabványos kimenetre.
		cout << "A tomb hossza: " << n << endl;
		cout << "A tomb elemei: ";
		for (int i = 0; i < n - 1; ++i) cout << v[ i ] << ", ";
		cout << v[ n - 1 ] << endl;
		//Osztó bekérése
		cout << "Az oszto := ";
		int k = getNum(false); //bekerem az osztot!!!

		//Ellenõrizzük az elõfeltételt
		if (n < 1){
			cout << "A tomb ures!";
			exit(2);
		}
		//Minimum kiválasztás k oszto szerint..
		int ind = 0, min = -1;
		bool l = false;
		for(int i = 0; i < n; ++i){
			if((v[ i ] % k) == 0 && l)
			{
				if(v[ i ] < min)
				{
					min = v[ i ]; 
					ind = i;
				}
			}
			else if((v[ i ] % k) == 0 && !l)
			{
				min = v[ i ]; 
				ind = i;
				l = true; 
			}
		}

		//Kiíratás
		if(l != false)
		{
			cout << "A tomb egyik legkisebb, k-val oszthato eleme: " << min << endl;
			cout << "Ez a " << (ind + 1) << ". elem." << endl;
		}
		else
		{
			cout << "A tomben nem talalhato k osztoja!!!" << endl;
		}
		do
		{
			cout << "Ujbol, \nakarja? ( I/N ) := ";
			cin >> con;
			char * copySTR = _strdup( con.c_str() );
			con = _strupr( copySTR );
		}while(!(con == "N") && !(con == "I"));
    }while( !(con == "N"));
	
	return 0;
}
//---------------------------------
int* getTMB(int &n, bool file, bool klav, string filename)
{
		//Bekérjük billentyûzetrõl, ha nem file-ból akarja feltölteni a tmb-t
		//ciklusbol kell bekérni, hogy mit is akarok..
		if(klav == true)
		{
			cout << "A Tomb hossza := ";
			n = getNum(false); //nem lehet majd 0as a tomb!!

			int* v = new int[ n ];
			bool ell = false;
			for(int i = 0; i < n; i++)
			{
				if(ell == true)
				{
					i--;
				}
				cout << "Adja meg a(z) " << i << " helyiertekeket := ";
				v[ i ] = getNum(true);
				ell = checkTMB(v, i, v[ i ]);
			}
			return v;
		}
		//Bekérjük az állomány nevét a szabványos bemenetrõl.
		if(file == true)
		{
			if(filename == "")
			{
				cout << "A fajl neve := ";
				cin >> filename;
			}
			//Definiáljuk és megnyitjuk a fájlt
			ifstream x(filename.c_str());
			//Ha hiba van befejezzük a programot
			if (x.fail()){
				cout << "A megadott fajlt nem talalom!";
				exit(1);
			}
			//Beolvassuk/kiírjuk a tömb hosszát
			x >> n;
			//Létrehozunk egy n elemû tömböt és kitöltjük
			int* v = new int [ n ];
			for(int i = 0; i < n; ++i)
			{
				x >> v[ i ];
				bool ell = checkTMB(v, i, v[ i ]);
				if(ell == true)
				{
					cout << "Utkozik a(z) " << i << " helyen levo adattal! \nKerem ellenorizze a parameter fajt!";
				}
			}
			return v;
		}
}//int getTMB(ifstream &x)
//---------------------------------
int checkNum(string tmp, char kezd)
{
	for(int h = 0; h < (signed)tmp.length(); h++)
	{
		if(tmp[ h ] >= kezd && tmp[ h ] <= '9')
		{
			//hmm
		}
		else
		{
			cout << "hiba! tartalmaz mas erteket is!" << endl;
			cout << "Adja meg ujra a kert erteket := ";
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
		cin >> str;
		if(end == true)
		{
			ell = 0;
			n = checkNum( str, '0' );
		}
		else if(end == false)
		{
			ell = 1;
			n = checkNum( str, '1' );
		}
	}while( !(ell <= n));
return n;
}//int getNum()
//----------------------------------------
bool checkTMB(int v[], int n, int act)
{
	bool ell = false;
	for(int i = 0; i < n; i++)
	{
		if(v[ i ] == act)
		{
			ell = true;
			cout << "Utkozik a(z) " << i << " helyen levo adattal! Kerem adja meg ujra.";
		}
	}
	return ell;
}//bool checkTMB(int v[], int n, int act)

/*
		int ind, min;
		ind = 0; min = -1;
		bool l = false;
		for(int i = 0; i < n; ++i){
			int vectCheck = v[ i ] % k;
			int minCheck = min % k;
			if ((v[ i ] < min || minCheck != 0) && (vectCheck == 0)){
				ind = i;
				min = v[ i ];
				l = true;
			}
			else
			{
				if(minCheck != 0)
				{
					l = false;
				}
			}
		}
*/