   M S B A M L   `   `   `  ����C  ?HASystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:HASystemHASystem  5   	       HASystem.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:HASystem     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Icon$ Image/thunder.jpg��5	   	    � 
ResizeMode$ NoResize=�5
   	    � Title$ 登录��5   	   $	��350��6   $	��400��6!   .��5      ��  � 	Resources 5   
      ComboBoxTextBox �� 
TargetType ��Name  ComboBoxToggleButton � ColumnDefinitions J� Opacity	 � Cursor
 F�Name �� Stretch �� 
TargetType �� Property �� Value d�Name �� 	ClickMode d� 	IsChecked �� Path �� Mode �� RelativeSource ��Name �� SelectionBoxItem �� SelectionBoxItemTemplate J� IsHitTestVisible ��Name �� 
IsReadOnly �� 
IsReadOnly �Name �� IsDropDownOpen � AllowsTransparency �Name  �� MaxDropDownHeight! � ActualWidth" J� SnapsToDevicePixels  WindowBackgroundBrush  SolidBorderBrush  SimpleScrollViewer# K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation$   DirectionalNavigation��%�  &	        &	 a     '��     '�� E    �� 5      " ��66   .R�5      �� $ PART_ContentHost��6   |�.  #��z��6E   5      �� 5      " d�6;   .R�5      �  5      �� 5      �� 5      $��40 �6+   .+�5      �� $��0��6   $��2q�6-   
���   �$	 0.8Q�6U   �� 5      $��1��6   
���   �$	 0.8Q�6@   $
	 Hand��6N   F� 5      $
 Arrow��6   $��1��6*   $��Center=�6:   $��Center=�6W   "����        � 	=  	= � z     	$��11,10,11,10q�6�   $
 Fill=�6�   
�������6�   5      5      �� 5      " ��6   .�5       ��  � �� 5!      �� 5"      " ��6*   .R�5#      � .+�5%   "   d� $ ToggleButton��6q   $��2��6/   #���  6?   |�.  $ Press=�6   6�   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+6  �� 5&   "   $ ContentSite��6b   $��Center=�63   $��3,3,23,3q�6P   $��Center=�6w   #��z 6�   #��z 6�   #��z��6�    .  6=  �� 5)   "   $ PART_EditableTextBox��6�   $y�Hidden=�6*   #���   6>   $
��Left=�6k   $��3,3,23,3q�6�   $��Center=�6�   |�. 
������ # z 6  ��6�   Z� 6  � 5,   "   $ Popup��6e   #��z 6(   $��Bottom=�6R   |�.   . $��Slide=�6�   .��5-   &   � $ DropDown��6�   #��z  6+   #��z! 6[   " . .+�5.   *   �� $ DropDownBorder��61   #���  6I   #���  6~   $��1q�6�   �� 5/   *   $��4,6,4,6q�67   #���  6H   " . $
��Auto=�6�   $
��Auto=�6�   ��. .��51   .   �� # . $$ 	Contained=�6L   
�������6�   52   +   53   '   54   #   55      56      58      �� 59       � ��$ 13(�6-   5:      �� 5<      " ��6   .�5=      ��  � ��
 �����6/   5>      5?      ��5@   
   �� 5A      % �� ImageSource$% Image/thunder.jpg��6   & �� Opacity$	& 0.8Q�69   .+�5C   
   �� $��30��6   �� 5D   
   $!��电压与内阻测试系统��6   $��24(�6$   
���   �$��Center=�6F   �� 5E   
   $��20��6   � 5F   
   $��34��6    5G      �� 5H      $
��1.5* �6#   �� 5I      $��2* �6#   .+�5K      �� $��用户账号：��6   $��Right=�6$   $��Center=�6@   �� 5L      -   5        $ 	txtUserId��5L      $��Center=�6+   $��1��6O   $	��150��6_   $
��Left=�6k   $��Center=�5M      65   5N      �� 5O   
   $��20��6   � 5P   
   $��34��6    5Q      �� 5R      $
��1.5* �6#   �� 5S      $��2* �6#   .+�5U      �� $��	密码：��6   $��Right=�6"   $��Center=�6>   G� 5V      -   5        ' G�Name$' txtPwassWord��5V      $��Center=�62   $��1��6V   $	��150��6f   $
��Left=�6r   $��Center=�5W      64   5X      �� 5Y   
   $��20��6   � 5Z   
   $��34��6    5[      �� 5\      $
��1.5* �6#   �� 5]      $��2* �6#   .+�5_      �� $��用户类型：��6   $��Right=�6$   $��Center=�6@   �� 5`      -   5        ( ��Name$( comboUserType��5`      $��1��6O   $	��150��6_   $
��Left=�6k   5a      �� 5b   
   $��20��6   �� 5c   
   -   5        ) ��Name$) btnLogin��5c      $��	登   录��6#   $��56��63   $��30��6>   
����������#323b446�   5e      5f      