   M S B A M L   `   `   `  ����A  =AMONIC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AMONICAMONIC  5   	       AMONIC.UserMainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:AMONIC     5   	     � Title$'  !AMONIC Airlines Automation System��5   	   $	��450��63   $	��800��6@   $	��450��6L   $	��800��6\    � Icon$  /Image/logo_color_blue.png��5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      �� 5      $��50 �6   .+�5   
   �� $
��Left=�6    �� Orientation$ 
Horizontal=�60   $��15,0,0,0q�6I   .+�5      �� -   5        $��20��5      $
��Exit��6!   $	��100��6H   $��15,0,0,0q�6T   5      �� 5   
   $ Vertical=�6   $��1��6,   $��15,0,0,0q�69   .+�5      �� -   5         ��Name$ TextBlockHello��5      $��Center=�60   �� 5      -   5        $ TextBlockCountCrashes��5      $��Center=�67   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataGridLogUsers��5         AutoGenerateColumns .  $��2��6I   $��15q�6V      	CellStyle 5      �� 5       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   	 �� Triggers	 5      �� 5       
 �� Binding
 6&   �� *TYPE+ �� Value WARNING6?   .a�5!      ��  �� Property � �� �� Value
 �  ��6;   5"      5$         Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 67      Header$
 Date��6A   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6O   �� *
DataTime+  5*       . $ * 67   $ 	LoginTime��6A    6T   �� *
DataTime+  5+       . $ * 67   $! Тип активности��6A    6Y   �� *TYPE+  5-       . $ * 67   $ Описание��6A    6S   �� *description+50      52      53      