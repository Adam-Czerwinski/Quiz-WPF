#użycie bazy danych
USE `quiz`;

#uzupełnienie tabeli Test
INSERT INTO `Test`(`Test Name`,`Category`) VALUES
	("Gry komputerowe","Gry"),
	("Piłka nożna","Sport"),
	("C++","Programowanie"),
	("Telefony komórkowe","Mobilne"),
	("Muzyka","Muzyka");
    
    
#uzupełnienie tabeli Question
INSERT INTO `Question`(`Question`,`Answer A`,`Answer B`,`Answer C`,`Answer D`,`Correct Answers`) VALUES
	("Czy Wiedźmin 3: Dziki Gon to gra stworzona przez polskie studio?","Tak","Nie","Przez wielonarodowe","Nie wiem","A"),
	("W którym roku Metin 2 został wydany w Polsce?","2004","2005","2006","2007","D"),
	("W jakiej drużynie gra Leo Messi","FC Barcelona","Real Madrid","Manchester United","Manchester City","A"),
	("Czy Ronaldinho zakończył karierę piłkarską?","Tak","Nie. Wciąż gra zawodowo","Oficjalnie nie zakończył ale już nie gra zawodowo","Nie wiem","A"),
    ("Jak usunąć przydzieloną pamięć?","Nie trzeba usuwać. Zajmuje się tym garbage collector","za pomocą słowa kluczowego delete","za pomocą słowa kluczowego clear","Nie ma możliwości przydzielania dynamicznie pamięci","B"),
    ("W której wersji C++ dodano klasę valarray?","C++11","C++03","C++98","C++14","B");
    
#Uzupełnienie tabeli Test/Question
INSERT INTO `Test/Question`(`ID_Test`,`ID_Question`) VALUES
	(1,1),
    (1,2),
    (2,3),
    (2,4),
    (3,5),
    (3,6);
