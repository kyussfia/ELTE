create or replace PROCEDURE cl_index IS
  CURSOR curs1 IS 
    --select index_name, sample_size from dba_indexes where owner='NIKOVITS' and index_type='CLUSTER' order by index_name ASC;
    select segment_name, bytes from dba_segments d join dba_indexes i on i.index_name=d.segment_name and i.owner='NIKOVITS' and  i.index_type='CLUSTER'
    where d.segment_type='INDEX' and d.owner='NIKOVITS' order by index_name ASC;
  rec curs1%rowtype;
  
  len NUMBER;
BEGIN
   select count(segment_name) into len from (select segment_name, bytes from dba_segments d join dba_indexes i on i.index_name=d.segment_name and i.owner='NIKOVITS' and  i.index_type='CLUSTER'
    where d.segment_type='INDEX' and d.owner='NIKOVITS' order by index_name ASC);
   OPEN curs1;
  LOOP
    FETCH curs1 INTO rec;
    EXIT WHEN curs1%NOTFOUND;
    --dbms_output.put(rec.index_name||':'||rec.sample_size);
    dbms_output.put(rec.segment_name||':'||rec.bytes);
    if curs1%rowcount != len then
      dbms_output.put(',');
    end if;
  END LOOP;
  dbms_output.new_line;
  CLOSE curs1;
END;