#include "amobawidget.h"
#include "ui_amobawidget.h"

AmobaWidget::AmobaWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::AmobaWidget)
{
    this->gm = new GameManager();
    setupUi();
}

void AmobaWidget::setupUi()
{
    ui->setupUi(this);
    this->setFixedSize(1089,689);
    this->setWindowTitle("Kiszúrós Amőba");

    connect (ui->newgameButton, SIGNAL(clicked(bool)), this, SLOT(newGameClicked()));
    connect(ui->exitButton, SIGNAL(clicked(bool)), QApplication::instance(), SLOT(quit()));

    if (gm->saveAvailable())
    {
        ui->loadButton->setText("Játék betöltése");
        connect(ui->loadButton, SIGNAL(clicked(bool)), gm, SLOT(loadGameRequest()));
        connect(gm, SIGNAL(gameLoadedResponse()), this, SLOT(gameLoaded()));

    }connect(gm, SIGNAL(gameSavedResponse()), this, SLOT(gameSaved()));
}

AmobaWidget::~AmobaWidget()
{
    delete ui;
    delete gm;
    delete sure;
    delete newGameSignalMapper;

    delete fieldClickedSignalMapper;
    delete winBox;
    delete tiedBox;
    delete saveBox;
}

void AmobaWidget::newGameClicked()
{
    //disconnect(gm, SIGNAL(newGameResponse()), this, SLOT(newGame()));
    int gS = getCheckedFieldSize();
    bool skip = false;

    if (gm->inGame)
    {
        switch (areYouSure()) {
            case QMessageBox::Yes:
                sure->close();
                delete newGameSignalMapper;
                delete fieldClickedSignalMapper;
                newGameSignalMapper = new QSignalMapper(this);
                fieldClickedSignalMapper = new QSignalMapper(this);
                break;
            case QMessageBox::Cancel:
                skip = true;
                break;
        }
    }

    if (!skip)
    {
        newGameSignalMapper = new QSignalMapper(this);
        fieldClickedSignalMapper = new QSignalMapper(this);
        connect(gm, SIGNAL(newGameResponse()), this, SLOT(newGame()));
        connect(this, SIGNAL(newGameRequest()), newGameSignalMapper, SLOT(map()));
        newGameSignalMapper->removeMappings(this);
        newGameSignalMapper->setMapping(this, gS);
        connect(newGameSignalMapper, SIGNAL(mapped(int)), gm, SLOT(newGameRequest(int)));
        emit newGameRequest();
    }
}

int AmobaWidget::areYouSure()
{
    sure = new QMessageBox(this);
    sure->setText("Biztos hogy új játékot szeretnél kezdeni?");
    sure->setStandardButtons(QMessageBox::Yes | QMessageBox::Cancel);
    sure->setDefaultButton(QMessageBox::Yes);
    return sure->exec();
}


int AmobaWidget::getCheckedFieldSize()
{
    if(ui->skillEasy->isChecked()) {
        return 6;
    } else {
        if (ui->skillMid->isChecked()) {
            return 10;
        } else { //skillHard
            return 14;
        }
    }
}

void AmobaWidget::newGame()
{
    disconnect(ui->saveButton, SIGNAL(clicked(bool)), gm, SLOT(saveGameRequest()));
    disconnect(gm, SIGNAL(gameSavedResponse()), this, SLOT(gameSaved()));
    foreach (QPushButton *r, field) {
        ui->maze->removeWidget(r);
        delete r;
    }
    ui->saveButton->setText("Játék Mentése");
    connect(ui->saveButton, SIGNAL(clicked(bool)), gm, SLOT(saveGameRequest()));
    connect(gm, SIGNAL(gameSavedResponse()), this, SLOT(gameSaved()));

    field.clear();

    drawUpField();

    setUpEvents();

    displayCurrentPlayer();
}

void AmobaWidget::gameSaved()
{
    disconnect(ui->loadButton, SIGNAL(clicked(bool)), gm, SLOT(loadGameRequest()));
    disconnect(gm, SIGNAL(gameLoadedResponse()), this, SLOT(gameLoaded()));

    ui->loadButton->setText("Játék betöltése");
    connect(ui->loadButton, SIGNAL(clicked(bool)), gm, SLOT(loadGameRequest()));
    connect(gm, SIGNAL(gameLoadedResponse()), this, SLOT(gameLoaded()));

    saveBox = new QMessageBox(this);
    saveBox->setText("A játék metésre került.");
    saveBox->exec();
}

