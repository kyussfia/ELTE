module Fordit where

import Prelude

{-
f :: String -> String

függvényt, ami megfordítja egy szövegben a 3 karaternél hosszabb szavakat!
(Feltételezhetjük, hogy a szavakat egy-egy szóköz választja el.)
Javasolt függvények: map, words, unwords, length

Teszteset:

f "fuggveny ami megforditja a 3 karaternel hosszabb szavakat"
 == "ynevgguf ami ajtidrofgem a 3 lenretarak bbazssoh takavazs"
-}

f :: String -> String

f [] = []
f xs = unwords(map felt (words xs)) where
 felt ys
  |length ys > 3 =  reverse ys
  |otherwise = ys  