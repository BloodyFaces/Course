   M S B A M L   `   `   `  ����  
CourseMVVM#    CourseMVVM.Views.CategoryView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  5   	      WindowBrush#���   5   	   $��3q�5   	   
���   �  � Title$  BrainC#��5   	   $��588.87��6   $��624.711��6)    � 
ResizeMode$ NoResize=�69    � WindowStartupLocation$ CenterScreen=�6O    � WindowStyle$
 None=�6t    � 	Resources 5	       �� Color��%+   &	        �� 5
   
   $ #F5F5F5��6.   5      .��5      � .+�5   
   �� $��
Выйти��6   $
��Left=�6!   $	��Top=�6<   $	��121��6T   $��
480,10,0,0q�6�   $��38��6�   $��18(�6�   ��6`   �� *LogoutCommand+6�   �� 5   
   -   5         ��Name$
 List��5      $
��Left=�6    $	��297��6;   $��	11,63,0,0q�6H   $	��Top=�6[   $	��275��6s   $��16(�6�   ��6   �� *	CatList+ �� Mode$ TwoWay=� �� SelectedItem 6�   �� *SelectedQuestion+��5      �� 5      .R�5      �� .+�5      �� ��6$   �� *
Cat_name+$ TwoWay=�5      5      .  (System.Windows.Interactivity.Interaction	   Triggers	 5      /  )System.Windows.Interactivity.EventTrigger  5      
   	EventName$
 SelectionChanged��6"   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6-   �� *SelectionChangedCommand+   CommandParameter 6Y   ��  �� ElementName$
 List�� �� Path$ SelectedIndex�5      5      �� 5   
   -   5        $ SList��5      $
��Left=�6!   $	��297��6<   $��
325,63,0,0q�6I   $	��Top=�6]   $	��277��6u   $��16(�6�   ��6�   �� *CatQuestions+��5      �� 5      .R�5      �� .+�5       �� ��6$   �� *QDesc+5!      5"      	 5$        5%      $
 SelectionChanged��6"   . 5&         6-   �� *!SelectionChangedQuestionCommand+ 6a   �� $ SList��$ SelectedIndex�5'      5)      �� 5*   
   $#��Новая категория��6   $
��Left=�6+   $��
11,375,0,0q�6F   $	��Top=�6Z   $	��274��6r   $��55��6~   $��16(�6�   ��6�   �� *AddCatCommand+6�   �� 5+   
   $#��Добавить вопрос��6   $
��Left=�6+   $��325,375,0,0q�6F   $	��Top=�6[   $	��276��6s   $��55��6   $��16(�6�   ��6�   �� *AddQuestCommand+6�   �� 5,   
   $!��Удалить вопрос��6   $
��Left=�6*   $��325,438,0,0q�6E   $	��Top=�6Z   $	��136��6r   $��55��6~   $��16(�6�   ��6�   �� *RemoveQuestionCommand+6�   �� 5-   
   $��К результатам��6   $
��Left=�6)   $��186,508,0,0q�6D   $	��Top=�6Y   $	��241��6q   $��55��6}   $��16(�6�   ��6�   �� *ToResultsCommand+6�   �� 5.   
   $'��!Удалить категорию��6   $
��Left=�6-   $��
11,438,0,0q�6H   $	��Top=�6\   $	��141��6t   $��55��6�   $��14(�6�   ��6�   �� *RemoveCategoryCommand+��6�   �� $
 List��$ SelectedIndex�6�   �� 5/   
   $ ��Редактировать��6   $
��Left=�6)   $��466,438,0,0q�6D   $	��Top=�6Y   $	��135��6q   $��55��6}   $��16(�6�   ��6�   �� *EditCommand+6�   �� 50   
   $ ��Переименовать��6   $
��Left=�6)   $��157,438,0,0q�6D   $	��Top=�6Y   $	��128��6q   $��55��6}   $��16(�6�   ��6�   �� *RenameCommand+6�   51      52      