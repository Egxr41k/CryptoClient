   M S B A M L   `   `   `  ����G  CCryptoClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CryptoClientCryptoClient  5      7 clr-namespace:CryptoClient.ViewsCryptoClient.Views  5      )    #CryptoClient.Views.CryptoClientView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:CryptoClient     5      .v clr-namespace:CryptoClient.Views     5        � 	Resources  5
       �� 
TargetType �� Property �� Value �� 
TargetType��%�  '��       �� 5   
   " ��6   �5      �� 5       � ��$ 0q�64   �� 5       � �� �#0006/   �� 5       � ��
 ���� 6/   �� 5       � ��$ 50��6+   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� #��z��6%   ._�5   "   �� #��z��5   %   $
��Left=�5   %   $��Center=�5   %   $��20, 0, 0, 0q�5   %   5      5      5      5      5       .��5!      �  � RowDefinitions 5"   
   �� 5#      $��50 �6   �� 5$      $	��100 �6   �� 5%       � ColumnDefinitions 5(   
   �� 5)      $	��220 �6   �� 5*      .+�5-   
   �� 
�������.+�5.      �� $��5q�50      $��20(�51      $��Center=�52         DarkText#���   53      ��5/      �� *	AppName+53      �� 54      $��Egxr41k software��55      $��12(�56      $��Center=�57      #���   58      59      �� 5;   
   
�������$��1��6,   .+�5<      �� $
��Home��6   ��6$   �� *HomeViewCommand+�� 5=      $��Convert��6   ��6'   �� *ConvertViewCommand+5>      $   CryptoClient.Views.ListingView  5G   
   $��2��6    � DataContext 6%   �� *	ListingVM+�� 5I   
   $��1��5J      $��3��5K      $��10q�5L      ��5M      �� *CurrentView+5R      5S      