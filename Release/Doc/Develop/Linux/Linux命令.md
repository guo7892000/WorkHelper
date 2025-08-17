# Linux 常用命令

## 辅助
清除当前终端会话中的所有显示内容：clear  
显示历史命令：history  

## 进程
显示进程信息：ps -ef|grep jboss\
杀掉进程（9007为进程号）：kill -9 9007

## 查看
查看nohup的输出信息：tail -f nohup.out 

## 具体应用的命令
启动JBOSS：nohup ./standalone.sh -b 0.0.0.0 &  

## 文件相关
无提示地强制递归删除文件：rf -rm BF20250728<br>
无提示地强制递归删除文件：rm -rf HouseWeb.war.zip<br>
复制文件夹：cp -r /opt/app/web/jboss-as-7.2.0.Final/standalone/deployments/. /opt/app/web/jboss-as-7.2.0.Final/standalone/BF20250728  
编辑文件：touch ../standalone/deployments/HouseWeb.war.dodeploy  
查找文件：find / -name jackson  

## 切换目录相关命令
显示用户当前所在的目录路径：pwd  
显示目录文件列表：ll，或ls -l  
进入JBOSS目录： cd /opt/app/web/jboss-as-7.2.0.Final  
返回上一级目录：cd ..  
返回上二级目录：cd ../../  
进入用户主目录：cd ~  
进入根目录：cd /  

## tar压缩/解压命令
*-c：建立压缩档案、 -x：解压、-t：查看内容、-r：向压缩归档文件末尾追加文件、-u：更新原压缩包中的文件*  
压缩文件：tar -czvf HouseWeb.war.tar.gz HouseWeb.war  
将目录里所有jpg文件打包成jpg.tar：tar -cvf jpg.tar *.jpg  
解压指定文件：tar -xzf HouseWeb.war  
解压文件：unzip HouseWeb.war.zip  
tar -cvf jpg.tar *.jpg        // 将目录里所有jpg文件打包成jpg.tar   
tar -czf jpg.tar.gz *.jpg   // 将目录里所有jpg文件打包成jpg.tar后，并且将其用gzip压缩，生成一个gzip压缩过的包，命名为jpg.tar.gz  
tar -cjf jpg.tar.bz2 *.jpg  // 将目录里所有jpg文件打包成jpg.tar后，并且将其用bzip2压缩，生成一个bzip2压缩过的包，命名为jpg.tar.bz2  
tar -cZf jpg.tar.Z *.jpg    // 将目录里所有jpg文件打包成jpg.tar后，并且将其用compress压缩，生成一个umcompress压缩过的包，命名为jpg.tar.Z  
rar a jpg.rar *.jpg           // rar格式的压缩，需要先下载rar for linux  
zip jpg.zip *.jpg             // zip格式的压缩，需要先下载zip for linux  
tar -xvf file.tar              //解压 tar包  
tar -xzvf file.tar.gz        //解压tar.gz  
tar -xjvf file.tar.bz2      //解压 tar.bz2  
tar -xZvf file.tar.Z        //解压tar.Z  
unrar e file.rar             //解压rar  
unzip file.zip               //解压zip  
