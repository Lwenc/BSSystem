   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      C ?HASystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:DPCMS.UCDPCMS.UC 5      1clr-namespace:HASystem.PanelsHASystem.Panels 5      "   HASystem.Panels.AddUserPanel   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      *numericclr-namespace:DPCMS.UC     5      /localclr-namespace:HASystem.Panels     5      .��5      �   � 	Resources  5   
      ComboBoxTextBox �� 
TargetType ��Name  ComboBoxToggleButton � ColumnDefinitions J� Opacity � Cursor F�Name �� Stretch �� 
TargetType	 �� Property
 �� Value d�Name �� 	ClickMode d� 	IsChecked �� Path �� Mode �� RelativeSource ��Name �� SelectionBoxItem �� SelectionBoxItemTemplate J� IsHitTestVisible ��Name �� 
IsReadOnly �� 
IsReadOnly �Name �� IsDropDownOpen � AllowsTransparency �Name �� MaxDropDownHeight � ActualWidth J� SnapsToDevicePixels  WindowBackgroundBrush  SolidBorderBrush  SimpleScrollViewer K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation    DirectionalNavigation��%b  &	        &	 a     '��     �� 5      " ��66   .R�5      �� $ PART_ContentHost��6   |�.  #��z��6E   5      �� 5      " d�6;   .R�5      �  5      �� 5      �� 5      $��40 �6+   .+�5      �� $��0��6   $��2q�6-   
���   �$	 0.8Q�6U   �� 5      $��1��6   
���   �$	 0.8Q�6@   $
 Hand��6N   F� 5      $ Arrow��6   $��1��6*   $��Center=�6:   $��Center=�6W   "����        � 	=  	= � z     	$��
11,10,11,5q�6�   $
 Fill=�6�   
�������6�   5      5      �� 5      " ��6   .�5      �� 	 � ��
 5       �� 5!      " ��6*   .R�5"      � .+�5$   "   d� $ ToggleButton��6q   $��2��6/   #���  6?   |�.  $ Press=�6   6�   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+6  �� 5%   "   $ ContentSite��6b   $��Center=�63   $��3,3,23,3q�6P   $��Center=�6w   #��z 6�   #��z 6�   #��z��6�    .  6=  �� 5(   "   $ PART_EditableTextBox��6�   $y�Hidden=�6*   #���   6>   $
