#!/bin/sh
f=`cat input.txt`
for i in `seq 5`
do
    g=`echo $f|sed "s/[^$i]//g"`
    a=`echo $g|wc -m`
    b=`echo $g|wc -l`
    echo $i": "`expr $a - $b`
done