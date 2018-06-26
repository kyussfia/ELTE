create or replace PROCEDURE sh_tablak IS
  CURSOR curs1 IS 
      select table_name from (select table_name from dba_indexes where table_type='TABLE' and table_owner='SH' and index_type='BITMAP'
      minus
      select table_name from DBA_TAB_COLUMNS where owner='SH' and data_type='NUMBER' and data_precision=10 and data_scale=2) order by table_name ASC;
  rec dba_indexes.table_name%type;
  
  len NUMBER;
BEGIN
    select COUNT(table_name) INTO len from (select table_name from dba_indexes where table_type='TABLE' and table_owner='SH' and index_type='BITMAP'
    minus 
    select table_name from DBA_TAB_COLUMNS where owner='SH' and data_type='NUMBER' and data_precision=10 and data_scale=2) order by table_name ASC;

  OPEN curs1;
  LOOP
    FETCH curs1 INTO rec;
    EXIT WHEN curs1%NOTFOUND;
    dbms_output.put(rec||'');
    if curs1%ROWCOUNT != len then
      dbms_output.put(',');
      end if;
  END LOOP;
    dbms_output.new_line;
  CLOSE curs1;
END;