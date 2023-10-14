/*DROP USER 'serviceteamsuser'@'localhost' ;*/
CREATE USER 'serviceteamsuser'@'localhost' IDENTIFIED BY 'T3M34s4msUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'serviceteamsuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;