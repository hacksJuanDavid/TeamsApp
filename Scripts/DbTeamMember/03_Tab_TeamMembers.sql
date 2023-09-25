CREATE TABLE `membersservicedb`.`TeamMembers` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `TeamId` INT NOT NULL,
    `FirstName` VARCHAR(50) NOT NULL,
    `LastName` VARCHAR(50) NOT NULL,
    `BirthDate` DATETIME NOT NULL,
    `Position`  VARCHAR(20) NULL,
    `Phone`  VARCHAR(50) NULL,
  PRIMARY KEY (`Id`));
