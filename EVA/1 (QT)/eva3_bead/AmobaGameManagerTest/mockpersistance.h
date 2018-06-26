#ifndef MOCKPERSISTANCE
#define MOCKPERSISTANCE

#include <QVector>
#include <QFile>
#include <QObject>

class MockPersistance : public QObject
{
    Q_OBJECT
public:
    MockPersistance(bool l)
    {
        hS = l;
    };

    ~MockPersistance() {
        delete file;
    };

    bool hasSave()
    {
        return hS;
    }

    void save(QVector< QVector<int> > field, int currentPlayer, int gameSize)
    {
        if (field.size() == gameSize)
        {
            currentPlayer = currentPlayer + 0;
        }
    }

    bool load(QVector< QVector<int> > &field, int &currentPlayer, int &gameSize)
    {
        currentPlayer = 2;
        gameSize = 6;

        field.resize(gameSize);

        for (int i=0; i<gameSize; i++)
        {
            field[i].clear();
            field[i].resize(gameSize);
            for(int j=0; j<gameSize; j++)
            {
                if ((i==1 && j==1) || (i==1 && j==3)) {
                    field[i][j] = 2;
                } else if (i==2 && j==2)
                {
                    field[i][j] = 1;
                } else {
                    field[i][j] = 0;
                }
             }
        }
        return true;
    }

private:
    bool hS;

    QString filename;
    QFile* file;
};

#endif // MOCKPERSISTANCE

