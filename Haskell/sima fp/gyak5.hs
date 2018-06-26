module Gyak5 where

import Prelude hiding (even, odd, (||))

even n = n `mod` 2 == 0

odd n = not $ even n

mountain :: Integer -> [Integer]
mountain n = [1..n] ++ reverse[1..n-1]

areTriangleSides :: Real a => a -> a -> a -> Bool
areTriangleSides a b c
 |a + b <= c || b + c <= a || a + c <= b = False
 |otherwise = True
 
divides :: Integer -> Integer -> Bool
a `divides` b 
 | b `mod` a == 0 = True
 | otherwise = False
 
isLeapYear :: Integer -> Bool
isLeapYear n 
 | 400 `divides` n = True
 | 4 `divides` n == True && 100 `divides` n == False = True 
 | otherwise = False
 
sumSquaresTo :: Integer -> Integer
sumSquaresTo n = sum [ c^2 | c <- [1..n]]

divisors :: Integer -> [Integer]
divisors n = [ a | a<-[1..n], a `divides` n]

properDivisors :: Integer -> [Integer]
properDivisors n = [ a | a<-[2.. n `div` 2], a `divides` n]

(||) :: Bool -> Bool -> Bool
True || _ = True
False || x = x

xor :: Bool -> Bool -> Bool
--xor a b = (a || b) && not (a && b)
--xor a b = (a /= b)
xor = (/=)

swap :: (a, b) -> (b, a)
swap (a,b) = (b,a)

mirrorX :: Num a => (a, a) -> (a, a)
mirrorX (x,y) = (x,-y)

scale' :: Num a => a -> (a, a) -> (a, a)
scale' n (x,y) = (n*x, n*y)

mirrorP :: Num a => (a, a) -> (a, a) -> (a, a)
mirrorP (o,p) (x,y) = (o-(x-o),p-(y-p))

distance :: Floating t => (t, t) -> (t, t) -> t
distance (x1, y1) (x2, y2) = sqrt ((x1-x2)^2 + (y1-y2)^2)

