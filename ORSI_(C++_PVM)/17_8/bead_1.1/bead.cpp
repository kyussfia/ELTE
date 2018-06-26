#include <fstream>
#include <iostream>
#include <sstream>
#include <algorithm>
#include <string>
#include <vector>
#include <future>
/**
* @author Mikus Márk - CM6TSV
**/

std::vector<std::string> splitLine(std::string& string)
{
	std::string tmp;
	std::vector<std::string> result;
	std::stringstream ss(string);

	while (ss.good() && ss >> tmp)
	{
		result.push_back(tmp);
	}

	return result;
}

bool isPrime(int number) {

    if(number < 2) return false;
    if(number == 2) return true;
    if(number % 2 == 0) return false;
    for(int i=3; (i*i)<=number; i+=2){
        if(number % i == 0 ) return false;
    }
    return true;

}

unsigned int hash_char(char& c)
{
	unsigned int code = 1638;
	unsigned int k = 305419896;
	if (int(c) % 2 != 0) {
		code <<= 11;
	} else {
		code <<= 6;
	}
	code ^= (int(c) & 255);

	return isPrime(code) ? code|k : code&k;
}

unsigned int hash_word(std::string& string)
{
	unsigned int hashSum = 0;

	for(char& c : string)
	{
		hashSum += hash_char(c);
	}

	return hashSum;
}

std::string hash_line(std::string data)
{
	std::vector<std::string> words = splitLine(data);

	std::ostringstream ss;

	for (unsigned int i = 0; i<words.size(); i++)
	{
		ss << hash_word(words[i]) << " ";
	}

	return ss.str();
}

int main()
{
	std::ifstream input("input.txt");
	
	unsigned int N;
	std::string line;

	std::getline(input, line);

	std::istringstream ss(line);
	ss >> N;
	
	std::vector<std::string> data(N, "");

	if (input.is_open())
	{
		for (unsigned int i = 0; i < N; i++)
		{
			std::getline(input, data[i]);
		}

		input.close();
	}

	std::vector<std::future<std::string>> result;
	for (unsigned int i = 0; i < N; i++)
	{
		result.push_back(std::async(std::launch::async, hash_line, data[i]));
	}
	
	std::ofstream output("output.txt");
	for (std::future<std::string>& f : result)
	{
		f.wait();
		output << f.get() << std::endl;
	}
	output.close();
	
	return 0;
}