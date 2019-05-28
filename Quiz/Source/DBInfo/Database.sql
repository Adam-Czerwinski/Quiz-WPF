#stworzenie bazy danych
CREATE DATABASE `quiz`;

#uzycie bazy danych Quiz
USE `quiz`;

#utworzenie tabeli Test
CREATE TABLE `Test`(
	`ID_Test` INT UNSIGNED AUTO_INCREMENT,
    `Test Name` VARCHAR(50) NOT NULL,
	`Category` VARCHAR(50) NOT NULL,
    PRIMARY KEY(`ID_Test`)
);

#utworzenie tabeli Question
CREATE TABLE `Question`(
	`ID_Question` INT unsigned AUTO_INCREMENT,
    `Question` VARCHAR(256) NOT NULL, 
    `Answer A` VARCHAR(64) NOT NULL, 
    `Answer B` VARCHAR(64) NOT NULL, 
    `Answer C` VARCHAR(64) NOT NULL, 
    `Answer D` VARCHAR(64) NOT NULL, 
    `Correct Answers` VARCHAR(8) NOT NULL, 
    PRIMARY KEY(`ID_Question`)
);

#utworzenie tabeli łączącej tabele Test oraz Question
CREATE TABLE `Test/Question`(
    `ID_Test` INT unsigned,
    `ID_Question` INT unsigned,
    FOREIGN KEY(`ID_Test`) REFERENCES Test(`ID_Test`),
    FOREIGN KEY(`ID_Question`) REFERENCES Question(`ID_Question`)
);
