#ifndef AMOBAWIDGET_H
#define AMOBAWIDGET_H

#include <QWidget>
#include <QMessageBox>
#include <QSignalMapper>
#include <QPushButton>
#include <QVector>
#include <QString>
#include "gamemanager.h"

namespace Ui {
class AmobaWidget;
}

class AmobaWidget : public QWidget
{
    Q_OBJECT

public:
    explicit AmobaWidget(QWidget *parent = 0);
    ~AmobaWidget();

signals:
    void newGameRequest();
    void fieldClickedRequest();

private slots:
    void newGameClicked();
    void newGame();
    void fieldClicked();
    void endRound();
    void gameSaved();
    void gameLoaded();

private:
    Ui::AmobaWidget *ui;
    QMessageBox* sure;
    QSignalMapper* newGameSignalMapper;
    QSignalMapper* fieldClickedSignalMapper;
    GameManager* gm;
    QVector<QPushButton*> field;
    QMessageBox* winBox;
    QMessageBox* tiedBox;
    QMessageBox* saveBox;

    void setupUi();
    int getCheckedFieldSize();
    int areYouSure();
    int getButtonSizeByGameSize();
    void setButtonSize(QPushButton* b);
    void setUpEvents();
    void handleButton(QPushButton* button);

    void win();
    void tied();

    QString getCurrentPlayerQString();
    QString getCurrentPlayerMarkQString();
    QString getCurrentPlayerColorQString();
    QString getPlayerQString(GameManager::players player);
    void displayCurrentPlayer();
    void markField(QPushButton* b);


    void drawUpField();
    void refreshField();
    void setPlayerOOnButton(QPushButton* b);
    void setPlayerXOnButton(QPushButton* b);
    void setPlayerNoOnButton(QPushButton* b);
    void setPlayerOnButton(GameManager::players, QPushButton* b);
};

#endif // AMOBAWIDGET_H
