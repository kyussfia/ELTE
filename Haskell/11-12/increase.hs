module INK where

import Prelude

{-

Teszteset:

increasing [1,2,4,2,1,5,3]  == [1,2,4,5]

-}

increasing :: Ord a => [a] -> [a]

increasing xs
 |0==length xs = []
 |1==length xs = xs
 |last xs <= last (increasing (take ((length xs)-1) xs)) = increasing (take ((length xs)-1) xs)
 |otherwise = increasing (take ((length xs)-1) xs) : last xs
