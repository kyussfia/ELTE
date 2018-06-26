#ifndef PERSISTANCE_H
#define PERSISTANCE_H

#include <QObject>
#include <QVector>
#include <QFile>

class Persistance : public QObject
{
    Q_OBJECT
public:
    Persistance();

    ~Persistance() {
        delete file;
    };

    bool hasSave();
    void save(QVector< QVector<int> > field, int currentPlayer, int gameSize);
    bool load(QVector< QVector<int> > &field, int &currentPlayer, int &gameSize);

private:
    QString filename;
    QFile* file;
};

#endif // PERSISTANCE_H
