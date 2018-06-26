#include "amobawidget.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    AmobaWidget w;
    w.show();

    return a.exec();
}
