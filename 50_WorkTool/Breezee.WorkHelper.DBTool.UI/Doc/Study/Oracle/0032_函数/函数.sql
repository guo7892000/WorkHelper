-- PL/SQL -> Help -> Support Info���ɲ鿴TNS��Ϣ
-- ��ѯ�汾
SELECT version FROM v$instance;

/*
regexp_like(search_string ,pattern[,match_option])
����˵����
search_string��������ֵ
pattern��������ʽԪ�ַ����ɵ�ƥ��ģʽ,����������512�ֽ���
match_option����һ���ı����������û����øú�����ƥ����Ϊ������ʹ�õ�ѡ���У�
c ����Сд���У�Ĭ��ֵ
i ����Сд������
n ������ʹ��ԭ�㣨.��ƥ���κ������ַ�
m ������Դ�ַ���Ϊ����ַ����Դ�
*/
AND regexp_like(B.NAME_CN,#{NAME_CN,jdbcType=VARCHAR},'i')
AND instr((','||#{itemType,jdbcType=VARCHAR}||','),(','||B.ITEM_TYPE||','))>0


/*utl_raw.cast_to_raw �� Oracle ���ݿ��е�һ�����������ڽ��ַ���ת��Ϊ RAW �������͡�
RAW �����������ڴ洢���������ݣ���ͼ�������ļ����������ı����������ݡ�*/
utl_raw.cast_to_raw('string_to_convert')

/*��Oracle���ݿ��У�MD5������ͨ��DBMS_OBFUSCATION_TOOLKIT���е�MD5����ʵ�ֵġ��ú����������ַ�������Ϊ16�ֽڵ�RAW����ֵ��
ͨ����Ҫʹ��UTL_RAW.CAST_TO_RAW��������ת��Ϊʮ�������ַ����Ա���ʾ�ͱȽϡ�*/
DECLARE
 v2 VARCHAR2(32);
BEGIN
 v2 := UTL_RAW.CAST_TO_RAW(SYS.DBMS_OBFUSCATION_TOOLKIT.MD5(INPUT_STRING => '123456'));
 DBMS_OUTPUT.PUT_LINE(v2);
END;

/*Oracle �洢�����е� =>����Ϊ�βζ�Ӧ����Ϊλ�ö�Ӧ������ȱ�ޣ�����һ��������3������������2���ǿ��Բ�������Ĭ��ֵ),�����û�취λ�ö�Ӧ������
oralce�ڲ�һ���ô��ַ������������ݡ����磺*/
select dbms_obfuscation_toolkit.MD5(input_string => '11133') from dual;