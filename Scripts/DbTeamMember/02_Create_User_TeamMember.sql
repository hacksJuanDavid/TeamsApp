/*DROP USER 'servicememberuser'@'localhost' ;*/
CREATE USER 'servicememberuser'@'localhost' IDENTIFIED BY 'M34msS3rv1c3Us3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'servicememberuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;