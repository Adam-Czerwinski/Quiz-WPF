   M S B A M L   `   `   `  ����?  ;Quiz, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:QuizQuiz  5      "    Quiz.Source.View.QuizSolveUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:Quiz     5        � DataContext  5	      �� *Quiz+   Locator �� Source# [  .��5      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��4* �6   �� 5      $��* �6    � 	Resources 5   
    �� 
TargetType �� Property �� Value  
labelStyle  labelAnswersStyle �� BasedOn  labelQuestionStyle��%4  '��       &	     '��     &	 �    7  &	 _    7  �� 5      " ��6   .�5      ��  � ��$ Center=�66   �� 5       � ��$ Center=�68   �� 5       � ��$ 10 25q�6+   �� 5       � ��$ 14(�6-   �� 5       � ��$ Center=�6=   �� 5       � ��$ Center=�6?   5       �� 5$      " ��6'   .�5%      ��  � ��$ Center=�66   �� 5&       � ��$ 14(�6-   5'      �� 5+      " ��6   .�5,      ��  � ��$ 20(�6-   �� 5-       � ��$ 5q�6,   5.      �� 56      8   " ��6T   .�57      ��  � ��
 �����6/   �� 58       � ��
 �  ��6/   59      �� 5=      8   " ��6U   .�5>      ��  � ��$ Center=�68   �� 5?       � ��$ 20(�6-   �� 5@       � ��
 �����6/   5A      5F      .+�5J   
   �� $��0��6   $��Center=�6M   $��40(�6j   $
��0 40q�6x   ��6   �� *Test.TestName+ �� Mode$ OneTime=�6x   �� 5N   
   $��1��6   .+�5Q      �� #��[ 6U   ��6   �� *!CurrentQuestion.QuestionContent+$ OneWay=�6U   �� 5V      	 �� Orientation$	 
Horizontal=�6   .+�5W      �� 
 d� 	IsChecked
 6   �� *!CurrentAnswersABCDCheckBoxes[0]+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5X      #��[ 6   ��6C   �� * CurrentQuestion.AnswersABCD[0]+$ OneWay=�5Y      �� 5]      $	 
Horizontal=�6   .+�5^      �� 
 6   �� *!CurrentAnswersABCDCheckBoxes[1]+$ TwoWay=�$ PropertyChanged=��� 5_      #��[ 6   ��6C   �� * CurrentQuestion.AnswersABCD[1]+$ OneWay=�5`      �� 5d      $	 
Horizontal=�6   .+�5e      �� 
 6   �� *!CurrentAnswersABCDCheckBoxes[2]+$ TwoWay=�$ PropertyChanged=��� 5f      #��[ 6   ��6C   �� * CurrentQuestion.AnswersABCD[2]+$ OneWay=�5g      �� 5k      $	 
Horizontal=�6   .+�5l      �� $��
10 25 10 0q�6�   
 6   �� *!CurrentAnswersABCDCheckBoxes[3]+$ TwoWay=�$ PropertyChanged=�6�   �� 5m      #��[ 6   $��0 25 0 0q�6�   ��6D   �� * CurrentQuestion.AnswersABCD[3]+$ OneWay=�6�   5n      5p      ]� 5w   
   $��2��6   .+�5y      �� $��10q�6   .+�5z      ��  �� Padding$	 0 5q�6   �� 5{      $�	Pytanie: ��6    �� 5|      �6   �� *CurrentQuestionIndex+$ OneWay=�$ PropertyChanged=� �� 5}      $�/��6    �� 5~      �6   �� *NumberOfQuestions+$ OneTime=�5      �� 5�      �� 5�      $�Pozostaly czas: ��6    �� 5�      �6   �� *
TimeLeft+$ OneWay=�$ PropertyChanged=� �� 5�      $�sekund��6   5�      5�      �� 5�      $	 
Horizontal=�6   $��Right=�62   $��20q�6N   .+�5�      �� $��Wyjdź��6   ��6)   �� *QuitQuizCommand+�� 5�      $��
Poprzednie��6   $
��30 0q�6�   {�6.   �� *IsAbleToGoPreviousQuestion+��6_   �� *PreviousQuestionCommand+6�   �� 5�      ��6   �� *NextQuestionOrEndQuiz+$ OneWay=�$ PropertyChanged=���6u   �� *!NextQuestionOrSubmitQuizCommand+5�      5�      5�      5�      