# Linux ��������

## ����
�����ǰ�ն˻Ự�е�������ʾ���ݣ�clear  
��ʾ��ʷ���history  

## ����
��ʾ������Ϣ��ps -ef|grep jboss\
ɱ�����̣�9007Ϊ���̺ţ���kill -9 9007

## �鿴
�鿴nohup�������Ϣ��tail -f nohup.out 

## ����Ӧ�õ�����
����JBOSS��nohup ./standalone.sh -b 0.0.0.0 &  

## �ļ����
����ʾ��ǿ�Ƶݹ�ɾ���ļ���rf -rm BF20250728<br>
����ʾ��ǿ�Ƶݹ�ɾ���ļ���rm -rf HouseWeb.war.zip<br>
�����ļ��У�cp -r /opt/app/web/jboss-as-7.2.0.Final/standalone/deployments/. /opt/app/web/jboss-as-7.2.0.Final/standalone/BF20250728  
�༭�ļ���touch ../standalone/deployments/HouseWeb.war.dodeploy  
�����ļ���find / -name jackson  

## �л�Ŀ¼�������
��ʾ�û���ǰ���ڵ�Ŀ¼·����pwd  
��ʾĿ¼�ļ��б�ll����ls -l  
����JBOSSĿ¼�� cd /opt/app/web/jboss-as-7.2.0.Final  
������һ��Ŀ¼��cd ..  
�����϶���Ŀ¼��cd ../../  
�����û���Ŀ¼��cd ~  
�����Ŀ¼��cd /  

## tarѹ��/��ѹ����
*-c������ѹ�������� -x����ѹ��-t���鿴���ݡ�-r����ѹ���鵵�ļ�ĩβ׷���ļ���-u������ԭѹ�����е��ļ�*  
ѹ���ļ���tar -czvf HouseWeb.war.tar.gz HouseWeb.war  
��Ŀ¼������jpg�ļ������jpg.tar��tar -cvf jpg.tar *.jpg  
��ѹָ���ļ���tar -xzf HouseWeb.war  
��ѹ�ļ���unzip HouseWeb.war.zip  
tar -cvf jpg.tar *.jpg        // ��Ŀ¼������jpg�ļ������jpg.tar   
tar -czf jpg.tar.gz *.jpg   // ��Ŀ¼������jpg�ļ������jpg.tar�󣬲��ҽ�����gzipѹ��������һ��gzipѹ�����İ�������Ϊjpg.tar.gz  
tar -cjf jpg.tar.bz2 *.jpg  // ��Ŀ¼������jpg�ļ������jpg.tar�󣬲��ҽ�����bzip2ѹ��������һ��bzip2ѹ�����İ�������Ϊjpg.tar.bz2  
tar -cZf jpg.tar.Z *.jpg    // ��Ŀ¼������jpg�ļ������jpg.tar�󣬲��ҽ�����compressѹ��������һ��umcompressѹ�����İ�������Ϊjpg.tar.Z  
rar a jpg.rar *.jpg           // rar��ʽ��ѹ������Ҫ������rar for linux  
zip jpg.zip *.jpg             // zip��ʽ��ѹ������Ҫ������zip for linux  
tar -xvf file.tar              //��ѹ tar��  
tar -xzvf file.tar.gz        //��ѹtar.gz  
tar -xjvf file.tar.bz2      //��ѹ tar.bz2  
tar -xZvf file.tar.Z        //��ѹtar.Z  
unrar e file.rar             //��ѹrar  
unzip file.zip               //��ѹzip  
