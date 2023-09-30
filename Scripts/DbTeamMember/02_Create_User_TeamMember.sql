/*DROP USER 'membersservice'@'localhost' ;*/
CREATE USER 'membersserviceuser'@'localhost' IDENTIFIED BY 'M34msS3rv1c3Us3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'membersserviceuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;