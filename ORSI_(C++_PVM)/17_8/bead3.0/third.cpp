#include <pvm3.h>
#include <vector>
#include "functions.h"
#include <future>

void recieveBaseData(const int master, int& prev, unsigned int& picturesNum);
std::vector< std::vector<int> > getColSum(const std::vector< std::vector<int> > picture);
std::vector< std::vector<int> > getRowSum(const std::vector< std::vector<int> > picture);
std::vector<int> getLabel(const std::vector<int> row);
std::vector< std::vector<int> > getColumnMatrix(const std::vector< std::vector<int> > picture);

int main()
{
	const int master = pvm_parent();
	int prev;
	unsigned int picturesNum;

	recieveBaseData(master, prev, picturesNum);

	for (unsigned int i = 0; i < picturesNum; i++)
	{
		std::vector< std::vector<int> > picture = receivePicture(prev);

		std::vector< std::vector<int> > rowSum = getRowSum(picture);

		std::vector< std::vector<int> > colSum = getRowSum(getColumnMatrix(picture));

		pvm_initsend(PvmDataDefault);
		buffer(picture);
		buffer(rowSum);
		buffer(colSum);
		pvm_send(master, 1);
	}
	pvm_exit();
	return 0;
}

void recieveBaseData(const int master, int& prev, unsigned int& picturesNum)
{
	pvm_recv(master, 1);
	pvm_upkint(&prev, 1, 1);
	pvm_upkuint(&picturesNum, 1, 1);
}

std::vector< std::vector<int> > getRowSum(const std::vector< std::vector<int> > picture)
{
	std::vector< std::vector<int> > rowLabel;
	std::vector< std::future< std::vector<int> > > results;
	for (auto& i : picture)
	{
		results.push_back(std::async(std::launch::async, getLabel, i));
	}

	for (auto& i : results)
	{
		rowLabel.push_back(i.get());
	}

	return rowLabel;
}

std::vector<int> getLabel(const std::vector<int> row)
{
	std::vector<int> label;
	unsigned int j;
	for (unsigned int i = 0; i < row.size(); i = j)
	{
		int c = 0;
		for (j = i; j < row.size() && rgbEquals(row[i], row[j]); j++) {
			c++;
		}
		label.push_back(c);
	}

	while (label.size() < row.size())
	{
		label.push_back(0);
	}
	return label;
}

std::vector< std::vector<int> > getColumnMatrix(const std::vector< std::vector<int> > picture)
{
	std::vector< std::vector<int> > cols(picture.size(), std::vector<int>(picture.size()));
	for (unsigned int i = 0; i < picture.size(); ++i)
	{
		for (unsigned int j = 0; j < picture.size(); j++)
		{
			cols[j][i] = picture[i][j]; 
		}
	}
	return cols;
}