void AmobaWidget::gameLoaded()
{
    delete fieldClickedSignalMapper;
    fieldClickedSignalMapper = new QSignalMapper(this);
    disconnect(gm, SIGNAL(gameSavedResponse()), this, SLOT(gameSaved()));
    disconnect(ui->saveButton, SIGNAL(clicked(bool)), gm, SLOT(saveGameRequest()));
    foreach (QPushButton *r, field) {
        ui->maze->removeWidget(r);
        delete r;
    }

    ui->saveButton->setText("Játék Mentése");
    connect(ui->saveButton, SIGNAL(clicked(bool)), gm, SLOT(saveGameRequest()));
    connect(gm, SIGNAL(gameSavedResponse()), this, SLOT(gameSaved()));

    field.clear();

    drawUpField();

    setUpEvents();

    displayCurrentPlayer();

    saveBox = new QMessageBox(this);
    saveBox->setText("A játék betöltésre került.");
    saveBox->exec();
}

void AmobaWidget::setUpEvents()
{
    disconnect(fieldClickedSignalMapper, SIGNAL(mapped(int)), gm, SLOT(fieldClickedRequest(int)));
    disconnect(this, SIGNAL(fieldClickedRequest()), fieldClickedSignalMapper, SLOT(map()));
    disconnect(gm, SIGNAL(fieldClickedResponse()), this, SLOT(endRound()));
    foreach (QPushButton* b, field) {
         disconnect(b, SIGNAL(clicked(bool)), this, SLOT(fieldClicked()));
         connect(b, SIGNAL(clicked(bool)), this, SLOT(fieldClicked()));
    }
    connect(fieldClickedSignalMapper, SIGNAL(mapped(int)), gm, SLOT(fieldClickedRequest(int)));
    connect(this, SIGNAL(fieldClickedRequest()), fieldClickedSignalMapper, SLOT(map()));
    connect(gm, SIGNAL(fieldClickedResponse()), this, SLOT(endRound()));
}

void AmobaWidget::endRound()
{
    //refresh buttons
    refreshField();

    //player plabel csere
    displayCurrentPlayer();

    if (!gm->inGame)
    {
        foreach (QPushButton* b, field) {
            disconnect(b, SIGNAL(clicked(bool)), this, SLOT(fieldClicked()));
        }
        disconnect(fieldClickedSignalMapper, SIGNAL(mapped(int)), gm, SLOT(fieldClickedRequest(int)));
        disconnect(ui->saveButton, SIGNAL(clicked(bool)), gm, SLOT(saveGameRequest()));
        ui->saveButton->setText("");
        if (gm->winner != gm->getNoP())
        {
            win();
        } else {
            tied();
        }
    }
}

void AmobaWidget::tied()
{
    tiedBox = new QMessageBox(this);
    tiedBox->setText("A játéknak vége! Döntetlen!");
    int ret = tiedBox->exec();
    if (ret)
    {
        emit ui->newgameButton->clicked();
    }
}

void AmobaWidget::win()
{
    winBox = new QMessageBox(this);
    winBox->setText("A játéknak vége! A győztes: <br/>"+getPlayerQString(gm->winner));
    int ret = winBox->exec();
    if (ret)
    {
        emit ui->newgameButton->clicked();
    }
}

void AmobaWidget::markField(QPushButton* b)
{
    b->setText(getCurrentPlayerMarkQString());
    b->setStyleSheet("font-size: 50px;font-weight: bold; color:"+ getCurrentPlayerColorQString() + ";");
    b->setDisabled(true);
}

void AmobaWidget::fieldClicked()
{
    //disable pressed button and setsign of player
    QPushButton* sender = qobject_cast<QPushButton*>(QObject::sender());
    int index = ui->maze->indexOf(sender);

    handleButton(sender);
    disconnect(sender, SIGNAL(clicked(bool)), this, SLOT(fieldClicked()));


    //markField(pressed);
    fieldClickedSignalMapper->removeMappings(qobject_cast<QPushButton*>(QObject::sender()));
    fieldClickedSignalMapper->setMapping(this, index);
    emit fieldClickedRequest();
}


