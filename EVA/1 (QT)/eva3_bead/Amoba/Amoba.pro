#-------------------------------------------------
#
# Project created by QtCreator 2017-05-14T08:54:20
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = Amoba
TEMPLATE = app


SOURCES += main.cpp\
        amobawidget.cpp \
    gamemanager.cpp \
    persistance.cpp

HEADERS  += amobawidget.h \
    gamemanager.h \
    persistance.h

FORMS    += amobawidget.ui

RESOURCES +=
