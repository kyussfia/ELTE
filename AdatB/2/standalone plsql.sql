SET serveroutput ON
-- Felhasználó által megadott változó definiálása 
-- és értékének bekérése:
ACCEPT valtozo_I PROMPT "Kérem adjon meg egy karakterláncot: "
-- Hozzárendelt (gazdakörnyezeti) változó deklarálása:
VARIABLE valtozo_II  VARCHAR
-- A PL/SQL blokk deklarációs szegmense:
-- Változó deklarálása:
DECLARE
  valtozo_III  NUMBER;
-- A PL/SQL blokk végrehajtási szegmense:
BEGIN
-- Értékadás:
  valtozo_III := &valtozo_I;
-- Feltételes utasítás:
  IF valtozo_III > 100 
  THEN
    -- PL/SQL futás-közbeni kiíratás
    DBMS_OUTPUT.PUT_LINE('A megadott szám: '|| valtozo_III);
  ELSE 
    -- Hozzárendelt változó felveszi a PL/SQL változó értékét
   :valtozo_II := valtozo_III;
  END IF;
-- A PL/SQL blokk vége
end;
-- A PL/SQL blokk futtatása:
/