   M S B A M L   `   `   `  ����[  WInternetProviderManagementStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Ipms.UI.Views.TariffIpms.UI.Views.Tariff  5      +    %Ipms.UI.Views.Tariff.CreateTariffPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Ipms.UI.Views.Tariff     5        N� 
Background
  ����� N� Title$ CreateTariffPage��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��209* �6   �� 5      $	��52* �6   �� 5      $	��52* �6   �� 5      $
��104* �6   �� 5      $	��53* �6   �� 5      $	��75* �6   �� 5      $��30 �6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��40 �6   �� 5      .+�5   
   �� $��Insert desired values��6   $��0��61   $��0��6A   $��6��6N   $��1��6b   �� 5   
   $	��5,5q�6   $��0��6g   $��2��6w   ��6   ��  �� Path$ NewItem.Name� �� UpdateSourceTrigger$ PropertyChanged=�6w   �� 5   
   $	��5,5q�6   $��1��6p   $��2��6�   ��6   �� $ NewItem.DownloadSpeed�$ PropertyChanged=�6�   �� 5   
   $	��5,5q�6   $��2��6n   $��2��6~   ��6   �� $ NewItem.UploadSpeed�$ PropertyChanged=�6~   �� 5    
   $	��5,5q�6   $��3��6h   $��2��6x   ��6   �� $ NewItem.Price�$ PropertyChanged=�6x   �� 5!   
   $��4��6@   $	��5,5q�6P   $��Center=�6]   $��Center=�6   $��2��6�    d� 	IsChecked 6   �� $ NewItem.IsArchive�6�   �� 5"   
   $	��5,5q�6   $��Create��6   $��5��6Z   $��2��6j   ��6/   �� *EndCreateTariffCommand+6j   �� 5#   
   $
��Name��6   $��1��6   $��0��6,   �� 5$   
   $��D. speed��6   $��1��6#   $��1��60   �� 5%   
   $��U. speed��6   $��1��6#   $��2��60   �� 5&   
   $��Price��6   $��1��6    $��3��6-   �� 5'   
   $��Archive��6   $��1��6"   $��4��6/   �� 5(   
   $��12(�6   $��6��6   $��0��6/   $	��✕��6=   ��6I   �� *CloseCustomPageCommand+5)      5*      