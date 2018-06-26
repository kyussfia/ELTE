#-------------------------------------------------
#
# Project created by QtCreator 2017-05-21T21:32:53
#
#-------------------------------------------------

QT       += widgets testlib

QT       -= gui

TARGET = amobagamemanagertest
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += amobagamemanagertest.cpp \
    gamemanager.cpp
DEFINES += SRCDIR=\\\"$$PWD/\\\"

HEADERS += \
    mockpersistance.h \
    gamemanager.h
