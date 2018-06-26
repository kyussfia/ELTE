module First where

import Prelude

--f :: Integer -> [Integer]
f n = n*([0..n] ++ tail[0..n])

--g:: Integer->Integer->Integer
--g a b = (a*b)+1

{-feladat: hegy.

mountain n = [1..n] ++ reverse[1..n-1]

-}
{-
Definiáljunk egy f nevű függvényt, ami egy n nemnegatív egész számhoz
rendeli hozzá a követekző egész számokat tartalmazó listát:

0,1,2,...,n-1,n, 1,2,...,n-1,n, ..., n-1,n, n

tesztesetek:

f 0 == [0]
f 1 == [0,1,1]
f 2 == [0,1,2,1,2,2]
f 3 == [0,1,2,3,1,2,3,2,3,3]

A megoldásban a teljes függvénydefiníciót adjuk be (ami úgy kezdődik, hogy "f").

-}