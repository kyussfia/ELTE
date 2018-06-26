#include <iostream>
#include <pvm3.h>
#include <string>
#include <fstream>
#include <vector>
#include <stdlib.h>

void listenChild(int &ch, std::string out);

int main(int argc, char* argv[])
{
	if (!argv[1] || !argv[2] || !argv[3] || argc != 4)
	{
		std::cout << "Parameter error! master k input output" << std::endl;
		exit(1);
	}

//	int* child_task = new int();

	int child_task;

	int started = pvm_spawn((char*)"child", 0, PvmTaskDefault, 0, 1, &child_task);
	if (started < 1)
	{
		for (int i = 0; i < started; i++)
		{
			pvm_kill(child_task);
		}
		
		pvm_exit();
		std::cout << "Spawn error!" << std::endl;
		
		exit(2);
	}

	int k = atoi(argv[1]);
	std::string inFileName(argv[2]);
	std::string outputName(argv[3]);

	std::cout << "me: "<< pvm_mytid() << " Master k: "<< k << std::endl;
	
	pvm_initsend(PvmDataDefault);
	pvm_pkint(&k, 1, 1);

	std::ifstream input(inFileName.c_str());
	if (input.is_open()) {
		int N;
		input >> N;

		pvm_initsend(PvmDataDefault);
		pvm_pkint(&N, 1, 1);

		std::vector<int> data(N);

		int tmp;
		while (input >> tmp)
		{
			data.push_back(tmp);
		}
		input.close();

		pvm_initsend(PvmDataDefault);
		pvm_pkint(data.data(), N, 1);
			 	
		pvm_send(child_task, 1);
	}

	listenChild(child_task, outputName);

//	delete child_task;

	return 0;
}

void listenChild(int &child, std::string outFileName)
{
	std::ofstream output(outFileName.c_str());

	int answer;

	pvm_recv(child, -1);
	pvm_upkint(&answer, 1, 1);

	std::string ans = (answer ? "May the subset be with You!" : "I find your lack of subset disturbing!");

	output << ans;
	
	output.close();
	
	pvm_exit();
}
