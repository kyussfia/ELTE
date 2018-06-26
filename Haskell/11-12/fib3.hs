{-
fibPairs = iterate (\(a,b) -> (b,a+b)) (0,1) 

fib n = map fst (iterate (\(a,b) -> (b,a+b)) (0,1)) !! n


fib3 :: [(Integer,Integer,Integer)]
fib3 = [(0,0,1),(0,1,0),(1,0,1),(0,1,1),(1,1,1),(1,1,2),(1,2,2),(2,2,3),(2,3,4),(3,4,5),(4,5,7),(5,7,9),(7,9,12),(9,12,16),(12,16,21),(16,21,28),(21,28,37),(28,37,49),(37,49,65),(49,65,86),...

A hozzárendelési szabály a következő:
A hármasok elemei bal felé tolódnak, a belépő új elem az előző első két elem összege.


-}

module Fib3 where

import Prelude

fib3 :: [(Integer,Integer,Integer)]

fib3 = iterate (\(a,b,c) -> (b,c,a+b)) (0,0,1)

mirrorP :: Num a => (a, a) -> (a, a) -> (a, a)
mirrorP (x1,y1) (x2,y2) = (x2+2*(x1-x2),y2+2*(y1-y2))

