   M S B A M L   `   `   `  ����G  CCryptoClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CryptoClient.ViewsCryptoClient.Views  5      $    CryptoClient.Views.ConvertView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:CryptoClient.Views     5        � 	Resources  5	       �� 
TargetType �� Property �� Value �� 
TargetType��%�  '��       �� 5
   
   " ��6   �5      �� 5       � ��$ 0q�64   �� 5       � �� �#0006/   �� 5       � ��
 ���� 6/   �� 5       � ��$ 50��6+   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� #��z��6%   $��1q�6P   
���   �._�5   "   �� #��z��5   %   $��Center=�5   %   $��Center=�5   %   5      5      5      5      5      .��5      �  � ColumnDefinitions 5    
   �� 5!      �� 5"      $	��120 �6   �� 5#       � RowDefinitions 5%   
   �� 5&      �� 5'      $
��auto �6   �� 5(      .+�5*   
   �� $@��:Convert one currency to another, by picking them from list��6   $��20q�6V   $��32(�6b   $
��Wrap=�6p   �� 5,   
   $��0��6   $��1��6%    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5-      �� 
������ $	��100��50      $��20(�51      
���   �$��Center=�53      ��5.      �� *Count+ �� Mode$ TwoWay=�53      �� 54      $��0q�55      
���   �
�������$	��200��58      $��20(�59      $��Center=�5<      	 �� SelectedItem	 5:      �� *FirstCurrency+��5;      �� *AllowedCurrencies+��5=      �� 5>      .R�5?      �� $ 
Horizontal=�6%   .+�5@      �� $��Center=�5A   !   $��12(�5B   !   $��	0,0,100,0q�6/   ��5C   !   �� 
 �� Path$

 Name�5D      5E      5G      5H      �� 5J   
   $��2��6   $��1��6%   $ 
Horizontal=�62   .+�5K      �� 
���   �$��1q�6)   ._�5L      �� $��20(�5N      $	��100��5O      $��Center=�5P      ��5M      �� *Result+5P      5Q      �� 5S      $	��200��5T      $��20(�5W      $��0q�5X      $��Center=�5Y      	 5U      �� *SecondCurrency+��5V      �� *AllowedCurrencies+��5Z      �� 5[      .R�5\      �� $ 
Horizontal=�6%   .+�5]      �� $��Center=�5^   !   $��12(�5_   !   $��	0,0,100,0q�6/   ��5`   !   �� $

 Name�5a      5b      5d      5e      �� 5f   
   $��1��6   $��2��6%   .+�5g      �� $��16(�6   $
��Wrap=�6@   
���  ��$��0,20,0,0q�6e   ��6&   �� *
ErrorMsg+6e   �� 5h      $��50��6   $	��Top=�6!   $��0,20,0,0q�69   $��Convert��6K   ��6]   �� *ConvertCommand+5i      5m      5n      