   M S B A M L   `   `   `  ����[  WInternetProviderManagementStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Ipms.UI.Views.SharedIpms.UI.Views.Shared  5      #    Ipms.UI.Views.Shared.ViewPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Ipms.UI.Views.Shared     5        N� Title$  ViewPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��40 �6   �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGrid��5   !   $��14(�6      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . $��2��6�   ��61   �� *Items+ �� SelectedItem 6O   �� *SelectedItem+��5      �� 5         	Color_000 �� Color# �   6"      HorizontalGridLinesBrush 5      �� 5      # �   6"   	   VerticalGridLinesBrush	 5      �� 5      # �   6"   5      �� 5   
   $��1��6   $��5,5,0,5q�6   $
��Left=�6~   $	��160��6�   ��60   �� *SearchString+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Delay$	 250��6�   �� 5   
   $��1��6   $��	170,5,0,5q�6    $
��Left=�6�   $	��140��6�    63   �� *SelectedSearchColumn+��6a   �� *SearchColumns+6�   �� 5   
   $��1��6   $	��5,5q�6   $��Refresh��6+   $��Right=�6`   $��90��6|   ��6=   �� *RefreshCommand+6|   �� 5   
   $��Search text��6   $
��Left=�6&   $��88��6A   $��5,0,0,0q�6L   �� 5    
   $��Search column��6   $��	170,0,0,0q�6(   $
��Left=�6;   $	��103��6V   5!      5"      