/*视图创建同义词并授权*/
GRANT SELECT ON UC.V_UC_QUERY_STORE_CAR TO MDS_ROLE;  
CREATE OR REPLACE SYNONYM MDS.V_UC_QUERY_STORE_CAR FOR UC.V_UC_QUERY_STORE_CAR;  
GRANT SELECT ON UC.V_UC_QUERY_STORE_CAR TO MDS WITH GRANT OPTION;

/*视图授权与创建同义词*/
grant select on uc.v_uc_carprice to mds ;
create or replace synonym mds.v_uc_carprice for uc.v_uc_carprice;

