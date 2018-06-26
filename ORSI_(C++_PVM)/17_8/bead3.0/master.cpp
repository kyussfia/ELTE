#include <iostream>
#include <pvm3.h>
#include <string>
#include <fstream>
#include <vector>
#include "functions.h"
#include <stdlib.h> //atoi

std::vector<int> spawnChildren();
int spawnChild(const char* name);
std::vector< std::vector< std::vector<int> > > loadPictures(const std::string inFileName, unsigned int& N);
void sendData(int to, std::vector< std::vector< std::vector<int> > >& pictures);
void sendPreData(std::vector<int>& children, const unsigned int i, unsigned int& N, unsigned int& p);
void writeResult(const std::string outFileName, const unsigned int N, int channelEnd);
void printPicture(std::ofstream& output, std::vector< std::vector<int> > picture);
void printMatrix(std::ofstream& output, const std::vector< std::vector<int> > matrix);
std::vector< std::vector< std::vector<int> > > recieveQuiz(int from);
bool isValidPercentage(const int p);

int main(int argc, char* argv[])
{
	//validate args
	if (!argv[1] || !argv[2] || !argv[3] || argc != 4 || !isValidPercentage(atoi(argv[1])))
	{
		std::cout << "Parameter error! master <25, 50, 100> <inputfile> <outputfile>" << std::endl;
		return -1;
	}

	std::string inFileName(argv[2]);
	std::string outFileName(argv[3]);

	unsigned int p = atoi(argv[1]);

	unsigned int N; //pictures num
	std::vector< std::vector< std::vector<int> > > pictures = loadPictures(inFileName, N);

	std::vector<int> children = spawnChildren();

	//validate children's spawn error
	for (unsigned int i = 0; i < children.size(); i++)
	{
		if (children[i] == -2)
		{
			std::cout << "Spawn error!" << std::endl;
			return -2;
		} 
		else
		{
			sendPreData(children, i, N, p);
		}
	}

	//start Channel
	sendData(children[0], pictures);
	//end Channel
	writeResult(outFileName, N, children[2]);
	return 0;
}

bool isValidPercentage(const int p)
{
	return p <= 100 && p > 0 && (p == 25 || p == 50 || p == 100);
}

void sendPreData(std::vector<int>& children, const unsigned int i, unsigned int& N, unsigned int& p)
{
	pvm_initsend(PvmDataDefault);
	switch (i)
	{
		case 0 : pvm_pkint(&children[i+1], 1, 1);
				 pvm_pkuint(&p, 1, 1);
			break;
		case 1 : pvm_pkint(&children[i-1], 1, 1);
				 pvm_pkint(&children[i+1], 1, 1);
			break;
		case 2 : pvm_pkint(&children[i-1], 1, 1);
	}
	pvm_pkuint(&N, 1, 1);
	pvm_send(children[i], 1);
}

std::vector<int> spawnChildren()
{
	std::vector<int> children(3);
	children[0] = spawnChild("first");
	children[1] = spawnChild("second");
	children[2] = spawnChild("third");
	return children;
}

int spawnChild(const char* name)
{
	int child_task;
	int started = pvm_spawn(const_cast<char*>(name), 0, PvmTaskDefault, 0, 1, &child_task);

	if (started < 1)
	{
		pvm_kill(child_task);		
		pvm_exit();
		return -2;
	}

	return child_task;
}

std::vector< std::vector< std::vector<int> > > loadPictures(const std::string inFileName, unsigned int& N)
{
	std::ifstream input(inFileName.c_str());
	std::vector< std::vector< std::vector<int> > > pictures;

	if (input.is_open())
	{
		input >> N;
		pictures.resize(N);

		int tmpPicSize;

		for (auto& picture : pictures)
		{
			input >> tmpPicSize;
			picture.resize(tmpPicSize);
			for (auto& row : picture)
			{
				row.resize(tmpPicSize);
				for (int& pixel : row)
				{
					int tmpR, tmpG, tmpB;
					input >> tmpR >> tmpG >> tmpB;
					pixel = createRGB(tmpR, tmpG, tmpB);
				}
			}
		}

		input.close();
	}

	return pictures;
}

void sendData(int to, std::vector< std::vector< std::vector<int> > >& pictures)
{
	for (auto& picture : pictures)
	{
		sendPicture(to, picture);
	}
}


void writeResult(const std::string outFileName, const unsigned int N, int channelEnd)
{
	std::ofstream output(outFileName.c_str());
	for (unsigned int i = 0; i < N; i++)
	{
		std::vector< std::vector< std::vector<int> > > quiz = recieveQuiz(channelEnd);
		printPicture(output, quiz[0]);
		printMatrix(output, quiz[1]);
		printMatrix(output, quiz[2]);
	}
	output.close();
	pvm_exit();
}

void printPicture(std::ofstream& output, std::vector< std::vector<int> > picture)
{
	for (auto row : picture)
	{
		for (int pixel : row)
		{
			std::vector<int> rgb = fromRGB(pixel); 
			output << "(" << rgb[0] << "," << rgb[1] << "," << rgb[2] << ") ";
		}
		output << std::endl;
	}
}

void printMatrix(std::ofstream& output, const std::vector< std::vector<int> > matrix)
{
	for (auto row : matrix)
	{
		for (int e : row)
		{
			if (e != 0)
			{
				output << e << " ";
			}
		}
		output << std::endl;
	}
}

std::vector< std::vector< std::vector<int> > > recieveQuiz(int from)
{
	std::vector< std::vector< std::vector<int> > > quiz(3);
	pvm_recv(from, 1);
	deBuffer(quiz[0]);
	deBuffer(quiz[1]);
	deBuffer(quiz[2]);
	return quiz;
}