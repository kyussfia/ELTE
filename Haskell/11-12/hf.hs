module Hf where

import Prelude

f:: Integer -> Integer

f n 
 |n==1 = 1
 |even n = f (n `div` 2)
 |otherwise = f (3*n + 1)
 
 
	