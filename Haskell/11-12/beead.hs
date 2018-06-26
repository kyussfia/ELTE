{-
Készített: Mikus Márk
NK-kód: CM6TSV

Funkcionális programozás Beadandó feladat.
-}

module Beead where

import Prelude
import Data.List	-- showTime miatt

{-
 Adjuk meg azt a függvényt, amely egy listában megadott számok közül fordított sorrendben visszaadja az előállítandó számnál kisebb vagy egyenlő számokat! Feltehetjük, hogy az elemeket monoton növekvő sorrendben adtuk meg.
-}
elemsForZ n xs
 |n<=0 || xs==[] = []
 |otherwise = reverse $ takeWhile (<=n) xs
 {-
 Adjuk meg azt a függvényt, amely egy listának visszaadja azon elemeit, amelyek kisebbek vagy egyenlőek a megadott számnál! A lista elemei monoton csökkenő sorozatot alkotnak.
-}
remainingElems :: Integral a => a -> [a] -> [a]
 
remainingElems n xs
 |n<=0 || xs==[] = []
 |otherwise = dropWhile (>n) xs
 
 {-
 Adjuk meg azt a függvényt, amely egy adott számot előállít egy listában adott számok összegeként! A listából nem minden számot kell feltétlenül felhasználni, de a nagyobb számokat előnyben kell részesíteni a kisebbekkel szemben.

Feltételezhetjük, hogy a lista elemeivel mindig felbontható a paraméterként kapott szám. Feltételezhetjük továbbá, hogy a felbontásban a nagyobb számokat előnyben részesítve mindig megkapható visszalépés nélkül a felbontás.
-}
z :: Integral a => [a] -> a -> [a]
z [] _ = []
z xs n =reverse $ accZ (elemsForZ n xs) n [] where
  accZ [] m ls = ls
  accZ (y:ys) m ls
   |y + sum ls <= m = accZ ys m (y:ls)
   |otherwise = accZ ys m ls
   
{-
 Adjuk meg a Fibonacci-sorozatot előállító függvényt!
-} 
fibs :: Integral a => [a]
fibs = map fst $ iterate (\(a,b) -> (b,a+b)) (1,1)

{-
Adjuk meg a z függvénynek azon változatát, amely Fibonacci-számok segítségével állítja elő a szám felbontását!
-}
zFromFibs :: Integral a => a -> [a]
zFromFibs n = z (fibs) n


{-Tesztelés kettő hatványaival

Állítsuk elő kettő hatványait egy listában!
-}

powersOf2 :: Integral a => [a]
powersOf2 = iterate (*2) 1

{-
Adjuk meg a z függvény azon változatát, amely a kettő hatványainak felhasználásával bontja fel a számot!
-}
zFromPowersOf2 :: Integral a => a -> [a]
zFromPowersOf2 n = z (powersOf2) n
{-
Adjuk meg azt a függvényt, amely előállít egy listát, amely tíz minden hatványát kilencszer tartalmazza!
-}
powersFor10 :: Integral a => [a]
powersFor10 = [10^i | i<-[0..], j<-[1..9], j<=9]

{-
CSAK 5LET!
powersFor10 = f 1 [1..]   where
 f k (x:xs) 
  |x `mod` 9==0 = 10^k:(f xs (k+1))
  |otherwise = x:(f xs (k+1)) 
-} 

{-
Adjuk meg a z függvény azon változatát, amely az előbb megadott függvény segítségével a számot tíz hatványainak összegévé bontja fel!
-}
zFromPowersFor10 :: Integral a => a -> [a]
zFromPowersFor10 n = z (powersFor10) n 
{-
Tesztelés római számokkal
Adott egy konstans, amely tartalmazza a római számjegyek arab szám szerinti megfelelőit. Vegyük észre, hogy ezek segítségével bármilyen 4331-nél kisebb szám felbontható!
roman :: Integral a => [a]
roman = [1,1,1,4,5,9,10,10,10,40,50,90,100,100,100,400,500,900,1000,1000]
Adjuk meg azt a függvényt, amely a fenti lista alapján egy arab számokhoz megadja annak római megfelelőjét, amennyiben ez létezik (benne van a listában)!
A római számjegyekhez a neki megfelelő betűket használjuk: I, V, X, L, C, D, M.
A függvény megadásakor tehát a következő arab számokhoz kell megadni a római megfelelőjét: 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000. Ügyeljünk arra, hogy a függvény parciális, vagyis a többi szám esetén nem ad eredményt! Ezt az error függvény használatával tudjuk jelezni.
-}
roman :: Integral a => [a]
roman = [1,1,1,4,5,9,10,10,10,40,50,90,100,100,100,400,500,900,1000,1000]

