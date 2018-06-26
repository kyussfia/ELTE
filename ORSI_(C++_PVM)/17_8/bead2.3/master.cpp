#include <iostream>
#include <pvm3.h>
#include <string>
#include <fstream>
#include <vector>
#include <stdlib.h>
/* CM6TSV */
std::vector<int> loadToVector(const std::string inFileName, unsigned int& N);
void sendData(int to, int& k, unsigned int& N, std::vector<int>& data);
void writeResult(const std::string outFileName, const unsigned short int answer);
unsigned short int recieveAnswer(int from);

int main(int argc, char* argv[])
{
	//validate args
	if (!argv[1] || !argv[2] || !argv[3] || argc != 4)
	{
		std::cout << "Parameter error! master k input output" << std::endl;
		return -1;
	}

	//spawn & check child
	int child_task;
	int started = pvm_spawn(const_cast<char*>("child"), 0, PvmTaskDefault, 0, 1, &child_task);

	if (started < 1)
	{
		pvm_kill(child_task);		
		pvm_exit();
		std::cout << "Spawn error!" << std::endl;
		return -2;
	}

	//stream-arguments
	std::string inFileName(argv[2]);
	std::string outFileName(argv[3]);

	//init start params
	int k = atoi(argv[1]);
	unsigned int N;

	std::vector<int> data = loadToVector(inFileName, N);
	sendData(child_task, k, N, data);
	writeResult(outFileName, recieveAnswer(child_task));

	return 0;
}

std::vector<int> loadToVector(const std::string inFileName, unsigned int& N)
{
	std::ifstream input(inFileName.c_str());
	std::vector<int> data;

	if (input.is_open())
	{
		input >> N;
		data.resize(N);

		for (int& i : data)
		{
			input >> i;
		}

		input.close();
	}

	return data;
}

void sendData(int to, int& k, unsigned int& N, std::vector<int>& data)
{
	pvm_initsend(PvmDataDefault);
	pvm_pkint(&k, 1, 1);
	pvm_pkuint(&N, 1, 1);
	pvm_pkint(data.data(), N, 1);		 	
	pvm_send(to, 1);
}

void writeResult(const std::string outFileName, const unsigned short int answer)
{
	std::ofstream output(outFileName.c_str());
	output << (answer ? "May the subset be with You!" : "I find your lack of subset disturbing!");
	output.close();
	pvm_exit();
}

unsigned short int recieveAnswer(int from)
{
	unsigned short int answer;
	pvm_recv(from, 1);
	pvm_upkushort(&answer, 1, 1);
	return answer;
}