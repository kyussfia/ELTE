#ifndef DISC_H
#define DISC_H

#include <QPushButton>

class Disc : public QPushButton
{
    Q_OBJECT
public:
    /*
     * size(0) = 50
     * size(1) = 80
     * size(2) = 110
     * size(3) = 140
     * size(4) = 170
     * size(5) = 200
     * size(6) = 230
     * size(7) = 260
     */

    //Disc(QFrame *parent) : QPushButton(qobject_cast<QWidget*>(parent)) {
    Disc(int s) : QPushButton() {
        size = s;
        setCheckable(false);
        setFixedSize(getDisplaySize(size),22);
        //setText(QString::number(getDisplaySize(size)));
        setStyleSheet("position:relative; margin-right: 0px;margin-left:" + QString::number(getMargin(size)) + ";");
        //setStyleSheet("margin-left:" + QString::number(getMargin(size)) + "px;"
                      //"background-color: red;color: #0000dd;");
    }

    int getSize() {
        return size;
    }

private:
    int size;

    int getDisplaySize(int size) {
        switch (size) {
        case 0:
            return 50;
            break;
        case 1:
            return 80;
            break;
        case 2:
            return 110;
            break;
        case 3:
            return 140;
            break;
        case 4:
            return 170;
            break;
        case 5:
            return 200;
            break;
        case 6:
            return 230;
            break;
        case 7:
            return 260;
            break;
        }
        return 0;
    }

    int getMargin(int size) {
        switch (size) {
        case 0:
            return 0;
            break;
        case 1:
            return 0;
            break;
        case 2:
            return 0;
            break;
        case 3:
            return 0;
            break;
        case 4:
            return 0;
            break;
        case 5:
            return 0;
            break;
        case 6:
            return 0;
            break;
        case 7:
            return 0;
            break;
        }
        return 0;
    }
};

#endif // DISC_H