toRoman :: Integral a => a -> String
toRoman n
 |n==1 = "I"
 |n==4 = "IV"
 |n==5 = "V"
 |n==9 = "IX"
 |n==10 = "X"
 |n==40 = "XL"
 |n==50 = "L"
 |n==90 = "XC"
 |n==100 = "C"
 |n==400 = "CD"
 |n==500 = "D"
 |n==900 = "CM"
 |n==1000 ="M"
 |otherwise = error ("toRoman: No roman numeral is assigned to the given number." ++ show n)
{-
Adjuk meg azt a függvényt, amely a z függvény segítségével egy arab számból előállítja annak római számjegyekre történő felbontását!
-}
zToRoman :: Integral a => a -> String
zToRoman n =concat $ map toRoman $ z roman n
{-
Adjuk meg azt a függvényt, amelyik paraméterül egy számokat tartalmazó listát vár, majd a következőket csinálja:
Előállítja a lista összes kezdőszeletének a produktumát egy listában.
Az eredeti lista segítségével az előállított (produktumokat tartalmazó) lista elemeit duplikálja.
Például:
A bemeneti lista: [2,3,4,5].
A kezdőszeleteinek produktuma ennek megfelelően: [1,2,6,24,120].
Azaz, páronként nézve: az 1-ből 2 darab, a 2-ből 3 darab, a 6-ból 4 darab, 24-ből pedig 5 darab kell.
Tehát a műveletek végeredménye:
[1, 1, 2, 2, 2, 6, 6, 6, 6, 24, 24, 24, 24, 24]
Lássuk a definíciót! (Ha esetleg Int és Integer típusú értékek közti konverzióra lenne közben szükségünk, nyugodtan használhatjuk a fromIntegral függvényt.)
-}
makeSystem :: Integral a => [a] -> [a]
makeSystem as = f as (map product $ inits as)  where
 f [] _ = []
 f (x:xs) (y:ys) = concat(replicate (fromIntegral x) [y]) ++ (f xs ys)

{-
Továbbá készítsünk egy olyan függvényt, amellyel egész számok tízes számrendszerbeli, szöveges reprezentációját tudjuk előállítani!
-}
showInteger :: Integral a => a -> String
showInteger n = show $ fromIntegral n
{-
Az alábbi konstans felhasználásával egy másodpercekben megadott időt tudunk hét (w), nap (d), óra (h), perc (m) és másodperc (s) alakban megadni.

time :: Integral a => [a]
time = makeSystem [60,60,24,7,52]
Adjuk meg azt a függvényt, amely a time konstans segítségével felbontja a másodpercekben megadott időt és ezt szöveges formában visszaadja!
-}
time :: Integral a => [a]
time = makeSystem [60,60,24,7,52]
times :: Integral a => [a]
times = makeSystem [60,60,24,7,52,52]
timeszzz :: Integral a => [a]
timeszzz = makeSystem [60,60,24,7,52,2000]-- Bizarr noveléssel (pl.:2000) jelentősen nő a kiértékelési idő.Óriási szám esetén.


showTime :: Integral a => a -> String


showTime  n = ch y "y" ++ ch w "w" ++ ch d "d" ++ ch h "h" ++ ch m "m" ++ ch s "s" where
 ch k l = if k > 0 then (showInteger k) ++ l else ""
 count x = length $ filter (== x) (z time n)	--ITT van az egyetlen módosítandó, mivel itt kell megadni, 
 y = count 31449600										--az "időkészletünk" forrását (z time n)	
 w = count 604800									
 d = count 86400
 h = count 3600
 m = count 60
 s = count 1
{-
Megj.: 	A showTime függvényt kibővítettem,úgy éveket is képes mutatni,a times konstans fv segítségével.
	Tetszőleges máretűre állíthatjuk így már a "naptárunkat", ehhze csupán egy ujabb konstans kell ezuttal több évet engedünk meg. pl. timeszzz
-}

{-
Készítette: Mikus Márk
Neptun-kód: CM6TSV
e-mail: kyussfia@gmail.com
Gy.V: Diviánszky Péter /csop:1.
-}
