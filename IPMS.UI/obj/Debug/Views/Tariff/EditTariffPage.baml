   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics  5      [ WInternetProviderManagementStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Ipms.UI.Views.TariffIpms.UI.Views.Tariff 5      )   #Ipms.UI.Views.Tariff.EditTariffPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase      5      4local"clr-namespace:Ipms.UI.Views.Tariff      5        N� 
Background
  ����� N� Title$ 
TariffView��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��228* �6   �� 5      $	��70* �6   �� 5      $	��75* �6   �� 5      $
��114* �6   �� 5      $	��83* �6   �� 5      $��30 �6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��40 �6   �� 5      .+�5   
   �� $��Chage desired values��6   $��0��6/   $��0��6?   $��5��6L   $��1��6`   �� 5   
   $��5q�6   $��2��6j   ��6   ��  �� Path$ SelectedItem.Name� �� UpdateSourceTrigger$ PropertyChanged=�6j   �� 5   
   $	��5,5q�6   $��1��6u   $��2��6�   ��6   �� $  SelectedItem.DownloadSpeed�$ PropertyChanged=�6�   �� 5   
   $	��5,5q�6   $��2��6s   $��2��6�   ��6   �� $ SelectedItem.UploadSpeed�$ PropertyChanged=�6�   �� 5    
   $	��5,5q�6   $��3��6m   $��2��6}   ��6   �� $ SelectedItem.Price�$ PropertyChanged=�6}   �� 5!   
   $	��5,5q�6   $
��Save��6   $��4��6S   $��2��6c   ��6-   �� *EditTariffCommand+6c   �� 5"   
   $
��Name��6   $��1��6   $��5,0,0,0q�6,   �� 5#   
   $��D. speed��6   $��1��6#   $��1��60   �� 5$   
   $��U. speed��6   $��1��6#   $��2��60   �� 5%   
   $��Price��6   $��1��6    $��3��6-   �� 5&   
   $��12(�6   $��5��6   $��0��6/   $	��✕��6=   ��6I   �� *CloseCustomPageCommand+5'      5(      