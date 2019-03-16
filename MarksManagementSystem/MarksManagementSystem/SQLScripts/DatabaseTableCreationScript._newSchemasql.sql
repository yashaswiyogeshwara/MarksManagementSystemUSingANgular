use marksmanagement;
select * from marks;
--Drop Table Class
CREATE TABLE `standard` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Year` int(11) NOT NULL,
  `Sem` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--Drop Table Subject
CREATE TABLE `subject` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Code` varchar(45) NOT NULL,
  `standardId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `standardId` (`standardId`),
  CONSTRAINT `subject_ibfk_1` FOREIGN KEY (`standardId`) REFERENCES `standard` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--Drop Table students
CREATE TABLE `students` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Hallticket` varchar(45) NOT NULL,
  `Yearofjoin` int(11) NOT NULL,
  `Dept` varchar(45) NOT NULL,
  `Section` varchar(45) NOT NULL,
  `Backlogs` int(11) DEFAULT NULL,
  `NAAC` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `Hallticket_UNIQUE` (`Hallticket`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--Drop Table Marks
CREATE TABLE `marks` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SubjectId` int(11) DEFAULT NULL,
  `StudentId` int(11) DEFAULT NULL,
  `standardId` int(11) DEFAULT NULL,
  `Grade` varchar(45) DEFAULT NULL,
  `GradePoint` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `SubjectId` (`SubjectId`),
  KEY `StudentId` (`StudentId`),
  KEY `StanderdId` (`standardId`),
  CONSTRAINT `marks_ibfk_1` FOREIGN KEY (`SubjectId`) REFERENCES `subject` (`Id`),
  CONSTRAINT `marks_ibfk_3` FOREIGN KEY (`standardId`) REFERENCES `standard` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
