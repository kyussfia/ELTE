module G where

import Prelude
import Data.List

makeSystem :: Integral a => [a] -> [a]
makeSystem as = f as (map product $ inits as)  where
 f [] _ = []
 f (x:xs) (y:ys) = concat(replicate (fromIntegral x) [y]) ++ (f xs ys)

