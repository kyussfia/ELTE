module Gy4 where

import Prelude

--concat
concat :: [[a]] -> [a]
concat [] = []
concat (x:xs) = [x] ++ concat xs 

--Merge
merge :: [a] -> [a] -> [a]
[] `merge` xs = xs
xs `merge` [] = xs
(a:as) `merge` (b:bs) = a: ((b:bs) `merge` as)

--(++)
(++) :: [a] -> [a] -> [a]
[] ++ xs = xs
(a:cs) ++ (b:bs) = a: (cs ++ (b:bs))

--ZIP
zip [] _ = []
zip (a:as) (b:bs) = [(a,b)] ++ (zip as bs) 

--nub
nub :: Eq a => [a] -> [a]
nub [] = []
nub l  = nub' l []            
  where
    nub' [] _           = []                    
    nub' (x:xs) ls                              
        | x `elem` ls   = nub' xs ls            
        | otherwise     = x : nub' xs (x:ls) 

--tails
tails :: [a] -> [[a]] 
tails [] = [[]]
tails (x:xs) = [(x:xs)] ++ tails xs

--prefix
isPrefixOf :: Eq a => [a] -> [a] -> Bool

 
