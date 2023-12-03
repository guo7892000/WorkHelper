/*合并更新语句*/
MERGE INTO T_PA_BU_PUR_ORDER_D U
USING (SELECT SUM(NVL(B.IN_QTY, 0)) AS T_IN_QTY, B.PART_NO
             FROM T_PA_BU_IN_ORDER A
             LEFT JOIN T_PA_BU_IN_ORDER_D B
               ON A.IN_ORDER_ID = B.IN_ORDER_ID
            WHERE B.SOURCE_ORDER_CODE = V_ORDER_NO /*关联单号:外购单号*/
              and A.net_code = NetCode
            GROUP BY B.PART_NO
	  ) F
ON (U.PART_NO = F.PART_NO)
    WHEN MATCHED THEN
      UPDATE
         SET U.IN_STOCK_QTY      = F.T_IN_QTY,
             U.LAST_UPDATED_DATE = systimestamp
       WHERE POD.PUR_ORDER_ID =
             (select pur_order_id
                from T_PA_BU_PUR_ORDER
               where pur_order_code = V_ORDER_NO
                 and net_code = NetCode); /*采购单ID*/

