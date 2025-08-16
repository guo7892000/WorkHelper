create materialized view MV_MDS_DLR_ORG_PA
refresh force on demand
start with to_date('12-05-2025 20:32:19', 'dd-mm-yyyy hh24:mi:ss') next SYSDATE + 1/144 
as
Select tree_type, company_id, area_id, area_code, area_name, big_area_id, big_area_code, big_area_name, small_area_id, small_area_code, small_area_name, province_id, province_code, province_name, city_id, city_code, city_name, is_limit_license_city, dlr_id, dlr_code, dlr_short_name, dlr_full_name, parent_dlr_id, comp_type, org_type, dlr_level, manager_name, manager_tel, car_brand_code, car_brand_cn, order_no, is_enable, group_id, zone_order_no, big_area_order_no, small_area_order_no, province_order_no, city_order_no, dlr_order_no, org_type_name, dlr_status FROM MDS.V_MDS_DLR_ORG_PA;
