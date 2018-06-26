#include "gamemanager.h"

GameManager::~GameManager()
{
    delete gameLoader;
}

void GameManager::newGameRequest(int size)
{
    this->gameSize = size;
    initField();
    clearField();

    this->inGame = true;
    this->winner = GameManager::NoPlayer;
    this->currentPlayer = playerO;

    emit newGameResponse();
}

void GameManager::clearField()
{
    for(int i = 0; i<field.size(); i++)
    {
        for(int j = 0; j<field[i].size(); j++)
        {
            field[i][j] = NoPlayer;
        }
    }
}

void GameManager::initField()
{
    field.resize(this->gameSize);
    for(int i = 0; i<this->gameSize; i++)
    {
        field[i].resize(this->gameSize);
    }
}

QVector<QVector< GameManager::players > > GameManager::getField()
{
    return this->field;
}

void GameManager::fieldClickedRequest(int index)
{
    int y = (index + 1 - 1) / this->gameSize;
    int x = index + 1 - 1 - this->gameSize*y;

    field[y][x] = currentPlayer;

    if (fieldIsFull()) {
        inGame = false;
    } else
    if (gotStraight(y, x) || gotDiagonal(y, x))
    {
        winner = currentPlayer;
        inGame = false;
    } else
    if (gotStraightQuad(y, x) || gotDiagonalQuad(y, x))
    {
        removeRandomMarkOfCurrentPlayer();
        removeRandomMarkOfCurrentPlayer();
    } else
    if (gotStraightDrill(y, x) || gotDiagonalDrill(y, x))
    {
        removeRandomMarkOfCurrentPlayer();
    }

    currentPlayer = inGame ? getNextPlayer() : NoPlayer;
    emit fieldClickedResponse();
}

void GameManager::removeRandomMarkOfCurrentPlayer()
{
    bool found = false;
    int rY, rX;
    while(!found)
    {
        rY = randInt();
        rX = randInt();
        if(field[rY][rX] == currentPlayer)
        {
            found = true;
        }
    }
    if (found)
    {
        field[rY][rX] = NoPlayer;
    }
}

int GameManager::randInt()
{
    // Random number between low and high
    return qrand() % ((gameSize - 1) - 0) + 0;
}

GameManager::players GameManager::getNextPlayer()
{
    if (currentPlayer == GameManager::playerO)
    {
        return GameManager::playerX;
    } else {
        return GameManager::playerO;
    }
}

bool GameManager::fieldIsFull()
{
    for (int i = 0; i<field.size(); i++)
    {
        for(int j = 0; j<field[i].size(); j++)
        {
            if (field[i][j] == NoPlayer) {
                 return false;
            }
        }
    }
    return true;
}

bool GameManager::gotDiagonal(int lastY, int lastX)
{
    bool got;
    //left top
    for(int i=0; i<=5 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX-i, lastY+1-i, lastX+1-i, lastY+2-i, lastX+2-i, lastY+3-i, lastX+3-i, lastY+4-i, lastX+4-i);
    }

    //right top
    for (int i=0; i<=5 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX+i, lastY+1-i, lastX-1+i, lastY+2-i, lastX-2+i, lastY+3-i, lastX-3+i, lastY+4-i, lastX-4+i);
    }

    return got;
}

bool GameManager::gotStraight(int lastY, int lastX)
{
    bool got;
    //horizontal
    for (int i=0; i<=5 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY, lastX-i, lastY, lastX+1-i, lastY, lastX+2-i, lastY, lastX+3-i, lastY, lastX+4-i);
    }

    //vertical
    for (int i = 0; i<=5 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX, lastY+1-i, lastX, lastY+2-i, lastX, lastY+3-i, lastX, lastY+4-i, lastX);
    }
    return got;
}

bool GameManager::gotStraightQuad(int lastY, int lastX)
{
    bool got;
    //horizontal
    for (int i=0; i<=4 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY, lastX-i, lastY, lastX+1-i, lastY, lastX+2-i, lastY, lastX+3-i);
    }

    //vertical
    for (int i = 0; i<=4 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX, lastY+1-i, lastX, lastY+2-i, lastX, lastY+3-i, lastX);
    }
    return got;
}

