module Gy2 where

import Prelude

{-
(^) :: (Integral b, Num a) => a -> b -> a
(^^) :: (Fractional a, Integral b) => a -> b -> a
(**) :: Floating a => a -> a -> a
-}

--Hónapok
monthDays = [(m,d) | m<-[1..12], d<-[1..31], (m `elem` [4,6,9,11] ) <= (d < 31), (m `elem` [2]) <= (d < 29)]

--Zip
--zip :: [a] -> [b] -> [(a, b)]

--[1..] `zip` ['a'.. 'z']

--Állítsuk elő az 1,2,1,2,3,2,1,2,3,4,3,2,1,2,3,4,5,4,3,2,1,2,3,4,5,6,5,4,3,2,1,.. sorozatot!

--concat [[1..n] ++ [n-1, n-2 ..2] | n<-[2..]]

--unwords [ replicate n '*' | n<-[1..]]