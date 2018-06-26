SET serveroutput ON
-- Felhaszn�l� �ltal megadott v�ltoz� defini�l�sa 
-- �s �rt�k�nek bek�r�se:
ACCEPT valtozo_I PROMPT "K�rem adjon meg egy karakterl�ncot: "
-- Hozz�rendelt (gazdak�rnyezeti) v�ltoz� deklar�l�sa:
VARIABLE valtozo_II  VARCHAR
-- A PL/SQL blokk deklar�ci�s szegmense:
-- V�ltoz� deklar�l�sa:
DECLARE
  valtozo_III  NUMBER;
-- A PL/SQL blokk v�grehajt�si szegmense:
BEGIN
-- �rt�kad�s:
  valtozo_III := &valtozo_I;
-- Felt�teles utas�t�s:
  IF valtozo_III > 100 
  THEN
    -- PL/SQL fut�s-k�zbeni ki�rat�s
    DBMS_OUTPUT.PUT_LINE('A megadott sz�m: '|| valtozo_III);
  ELSE 
    -- Hozz�rendelt v�ltoz� felveszi a PL/SQL v�ltoz� �rt�k�t
   :valtozo_II := valtozo_III;
  END IF;
-- A PL/SQL blokk v�ge
end;
-- A PL/SQL blokk futtat�sa:
/