#!bin/bash
if [ $# -eq 0 ]
then
echo "Kerem addj meg egy felhasznaloi nevet: "
read $nev
echo $nev
fi
if [ $# = 1 ]
then
nev=$1
van='cat /etc/passwd |cut  -fl -d ":" |grep $nev'
if [$van = 0]
then
echo "Nincs $nev nevu felhasznalo!"
fi
echo "A felhasznlo: $nev"
fi 
