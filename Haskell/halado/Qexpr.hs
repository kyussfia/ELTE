module Qexpr where

import Prelude

{-
QExpr
-----

Beadási határidő: 2013.10.04. (péntek) 12:00 CEST

Készítsünk egy olyan algebrai adattípust `QExpr` néven, amellyel
törteket (`Rational` típusú) tartalmazó kifejezéseket tudunk
ábrázolni (ld. `Data.Ratio` module)!

    QExpr :: *

Tegyük a `QExpr` típust a `Num` osztály példányává!  Ehhez először nyilván
az algebrai adattípusban modelleznünk kell a `Num` osztályhoz tartozó
műveleteket.

Ezután adjuk meg a `Fractional` példányt is a `QExpr` típusra!  Hasonlóan a
`Num` példányhoz, itt is tudnunk kell valahogy modellezni a szükséges
műveleteket.

Így az alábbi kifejezés szabályos már lesz:

    1 + 2 - 3 * 4 :: QExpr

Ezen kívül még az `QExpr` definíciója alapján adjuk meg a `Show`
és `Eq` osztályok példányait manuálisan!

A `Show` esetében például a következő teljesül:

  show (1 + 2 / 3 - 4 * 5 :: QExpr) == "((1 + (2 / 3)) - (4 * 5))"

Az `Eq` osztály példánya pedig tekintsen két `QExpr` értéket
egyenlőnek abban az esetben, ha azok strukturálisan megegyeznek!

Például:

  ((1 + 2 * 3 - 4 * 5) :: QExpr) /= (((-3) * 7 - (-2)) :: QExpr)
  ((1 + 2 * 3 - 4 * 5) :: QExpr) == (((1 + (2 * 3)) - (4 * 5)) :: QExpr)
  -}