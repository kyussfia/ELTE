//functions.h

void buffer(std::vector< std::vector<int> >& picture)
{
	int size = picture.size();
	pvm_pkint(&size, 1, 1);
	for (auto& row : picture)
	{
		pvm_pkint(row.data(), row.size(), 1);
	}
}

void deBuffer(std::vector< std::vector<int> >& picture)
{
	int tmpSize;
	pvm_upkint(&tmpSize, 1, 1);
	picture.resize(tmpSize);
	for (auto& row : picture)
	{
		row.resize(tmpSize);
		pvm_upkint(row.data(), tmpSize, 1);
	}
}

void sendPicture(int to, std::vector< std::vector<int> >& picture)
{
	pvm_initsend(PvmDataDefault);
	buffer(picture);
	pvm_send(to, 1);
}

std::vector< std::vector<int> > receivePicture(int from)
{
	pvm_recv(from, 1);
	std::vector< std::vector<int> > picture;
	deBuffer(picture);
	return picture;
}

int createRGB(const int r, const int g, const int b)
{
	int rgb = 0;
	rgb |= (r << 16);
	rgb |= (g << 8);
	rgb |= b;
	return rgb;
}

std::vector<int> fromRGB(const int rgb)
{
	std::vector<int> result(3);
	result[0] = ((rgb >> 16) & 255);
	result[1] = ((rgb >> 8) & 255);
	result[2] = (rgb & 255);
	return result;
}

bool rgbEquals(const int a, const int b)
{
	std::vector<int> A = fromRGB(a);
	std::vector<int> B = fromRGB(b);
	return A[0] == B[0] && A[1] == B[1] && A[2] == B[2];
}