module Gy5 where


difference :: Shape -> Shape -> Shape
sh1 `difference` sh2 = sh1 `intersect` (invert sh2)

vec :: Point -> Vector
vec = V

vec' :: Double -> Double -> Vector
vec' x y = V (x, y)
--translate :: Vector ->Shape -> Shape
--translate v sh = Shape $ \p