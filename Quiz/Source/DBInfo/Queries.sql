    use quiz;
    
    #Wyświetla nazwę testu, nazwę pytania, i poprawne odpowiedzi
  	SELECT t.`Test Name`, q.`Question`, q.`Correct Answers`
    FROM Test t
		INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question;
        
	#Wyświetla nazwę testu, nazwę pytania, i poprawne odpowiedzi po ID testu
	SELECT t.`Test Name`, q.`Question`, q.`Correct Answers`
    FROM Test t
		INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question
    WHERE t.`ID_Test`=1;
        
	#wyświetla nazwę testu, pytania, odpowiedzi ABCD oraz poprawną odpowiedz
	SELECT t.`Test Name`, q.`Question`, q.`Answer A`, q.`Answer B`, q.`Answer C`, q.`Answer D`, q.`Correct Answers`
    FROM Test t
		INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question;
        
	#wyświetla nazwę testu, pytania, odpowiedzi ABCD oraz poprawne odpowiedzi po ID testu
	SELECT t.`Test Name`, q.`Question`, q.`Answer A`, q.`Answer B`, q.`Answer C`, q.`Answer D`, q.`Correct Answers`
    FROM Test t
		INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question
    WHERE t.`ID_Test`=1;
    
	#wyświetla pytania, odpowiedzi ABCD oraz poprawne odpowiedzi po ID testu
	SELECT q.`Question`, q.`Answer A`, q.`Answer B`, q.`Answer C`, q.`Answer D`, q.`Correct Answers`
    FROM Test t
		INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question
    WHERE t.`ID_Test`=1;
    
    #Zwraca ilość pytań po ID testu
    SELECT Count(*)
    FROM Test t
		INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question
    WHERE t.`ID_Test`=1;
    
        SELECT t.`ID_Test`, t.`Test Name`
    FROM Test t
		INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question
        Group by 1;
        










