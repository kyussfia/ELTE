create or replace PROCEDURE uresblokk IS
  ossz_blokk NUMBER;
  sor_blokk NUMBER;
  ures_blokk NUMBER;
BEGIN
  SELECT sum(blocks) into ossz_blokk FROM dba_extents d
  where owner='NIKOVITS' and segment_name='CUSTOMERS' and segment_type='TABLE';
  
  select count(blokk_id) into sor_blokk  from (
  select dbms_rowid.rowid_block_number(ROWID) blokk_id
  from nikovits.customers GROUP BY dbms_rowid.rowid_block_number(ROWID)
  );

  ures_blokk := ossz_blokk - sor_blokk;
  DBMS_OUTPUT.PUT_LINE(ures_blokk);
END;