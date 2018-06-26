union [circle 3 `move` (0, x/4 - 1) | x<-[1..50] ] `move` (-1,-3)


union [circle 3  `fill` yellow `move` (0, x/4 - 1) | x<-[1..50] ] `move` (-1,-3)

union [(union [circle 3  `fill` yellow `move` (0, x/4 - 1) | x<-[1..50] ] `move` (5,-3) ) `move` (k+3)| k <- [5..10]] 

-- coin tomes

-- union [ (union [circle 3  `fill` yellow `move` (0, x/4 - 1) | x<-[1..50] ] `move` (k 
--*6, -3) )  | k <- [1..10]] `move` (-20,-3)