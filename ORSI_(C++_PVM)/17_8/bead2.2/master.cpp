#include <iostream>
#include <pvm3.h>
#include <string>
#include <fstream>
#include <vector>
/* CM6TSV 

+ Miért kell az int pointer? Át tudnád adni a címét. (include)
+ A master spawn ellenőrzése érdekes, egy gyereket indít, miért kell a ciklus? 
+ Exit helyett return. (include)
+ A vektoron tudnál iterálni range-for segítségével, nem kell a számláló, se a tmp.
Az '1' válasz helyett lehetne bool.
+ Picit a formázásra figyelhetnél, random új sorok, nyitó zárójel új sorban vagy nem.
+ Esetleg logikai egységekre is bonthatnád. ( Adatok betöltése, küldés gyereknek, fogadás, kiírás )
*/

void load(const std::string inFileName, unsigned int N, std::vector<int> data);
void send(int to, int k, unsigned int N, std::vector<int> data);
void recieve(int from, char answer);
void write(const std::string outFileName, const char answer);

int main(int argc, char* argv[])
{
	if (!argv[1] || !argv[2] || !argv[3] || argc != 4)
	{
		std::cout << "Parameter error! spawn -> master k input output" << std::endl;
		return -1;
	}

	int child_task;
	int started = pvm_spawn(const_cast<char*>("child"), 0, PvmTaskDefault, 0, 1, &child_task);

	if (started < 1)
	{
		pvm_kill(child_task);
		pvm_exit();
		std::cout << "Spawn error!" << std::endl;
		return -2;
	}

	int k = atoi(argv[1]);
	std::string inFileName(argv[2]);
	std::string outFileName(argv[3]);
	unsigned int N;
	std::vector<int> data;
	char answer;

	load(inFileName, N, data);
	pvm_initsend(PvmDataDefault);
	send(child_task, k, N, data);
	recieve(child_task, answer);
	write(outFileName, answer);

	return 0;
}

void load(const std::string inFileName, unsigned int N, std::vector<int> data)
{
	std::ifstream input(inFileName.c_str());
	if (input.is_open())
	{
		input >> N;
		data.reserve(N);

		for (auto i : data)
		{
			input >> i;
		}

		input.close();
	}
}

void send(int to, int k, unsigned int N, std::vector<int> data)
{
	pvm_pkint(&k, 1, 1);
	pvm_pkuint(&N, 1, 1);
	pvm_pkint(data.data(), N, 1);
	pvm_send(to, 1);
}

void recieve(int from, char answer)
{
	pvm_recv(from, 1);
	pvm_upkbyte(&answer, 1, 1);
}

void write(const std::string outFileName, const char answer)
{
	std::ofstream output(outFileName.c_str());
	output << (answer == '1' ? "May the subset be with You!" : "I find your lack of subset disturbing!");
	output.close();
	pvm_exit();
}