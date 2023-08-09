# WorkHelper

##Introduction
Work Assistant is a mini framework based on personal development (with only simple user login, no role authorization, organizational management, etc.) tool software, mainly designed for software development and design, database development and operation, testing personnel, and other uses,
Used to generate database change scripts and other related operations. Of course, it also includes some commonly used character splicing, copying and other functions, suitable for anyone using a computer!!
#### Software Architecture
Software architecture description

##Software architecture
Developed using C #+WinForm based on. Net Framework 4.8, supports five database types: SqlServer, Oracle, MySql, SQLite, and PostgreSql. Because it is a tool class, the default is to use a simple and easy-to-use SQLite database.
This software is based on the personal MiniWinFormApp framework and implements modularity. Currently, there is only one database tool submodule. The project also uses some open-source frameworks, such as using Castle. Windsor for IOC and NPOI for Excel import and export.
UI components are generally shipped with Microsoft. This project also integrates the implementation of two other personal projects, MyPeachNet and AutoSQLExtractor, under the. Net Framework 4.8.



##Development debugging
*After downloading the source code, set [Breezee. Framework. Mini. StartUp] as the startup project and ensure that the project references the following projects:<br>
<img src="Projects that need to be referenced for Mini startup projects. png"/>

##Download and use
*1. Open the release package link and download the software:
[Release Package Download]（ https://gitee.com/breezee2000/WorkHelper/releases ）
*After downloading and extracting, double-click on Breezee. Framework. Mini. StartUp. exe to open the program.
Note: Before 1.02, installation packages are used and require installation; After 1.0.3, use the green package method to decompress and use it.
*3. The default user is xtadmin with a password of 1. After logging in, we can also make modifications.
*4. Each function has operating instructions. We can click on 'Question Book' on the toolbar or 'User Manual' under the 'Help' menu to view it.

##Common functions:
Copy data splicing: Copy data from Excel and then customize the splicing! Commonly used!!    
Click to copy characters: By configuring, you can click to copy characters or open a directory. Commonly used!!    
Table structure SQL generation: includes Excel template generation, reading database to generate table SQL. Commonly used!!    
Add, delete, modify, and query SQL generation: Read the database and generate parameterized add, delete, modify, and query SQL. Commonly used!!    
Table column character concatenation: Read the column information of the data table for character concatenation. Commonly used!!    
Database data SQL generation: Read the database data and generate new data SQL.    
Data Excel formula generation: Quickly generate new data formulas for Excel templates.    
Data Dictionary: Read the column information of the data table, and generate corresponding characters such as SQL query results and SQL query conditions.    
Code file generation: Generate code through custom Excel templates.    
Entity file generation: Generate entity objects for data tables through custom Excel templates.    
Merge script: Merge multiple database scripts into one file.    
Merge two Excel files: By copying the contents of two Excel files and defining conditions to associate and merge data.    
IN List Splicing: Generate an Excel column of data as an IN list.    
Directory character generation: Read all directory or file information under a certain directory.    

##Issue and bug submission
When submitting a bug issue, please provide a detailed description or screenshot explanation. Thank you!    
[Email feedback suggestions or questions]（ guo7892000@126.com ）    
[WeChat] BreezeeHui    