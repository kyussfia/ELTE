#include <pvm3.h>
#include <vector>
#include <iostream>
/* CM6TSV */
void sendAnswer(const int parent_id, unsigned short int answer = 0);
int spawnChildren(std::vector<int>& child_tasks);
std::vector<int> recieveData(const int parent_id, int& k, unsigned int& n);
void sendData(int child, int& k, unsigned int& n, std::vector<int>& data);
unsigned short int recieveAnswer(std::vector<int>& child_tasks);

int main()
{
	const int parent_id = pvm_parent();
	
	int k;
	unsigned int n;
	std::vector<int> data = recieveData(parent_id, k, n);

	if (k == 0)
	{
		sendAnswer(parent_id, 1); //true
	}
	else if (n == 0)
	{
		sendAnswer(parent_id); //false
	} 
	else
	{
		std::vector<int> child_tasks(2);
		spawnChildren(child_tasks);
		int tail = data.back();
		data.pop_back();
		n = n - 1;	
		sendData(child_tasks[0], k, n, data);
		k = k - tail;
		sendData(child_tasks[1], k, n, data);
		sendAnswer(parent_id, recieveAnswer(child_tasks));
	}

	return 0;
}

void sendAnswer(const int parent_id, unsigned short int answer)
{
	pvm_initsend(PvmDataDefault);
	pvm_pkushort(&answer, 1, 1);
	pvm_send(parent_id, 1);
	pvm_exit();
}

int spawnChildren(std::vector<int>& child_tasks)
{
	int started = pvm_spawn(const_cast<char*>("child"), 0, PvmTaskDefault, 0, 2, child_tasks.data());
	if (started < 2)
	{
		for (int i = 0; i < started; i++)
		{
			pvm_kill(child_tasks[i]);
		}
		
		pvm_exit();
		std::cout << "Spawn error!" << std::endl;
		return -2;
	}
	return started;
}

std::vector<int> recieveData(const int parent_id, int& k, unsigned int& n)
{
	pvm_recv(parent_id, 1);
	pvm_upkint(&k, 1, 1);
	pvm_upkuint(&n, 1 ,1);
	std::vector<int> data(n);
	pvm_upkint(data.data(), n, 1);
	return data;
}

void sendData(int child, int& k, unsigned int& n, std::vector<int>& data)
{
	pvm_initsend(PvmDataDefault);
	pvm_pkint(&k, 1, 1);
	pvm_pkuint(&n, 1, 1);
	pvm_pkint(data.data(), n, 1);
	pvm_send(child, 1);
}

unsigned short int recieveAnswer(std::vector<int>& child_tasks)
{
	unsigned short int answer, tmp;
	pvm_recv(child_tasks[0], 1);
	pvm_upkushort(&answer, 1, 1);
	pvm_recv(child_tasks[1], 1);
	pvm_upkushort(&tmp, 1, 1);
	return tmp ? tmp : answer;
}