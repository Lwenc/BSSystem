   M S B A M L   `   `   `  ����C  ?HASystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HASystem.UCHASystem.UC  5          HASystem.UC.PassageWayUC   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:HASystem.UC     5      .��5	      � .+�5
   
   �� -   5          ��Name$  gbHeader��5
      $��通道1��6%   $
��Bold"�62   $��18(�6D   .��5      �  � ColumnDefinitions 5      �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��3* �6    � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$
 dgDG��5      $��0��6*   $��3��67   $��2��6K      CanUserResizeRows .  $
��Auto=�5         
IsReadOnly . $��9(�6_      ColumnHeaderStyle 5      �� 5       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5      ��  �� Property � ��	 �� Value	 5   "   �� 5    &   
 �� 
StartPoint$	
 0,0&�6:    �� EndPoint$	 0,1&�6K   .��5!   *   �  � Color$ Transparent��67    � Offset$ 0Q�6K   � 5"   *   $ #323b44��67   $	 0.9Q�6G   � 5#   *   $ Transparent��67   $ 1Q�6K   5$   '   5&      �� 5'       � ��
	 �   �6;   �� 5(       � ��$	 13(�69   5)         Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6I      Header$ 电池条码��6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *	Barcode+6S     5-      $ * 6F   $ 测试类型��6P    6-   �� *Type+6P     5.      $ * 6F   $ 电压/V��6P    6-   �� *Volt+6P     5/      $ * 6L   $ 
电阻/mΩ��6V    6-   �� *
Resistance+6V     50      $ * 6H   $ 是否合格��6R    6-   �� *KValue+6R     51      $ * 6H   $
 K值��6R    6-   �� *IsPass+6R      RowStyle 53      �� 54      )  #System.Windows.Controls.DataGridRow"  6    �� Triggers 55      �� 56       �� Binding 6&   �� *
BatQuality+ �� Value 否6E   .a�57      ��  � ��
	 �  ��6<   58      5:      5<      5=      5>      5?      5@      