   M S B A M L   `   `   `  ����C  ?HASystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:HASystemHASystem  5   	   )clr-namespace:HASystem.UCHASystem.UC  5   	   1clr-namespace:HASystem.PanelsHASystem.Panels  5   	       HASystem.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:HASystem     5   	   (ucclr-namespace:HASystem.UC     5   	   /panelclr-namespace:HASystem.Panels     5   	     � Icon$  Image/thunder.jpg��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$! 电压与内阻测试系统��5   	   $	��700��6   $
��1024��6(   .��5      � ��5   
   �� 5       �� ImageSource$ Image/thunder.jpg��6    � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      �� 5      $��0.05* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   .+�5!          HASystem.UC.btnUserControl  -   5          Name$ rdiTest��5!           HASystem.UC.btnUserControl   Click$ 	Switching��6S      AppearanceSource$ Image/test.png��61   $	��100��6e   $	��100��6q   $��0��6~     5"      -   5        $ rdiData��5"       $ 	Switching��6S   $ Image/data.png��61   $	��100��6e   $	��100��6q   $��1��6~     5#      -   5        $ rdiModel��5#       $ 	Switching��6U   $ Image/model.png��62   $	��100��6g   $	��100��6s   $��2��6�     5$      -   5        $ rdiUser��5$       $ 	Switching��6S   $ Image/user.png��61   $	��100��6e   $	��100��6q   $��3��6~     5%      -   5        $ 	rdiSystem��5%       $ 	Switching��6W   $ Image/system.png��63   $	��100��6i   $	��100��6u   $��4��6�   5&      � 5'   
   -   5        	 �Name$	 contentDisplay��5'      
�������
 J� Opacity$	
 0.8Q�6:   $��1��6H   z�5(      T� 5)      -   5        	trans��5)   %   .+�5+      ��  J� Effect 5,      3  -System.Windows.Media.Effects.DropShadowEffect  5-         
BlurRadius$ 5Q�6'      ShadowDepth$ 3Q�66      	Direction$ 2Q�6F      Opacity$	 0.5Q�6T   ��5/      �� 50       � ColorInterpolationMode$ SRgbLinearInterpolation=�6*    �� 
StartPoint$	 0,0&�6[    �� EndPoint$	 0,2&�6l   .��51      �  � Color$ Transparent��6'    � Offset$
 -0.2Q�6;   � 52      $ White��6'   $
 0.02Q�65   � 53      $ White��6'   $
 0.03Q�65   � 54      $ White��6'   $	 0.1Q�65   � 55      $ Transparent��6'   $	 1.2Q�6;   56      58      $   HASystem.Panels.GoodsTestPanel  59      -   5          Name$ panelGoodsTest��59   #   $y�	Collapsed=�6;   &    HASystem.Panels.DataManagerPanel  5:      -	   5          Name$ panelDataManager��5:   %   $y�	Collapsed=�6?   '   !HASystem.Panels.ModelManagerPanel  5;      -
   5          Name$ panelModelManager��5;   &   $y�	Collapsed=�6A   &    HASystem.Panels.UserManagerPanel  5<      -   5          Name$ panelUserManager��5<   %   $y�	Collapsed=�6?   (   "HASystem.Panels.SystemManagerPanel  5=      -   5          Name$ panelSystemManager��5=   '   $y�	Collapsed=�6C   5>      � 5?   
   $��2��6   .+�5@      �� $7��1东莞莱克软件科技有限公司 版权所有��6   
�������$��Center=�6C   $��Center=�6^   5A      5B      5C      