   M S B A M L   `   `   `  ����[  WInternetProviderManagementStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Ipms.UI.Views.MainIpms.UI.Views.Main  5   	   #    Ipms.UI.Views.Main.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Ipms.UI.Views.Main     5   	     � Icon$4  .pack://application:,,,/Resources/main_icon.png��5   	    � Title$) #Internet Provider Management Studio��5	   	   $	��720��65   $
��1280��6B    � WindowStartupLocation$ CenterScreen=�6O   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��227* �6   �� 5      $��5 �6   �� 5      $��1028* �6    � RowDefinitions 5   
   �� 5      $
��120* �6   �� 5      $��5 �6   �� 5      $
��400* �6   �� 5      $��5 �6   �� 5      $
��159* �6   .+�5   
   �� $��Areas��6   $��0��6    $��0��6-   $��30��6=   $	��Top=�6I   � 5   
   $��0��6   $��0��6!   $��0,30,0,0q�61   .+�5      �� $��Customer��6   $	��5,5q�6}   $	��Top=�6�   ��6(   �� *ChangeViewModelCommand+��6S   �� *CustomerArea+6�   �� 5      $��House��6   $	��5,5q�6�   $	��Top=�6�   ��6&   �� *ChangeViewModelCommand+��6Q   �� *ConnectedHouseArea+6�   �� 5      $��Tariff��6   $	��5,5q�6z   $	��Top=�6�   ��6'   �� *ChangeViewModelCommand+��6R   �� *
TariffArea+6�   �� 5      $��
Action log��6   $	��5,5q�6�   $	��Top=�6�   ��6+   �� *ChangeViewModelCommand+��6V   �� *ActionLogArea+6�   5      �� 5   
   $��1��6   $��0��6$   $��Stretch=�64   $��Stretch=�6R   �� 5   
   $��0��6   $��3��6$   $��1��65   $��Stretch=�6E   $��Stretch=�6c   �� 5    
   $��3��6   $��0��6$   $��3��64   $��Stretch=�6H   $��Stretch=�6f   �� 5!   
   $��Actions��6   $��2��6"   $��0��6/   $��30��6?   $	��Top=�6K   �� 5"   
   $��5,30,5,5q�6   $��2��6)   $��0��66   ��6F   �� *ActionButtons+��5#      �� 5$      .R�5%      �� 5&      5(       � 5)   
   $	��5,5q�6   $��0��6   $��3��6*   $��2��6;     � NavigationUIVisibility$ Hidden=�6h   ��6K   �� *
ViewPage+6h    � 5*   
   $��10,10q�6   $��4��6   $��0��6,   $��3��6<   $ Hidden=�6o   ��6P   �� *
CustomPage+6o   5-      5.      