bool GameManager::gotDiagonalQuad(int lastY, int lastX)
{
    bool got;
    //left top
    for(int i=0; i<=4 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX-i, lastY+1-i, lastX+1-i, lastY+2-i, lastX+2-i, lastY+3-i, lastX+3-i);
    }

    //right top
    for (int i=0; i<=4 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX+i, lastY+1-i, lastX-1+i, lastY+2-i, lastX-2+i, lastY+3-i, lastX-3+i);
    }

    return got;
}

bool GameManager::gotStraightDrill(int lastY, int lastX)
{
    bool got;
    //horizontal
    for (int i=0; i<=3 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY, lastX-i, lastY, lastX+1-i, lastY, lastX+2-i);
    }

    //vertical
    for (int i = 0; i<=3 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX, lastY+1-i, lastX, lastY+2-i, lastX);
    }
    return got;
}

bool GameManager::gotDiagonalDrill(int lastY, int lastX)
{
    bool got;
    //left top
    for(int i=0; i<=3 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX-i, lastY+1-i, lastX+1-i, lastY+2-i, lastX+2-i);
    }

    //right top
    for (int i=0; i<=3 && !got; i++)
    {
        got = got || positionsGotByCurrentPlayer(lastY-i, lastX+i, lastY+1-i, lastX-1+i, lastY+2-i, lastX-2+i);
    }

    return got;
}

bool GameManager::positionsGotByCurrentPlayer(int y1, int x1, int y2, int x2, int y3, int x3, int y4, int x4, int y5, int x5)
{
    if (positionGotByCurrentPlayer(y1, x1) &&
            positionGotByCurrentPlayer(y2, x2) &&
            positionGotByCurrentPlayer(y3, x3) &&
            positionGotByCurrentPlayer(y4, x4) &&
            positionGotByCurrentPlayer(y5, x5)) {
        return true;
    }
    return false;
}

bool GameManager::positionsGotByCurrentPlayer(int y1, int x1, int y2, int x2, int y3, int x3, int y4, int x4)
{
    if (positionGotByCurrentPlayer(y1, x1) &&
            positionGotByCurrentPlayer(y2, x2) &&
            positionGotByCurrentPlayer(y3, x3) &&
            positionGotByCurrentPlayer(y4, x4)) {
        return true;
    }
    return false;
}

bool GameManager::positionsGotByCurrentPlayer(int y1, int x1, int y2, int x2, int y3, int x3)
{
    if (positionGotByCurrentPlayer(y1, x1) &&
            positionGotByCurrentPlayer(y2, x2) &&
            positionGotByCurrentPlayer(y3, x3)) {
        return true;
    }
    return false;
}

bool GameManager::positionGotByCurrentPlayer(int y, int x)
{
    if (positionExists(y, x) && field[y][x] == currentPlayer)
    {
        return true;
    }
    return false;
}

bool GameManager::positionExists(int y, int x)
{
    if (y < 0 || y  >= gameSize)
    {
        return false;
    } else
    if (x < 0 || x >= gameSize)
    {
        return false;
    }
    return true;
}

bool GameManager::saveAvailable()
{
    return gameLoader->hasSave();
}

void GameManager::saveGameRequest()
{
    gameLoader->save(convertFieldToIntField(), convertCurrentPlayerToInt(), gameSize);
    emit gameSavedResponse();
}

QVector< QVector<int> > GameManager::convertFieldToIntField()
{
    QVector< QVector<int> > result = QVector< QVector<int> >();
    result.resize(field.size());
    for(int i=0; i<field.size(); i++)
    {
        result[i].resize(field[i].size());
        for (int j=0; j<field[i].size(); j++)
        {
            result[i][j] = (int)field[i][j];
        }
    }
    return result;
}

int GameManager::convertCurrentPlayerToInt()
{
    return (int)currentPlayer;
}

void GameManager::loadGameRequest()
{
    int cP, gS;
    QVector< QVector<int> > loadedField = QVector< QVector<int> >();
    if (gameLoader->load(loadedField, cP, gS))
    {
        this->gameSize = gS;
        this->currentPlayer = (players)cP;
        convertIntFieldToPlayersField(loadedField);
        this->inGame = true;
        this->winner = GameManager::NoPlayer;
        emit gameLoadedResponse();
    }
}

void GameManager::convertIntFieldToPlayersField(QVector<QVector<int> > loaded)
{
    field.clear();
    field.resize(loaded.size());
    for(int i=0; i<field.size(); i++)
    {
        field[i].resize(loaded[i].size());
        for(int j=0; j<field[i].size(); j++)
        {
            field[i][j] = (players)loaded[i][j];
        }
    }
}
