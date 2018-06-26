#ifndef READ_FILE_H_INCLUDED
#define READ_FILE_H_INCLUDED
#include <vector>

#include "read_file.cpp"

//A fileból beolvasás

namespace file
    {
        void Fajlbol_olvas(const std::string &fajlnev, std::vector<string> &telep, std::vector<string> &madar, std::vector<vector<int> > &t);
    }

#endif // READ_FILE_H_INCLUDED
