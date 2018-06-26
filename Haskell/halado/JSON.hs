{-# LANGUAGE FlexibleInstances, OverlappingInstances #-}

module JSON 
 (
   toJValue
 , fromJValue
 , jary
 , fromJAry
 , compactJValue
 , prettyJValue
 )
 where

import Prelude
import Data.Maybe
import Doc

data JValue
 = JString String
 | JNum Double
 | JBool Bool
 | JObject [(String, JValue)]
 | JNull
-- | JArray [JValue]
 |JArray (JAry JValue)
 deriving Show
 --string, number(double), bool,
 --object [mezőnáv, érték]
 --array [JValue]
 --null


class JSON a where
 toJValue :: a -> JValue
 fromJValue :: JValue -> Maybe a
 
instance JSON Bool where
-- toJValue :: JSON a => a -> JValue
 toJValue x = JBool x

-- fromJValue :: JSON a => JValue -> a -> Maybe a
 fromJValue (JBool x) = Just x
 fromJValue    _ = Nothing


instance JSON [(String, JValue)] where
 toJValue x = JObject x
 
 fromJValue (JObject xs) = Just xs
 fromJValue _			= Nothing
 
instance JSON [JValue] where
 toJValue x = JArray (JAry x)
 
 fromJValue (JArray (JAry xs)) = Just xs
 fromJValue _			= Nothing
 
--JAry :: [a] -> JAry a
--fromJAry :: JAry a -> [a]
newtype JAry a = JAry { fromJAry :: [a] }


jary :: [a] -> JAry a
jary = JAry 

instance JSON a => JSON (JAry a) where
 toJValue ja = JArray (jary (map toJValue fromJAry ja))
 fromJValue (JArray (JAry xs))
  | (length xs) == (length ys) = Just (JAry xs)
  where ys = catMaybes (map fromJValue xs)
 fromJValue _ = Nothing  
{-
instance JSON a => JSON [a] where
 toJValue xs = JArray (map toJvalue xs)
 
 fromJValue (JArray xs)
   | (length justs) == (length xs) = Just xs
   where
    justs = catMaybes (map fromJValues xs)
 fromJValue _ = Nothing
 
 --vagy sikerül mindent átkonvertálni vagy nem
-}
renderJValue :: JValue -> Doc	--közös doc renderelése
renderJValue (JBool True) = text "true"
renderJValue (JBool False) = text "false"
renderJValue JNull = text "null"
renderJValue (JNumber num) = double num
renderJValue (JString str) = string str
renderJValue (JArray(JAry ary)) =
 series '[' ']' (nest 4 . renderJValue) ary --nest 4 = huzzuk be 4gyel
renderJValue (JObject (JObj obj)) =
 series '{' '}' field obj
 where
  field (name,val) = 
   nest 4 $ string name </> text ":" </> renderJValue val
   --</> összetapasztás
   
--gépi
compactJValue :: JValue -> String
compactJValue = compact . renderJValue
--emberi
prettyJValue ::Int -> JValue -> String
prettyJValue w = pretty w . renderJValue


 
 
 
 
 
 
 
 