   M S B A M L   `   `   `  ����A  =AMONIC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AMONICAMONIC  5   	       AMONIC.AdminMainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:AMONIC     5   	     � Title$'  !AMONIC Airlines Automation System��5   	   $	��450��63   $	��800��6@   $	��450��6L   $	��800��6\    � Icon$  /Image/logo_color_blue.png��5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      �� 5      $��50 �6   .+�5   
   �� $
��Left=�6    �� Orientation$ 
Horizontal=�60   $��15,0,0,0q�6I   .+�5      �� -   5        $��20��5      $��Add user��67   $	��100��6J   �� 5      -   5        $��20��5      $
��Exit��6!   $	��100��6B   $��15,0,0,0q�6N   5      �� 5   
   $ 
Horizontal=�6   $��1��6.   $��15,0,0,0q�6;   $��28��6M   .+�5      �� $��Office��6   $��Center=�6&   �� 5      -   5         ��Name$ ComboBoxType��5   G   $	��100��6]   $��15,0,0,0q�6i    �� DisplayMemberPath$ Title��6{    �� SelectedValuePath$ ID��6�   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataGridUsersList��5         AutoGenerateColumns .  $��2��6J   $��15q�6W   	   	CellStyle	 5      �� 5      
 �� 
TargetType*  $System.Windows.Controls.DataGridCell"
  6    �� Triggers 5      �� 5        �� Binding 6&   �� *RoleID+ �� Value 16A   .a�5!      ��  �� Property � �� �� Value
 � ���6;   5"      �� 5#       6&   �� *Active+	 False6A   .a�5$      ��  � ��
 �  ��6;   5%      5'         Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 67      Header$ 
Логин��6A   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6P   �� *Email+  5,       . $ * 67   $ Имя��6A    6N   �� *
LastName+  5-       . $ * 67   $ Фамилия��6A    6R   �� *	FirstName+  5/       . $ * 67   $ Офис��6A    6O   �� *Offices.Title+  50       . $ * 67   $ Роль��6A    6O   �� *Roles.Title+5;      �� 5=   
   $��3��6   $ 
Horizontal=�6"   .+�5>      �� -   5        $��Change Role��5>   4   $��Center=�6J   $��15,0,0,0q�6g   $��Center=�6y   $��30��6�   $	��200��6�   �� 5?      -   5        $��Enable/Disable Login��5?   (   $��15,0,0,0q�6G   $��Center=�6Y   $��Center=�6v   $��30��6�   $	��200��6�   5@      5B      5C      