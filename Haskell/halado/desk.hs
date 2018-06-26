module Desk where

import Prelude

{-
LList
-----

Beadási határidő: 2013.09.20. (péntek) 12:00 CEST

Készítsünk egy egyszerű csomagolótípust listákhoz, ahol a lista
hosszát konstans időt alatt mindig le tudjuk kérdezni!

A típus neve legyen `LList`, amelyet paraméterezünk a tárolandó
elemek típusával.  (Értelemszerűen a Haskell beépített lista
típusára építkezik.)

A műveletei pedig legyenek a következő függvények:

  - Lista átalakítása `LList` típusúra:

    fromList :: [a] -> LList a
	
-}
data LList a = LList Int [a]
 deriving (Show)
type LString = LList Char


pr12 :: LString
pr12 = fromList("alma")

fromList :: [a] -> LList a
fromList xs = LList (length xs) xs
{-
data LList a { len :: Int, toList :: [a] }

recordszintaxis
-}

{-
  - `LList` érték visszaalakítása listára:

    toList :: LList a -> [a]
-}
toList :: LList a -> [a] 
toList (LList _ xs) = xs 

{-
  - Hossz lekérdezése:

    len :: LList a -> Int

    Itt nyilván fontos, hogy valamilyen módon tároljuk előre (és tartsuk
    karban) a tárolt lista hosszát, hogy a lekérdezéskor csupán egyetlen
    művelettel, közvetlenül (tehát a `length` alkalmazása nélkül) hozzá
    tudjunk férni.
-}
len :: LList a -> Int
len (LList n _) = n
{-
  - Összefűzés:

    infixr 5 `cat`
    cat :: LList a -> LList a -> LList a
	
cat LList{len = h1. toList = l1} LList{len = h2, toList = l2}
-}
infixr 5 `cat`
cat :: LList a -> LList a -> LList a
cat (LList h1 l1) (LList h2 l2) =  LList (h1+h2) (l1 ++ l2)
 

{-
  - Egyenlőségvizsgálat:

    infix 4 `equals`
    equals :: (Eq a) => LList a -> LList a -> Bool

    Ebben az esetben némileg fel lehet gyorsítani az összehasonlítást úgy,
    ha a listák konkrét vizsgálata előtt egyszerűen csak megnézzük a
    hosszukat.
-}
infix 4 `equals`
equals :: (Eq a) => LList a -> LList a -> Bool

equals (LList g1 t1) (LList g2 t2)
 | g1 > g2 = False
 | g2 < g2 = False
 | t1 == t2 = True
 |otherwise = False
{-
Végezetül adjunk meg ehhez a típushoz egy egyszerű rövidítést a `String`
értékek számára, és nevezzük `LString` néven!

-}
