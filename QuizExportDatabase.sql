CREATE DATABASE  IF NOT EXISTS `quiz` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `quiz`;
-- MySQL dump 10.13  Distrib 8.0.15, for Win64 (x86_64)
--
-- Host: localhost    Database: quiz
-- ------------------------------------------------------
-- Server version	8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `question`
--

DROP TABLE IF EXISTS `question`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `question` (
  `ID_Question` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `question` varchar(256) NOT NULL,
  `Answer A` varchar(64) NOT NULL,
  `Answer B` varchar(64) NOT NULL,
  `Answer C` varchar(64) NOT NULL,
  `Answer D` varchar(64) NOT NULL,
  `Correct Answers` varchar(8) NOT NULL,
  PRIMARY KEY (`ID_Question`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `question`
--

LOCK TABLES `question` WRITE;
/*!40000 ALTER TABLE `question` DISABLE KEYS */;
INSERT INTO `question` VALUES (14,'3 do sześcianu to','9','6','27','81','C'),(15,'a = -2, b = -3, c = 4\n    Podaj wynik działania: a - b - c','-3','-1','-9','5','A'),(16,'Rzucamy dwa razy monetą. Jakie jest prawdopodobieństwo, że dwa razy wypadnie orzeł?','1/2','3/4','1/4','1/3','C'),(17,'Ile wynosi pierwiastek sześcienny ze 125?','5','4','25','15','A'),(18,'5^(-1) wynosi:','-1/5','-5','0.2','0','C'),(19,'Ile mililitrów znajduje się w 2 i 1/4 litra?','2500','22500','225000','Żadne z powyższych','D'),(20,'(2+3)*(5-6)*3+2','-30','17','-13','(-1/13)^(-1)','C D'),(21,'Czy Wiedźmin 3: Dziki Gon to gra stworzona przez polskie studio?','Tak','Nie','Przez wielonarodowe','Nie wiem','A'),(22,'W którym roku Metin 2 został wydany w Polsce?','2004','2005','2006','2007','D'),(23,'W jakiej drużynie gra Leo Messi','FC Barcelona','Real Madrid','Manchester United','Manchester City','A'),(24,'Czy Ronaldinho zakończył karierę piłkarską?','Tak','Nie. Wciąż gra zawodowo','Oficjalnie nie zakończył ale już nie gra zawodowo','Nie wiem','A'),(25,'Jak usunąć przydzieloną pamięć?','Nie trzeba usuwać. Zajmuje się tym garbage collector','za pomocą słowa kluczowego delete','za pomocą słowa kluczowego clear','Nie ma możliwości przydzielania dynamicznie pamięci','B'),(26,'W której wersji C++ dodano klasę valarray?','C++11','C++03','C++98','C++14','B');
/*!40000 ALTER TABLE `question` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `test`
--

DROP TABLE IF EXISTS `test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `test` (
  `ID_Test` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Test Name` varchar(50) NOT NULL,
  `category` varchar(25) NOT NULL,
  PRIMARY KEY (`ID_Test`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `test`
--

LOCK TABLES `test` WRITE;
/*!40000 ALTER TABLE `test` DISABLE KEYS */;
INSERT INTO `test` VALUES (1,'Matematyka dla przedszkolaków!','Matematyka'),(6,'Gry komputerowe','Gry'),(7,'Piłka nożna','Sport'),(8,'C++','Technologia komputerowa'),(9,'Telefony komórkowe','Technologia komputerowa'),(10,'Muzyka','Muzyka');
/*!40000 ALTER TABLE `test` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `test/question`
--

DROP TABLE IF EXISTS `test/question`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `test/question` (
  `ID_Test` int(10) unsigned DEFAULT NULL,
  `ID_Question` int(10) unsigned DEFAULT NULL,
  KEY `ID_Test` (`ID_Test`),
  KEY `ID_Question` (`ID_Question`),
  CONSTRAINT `test/question_ibfk_1` FOREIGN KEY (`ID_Test`) REFERENCES `test` (`ID_Test`),
  CONSTRAINT `test/question_ibfk_2` FOREIGN KEY (`ID_Question`) REFERENCES `question` (`ID_Question`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `test/question`
--

LOCK TABLES `test/question` WRITE;
/*!40000 ALTER TABLE `test/question` DISABLE KEYS */;
INSERT INTO `test/question` VALUES (1,14),(1,15),(1,16),(1,17),(1,18),(1,19),(1,20),(6,21),(6,22),(7,23),(7,24),(8,25),(8,26);
/*!40000 ALTER TABLE `test/question` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-05-28 20:13:38
