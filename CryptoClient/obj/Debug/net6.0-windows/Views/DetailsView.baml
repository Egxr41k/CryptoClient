   M S B A M L   `   `   `  ����G  CCryptoClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CryptoClient.ViewsCryptoClient.Views  5      _ [Syncfusion.SfChart.WPF, Version=22.1.39.0, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts 5      $    CryptoClient.Views.DetailsView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:CryptoClient.Views     5      ^
syncfusionGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF     5        � 	Resources  5
       �� 
TargetType �� Property �� Value �� 
TargetType��%�  '��       �� 5   
   " ��6   �5      �� 5       � ��$ 0q�64   �� 5       � �� �#0006/   �� 5       � ��
 ���� 6/   �� 5       � ��$ 50��6+   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� #��z��6%   $��1q�6P   
���   �._�5   "   �� #��z��5   %   $��Center=�5   %   $��Center=�5   %   5      5      5      5      5      .��5       �  � RowDefinitions 5!   
   �� 5"      $
��auto �6   �� 5#       � ColumnDefinitions 5%   
   �� 5&      $	��300 �6   �� 5'      �� 5(      $	��150 �6   .+�5*   
   �� $��36(�6+   ��6   �� *Title+6+   �� 5,   
   $��1��6   .+�5-      �� $��20(�5/      $��1��50      
���   � � ToolTip$( "Input crypto currency name or code��52      ��5.      �� *TextBoxContent+ �� Mode$ TwoWay=�52      �� 53      $��16(�6   $
��Wrap=�6@   $��0,10,0,0q�6U   ��6&   �� *
ErrorMsg+6U   54      �� 56   
   $��1��6   .+�57      �� $��24(�6   $��10q�6=   ��6&   �� *Price+6=   �� 58      $��24(�6   $��10q�6E   ��6&   �� *ChangePercent+��6Q   �� *Color+�� 59      $��12(�6   $��10q�6<   ��6&   �� *Link+�� 5:      ��6   �� *Link+5;      &   System.Windows.Controls.DataGrid  5>      	   AutoGenerateColumns$	 False��67   ��6   �� *	Markets+
   Columns
 5?      0  *System.Windows.Controls.DataGridTextColumn  5@      ,  &System.Windows.Controls.DataGridColumn   Header$ Market��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Key+  5A      $ Price��6)    68   �� *Value+5C      5E      �� 5G   
   $��2��5H      $��Serach��5I      
������ $
��20,0q�5K   &   ��5J      �� *SearchCommand+5K   &   '  !Syncfusion.UI.Xaml.Charts.SfChart  5M   
   )  #Syncfusion.UI.Xaml.Charts.ChartBase   Header$ Coast increase per year��5N      $��1��5O      $��1��5P      $��2��5Q         PrimaryAxis 5S      ,  &Syncfusion.UI.Xaml.Charts.CategoryAxis  5T      )  #Syncfusion.UI.Xaml.Charts.ChartAxis   Header$
 time��6*   $��14(�68      SecondaryAxis 5X      -	  'Syncfusion.UI.Xaml.Charts.NumericalAxis	  5Y      $ price��6+   $��14(�6:      Series. 5\      ,
  &Syncfusion.UI.Xaml.Charts.ColumnSeries
  /  )Syncfusion.UI.Xaml.Charts.ChartSeriesBase   Spacing$	 0.6Q�5]         XBindingPath$	 Key��5_      ,  &Syncfusion.UI.Xaml.Charts.XyDataSeries   YBindingPath$ Value��5`         ShowTooltip$
 True��5a         ItemsSource 5^      �� *	ChartData+5a      5b      5d      5e      