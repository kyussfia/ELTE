module Bead where

import Prelude

{-
SearchTree
----------

Beadási határidő: 2013.09.27. (péntek) 12:00 CEST

Készítsünk egy egyszerű keresőfát Haskellben!  A feladat megoldásához
először valamilyen módon ábrázolnunk kell a fát, majd megadnunk a
bővítéshez és a kereséshez szükséges műveleteket.  (A törlés nem része a
feladatnak, de gyakorlásképpen meg lehet próbálni annak megvalósítását
is.)

A típus neve legyen `SearchTree`, amelyet paraméterezünk a keresendő
adatok típusával:

    SearchTree :: * -> *
	
-}
newtype SearchTree a = SearchTree [a]
{-
A használatához szükséges műveletek pedig a következők:
-}
    -- üres keresőfa
empty :: SearchTree a
empty  = SearchTree 0

    -- fa bővítése
insert :: (Ord a) => a -> SearchTree a -> SearchTree a
insert 
    -- leképzés listára (inorder bejárással)
toList :: SearchTree a -> [a]

    -- létrehozás listából
fromList :: (Ord a) => [a] -> SearchTree a

    -- elemvizsgálat
contains :: (Ord a) => SearchTree a -> a -> Bool
{-
Néhány teszteset, amelynek teljesülnie kell:

    (toList . fromList $ "almafa") == "aaaflm"
    (42 `insert` empty) `contains` 42
    not ((fromList [1..10]) `contains` 42)
-}