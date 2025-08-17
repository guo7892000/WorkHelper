### DBLink
```
create database link MSKBT
 connect to BTINT
 using '(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.68)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = bom)))';
 ```
