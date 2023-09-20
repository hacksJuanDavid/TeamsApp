/*DROP USER 'teamMemberdb'@'localhost' ;*/
CREATE USER 'teamMemberdb'@'localhost' IDENTIFIED BY 'T3M34s4msUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'teamMemberdb'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;