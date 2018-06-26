module Signal where

type Time = Double

-- at :: Signal a -> Time -> a
newtype Signal a = Signal { at :: Time -> a }

constS :: a -> Signal a
constS x = Signal $ \t -> x

timeS :: Signal Time
timeS = Signal $ \t -> t

($$) :: Signal (a -> b) -> Signal a -> Signal b
fs $$ xs = Signal $ \t -> fs `at` t $ xs `at` t

mapS :: (a -> b) -> (Signal a -> Signal b)
mapS f xs = constS f $$ xs

mapT :: (Time -> Time) -> Signal a -> Signal a
mapT f xs = Signal $\t -> xs `at` (f t)