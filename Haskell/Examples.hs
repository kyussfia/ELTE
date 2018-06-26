import Control.Monad
import System.Console.ANSI
import Shape
import Signal
import Render

type Animation = Signal Shape

rotatingBox :: Animation
rotatingBox = constS rotate $$ timeS $$ constS square 

bouncingBall :: Animation
bouncingBall = constS translate $$ pos $$ constS circle
 where
  pos = constS vec' $$ bounceX $$ bounceY
  bounceX = constS 0
  bounceY = constS (sin . (3 *)) $$ timeS
  
exapmle :: Animation
example =  constS difference $$ rotatingBox $$ bouncingBall

runAnimation :: Animation -> IO ()
runAnimation a = animate defaultWindow 0 15 a

animate :: Window -> Time -> Time -> Animation -> IO ()
animate win t0 t1 animation = do
  clearScreen
  forM_ frames $ \sh -> do 
	setCursorPosition 0 0
    putStr $ render win sh
	threadDelay 100
  where 
   frames = (animation `at`) `map` ts
   ts	  = samples t0 t1 (round $ (t1 - t0) * 10)

   --cabal install ansi-terminal
   