QString AmobaWidget::getCurrentPlayerMarkQString()
{
    QString str;

    if (gm->getCurrentPlayer() == gm->getPX())
    {
        str = "X";
    }
    else if (gm->getCurrentPlayer() == gm->getPO())
    {
        str = "O";
    }
    else {
        str = "";
    }
    return str;
}

QString AmobaWidget::getCurrentPlayerColorQString()
{
    QString str;
    if (gm->getCurrentPlayer() == gm->getPX())
    {
        str = "blue";
    }
    else if (gm->getCurrentPlayer() == gm->getPO())
    {
        str = "red";
    }
    else {
        str = "";
    }
    return str;
}

void AmobaWidget::displayCurrentPlayer()
{
    ui->currentPlayerLabel->setText(getCurrentPlayerQString());
}

QString AmobaWidget::getCurrentPlayerQString()
{
    return getPlayerQString(gm->getCurrentPlayer());
}

QString AmobaWidget::getPlayerQString(GameManager::players player)
{
    QString str;
    if (player == gm->getPX())
    {
        str = "<span style='color:blue'>2. Játékos</span>";
    }
    else if (player == gm->getPO())
    {
        str = "<span style='color:red'>1. Játékos</span>";
    }
    else {
        str = "";
    }
    return str;
}

void AmobaWidget::drawUpField()
{
    QVector< QVector<GameManager::players> > f = gm->getField();
    for(int i = 0; i<f.size(); i++)
    {
        for(int j = 0; j<f[i].size(); j++)
        {
            //qDebug() << f[i][j];
            QPushButton* button = new QPushButton(ui->gameplace);
            setButtonSize(button);

            if (f[i][j] == gm->getPO())
            {
                setPlayerOOnButton(button);
            }
            else if (f[i][j] == gm->getPX())
            {
                setPlayerXOnButton(button);
            }
            else
            {
                setPlayerNoOnButton(button);
            }

            field.append(button);
            ui->maze->addWidget(button, i, j);
        }
    }
}

void AmobaWidget::refreshField()
{
    QVector< QVector<GameManager::players> > f = gm->getField();
    for(int i = 0; i<f.size(); i++)
    {
        for(int j = 0; j<f[i].size(); j++)
        {
            QPushButton* button = field[f[i].size() * i + j];
            if (f[i][j] == gm->getPO())
            {
                setPlayerOOnButton(button);
            }
            else if (f[i][j] == gm->getPX())
            {
                setPlayerXOnButton(button);
            }
            else
            {
                connect(button, SIGNAL(clicked(bool)), this, SLOT(fieldClicked()));
                setPlayerNoOnButton(button);
            }
        }
    }
}

void AmobaWidget::handleButton(QPushButton* button)
{
    if (gm->getCurrentPlayer() == gm->getPO())
    {
        setPlayerOOnButton(button);
    }
    else if (gm->getCurrentPlayer() == gm->getPX())
    {
        setPlayerXOnButton(button);
    }
    else
    {
        setPlayerNoOnButton(button);
    }
}

void AmobaWidget::setButtonSize(QPushButton* b)
{
    int size = getButtonSizeByGameSize();
    b->setFixedSize(size, size);
}

int AmobaWidget::getButtonSizeByGameSize()
{
    int r;
    switch (gm->getGameSize()) {
    case 6:
        r = 95;
        break;
    case 10:
        r = 57;
        break;
    case 14:
        r = 45;
        break;
    }
    return r;
}

void AmobaWidget::setPlayerOOnButton(QPushButton* b)
{
    setPlayerOnButton(gm->getPO(), b);
}

void AmobaWidget::setPlayerXOnButton(QPushButton* b)
{
    setPlayerOnButton(gm->getPX(), b);
}

void AmobaWidget::setPlayerNoOnButton(QPushButton* b)
{
    setPlayerOnButton(gm->getNoP(), b);
}

void AmobaWidget::setPlayerOnButton(GameManager::players p, QPushButton* b)
{
    if (p == gm->getPO())
    {
        b->setText("O");
    }
    else if (p == gm->getPX())
    {
        b->setText("X");
    }
    else
    {
        b->setText("");
    }
}
