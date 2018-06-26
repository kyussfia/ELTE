#include <pvm3.h>
#include <vector>
#include "functions.h"
#include <future>

void recieveBaseData(const int master, int& next, unsigned int& picturesNum, unsigned int& p);
std::vector< std::vector<int> > compress(const unsigned int p, const std::vector< std::vector<int> > picture);
int avg(const int i, const int j, int p, const std::vector< std::vector<int> >& picture);
std::vector< std::vector< std::vector<int> > > explode(const std::vector< std::vector<int> > picture);
std::vector< std::vector<int> > implode(const int newSize, std::vector< std::vector< std::vector<int> > >& quarters);
std::vector< std::vector< std::vector<int> > > divide(const unsigned int p, std::vector< std::vector< std::vector<int> > > quarters);

int main()
{
	const int master = pvm_parent();
	int next;
	unsigned int picturesNum;
	unsigned int p;

	recieveBaseData(master, next, picturesNum, p);

	for (unsigned int i = 0; i < picturesNum; i++)
	{
		std::vector< std::vector<int> > picture = receivePicture(master);
		std::vector< std::vector<int> > result = compress(p, picture);
		sendPicture(next, result);
	}
	pvm_exit();
	return 0;
}

std::vector< std::vector<int> > compress(const unsigned int p, const std::vector< std::vector<int> > picture)
{
	static const unsigned int K = 5;
	int newSize = p * picture.size() / 100;
	if (picture.size() < K)
	{
		std::vector< std::vector<int> > result(newSize);
		for (unsigned int i = 0; i < result.size(); i++)
		{
			result[i] = std::vector<int>(newSize);
			for (unsigned int j = 0; j < result.size(); j++)
			{
				result[i][j] = avg(i, j, p, picture);
			}
		}
		return result;
	} 
	else
	{
		std::vector< std::vector< std::vector<int> > > quarters = explode(picture);
		std::vector< std::vector< std::vector<int> > > q = divide(p, quarters);
		// & Conquer
		return implode(newSize, q);
	}
}

std::vector< std::vector< std::vector<int> > > divide(const unsigned int p, std::vector< std::vector< std::vector<int> > > quarters)
{
	std::vector< std::vector< std::vector<int> > > result(4, std::vector< std::vector<int> >(quarters[0].size()/2, std::vector<int>(quarters[0].size()/2)));
	std::vector< std::future< std::vector< std::vector<int> > > > futureResults;

	for (unsigned int i = 0; i < 4; i++)
	{
		futureResults.push_back(std::async(std::launch::async, compress, p, (quarters[i])));
	}

	for (unsigned int i = 0; i < 4; i++)
	{
		result[i] = futureResults[i].get();

	}

	return result;
}

std::vector< std::vector<int> > implode(const int newSize, std::vector< std::vector< std::vector<int> > >& quarters)
{
	std::vector< std::vector<int> > result(newSize, std::vector<int>(newSize));

	for (int i = 0; i < newSize; i++)
	{
		for (int j = 0; j < newSize ; j++)
		{
			int quarterIndex = (i < (newSize / 2) ? (j < (newSize / 2) ? 0 : 1) : (j < (newSize / 2) ? 2 : 3));
			result[i][j] = quarters[quarterIndex][i % (newSize / 2)][j % (newSize / 2)];
		}
	}

	return result;
}

std::vector< std::vector< std::vector<int> > > explode(const std::vector< std::vector<int> > picture)
{
	std::vector< std::vector< std::vector<int> > > quarters(4, std::vector< std::vector<int> >(picture.size()/2, std::vector<int>(picture.size()/2)));

	for (unsigned int i = 0; i < picture.size(); i++)
	{
		for (unsigned int j = 0; j < picture[i].size(); j++)
		{
			int quarterIndex = i < (picture.size() / 2) ? (j < (picture.size() / 2) ? 0 : 1) : (j < (picture.size() / 2) ? 2 : 3);
			quarters[quarterIndex][i % (picture.size() / 2)][j % (picture.size() / 2)] = picture[i][j];
		}
	}

	return quarters;
}

int avg(const int i, const int j, int p, const std::vector< std::vector<int> >& picture)
{
	int count = 0, rsum = 0, gsum = 0, bsum = 0, ind = 100 / p;
	for (int k = 0; k < ind; k++)	
	{
		for (int l = 0; l < ind; l++)
		{
			count++;
			std::vector<int> rgb = fromRGB(picture[i * ind + k][j * ind + l]);
			rsum += rgb[0];
			gsum += rgb[1];
			bsum += rgb[2];
		}
	}

	return createRGB(rsum / count, gsum / count, bsum / count);
}

void recieveBaseData(const int master, int& next, unsigned int& picturesNum, unsigned int& p)
{
	pvm_recv(master, 1);
	pvm_upkint(&next, 1, 1);
	pvm_upkuint(&p, 1, 1);
	pvm_upkuint(&picturesNum, 1, 1);
}