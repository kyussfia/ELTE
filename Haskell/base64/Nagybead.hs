{-
Készítette: Mikus Márk
Neptun-kód: CM6TSV
Email: kyussfia@gmail.com
Gyakorlat-vezető: Bozó István
Tárgy: Base64 - Funkcionális programozás Nagybeadandó
Dátum: 2014.04.28.
-}
module Nagybead where

import Prelude 
import Data.List.Split.Internals (splitEvery) {- a chunksOf-hoz -}
import Data.Char  {- az ordhoz és a chr hez -}
{-
A Base64 egy 64 karakterből álló ábécén alapuló kódolási forma, amely segítségével tetszőleges adatot tudunk szöveges formában tárolni.

A feladatban egy egyszerűsített, tetszőleges (Unicode) karakterekből álló szöveget fogunk ábrázolni 64 előre megadott karakter segítségével. Természetesen a végén megadjuk az eredeti formára való visszaalakításért felelős műveletet is.

A kódoláshoz egy szótárat vezetünk be. A szótár egy rendezett párokból, mint kulcs-érték párokból, álló lista.

A type kulcsszó segítségével az Entry típus az (Int, Char) típus egy másik neve lesz, illetve a Dictionary típus az Entry típusból alkotott listákat jelenteni (ez lesz a szótárunk típusa), hasonlóan a String és [Char] viszonyához. A programban ez semmilyen további megszorítást nem indukál, csupán a beszédesebb függvénytípusok kialakításában segít
-}
type Entry = (Int, Char)
type Dictionary = [Entry]
{-
Mint azt már a bevezetőben említettük, a szótár 64 bejegyzést fog tartalmazni, amelyek az alábbiak:

az angol ábécé nagybetűi (A-Z);
az angol ábécé kisbetűi (a-z);
számjegyek (0-9);
további két speciális karakter: +, /.
Adjuk meg azt a függvényt, amely a fentebb megadott karakterek segítségével előállítja a szótárat!
-}
dictionary :: Dictionary
dictionary = [(i,chars !! i)|i<-nums] where
 nums = [0..63]
 chars = ['A'..'Z'] ++ ['a'..'z'] ++ ['0'..'9'] ++ ['+','/']
{-
Adjuk meg azt a magasabbrendű függvényt, amely segítségével egy sorozatot ki tudunk egészíteni megadott hosszúságúra, amikor arra szükség van!

* A függvény első paramétere egy olyan függvény, amely segítségével meg tudjuk adni, milyen módon (jobbról, balról, stb.) szeretnék a listát kiegészíteni. A függvény első paramétere a kiegészítés, a második pedig az kiegészítendő sorozat.

* A második paraméter az az elem, amellyel a kiegészítést végezzük.

* A harmadik paraméter egy szám, amely azt a méretet adja meg, amire ki akarjuk a listát egészíteni. Természetesen előfordulhat az is, hogy az eredeti sorozat hosszabb, mint a megadott szám, ekkor az eredeti szöveget változatlan formában kell visszaadni.

* A negyedik pedig a lista, amelyet kiegészíteni szeretnénk.
-}
pad :: ([a] -> [a] -> [a]) -> a -> Int -> [a] -> [a]
pad f e l xs
 |length xs >= l = xs 
 |otherwise = f (take (l - (length xs)) (repeat e)) xs

padLeft  = pad (\p -> (p ++))
padRight = pad (\p -> (++ p))
{-
A fenti függvény segítségével megadjuk a jobbról és balról kiegészítő függvényeket, amelyekre a későbbiekben egyébként szükségünk is lesz.

A könnyebb olvashatóság kedvéért bevezetünk egy szinomimát a típusra, amely ezzel a BitString-et definiálja mint számokból álló listát.
-}
type BitString = [Int]
{-
Adjuk meg azt a függvényt, amely bitek (0 és 1 értékek) sorozatává alakít egy tetszőleges nemnegatív egész számot!
-}
toBitString :: Int -> BitString
toBitString = reverse . toBin where
 toBin n
  |n == 0 = []
  |even n = 0: toBin (n `div` 2)
  |otherwise = 1 : toBin ((n-1) `div` 2)
  
{-
Adjuk meg azt a függvényt, amely bitek sorozatából előállja annak tízes számrendszerbeli megfelelőjét!
-}
fromBitString :: BitString -> Int
fromBitString [] = 0
fromBitString l@(x:xs)
 |x == 1 = 2 ^ (length l -1) + fromBitString xs
 |otherwise = fromBitString xs

 {-
 Definiáljuk azt a függvényt, amely egy szöveget bájtok sorozatává alakít!

Megjegyzés. A szöveg elemeit előbb a megfelelő karakter kóddá kell alakítani (ord), majd azokból bájtokat készíteni. Ne feledjük, hogy 8 bit az 1 bájt! Azaz, ha ennél rövidebb bitsorozatot kapunk, akkor azt egyenként ki kell egészíteni megfelelő hosszúságúra!
 -}
