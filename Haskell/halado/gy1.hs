import Prelude hiding(String)
import Data.Char

type Name = String
type Complex = (Double, Double)

type(String) = [Char]

--type Year = Int

type Square = (Char, Int)

type Two a = (a, a)

type ListList a = [[a]]

type P3 a = (a, a, a)

type PredicateOn a = a -> Bool

even' :: PredicateOn Int
even' = even

newtype Name' = N String
	deriving (Eq, Ord, Show)

isProperName :: Name' -> Bool
isProperName (N (c:_)) = isUpper c
isProperName _ = False

--infixr 5 ++|
--(++|) :: Name -> Name -> Name
--(N s1) ++| (N s2) = N (s1 ++ s2)

--s :: Char -> Int -> Square
--s (c,n)
-- |n>0 && n<9 && c
 
newtype Year = Y Int
	deriving (Eq, Ord, Show)
	
a `divides` b = b `mod` a == 0
	
isLeapYear :: Year -> Bool
isLeapYear (Y n) =  4 `divides` n == 0 && not (100 `divides` n) || 400 `divides` n == 0

-- az egység elem a haskellben "unit", a jele () hasonló a voidhoz
--newtypban min 1 konst. param kell
--databan nem lehet 0 is meg sok is

--data T = A | B

--szorzás:

data X = A | B
data Y = C | D
data Z = T X Y

