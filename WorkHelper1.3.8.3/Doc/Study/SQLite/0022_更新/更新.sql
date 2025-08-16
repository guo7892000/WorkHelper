/*新增数据*/
INSERT INTO OrderTest(ID,OrderCode,Remark)
SELECT 1,'','Test';

/*更新数据*/
UPDATE OrderTest
SET Remark = 'Test'
WHERE ID = '1';

/*删除数据*/
DELETE FROM OrderTest
WHERE ID = '1';

