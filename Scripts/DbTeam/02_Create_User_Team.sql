/*DROP USER 'teamsserviceuser'@'localhost' ;*/
CREATE USER 'servicetemasuser'@'localhost' IDENTIFIED BY 'T3M34s4msUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'servicetemasuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;