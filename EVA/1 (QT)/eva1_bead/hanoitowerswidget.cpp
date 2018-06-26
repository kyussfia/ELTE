#include "hanoitowerswidget.h"
#include "ui_hanoitowerswidget.h"
#include <QString>
#include <QMessageBox>

HanoiTowersWidget::HanoiTowersWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::HanoiTowersWidget)
{
    this->setFixedSize(1020,600);
    ui->setupUi(this);

    firstRun = true;
    ui->button1->setText("");
    ui->button2->setText("");
    ui->button3->setText("");

    connect(ui->newGameButton, SIGNAL(clicked(bool)), this, SLOT(startNewGame()));
    connect(ui->exitButton, SIGNAL(clicked(bool)), QApplication::instance(), SLOT(quit()));
}

HanoiTowersWidget::~HanoiTowersWidget()
{
    delete senderTower;
    delete ui;
}

void HanoiTowersWidget::startNewGame()
{
    bool skip = false;
    if (!firstRun) {
        QMessageBox *sure = new QMessageBox();
        sure->setText("Biztos hogy új játékot szeretnél kezdeni?");
        sure->setStandardButtons(QMessageBox::Yes | QMessageBox::Cancel);
        sure->setDefaultButton(QMessageBox::Yes);
        int ret = sure->exec();
        switch (ret) {
            case QMessageBox::Yes:
                sure->close();
                firstRun = true;
                startNewGame();
                break;
            case QMessageBox::Cancel:
                skip = true;
                break;
        }
    } else {
        firstRun = false;
    }

    if (!skip) {
        won = false;
        setCheckedDifficulty();
        ui->stepCounter->display(QString::number(0));
        initTowers(gameSize);
        startGameEvents();
    }
}

void HanoiTowersWidget::setCheckedDifficulty()
{
    int i;
    if(ui->difficulty3->isChecked()) {
        i = 3;
    } else {
        if (ui->difficulty5->isChecked()) {
            i = 5;
        } else {
            i = 8;
        }
    }
    gameSize = i;
}

void HanoiTowersWidget::initTowers(int plates) {
    ui->tower1->initTower(ui->viewField1, ui->button1, ui->vLayout1)->initStartTower(plates)->selectFrom();
    ui->tower2->initTower(ui->viewField2, ui->button2, ui->vLayout2)->initTargetTower()->selectFrom();
    ui->tower3->initTower(ui->viewField3, ui->button3, ui->vLayout3)->initNeutralTower()->selectFrom();
}

void HanoiTowersWidget::stepStarted() {
    senderTower = static_cast<Tower*>(qobject_cast<QPushButton*>(QObject::sender())->parentWidget());

    if (!senderTower->isEmtpy()) {
        foreach (Tower* t, ui->towers->findChildren<Tower*>()) {
            if (t != senderTower) {
                t->selectable();
            } else {
                t->select();
            }
        }
        endGameEvents();
    }
}

void HanoiTowersWidget::endGameEvents() {
    disconnect(ui->tower1->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepStarted()));
    disconnect(ui->tower2->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepStarted()));
    disconnect(ui->tower3->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepStarted()));
    connect(ui->tower1->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
    connect(ui->tower2->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
    connect(ui->tower3->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
}

void HanoiTowersWidget::setButtonsToSelect() {
    foreach (Tower* t, ui->towers->findChildren<Tower*>()) {
        t->selectFrom();
    }
}

void HanoiTowersWidget::stepEnded() {
    Tower* reciever = qobject_cast<Tower*>(qobject_cast<QPushButton*>(QObject::sender())->parentWidget());

    if (reciever != senderTower && isValidMove(reciever)) {
        ui->stepCounter->display(ui->stepCounter->intValue()+1);

        makeMove(reciever);
        setButtonsToSelect();

        //check to win
        if (reciever->isTargetTower() && reciever->hasAll(gameSize)) {
            win();
        }
    }

    setButtonsToSelect();

    if (!won) {
        startGameEvents();
    } else {
        ui->tower1->getButton()->setText("");
        ui->tower2->getButton()->setText("");
        ui->tower3->getButton()->setText("");
        disconnect(ui->tower1->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
        disconnect(ui->tower2->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
        disconnect(ui->tower3->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
    }
}

bool HanoiTowersWidget::isValidMove(Tower* reciever) {
    return reciever->isEmtpy() || senderTower->getTopDisc()->getSize() < reciever->getTopDisc()->getSize();
}

void HanoiTowersWidget::win() {
    won = true;
    QMessageBox* won = new QMessageBox(this);
    won->setText("Nyertél! Lépések száma: "+QString::number(ui->stepCounter->intValue())+", lehetséges legjobb: "+QString::number(getBestWinNumber()));
    firstRun = true;
    won->exec();
}

void HanoiTowersWidget::makeMove(Tower* reciever) {
    int size = senderTower->getTopDisc()->getSize();
    reciever->addDisc(size);
    senderTower->removeDisc();
}

void HanoiTowersWidget::startGameEvents() {
    disconnect(ui->tower1->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
    disconnect(ui->tower2->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
    disconnect(ui->tower3->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepEnded()));
    connect(ui->tower1->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepStarted()));
    connect(ui->tower2->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepStarted()));
    connect(ui->tower3->getButton(), SIGNAL(clicked(bool)), this, SLOT(stepStarted()));
}
