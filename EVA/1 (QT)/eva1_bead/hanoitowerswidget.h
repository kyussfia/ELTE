#ifndef HANOITOWERSWIDGET_H
#define HANOITOWERSWIDGET_H

#include <QWidget>
#include "tower.h"
#include <math.h>

namespace Ui {
class HanoiTowersWidget;
}

class HanoiTowersWidget : public QWidget
{
    Q_OBJECT

public:
    explicit HanoiTowersWidget(QWidget *parent = 0);
    ~HanoiTowersWidget();

private:
    int gameSize;
    bool won, firstRun;

    Ui::HanoiTowersWidget *ui;

    void setCheckedDifficulty();
    void initTowers(int plates);

    void startGameEvents();
    void endGameEvents();
    void makeMove(Tower* reciever);
    void setButtonsToSelect();

    void win();
    bool isValidMove(Tower* receiver);
    Tower* senderTower;

    int getBestWinNumber() {
        return pow(2,gameSize) - 1;
    }

private slots:
    void startNewGame();
    void stepStarted();
    void stepEnded();
};

#endif // HANOITOWERSWIDGET_H
