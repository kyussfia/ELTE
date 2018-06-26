#include <fstream>
#include <vector>
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
	
	// Start calculating length for every vector.
	std::vector<std::future<double>> results;
	for (size_t i = 0; i < N; i++)
	{
		results.push_back(std::async(std::launch::async, vector_length, data[i]));
		//async másik szálon indítunk vmit: 1 param: flag (egyől kezd el számolni), 2param: milyen fv. legyen külön szálon? 3param: előző fv pramétere

		//std::thread voidoknak future<voi> helyett
	}
	
	// Wait for the function to return, write the result into a file
	std::ofstream output("output.txt");
	for (const std::future<double>& f : results) // for (auto &f)
	{
		f.wait(); //ha még nem futott le várjuk be
		output << f.get() << std::endl;
	}
	output.close();
	
	//g++ main.cpp -std=c++11
	return 0;
}				