module Gy3 where

import Prelude

{-# LANGUAGE FlexibleInstances #-}
--{-# LANGUAGE OverlappingInstances #-}
{-# LANGUAGE TypeSynonymInstances #-}
{-# LANGUAGE StandaloneDeriving #-}
{-# LANGUAGE GeneralizedNewtypeDeriving #-}

newtype Dollars = Dollars Int
 deriving (Eq, Show, Num)
--general shit nélkül: -> hiba
--deriving (Eq, show , num)  

class C a where
 c :: a -> Int

--instance C [Char] where
-- c _ = 0
data T a = A a | B String
-- deriving Eq

--deriving instance Eq a => Eq (T a)
deriving instance Eq a => Eq (T [a])

 
newtype TString = TS [Char]

instance C TString where
 c _ = 0
 
instance C [a] where
 c _ = 1 
 
instance C String where
 c _ = 2
 

 
{-
pgj/haskell2

átfedő typuspéldányok:

xflexibleinstances -> szigoritás enyhitése

kommentként:(pragmák) pragmák a kód elejére !!!


xtypesynonyminstances -> tipusszinonimák engedélyezése

FlexibleInstances
:set -XflexibleInstances


átfedések kiküszöbölése:
(pragmákkal) a speciáűlist válassza, ha illeszkedik

OverlappingInstances  vigyázni kell vele!!!!

diszjunktá kell tenni ,a newtyppal tudjuk
c (TS "alma")

StandaloneDeriving - önéálló derivingek
deriving instance Eq a => Eq (T a)

GeneralizedNewtypeDeriving
bármilyen osztály levezethetünk rá
konstruktor átjáratható..

JSON:


-}