��Left=�6k   $��3,3,23,3q�6�   $��Center=�6�   |�. 
������ # z 6  ��6�   Z� 6  � 5+   "   $ Popup��6e   #��z 6(   $��Bottom=�6R   |�.   . $��Slide=�6�   .��5,   &   � $ DropDown��6�   #��z 6+   #��z 6[    . .+�5-   *   �� $ DropDownBorder��61   #���  6I   #���  6~   $��1q�6�   �� 5.   *   $��4,6,4,6q�67   #���  6H    . $
��Auto=�6�   $
��Auto=�6�   ��. .��50   .   ��  . $  	Contained=�6L   
�������6�   51   +   52   '   53   #   54      55      57      �� 58      	 � ��$
 13(�6-   59      5:      ! � RowDefinitions! 5;   
   �� 5<      $��7* �6   �� 5=      .+�5?   
   �� $��主要信息录入��6   $��20(�6#   $
��Bold"�61   $	��850��6C   $	��400��6O   $��0��6\   .��5@      � .+�5A      � ! 5B      �� 5C      �� 5D      �� 5E      .+�5G      � $��0��6    5H      �� 5I      �� 5J      �� 5K      �� 5L      .+�5N      �� $��	账号：��6    $��0��6.   $��Center=�6>   $��Right=�6Y   �� 5O      $��	姓名：��6    $��2��6.   $��Center=�6>   $��Right=�6Y   �� 5P      -   5        $ txtUserName��5P   "   $	��140��68   $��Center=�6D   $��Center=�5Q   "   $��30��6F   $
��Left=�6R   $��3��6m   $��12(�6}   " �� 	MaxLength$" 15��6�   �� 5R      -   5        $ txtId��5R   "   $
��Left=�61   $��28��6L   $��0,48,0,0q�6X   # �� TextWrapping$
# Wrap=�6j   $	��Top=�6   $	��140��6�   $��Center=�6�   $��Center=�6�   $��12(�6�   $��1��6�   $" 20��6  5S      � 5T      $��1��6    5U      �� 5V      �� 5W      �� 5X      �� 5Y      .+�5[      �� $��	密码：��6    $��0��6.   $��Center=�6>   $��Right=�6Y   G� 5\      -   5        $ G�Name$$ pwdPassWord��5\   &   $��1��6;   $	��140��6K   $��Center=�6W   $��Center=�6y   $��30��5]   &   $
��Left=�62   $��12(�6M   % G� 	MaxLength$% 50��6[   �� 5^      $��确认密码：��6    $��2��60   $��Center=�6@   $��Right=�6[   G� 5_      -   5        $$ confirmPassWord��5_   &   $��3��6?   $	��140��6O   $��Center=�6[   $��Center=�6}   $��30��5`   &   $
��Left=�62   $��12(�6M   $% 50��6[   5a      � 5b      $��2��6    5c      �� 5d      �� 5e      �� 5f      �� 5g      .+�5i      �� $��联系电话：��6    $��0��60   $��Center=�6@   $��Right=�6[     DPCMS.UC.NumericBox  5j      -   5        &  Name$& txtTelephone��5j   -   $��0��6C   $��1��6P   ' � DataContext$' 11位电话号码��5k      $��Center=�60   $��Center=�6R   $��12(�6v   (   MaxFractionDigits$( 4��5l      ) �� IsInputMethodEnabled) .  $
��Left=�6Y   $	��140��6t   $��30��6�   $" 11��6�   .��6�     5m        	HelpBrush* j� TileMode+ �� Opacity, j� Stretch- j� 
AlignmentX. !� Visual��%�   &	       !� 5n   "   $
* None=�6@   $	+ 0.3Q�6P   $
, None=�6^   $
- Left=�6m   . 5o   &   �� 5p   *   $��Italic$�64   ��6G   �� 5r   #   5s      .��61   ��5t      �� 5u   "   " ��6(   / �� Triggers/ 5v   &   P� 5w   *   0 P� Property0 � ��1 P� Value1 6B   Z� . �5x   .   �� 	 � ��#
 [ 6K   5y   +   P� 5z   *   0 � ��$1  ��6B   . �5{   .   �� 	 � ��#
 [ 6K   5|   +   5~   #   .��5   -   .   (System.Windows.Interactivity.Interaction2   	Behaviors2 5�      $  DPCMS.UC.TextBoxFilterBehavior  5�   "   3   TextBoxFilterOptions$3 Numeric=�6A   .��5�   7   5�      �� 5�      $��角色归属：��6    $��2��60   $��Center=�6@   $��Right=�6\   �� 5�      -   5        4 ��Name$4 	comBoRole��5�   #   $��30��66   $	��140��6B   $
��Left=�6N   $��3��6i   5�      5�      5�      5�      � 5�   
   $��1��6     5�      ��%�   '��       �� 5�      " ��6   .�5�      �� 	 � ��$
 80��6.   �� 5�      	 � ��$
 30��6/   �� 5�      	 � ��

 �����63   �� 5�      	 � ��
 �#323b4463   5�      5�       5�      �� 5�      $
��4.5* �6#   �� 5�      $
��0.5* �6#   �� 5�      $
��4.5* �6#   .+�5�      �� -   5        5 ��Name$5 btnSave��5�      $��保存��6&   $��0��63   $��Right=�6C   6_   �� 5�      -   5        $5 btnReset��5�      $��重置��6'   $��2��64   $
��Left=�6D   6_   5�      5�      5�      