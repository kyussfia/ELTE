#include <iostream>
#include <pvm3.h>
#include <string>
#include <fstream>
#include <vector>
#include <stdlib.h>
#include <memory>
/* CM6TSV */
int main(int argc, char* argv[])
{
	if (!argv[1] || !argv[2] || !argv[3] || argc != 4)
	{
		std::cout << "Parameter error! master k input output" << std::endl;
		exit(1);
	}

	std::unique_ptr<int> child_task(new int());

	int started = pvm_spawn(const_cast<char*>("child"), 0, PvmTaskDefault, 0, 1, child_task.get());
	if (started < 1)
	{
		for (int i = 0; i < started; i++)
		{
			pvm_kill(*child_task);
		}
		
		pvm_exit();
		std::cout << "Spawn error!" << std::endl;
		
		exit(2);
	}

	int k = atoi(argv[1]);
	std::string inFileName(argv[2]);
	std::string outFileName(argv[3]);
	
	pvm_initsend(PvmDataDefault);
	pvm_pkint(&k, 1, 1);

	std::ifstream input(inFileName.c_str());
	if (input.is_open()) {
		unsigned int N;
		input >> N;
		pvm_pkuint(&N, 1, 1);

		std::vector<int> data(N);

		int tmp;
		unsigned int i = 0;

		while (input >> tmp)
		{
			data[i] = tmp;
			i++;
		}
		input.close();

		pvm_pkint(data.data(), N, 1);
			 	
		pvm_send(*child_task, 1);
	}

	std::ofstream output(outFileName.c_str());

	char answer;

	pvm_recv(*child_task, 1);

	pvm_upkbyte(&answer, 1, 1);

	output << (answer == '1' ? "May the subset be with You!" : "I find your lack of subset disturbing!");
	
	output.close();
	
	pvm_exit();

	return 0;
}