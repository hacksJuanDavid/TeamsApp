/*DROP USER 'teamdbuser'@'localhost' ;*/
CREATE USER 'teamdbuser'@'localhost' IDENTIFIED BY 'Tea534msUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'teamdbuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;