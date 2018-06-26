module Doc where

--absztrakt szintaxis
data Doc
	= Empty
	| Text String
	| Char Char
	| Concat Doc Doc

--kombinatorok
empty :: Doc
empty = Empty

text :: String -> Doc
text "" = Empty
text s = Text s

char :: Char -> Doc
char  = Char

double :: Double -> Doc
double d = text (show d)

string :: String -> Doc
string = enclose '"' '"' (<>) .hcat . map oneChar --nem enged törést

enclose ::  Char -> Char -> (Doc -> Doc -> Doc) -> Doc -> Doc
enclose left rigth op x = char left `op` x `op` char rigth

hcat :: [Doc] -> Doc
hcat = foldr (<>) empty

(<>) :: Doc -> Doc -> Doc
Empty <> y = y
x <> Empty = x
x <> y = x `Concat` y

oneChar :: Char -> Doc
oneChar c =
 case(lookup c simpleEscapes) of
  Just r -> text r
  Nothing -> char c
  
simpleEscapes :: [(Char, String)]
simpleEscapes = 
 zipWith (\a b -> (a, '\\':[b])) "\b\n\f\r\t\\\"/" "bnfrt\\\"/"

series :: Char -> Char -> (a -> Doc) -> [a] -> Doc
series open close item =
 enclose open close (</>) .
 fsep . punctuate (char ',') . map item

(</>) :: Doc -> Doc -> Doc --engedi a törést
x </> y = x <> softline <> y

softline :: Doc
softline = group line

line :: Doc
line = undefined

group :: Doc -> Doc
group = undefined

nest :: Int -> Doc -> Doc
nest = undefined

--algoritmusok (interpretals)
compact :: Doc -> String
compact = undefined

pretty :: Int -> Doc -> String
pretty = undefined