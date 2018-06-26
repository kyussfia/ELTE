#include "persistance.h"
#include <stdlib.h>
#include <QDebug>

Persistance::Persistance()
{
    filename = "../Amoba/gm.sv";
    file = new QFile(filename);
    if (!file->open(QIODevice::ReadOnly | QIODevice::Text | QIODevice::ReadWrite))
    {
        qDebug() << "FAIL TO CREATE FILE / FILE NOT EXIT***";
    }
}

void Persistance::save(QVector< QVector<int> > field, int currentPlayer, int gameSize)
{
    delete file;
    file = new QFile(filename);
    /*
     * If file not exit it will
     * */
    if (!file->open(QIODevice::ReadOnly | QIODevice::Text | QIODevice::ReadWrite))
    {
        qDebug() << "FAIL TO CREATE FILE / FILE NOT EXIT***";
    }
    file->reset();
    /*for writing line by line to text file */
    QTextStream stream(file);

    stream << gameSize <<endl;
    stream << currentPlayer << endl;
    for (int i=0; i<field.size(); i++)
    {
        for (int j=0; j<field[i].size(); j++)
        {
            stream << field[i][j];
        }
        stream  << endl;
    }
}

bool Persistance::hasSave()
{
    if (file->atEnd())
    {
        return false;
    }
    return true;
}

bool Persistance::load(QVector< QVector<int> > &field, int &currentPlayer, int &gameSize)
{
    delete file;
    file = new QFile(filename);
    if (!file->open(QIODevice::ReadOnly | QIODevice::Text | QIODevice::ReadWrite))
    {
        qDebug() << "FAIL TO CREATE FILE / FILE NOT EXIT***";
    }
    /*for Reading line by line from text file*/
    if (hasSave())
    {
        field.clear();
        QTextStream s(file);

        s >> gameSize;
        s >> currentPlayer;

        field.resize(gameSize);
        s.read(1);
        for (int i=0; i<gameSize; i++)
        {
            QString line = s.readLine(gameSize);
            field[i].clear();
            field[i].resize(gameSize);
            for(int j=0; j<line.length(); j++)
            {
                if (line[j] == '0')
                {
                    field[i][j] = 0;
                } else if (line[j] == '1')
                {
                    field[i][j] = 1;
                } else if (line[j] == '2') {
                    field[i][j] = 2;
                }
             }
            s.read(1);
        }
        return true;
    }
    return false;
}
