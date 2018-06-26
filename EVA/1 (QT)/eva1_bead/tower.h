#ifndef TOWER_H
#define TOWER_H

#include <QWidget>
#include <QPushButton>
#include <QFrame>
#include <QVBoxLayout>
#include <QVector>
#include "disc.h"

class Tower : public QWidget
{
    Q_OBJECT

public:
    Tower(QWidget *parent) : QWidget(qobject_cast<QWidget*>(parent)) {}

    Tower* initTower(QFrame* vB, QPushButton *b, QVBoxLayout *l) {
        setViewBox(vB);
        setButton(b);
        setViewLayout(l);
        return this;
    }

    Tower* initStartTower(int plates) {
        clear();
        for(int i=0; i<plates; i++) {
            discs.append(new Disc(i));
            viewLayout->addWidget(discs[i]);
        }
        targetTower = false;
        return this;
    }

    Tower* initTargetTower() {
        clear();
        targetTower = true;
        discs.resize(0);
        return this;
    }

    Tower* initNeutralTower() {
        clear();
        targetTower = true; //false
        discs.resize(0);
        return this;
    }

    QPushButton* getButton()
    {
        return button;
    }

    QVBoxLayout* getLayout()
    {
        return viewLayout;
    }

    Disc* getTopDisc() {
        return discs.first();
    }

    bool isTargetTower() {
        return targetTower;
    }


    Tower* removeDisc() { // remove top disc
        delete discs.first(); //remove disc
        discs.erase(discs.begin()); //size must be changed
        return this;
    }

    Tower* addDisc(int discSize) { //to the top
        discs.prepend(new Disc(discSize));
        viewLayout->insertWidget(0, discs.first());
        return this;
    }

    bool hasAll(int plateNum) {
        return discs.count() == plateNum;
    }

    bool isEmtpy() {
        return discs.count() == 0;
    }

    void select() {
        setStyleSheet("background-color:#8ae234;");
        button->setText("Mozgatás erről a toronyról");
    }

    void selectFrom() {
        setStyleSheet("background-color:none;");
        if (!isEmtpy()) {
           button->setText("Levesz");
        } else {
           button->setText("");
        }
    }

     void selectable(bool withText = true) {
         setStyleSheet("background-color:none;");
         if (withText) {
            button->setText("Rárak");
         }
     }

private:
    QVector<Disc*> discs;
    QFrame *parent, *viewBox;
    QVBoxLayout *viewLayout;
    QPushButton *button;

    bool targetTower;

    Tower* setViewBox(QFrame* vB) {
        viewBox = vB;
        return this;
    }

    Tower* setButton(QPushButton *b) {
        button = b;
        button->setText("");
        return this;
    }

    Tower* setViewLayout(QVBoxLayout *l) {
        viewLayout = l;
        //viewLayout->setAlignment(Qt::AlignHCenter);
        //viewLayout->setAlignment(Qt::AlignBottom);
        return this;
    }

    Tower* clear() {
        foreach (Disc *disc, discs) {
            viewLayout->removeWidget(disc);
            delete disc;
        }
        discs.clear();
        return this;
    }
};
#endif // TOWER_H
