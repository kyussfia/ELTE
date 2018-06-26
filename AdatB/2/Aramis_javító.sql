-- összes jelenlegi blokk := blokkok amiben nincs sor + blokkok sorral

SELECT sum(blocks) FROM dba_extents
--JOIN dba_data_files d ON d.FILE_ID = dba_extents.FILE_ID 
where owner='NIKOVITS' and segment_name='CUSTOMERS' and segment_type='TABLE'; --1536

-- azon blokkok amelyek a táblában vannak
select count(*) from (
  select dbms_rowid.rowid_block_number(ROWID)
  from nikovits.customers GROUP BY dbms_rowid.rowid_block_number(ROWID)
);

CREATE OR REPLACE PROCEDURE uresblokk IS
  ossz_blokk NUMBER;
  sor_blokk NUMBER;
BEGIN;
  SELECT sum(blocks) into ossz_blokk FROM dba_extents d
  where owner='NIKOVITS' and segment_name='CUSTOMERS' and segment_type='TABLE'; --1536
  
  select count(*) into sor_blokk from (
  select dbms_rowid.rowid_block_number(ROWID)
  from nikovits.customers GROUP BY dbms_rowid.rowid_block_number(ROWID)
  );
  
  DBMS_OUTPUT.PUT_LINE('Válasz: '|| ossz_blokk - sor_blokk);
END;
/

set serveroutput on
call uresblokk();
/



select count(cust_id) from nikovits.customers;
select * from DBA_TABLESPACES;

select * from dba_free_spaces;

select * from dba_data_files;