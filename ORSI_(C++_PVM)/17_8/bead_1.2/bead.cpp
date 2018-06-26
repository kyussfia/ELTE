#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include <vector> 
#include <future> 
#include <math.h> //sqrt
#include <numeric> //accumulate
#include <iterator> //istream iterator
#include <cstdint>
/**
* @author Mikus Márk - CM6TSV
**/
bool isPrime(int number) {
    if (number < 2) 
    	return false;
    if (number == 2) 
    	return true;
    if (number % 2 == 0) 
    	return false;
    int root = sqrt(number);
    for (int i = 3; i <= root; i += 2) {
    	if (number % i == 0) 
    		return false;
    }
    return true;
}

unsigned int hash_char(char c) {
	static unsigned int k = 305419896;
	static unsigned int base_code = 1638;

	unsigned int code = base_code;

	code <<= (static_cast<int>(c) % 2 != 0 ? 11 : 6); 
	code ^= (static_cast<int>(c) & 255);

	return isPrime(code) ? code|k : code&k;
}

unsigned int hash_word(std::string& string) {
	return std::accumulate(
		string.begin(),
		string.end(),
		0,
		[](int a, char c) {
			return a + hash_char(c);
		}
	);

}

std::string to_string(unsigned int num) {
	const int n = snprintf(NULL, 0, "%u", num);
	char buf[n+1];
	int c = snprintf(buf, n+1, "%u", num);
	return std::string(buf);
}

std::string hash_line(std::string data) {
	std::istringstream iss(data);

	return std::accumulate(
		std::istream_iterator<std::string>{iss},
		std::istream_iterator<std::string>{},
		std::string(""),
		[](std::string r, std::string t) {
			//return r.append((static_cast<std::ostringstream&>(std::ostringstream().flush() << hash_word(t) << " ")).str());
			return r.append(to_string(hash_word(t)) + " ");
		}
	);
}

std::vector<std::string> read(std::ifstream& input) {	
	unsigned int N;
	std::string line;

	std::getline(input, line);

	std::istringstream ss(line);
	ss >> N;
	
	std::vector<std::string> data(N, "");

	if (input.is_open()) {
		for (auto& element : data) {
			std::getline(input, element);
		}

		input.close();
	}

	return data;
}

int main()
{
	std::ifstream input("input.txt");
	
	std::vector<std::string> data = read(input);

	std::vector<std::future<std::string>> result;
	for (auto element : data) {
		result.push_back(std::async(std::launch::async, hash_line, element));
	}
	
	std::ofstream output("output.txt");
	for (auto& f : result) {
		output << f.get() << std::endl;
	}
	output.close();
	
	return 0;
}