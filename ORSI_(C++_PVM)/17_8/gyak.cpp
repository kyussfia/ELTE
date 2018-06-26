#include <fstream>
#include <vector>
#include <chrono>
#include <future> // ASYNC, FUTURE
#include <cmath> // SQRT

double vector_length(const std::vector<double>& data)
{
	double square_length = 0;
	
	for (const double& num : data)
	{
		square_length += (num * num);
	}
	
	return std::sqrt(square_length);
}
	//feldarabolni szavakra a sort

	//szavakat hashelni

	//^= xor
	//&= bitenként és
	//|= butenként vgy

	//shiftelés code <<= 8;
		//std::endl;
	//return data;

	//people.inf.elte.hu/mykeesg
	//MiKTex ->fordító
	//	TexStudio

	//shared latex .com

	//msconfig a magokohoz

// a.exe >> result.txt (> felülírja)

int main()
{
	std::ifstream input("input.txt");
	
	unsigned int N, M;
	
	input >> N >> M;
	
	// Read all the data into one array
	std::vector<std::vector<double>> data(N, std::vector<double>(M));
	//vec(size, init_value)

	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < M; j++)
		{
			input >> data[i][j];
		}
	}
	
	input.close();
	
	auto t0 = std::chrono::high_resolution_clock::now();
	
	// Start calculating length for every vector.
	std::vector<std::future<double>> results;
	for (size_t i = 0; i < N; i++)
	{
		results.push_back(std::async(std::launch::async, vector_length, data[i]));
		//async másik szálon indítunk vmit: 1 param: flag (egyől kezd el számolni), 2param: milyen fv. legyen külön szálon? 3param: előző fv pramétere

		//std::thread voidoknak future<voi> helyett
	}

	//push back szétsedése, új vektor és csak a lényeget mérni

	auto t1 = std::chrono::high_resolution_clock::now();
	
	// Wait for the function to return, write the result into a file
	std::ofstream output("output.txt");		
	for (std::future<double>& f : results) // for (auto &f)
	{
		f.wait(); //ha még nem futott le várjuk be
		output << f.get() << std::endl;
	}
	output.close();

	auto t1 = std::chrono::high_resolution_clock::now();

	std::cout << std::chrono::duration_cast<std::chrono::milliseconds>(t1 - t0).count() << " msec" << std::endl;
	
	//g++ main.cpp -std=c++11
	return 0;
}				