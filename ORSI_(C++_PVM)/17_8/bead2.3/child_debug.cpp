#include <pvm3.h>
#include <vector> // Dynamic data storage
#include <iostream>
#include <stdlib.h>

int main()
{
	int parent_id = pvm_parent();
	
	pvm_recv(parent_id, 1);
	
	int k;
	pvm_upkint(&k, 1, 1);

	unsigned int n = 0;

	std::cout <<"par: " << parent_id << "  me: "<< pvm_mytid() <<": " << k  << std::endl;
	pvm_initsend(PvmDataDefault);

	if (k == 0)
	{
		n++; //true
		pvm_pkuint(&n, 1, 1);
		pvm_send(parent_id, 1);
		pvm_exit();
	} else {
		pvm_upkuint(&n, 1 ,1);
		std::cout <<"par: " << parent_id << "  me: "<< pvm_mytid() <<": " << n  << std::endl;

		if (n == 0)
		{
			pvm_pkuint(&n, 1, 1);
			pvm_send(parent_id, 1);
			pvm_exit();
		} else {
			std::vector<int> data(n);
			pvm_upkint(data.data(), n, 1);

			std::vector<int> child_tasks(2);

			int started = pvm_spawn((char*)"child", 0, PvmTaskDefault, 0, 2, child_tasks.data());
			if (started < 2)
			{
				for (int i = 0; i < started; i++)
				{
					pvm_kill(child_tasks[i]);
				}
				
				pvm_exit();
				//pvm_perror((char*)"HIBA: ");
				std::cout << "Spawn error!" << std::endl;
				exit(2);
			}

			int tail = data.back();
			data.pop_back(); //popped
			unsigned int nDecreased = n - 1;
			
			//1 child: k n-1 popped
			pvm_pkint(&k, 1, 1);
			pvm_pkuint(&nDecreased, 1, 1);
			pvm_pkint(data.data(), n, 1);
			pvm_send(child_tasks[0], 1);

			std::cout << "Send to "<<child_tasks[0]<<" k: " << k << " n: " << nDecreased << std::endl;
			int kDecreased = k - tail;
			//2 child: k-tail n-1 popped
			pvm_initsend(PvmDataDefault);
			pvm_pkint(&kDecreased, 1, 1);
			pvm_pkuint(&nDecreased, 1, 1);
			pvm_pkint(data.data(), n, 1);
			pvm_send(child_tasks[1], 1);
			std::cout << "Send to "<<child_tasks[1]<<" k: " << kDecreased << " n: " << nDecreased << std::endl;

			//-----------------------
			// Recieve
			//-----------------------
			unsigned int answer;

			for (auto i : child_tasks)
			{
				pvm_recv(i, 1);
				pvm_upkuint(&answer, 1, 1);

				if (answer)
				{
					std::cout << i << ". küldés: ans " << answer << std::endl;
					pvm_initsend(PvmDataDefault);
					pvm_pkuint(&answer, 1, 1);
					pvm_send(parent_id, 1);
					pvm_exit();
					break;
				}
			}

			if (!answer) {
				std::cout << "küld hamis: ans " << answer  << std::endl;
				pvm_initsend(PvmDataDefault);
				pvm_pkuint(&answer, 1, 1);
				pvm_send(parent_id, 1);
				pvm_exit();
			}
		}
	}
	return 0;
}