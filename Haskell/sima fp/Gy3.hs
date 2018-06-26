module Gy3 where

import Prelude

areTriangleSides :: Real a => a -> a -> a -> Bool
areTriangleSides a b c
 |a + b < c = False
 |b + c < a = False 
 |c + a < b = False
 |otherwise = True
 
mountain :: Integer -> [Integer]
mountain n = [1..n] ++ [n-1,n-2..1]

even :: Integer -> Bool
even x
 | x `mod` 2 == 0 = True
 |otherwise = False
 
odd :: Integer -> Bool
odd = not . even

divides :: Integer -> Integer -> Bool
divides a b
 |b `mod` a == 0 = True
 |otherwise = False
 
divisors :: Integer -> [Integer]
divisors n = [ y | y<-[1..n], y `divides` n ]

properDivisors :: Integer -> [Integer]
properDivisors n = [ y | y<-[2..n-1], y `divides` n ]

(||) :: Bool -> Bool -> Bool
x || y = max x y

{-
False || False = False
_ || _ = True
-}
swap :: (a, b) -> (b, a)
swap (a,b) = (b,a)

mirrorX :: Num a => (a, a) -> (a, a)
mirrorX (x,y) = (x,-(y))


scale' :: Num a => a -> (a, a) -> (a, a)
scale' b (c,d) = (b * c, b * d)

distance :: Floating t => (t, t) -> (t, t) -> t
distance (x1, y1) (x2, y2) = sqrt ((x1-x2)^2 + (y1-y2)^2) 

