module SearchTree where

import Prelude
import Data.List 

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
-}
--SearchTree :: * -> *
data SearchTree a =  SearchTree [a]
  deriving (Eq, Ord, Show)
{-
A használatához szükséges műveletek pedig a következők:
-}
    -- üres keresőfa

empty :: SearchTree a
empty = SearchTree []
  
    -- leképzés listára (inorder bejárással)
    --toList :: SearchTree a -> [a]

toList :: SearchTree a -> [a]
toList (SearchTree xs) = xs

    -- létrehozás listából
    --fromList :: (Ord a) => [a] -> SearchTree a

fromList :: (Ord a) => [a] -> SearchTree a
fromList ys = SearchTree (sort ys) 

    -- fa bővítéshez

insert :: (Ord a) => a -> SearchTree a -> SearchTree a
xs `insert` (SearchTree ys) = SearchTree (ys ++ [xs])

    --elemvizsgálat
    --contains :: (Ord a) => SearchTree a -> a -> Bool

contains :: (Ord a) => SearchTree a -> a -> Bool
n `contains` k = elem k (toList n)  
{-
Néhány teszteset, amelynek teljesülnie kell:

    (toList . fromList $ "almafa") == "aaaflm"
    (42 `insert` empty) `contains` 42
    not ((fromList [1..10]) `contains` 42)
-}