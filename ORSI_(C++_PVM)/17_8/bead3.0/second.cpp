#include <pvm3.h>
#include <vector>
#include "functions.h"
#include <future>

void recieveBaseData(const int master, int& prev, int& next, unsigned int& picturesNum);
void colorDecode(std::vector< std::vector<int> >& picture);
std::vector<int> decodeRow(std::vector<int> row);

int main()
{
	const int master = pvm_parent();
	int prev, next;
	unsigned int picturesNum;

	recieveBaseData(master, prev, next, picturesNum);

	for (unsigned int i = 0; i < picturesNum; i++)
	{
		std::vector< std::vector<int> > picture = receivePicture(prev);
		colorDecode(picture);
		sendPicture(next, picture);
	}
	pvm_exit();
	return 0;
}

void colorDecode(std::vector< std::vector<int> >& picture)
{
	std::vector< std::future< std::vector<int> > > results;
	for (auto i : picture)
	{
		results.push_back(std::async(std::launch::async, decodeRow, i));
	}

	for (unsigned int i = 0; i < results.size(); i++)
	{
		picture[i] = results[i].get();
	}
}

std::vector<int> decodeRow(std::vector<int> row)
{
	std::vector<int> result;
	for (auto pixel : row)
	{
		std::vector<int> rgb = fromRGB(pixel);
		for (auto& color : rgb)
		{
			if (color <= 127)
			{
				color = 0;
			} 
			else {
				color = 255;
			}
		}
		result.push_back(createRGB(rgb[0], rgb[1], rgb[2])); 
	}
	return result;
}

void recieveBaseData(const int master, int& prev, int& next, unsigned int& picturesNum)
{
	pvm_recv(master, 1);
	pvm_upkint(&prev, 1, 1);
	pvm_upkint(&next, 1, 1);
	pvm_upkuint(&picturesNum, 1, 1);
}