#include <QString>
#include <QtTest>
#include "gamemanager.h"

class AmobaGameManagerTest : public QObject
{
    Q_OBJECT

public:
    AmobaGameManagerTest() {};

private Q_SLOTS:
    void initTestCase();
    void testCase1_data();
    void testCase1();
    void falseTestCase();

    void newGame1TestCase();
    void newGame2TestCase();
    void newGame3TestCase();

    void makeMarkTestCase();
    void makeDrillTestCase();
    void makeQuadTestCase();

    void doubleNewGameTestCase();

    void testingNewGame(int size)
    {
        model->newGameRequest(size);
        QCOMPARE(model->getGameSize(), size);
        QCOMPARE(model->inGame, true);
        QCOMPARE(model->winner, GameManager::NoPlayer);
        QCOMPARE(model->getCurrentPlayer(), GameManager::playerO);
    }

private:
    GameManager* model;
};

void AmobaGameManagerTest::initTestCase()
{
    model = new GameManager();
}

void AmobaGameManagerTest::newGame1TestCase()
{
    testingNewGame(6);
}
void AmobaGameManagerTest::newGame2TestCase()
{
    testingNewGame(10);
}
void AmobaGameManagerTest::newGame3TestCase()
{
    testingNewGame(14);
}

void AmobaGameManagerTest::doubleNewGameTestCase()
{
    testingNewGame(6);
    model->fieldClickedRequest(1);
    model->fieldClickedRequest(1);
    model->fieldClickedRequest(2);
    model->fieldClickedRequest(6);
    model->fieldClickedRequest(4);
    testingNewGame(6);
    model->fieldClickedRequest(4);
    QCOMPARE(model->getField()[0][4], model->getPO());
    QCOMPARE(model->getField()[0][0], model->getNoP());
}

void AmobaGameManagerTest::makeDrillTestCase()
{
    testingNewGame(10);
    for(int i= 0;i < 80; i=i+10)
    {
        model->fieldClickedRequest(i);
        model->fieldClickedRequest(i+1);
    }

    QCOMPARE((model->getField()[0][0] == model->getNoP() || model->getField()[1][0] == model->getNoP() || model->getField()[2][0] == model->getNoP()), true);
}


void AmobaGameManagerTest::makeQuadTestCase()
{
    testingNewGame(10);
    for(int i= 0;i < 90; i=i+10)
    {
        model->fieldClickedRequest(i);
        model->fieldClickedRequest(i+1);
    }

    QCOMPARE((model->getField()[0][0] == model->getNoP() || model->getField()[1][0] == model->getNoP() ||
            model->getField()[2][0] == model->getNoP() || model->getField()[3][0] == model->getNoP()), true);
}

void AmobaGameManagerTest::makeMarkTestCase()
{
    testingNewGame(14);
    model->fieldClickedRequest(2);
    QCOMPARE(model->getField()[0][1], model->getNoP());
}

void AmobaGameManagerTest::testCase1_data()
{
    QTest::addColumn<QString>("data");
    QTest::newRow("0") << QString();
}

void AmobaGameManagerTest::testCase1()
{
    QFETCH(QString, data);
    QVERIFY2(true, "Failure");
}

void AmobaGameManagerTest::falseTestCase()
{
    QCOMPARE(0, 1);
}

QTEST_APPLESS_MAIN(AmobaGameManagerTest)

#include "amobagamemanagertest.moc"
