/*DROP USER 'membersservicedb'@'localhost' ;*/
CREATE USER 'membersservicedb'@'localhost' IDENTIFIED BY 'T3M34s4msUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'membersservicedb'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;