   M S B A M L   `   `   `  ����N  J6040CEM_GUI_Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:_6040CEM_GUI_Windows.View_6040CEM_GUI_Windows.View  5      )    #_6040CEM_GUI_Windows.View.FrontPage   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:_6040CEM_GUI_Windows.View     5        N� Title$  	FrontPage��5	      .��5      � .+�5   
   �� -   5         ��Name$ Serial_Comm_��5      $��Serial Com:��6&   $
��Left=�6<   $	��Top=�6W   $��16(�6o   $
��Bold"�6}   $��	10,10,0,0q�6�   �� 5   
   -   5         ��Name$ 
ConnectBtn��5      $��Connect��6%   $
��Left=�67   $��670,341,0,0q�6R   $	��Top=�6g   $��40��6   $	��100��6�    J� RenderTransformOrigin$ 0.491,1.083&�6�   
�������$
��Bold"�6�   ���	#FF386641$��14(�6�   6!  �� 5   
   -   5        $ DisconnectBtn��5      $��
Disconnect��6(   $
��Left=�6=   $��550,341,0,0q�6X   $	��Top=�6m   $��40��6�   $	��100��6�   $ 0.491,1.083&�6�   
�������$
��Bold"�6�   ���	#FFBC4749$��14(�6�   $y�Hidden=�6(  �� 5   
   -   5         ��Name$ SerialComRx��5      $
��Left=�6'   $��
150,32,0,0q�6B    �� TextWrapping$
 Wrap=�6V   $	��Top=�6j   $	��410��6�   $��50��6�   
���   �
��� � �$��3q�6�   ${�False��6�   $
��Bold"�6�   $��5q�6�   �� 5   
   $��Received Raw Data:��6   $
��Left=�6-   $��	10,44,0,0q�6H   $	��Top=�6[   $	��150��6s   $
��Bold"�6   �� 5   
   $��Filtered data��6   $	��202��6*   $	��Top=�67   $��10,87,425,0q�6O   .��5      � .+�5      � $��	0,0,81,31q�6   .+�5      �� $��Sensor Name:��6   $
��Left=�63   $	��Top=�6N   $
��Bold"�6f   �� 5      -   5        $ SensorNameLabel1��5      $��Temperature (LM35)��66   $
��Left=�6S   $��6,26,0,0q�6n   $	��Top=�6�   �� 5      -   5        $ SensorNameLabel2��5      $��Temperature (TCK)��66   $
��Left=�6R   $��6,52,0,0q�6m   $	��Top=�6   �� 5      -   5        $ SensorNameLabel3��5      $��Pressure (MPX4250)��66   $
��Left=�6S   $��6,78,0,0q�6n   $	��Top=�6�   5      � 5      $��262,0,10,31q�6   $ 0.426,0.358&�6,   .+�5      �� $��Values:��6   $��Center=�6.   $	��Top=�6K   $
��Bold"�6c   �� 5      -   5        $ SensorValueLabel1��5      $	��N/A��67   $��Center=�6E   $��0,26,0,0q�6b   $	��Top=�6t   �� 5      -	   5        $ SensorValueLabel2��5      $	��N/A��67   $��Center=�6E   $	��Top=�6b   $��0,52,0,0q�6z   �� 5      -
   5        $ SensorValueLabel3��5      $	��N/A��67   $��Center=�6E   $��0,78,0,0q�6b   $	��Top=�6t   5      5       5"      5$      5%      