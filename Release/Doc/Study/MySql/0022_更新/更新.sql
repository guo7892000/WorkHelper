/*MySql的更新语句不能使用别名，只能使用全表名来关联：*/
UPDATE t_gh_ven_bu_un_delivery_ivc
SET IS_VIP = '1' 
WHERE MV_DATE_STRING = DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y%m%d')   
AND (EXISTS (SELECT 1   
                    FROM t_pms_ven_bu_pur_order O 
                   INNER JOIN t_pms_ven_bu_pur_order_d OD   
                      ON O.PUR_ORDER_ID = OD.PUR_ORDER_ID   
                   WHERE O.PURCHASE_ORDER_TYPE_ID = '9'   
                     AND t_gh_ven_bu_un_delivery_ivc.DLR_ID = OD.DLR_ID   
                     AND t_gh_ven_bu_un_delivery_ivc.VIN = OD.VIN) OR EXISTS   
          (SELECT 1   
             FROM t_prc_mdsn_lookup_value L   
            WHERE L.LOOKUP_TYPE_CODE = 'VE0663'   
              AND L.LOOKUP_VALUE_CODE = t_gh_ven_bu_un_delivery_ivc.DLR_ID) OR   
          t_gh_ven_bu_un_delivery_ivc.SALE_TYPE IN ('4', '5', '6'))
;

/*Oralce的MERGE INTO替代更新方式*/
update t_scm_pa_bu_oem_out_store_d
SET DELIVERED_STATUS='2',
    DELIVERED_TIME=ifnull(DELIVERED_TIME,now())
WHERE exists  (select 1
    FROM (SELECT SUM(ifnull(B.IN_STORE_NUM, 0)) AS IN_STORE_NUM,
     A.RELATE_ORDER_CODE,
     B.PART_NO
    FROM t_scm_pa_bu_dlr_in_store A
    JOIN t_scm_pa_bu_dlr_in_store_d B
     ON A.IN_STORE_ID = B.IN_STORE_ID
    WHERE A.RELATE_ORDER_CODE = '#RELATE_ORDER_CODE#'
    GROUP BY A.RELATE_ORDER_CODE, B.PART_NO
    ) M
    where 1=1
    and M.RELATE_ORDER_CODE= t_scm_pa_bu_oem_out_store_d.OUT_STORE_CODE
    and M.PART_NO = t_scm_pa_bu_oem_out_store_d.PART_NO
    and M.IN_STORE_NUM = t_scm_pa_bu_oem_out_store_d.OUT_STORE_NUM
);
