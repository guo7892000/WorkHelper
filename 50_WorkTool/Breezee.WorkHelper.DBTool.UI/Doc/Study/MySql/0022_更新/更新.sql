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