toBinary :: String -> BitString
toBinary = concatMap (padLeft 0 8 . toBitString . ord)
{-
Adjuk meg azt a függvényt, amely egy listát a kért hosszúságú darabokra szabdal!

Megjegyzés. Ha a megadott méret nem pozitív szám, akkor az error függvény segítségével adjunk erről hibajelzést!
-}
chunksOf :: Int -> [a] -> [[a]]
chunksOf n l
 |n <= 0 = error "chunksOf: Invalid chunk length"
 |otherwise = splitEvery n l
{-
Egy másik lehetséges definíció:
Megjegyzés:
	splitAt felhasználásával: (Ha a splitAt nincs benne a Prelude -ben akkor az import Data.List utasítással már elérhető lesz!)
	
chunksOf n xs
 |n <= 0 = error "chunksOf: Invalid chunk length"
 |length xs == 0 = []
 |otherwise = [y1] ++ chunksOf n y2 where
   (y1,y2) = splitAt n xs
-}
{-Definiáljuk azt a függvényt, amelyik megadja az adott tulajdonságot teljesítő elemek közül az elsőt!

Megjegyzés. Ha a listában nincs a feltételnek megfelelő elem, akkor az error függvény segítségével adjunk erről hibajelzést!-}
findFirst :: (a -> Bool) -> [a] -> a
findFirst p = ans . filter p where
 ans ys 
  |length ys > 0 = head ys
  |otherwise = error "findFirst: No such entry"
{-
Egy rekurzív megoldás:
 findFirst p (x:xs)
 |p x = x
 |otherwise = findFirst p xs
findFirst p [] = error "findFirst: No such entry"
-}
{-
Adjuk meg azt a függvényt, amely megadja a számnak megfelelő karaktert a szótárból!

Megjegyzés. Ne feledjük, hogy a szótárban nem BitString értékeket társítottunk a karakterekhez, a BitString-et előbb át kell alakítani tízes számrendszerbeli számmá.
-}
findChar :: Dictionary -> BitString -> Char
findChar d xs = snd $ findFirst ((== fromBitString xs) . fst) d
{-
Adjuk meg azt a függvényt, amely egy tetszőleges szöveget átalakít Base64 formátumú ábrázolásra!

Megjegyzés. Az átalakító algoritmus részletes ismertetése lentebb található.
algoritmus lépései:

Első lépés: karakterkódok meghatározása.
Második lépés: karakterkódok bájtokká alakítása.
Harmadik lépés: bájtok összefűzése bitsorozattá.
Negyedik lépés (fontos!): Ha a bitsorozat hossza nem osztható 6-tal, akkor fel kell tölteni a sorozatot jobbról 0 értékekkel (ez az eset akkor fordulhat elő, ha az eredeti szöveg elemeinek száma nem osztható 3-mal). Majd ezt követően daraboljuk fel a bitsorozatot 6 hosszúságú bitsorozatokká.
Ötödik lépés: A szótár dictionary felhasználásával a bitsorozatokhoz tartozó karakterek meghatározása.
Hatodik lépés: A szöveg előállítása.
-}
translate :: Dictionary -> String -> String
translate d = map (findChar d) . chunksOf 6 . moder . toBinary where
 moder xs
  |length xs `mod` 6 /= 0 = moder $ padRight 0 (length xs + 1) xs
  |otherwise = xs
{-
Adjuk meg azt a függvényt, amely az előző függvény (translate) felhasználásával szabványos Base64 formátumú szöveget állít elő!

Az átfordítás eredménye akkor tekinthető szabványosnak, ha az elemszáma 4-gyel osztható. Ha a translate függvény eredménye nem ilyen, akkor a szöveget ki kell bővíteni jobbról '=' karakterekkel, hogy ez teljesüljön.
-}
encode :: Dictionary -> String -> String
encode d = rule . translate d where
  rule xs
   |length xs `mod` 4 /= 0 = rule $ padRight '=' (length xs + 1) xs
   |otherwise = xs
{-
Adjuk meg azt a függvényt, amely megadja az adott karakterhez tartozó szótárbeli indexet bináris formában!
-}
findCode :: Dictionary -> Char -> BitString
findCode d c = toBitString $ fst $ findFirst ((== c) . snd) d
{-
Definiáljuk azt a függvényt, amely a Base64 formátumban megadott szöveget visszaalakítja hagyományos Unicode (UTF-8) kódolásra!

Megjegyzés. Ne feledkezzünk meg a visszafejtés előtt arról, hogy a szöveg '=' karaktereket tartalmazhat (a szabványosítás miatt). Ezekre viszont nincs szükség a visszakódolás során.
-}
decode :: Dictionary -> String -> String
decode d = transback d . delhelp where
 delhelp = takeWhile (/='=')
 transback d = map (chr . fromBitString) . chunksOf 8 . validList . concatMap (padLeft 0 6 . findCode d) where
  validList = (\ l -> take (8*(length l `div` 8)) l)