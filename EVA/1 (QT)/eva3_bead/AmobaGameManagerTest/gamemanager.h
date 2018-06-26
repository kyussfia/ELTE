#ifndef GAMEMANAGER_H
#define GAMEMANAGER_H

#include <QObject>
#include <QVector>
#include "mockpersistance.h"

class GameManager : public QObject
{
    Q_OBJECT
public:
    GameManager() {
        this->gameLoader = new MockPersistance(true);
        this->inGame = false;
        this->winner = NoPlayer;
        this->field = QVector< QVector<players> >();
        this->currentPlayer = NoPlayer;
    }

    void newMockPersistance(bool l)
    {
        this->gameLoader = new MockPersistance(l);
    }

    enum players {NoPlayer, playerX, playerO};

    ~GameManager();

     bool inGame;

     players winner;

     QVector< QVector<players> > getField();

     players getNoP()
     {
         return NoPlayer;
     }

     players getPX()
     {
         return playerX;
     }

     players getPO()
     {
         return playerO;
     }

     int getGameSize()
     {
         return gameSize;
     }

     players getCurrentPlayer()
     {
         return currentPlayer;
     }

     bool saveAvailable();

signals:
     void newGameResponse();
     void fieldClickedResponse();
     void gameSavedResponse();
     void gameLoadedResponse();

public slots:
     void newGameRequest(int size);
     void fieldClickedRequest(int index);
     void saveGameRequest();
     void loadGameRequest();

private:
     int gameSize;
     QVector< QVector<players> > field;
     players currentPlayer;
     players getNextPlayer();
     MockPersistance* gameLoader;

     void initField();
     void clearField();

     bool fieldIsFull();
     bool positionExists(int y, int x);

     bool positionsGotByCurrentPlayer(int y1, int x1, int y2, int x2, int y3, int x3, int y4, int x4, int y5, int x5);
     bool positionsGotByCurrentPlayer(int y1, int x1, int y2, int x2, int y3, int x3, int y4, int x4);
     bool positionsGotByCurrentPlayer(int y1, int x1, int y2, int x2, int y3, int x3);

     bool positionGotByCurrentPlayer(int y, int x);
     bool gotStraight(int lastY, int lastX);
     bool gotDiagonal(int lastY, int lastX);

     bool gotStraightQuad(int lastY, int lastX);
     bool gotDiagonalQuad(int lastY, int lastX);
     bool gotStraightDrill(int lastY, int lastX);
     bool gotDiagonalDrill(int lastY, int lastX);

     void removeRandomMarkOfCurrentPlayer();
     int randInt();

     QVector< QVector<int> > convertFieldToIntField();
     int convertCurrentPlayerToInt();
     void convertIntFieldToPlayersField(QVector< QVector<int> > loaded);
};

#endif // GAMEMANAGER_H
