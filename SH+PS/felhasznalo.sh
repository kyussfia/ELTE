#!bin/bash
if [ $# -eq 0 ]
then
echo "Kerem addj meg egy felhasznaloi nevet: "
read nev
echo $nev
fi
if [ $# = 1 ]
then
nev=$1
fi

van='who |grep $nev'

echo "Nincs $nev nevu felhasznalo!"

