   M S B A M L   `   `   `  ����  
CourseMVVM    CourseMVVM.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  5   	      WindowBrush#���   5   	   $��3q�5   	   
���   �  � Title$  BrainC#��5   	   $	��350��6   $	��487��6&    � 
ResizeMode$ NoResize=�62    � WindowStartupLocation$ CenterScreen=�6H    � WindowStyle$
 None=�6m    � 	Resources 5	       �� Color��%+   &	        �� 5
   
   $ #F5F5F5��6.   5      .��5      � .+�5   
   �� $��
Войти��6   $
��Left=�6!   $��117,189,0,0q�6<   $	��Top=�6Q   $	��264��6i   $��52��6u   $��20(�6�   ��6�   �� *VerifyCommand+6�   �� 5   
   $
��Left=�6   $��32��6-   $��
117,76,0,0q�69    �� TextWrapping$
 Wrap=�6M   $	��Top=�6a   $	��264��6y   $��18(�6�   ��6�   �� *Login+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��Регистрация��6   $
��Left=�6'   $��117,248,0,0q�6B   $	��Top=�6W   $	��264��6o   $��36��6{   $��16(�6�   ��6�   �� *RegistrationCommand+6�   �� 5   
   $
��Left=�6   $��	56,76,0,0q�6/   $
��Wrap=�6B   $��
Логин��6V   $	��Top=�6c   $��32��6{   $��56��6�   $��18(�6�    �� TextAlignment$ Center=�6�   �� 5   
   $
��Left=�6   $��
46,133,0,0q�6/   $
��Wrap=�6C   $��Пароль��6W   $	��Top=�6e   $��32��6}   $��66��6�   $��18(�6�   $ Center=�6�   �� 5   
   $��Забыли пароль��6   $
��Left=�6)   $��117,290,0,0q�6D   $	��Top=�6Y   $	��264��6q   $��24��6�   ��6}   �� *ForgotPasswordCommand+6�   �� 5   
   $��X��6   $
��Left=�6   $��
428,10,0,0q�68   $	��Top=�6L   $��43��6d   $��33��6o   $��20(�6�   ��6{   �� *ExitCommand+6�   G� 5   
   -   5        	 G�Name$	 PasswordBox��5      $
��Left=�6+   $��117,133,0,0q�6F   $	��Top=�6[   $	��264��6s   $��32��6   .  (System.Windows.Interactivity.Interaction
   Triggers
 5      /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ PasswordChanged��6"   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6-   �� *PasswordChangedCommand+   CommandParameter 6X   ��  �� ElementName$ PasswordBox��5      5      5      5      