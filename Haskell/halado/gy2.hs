module Gy2 where

import Prelude hiding(Just)
{-
--data T = A | B --bool izomorf


{-
:i Bool

:t ()
-}
data X = A | B
	deriving (Enum, Show)

data Y = C | D
	deriving (Enum,Show)
	
data T = T X Y
--deriving (Enum)
--generálja a felsorolót erre  a typusra


--(,)  párok
--(,) :: a->b->(a,b)
--(,) 1 2 = (1,2)

--data weekDays = H | K | Sz | Cs | P | Szo | V
-}
data Days = Mon | Tue | Wed | Thu | Fri | Sat | Sun

data Square = Square Column Row
--konstr nevek nagybetű és lehet ugyanaz

data Column = A | B | C | D | E | F | G | H
data Row = R1 | R2 | R3 | R4 | R5 | R6 | R7 | R8

--számmal nem kezdődhet typius definicio
--van undefined tipus is.

--nem termináló -> végtelen
--x :: undefined
--x = x
type Name = String
type Age = Int
type Address = String

data Pair a b = P a b
--data Person = Person Name Age Address
{-
name :: Person -> Name
name (Person x _ _) = x

age :: Person -> Age
age (Person _ x _) = x

address :: Person -> Address
address (Person _ _ y) = y
-}
data Person = Person { name :: Name, age :: Age, address :: Address }

movePerson :: Person -> Address -> Person
movePerson p addr = p {address = addr}

mayRetire :: Person -> Bool
mayRetire (Person _ x _) = (x > 65)
--mayRetire (Person { age = x}) = (x > 65)

data IPoint = IPoint { px :: Int, py :: Int }
-- IPoint { px, py :: Int}

translate :: (Int, Int) -> IPoint -> IPoint
translate (vx, vy) p = IPoint (px p + vx) (py p + vy)
--IPoint { px = px p + vx, py = py p + vy}

--Tipusok tipusa = Kind!!!
-- :k ghcban
--a * fajta

-- :k [] Int = :k [Int]


data Maybe a = Nothing | Just a 	--ezzel fejezem  ki hogy igen nem

--Either :: * -> * -> *
--data Either a b = Left a | Right b

type Error a = Either String age     --either megengedő
--vagy type Error = Either String

data Nat = Zero | Succ Nat
	deriving (Show)
	
--Tipus osztályok

--read :: Read a => String -> a
-- => ez választja el a typust a megszoritásoktól!
-- :i vel kérdezhtejók le

--osztályok: Class

class Eq' a where	--tipusosztáély definiálása, akármennyi metodus lehet
	(==.) :: a -> a -> Bool 
	(/=.) :: a -> a -> Bool
	i /=. j = not (i ==. j)
--példáényositani kell a használathoz: instance
instance Eq' Int where
	i ==. j = i == j
--	i /=. j = i /= j --vagy not (i == j), vagy not(i ==. j))
instance Eq' Integer where
	i ==. j = abs i == abs j
--zárójelben akármennyi megszoritás lehet
--definiocioban is akármenyi lehet
instance Eq' a => Eq' [a] where 	--[Int]
	(i:is) ==. (j:js) = i ==. j 