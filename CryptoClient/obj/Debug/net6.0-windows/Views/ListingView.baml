   M S B A M L   `   `   `  ����G  CCryptoClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CryptoClient.ViewsCryptoClient.Views  5      $    CryptoClient.Views.ListingView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:CryptoClient.Views     5      .��5	      � 
�������  � RowDefinitions  5
   
   �� 5   
   $
��auto �6   �� 5   
   .+�5   
   �� .+�5      �� $.��(List of most expensive cryptocurrencies:��5      $��12(�5      $��0, 10q�5      5      �� 5   
   
�������$��0q�5      $��1��6!   ��5      �� *
CryptoList+ �� SelectedItem 5      �� *SelectedListingItemViewModel+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�5      $��50��5      .+�5      �� $��30��6$   $��Center=�5      $��12(�5       $��	0,0,100,0q�6+   ��5!      �� *CurrencyName+�� 5"      $��Center=�5#      $��12(�5$      ��5%      �� *CurrencyPercent+��5&      �� *Color+5'      5(      5*      5+      5,      