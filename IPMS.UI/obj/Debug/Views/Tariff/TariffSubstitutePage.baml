   M S B A M L   `   `   `  ����[  WInternetProviderManagementStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Ipms.UI.Views.TariffIpms.UI.Views.Tariff  5      /    )Ipms.UI.Views.Tariff.TariffSubstitutePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Ipms.UI.Views.Tariff     5        N� 
Background
  ����� N� Title$ TariffSubstituteView��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      $	��100 �6   �� 5      $��30 �6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      $��1* �6   .+�5   
   �� $/��)Setect another tariff plan for substitute��6   $��0��6D   $��0��6Q   �� 5   
   $��1��6   $	��5,5q�6    $��0��6�   ��6-   �� *SubstituteItems+ �� SelectedItem 6U   �� *SelectedSubstituteItem+��5      �� 5      .R�5      �� ��6   ��  �� Path$
 Name�5      5      �� 5    
   $��1��6   $��1��6!   $	��5,5q�6.   $
��Save��6;   ��6J   �� *ArchiveTariffCommand+�� 5!   
   $��12(�6   $��2��6   $��0��6/   $	��✕��6=   ��6I   �� *CloseCustomPageCommand+5$      5%      