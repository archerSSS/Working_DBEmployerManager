CREATE DATABASE `mydb4`;

USE mydb4;
CREATE TABLE `employers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `Patronymic` varchar(45) NOT NULL,
  `Born` datetime NOT NULL,
  `Gender` varchar(45) NOT NULL,
  `UnitName` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

CREATE TABLE `units` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UnitName` varchar(45) NOT NULL,
  `HeadId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_Units_Employers1_idx` (`HeadId`),
  CONSTRAINT `fk_Units_Employers1` FOREIGN KEY (`HeadId`) REFERENCES `employers` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

CREATE TABLE `orders` (
  `OrderId` int NOT NULL AUTO_INCREMENT,
  `Contractor` varchar(45) NOT NULL,
  `Date` datetime NOT NULL,
  `Employers_Id` int NOT NULL,
  PRIMARY KEY (`OrderId`),
  KEY `fk_Orders_Employers_idx` (`Employers_Id`),
  CONSTRAINT `fk_Orders_Employers` FOREIGN KEY (`Employers_Id`) REFERENCES `employers` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

CREATE TABLE `products` (
  `OrdersId` int NOT NULL,
  `ProductId` int NOT NULL AUTO_INCREMENT,
  `ProductName` varchar(45) NOT NULL,
  `Count` int NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ProductId`),
  KEY `fk_Products_Orders1_idx` (`OrdersId`),
  CONSTRAINT `fk_Products_Orders1` FOREIGN KEY (`OrdersId`) REFERENCES `orders` (`OrderId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;