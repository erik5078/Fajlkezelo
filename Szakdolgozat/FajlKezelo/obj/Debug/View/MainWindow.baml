   M S B A M L   `   `   `  ����  
FajlKezelo    FajlKezelo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Fájlkezelő��5   	   $��751.119��6   $
��1500��6-    � WindowState$ 	Maximized=�6:   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��363* �6   �� 5      $��1129* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
FajlLista1��5      $
��Left=�6'   $��
29,144,0,0q�6B   $	��Top=�6V   $	��349��6n   $	��600��6{      GridLinesVisibility$
 None=�6�      AutoGenerateColumns .     CanUserReorderColumns .     
IsReadOnly .    CanUserResizeColumns . 	   HeadersVisibility$	 Column=�6a  $
 �� IsSynchronizedWithCurrentItem$

 TrueY�6�   �� SelectedItem$ FajlLista1SelectedItem��6�  $��2��6     EnableRowVirtualization .  !   EnableColumnVirtualization .  ��6�   �� *
FajlLista1+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedIndex 6|  �� *KijeloltElem1+   RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5      -   5        �� 5       �� Property � �� �� Value 64   �� *FajlLista1Betuszin+5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Fájlnév��6%      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$	 250 6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   ��  �� Path$ FajlNev�6x     5      $ Kiterjesztés��6%    . $	 100 6t    6N   �� $ Kiterjesztes�6�     5      $ Méret��6%    . $	 100 6f    6G   �� $ Meret�6t     5      $ Dátum��6%    . $	 130 6�    6G   �� $ Datum� �� StringFormat$ {0:yyyy/MM/dd hh:mm}��6�      	CellStyle 5      �� 5      *  $System.Windows.Controls.DataGridCell"  6    �� Triggers 5      �� 5       �� Binding 6&   ��  �� RelativeSource ��   �� AncestorType"   $	 Tag�5      5!      5#        5%   
   -   5        $ 
FajlLista2��5%      $
��Left=�6(   $��364.895,144,0,0q�6C   $	��Top=�6\   $	��349��6t   $	��600��6�   $
 None=�6�    .   .   .  . $	 Column=�6g  $

 TrueY�6�  $��1��6�  $ FajlLista1SelectedItem��6
   .   .  ��6�   �� *
FajlLista2+$ TwoWay=�$ PropertyChanged=� 6�  �� *KijeloltElem2+��5&      �� 5'       5)      �� 5*      "  6   .�5+      -   5        �� 5,       � �� 64   �� *FajlLista2Betuszin+5-       50        51      $ 	Fájlnév��6%    . $	 250 6j    6H   �� $ FajlNev�6x     52      $ Kiterjesztés��6%    . $	 100 6t    6N   �� $ Kiterjesztes�6�     53      $ Méret��6%    . $	 100 6f    6G   �� $ Meret�6t     54      $ Dátum��6%    . $	 130 6�    6G   �� $ Datum�$ {0:yyyy/MM/dd hh:mm}��6�    59      �� 5:      "  6    5;      �� 5<       6&   ��  �� "   $	 Tag�.a�5>      ��  � ��
 �����6;   5?      5A      5C      �� 5E   
   -   5        ! ��Name$! AktualisMappa1��5E      $
��Left=�6}   $��
100,99,0,0q�6�   $	��Top=�6�   $��15(�6�   $	��524��6�   $��2��6�  ��6(   �� *AktualisMappa1+$ TwoWay=�$ PropertyChanged=���6�   �� *AktualisMappa1Kijeloles+$ TwoWay=�$ PropertyChanged=�6�  �� 5F   
   -   5        $! AktualisMappa2��5F      $
��Left=�6}   $��
445,99,0,0q�6�   $	��Top=�6�   $��15(�6�   $	��519��6�   $��1��6�  ��6(   �� *AktualisMappa2+$ TwoWay=�$ PropertyChanged=���6�   �� *AktualisMappa2Kijeloles+$ TwoWay=�$ PropertyChanged=�6�  �� 5G   
   -   5        " ��Name$" 	Meghajto1��5G      $
��Left=�6&   $��
29,109,0,0q�6A   $	��Top=�6U   $��60��6m   # �� Text$
# C://��6x   ��6�   �� $ 	Meghajtok� 6�   �� *	Meghajto1+�� 5H   
   -	   5        $" 	Meghajto2��5H      $
��Left=�6&   $��365,109,0,0q�6A   $	��Top=�6V   $��60��6n   $
# C://��6y   $��1��6�   ��6�   �� *	Meghajtok+ 6�   �� *	Meghajto2+6�   �� 5I   
   -
   5        $ ��Name$$ Kijelol��5I      $��Space Kijelölés��6"   $
��Left=�6<   $��
29,545,0,0q�6W   $	��Top=�6k   $	��176��6�   $��42��6�   $��20(�6�   6�   �� 5J   
   -   5        $$ 	Megnyitas��5J      $��Enter Megnyitás��6$   $
��Left=�6>   $��242,545,0,0q�6Y   $	��Top=�6n   $	��155��6�   $��42��6�   $��20(�6�   $��1��6�   �� 5K   
   -   5        $$ Lomtarba��5K      $��Delete Lomtárba��6#   $
��Left=�6=   $��
29,643,0,0q�6X   $	��Top=�6l   $	��176��6�   $��42��6�   $��20(�6�   6�   �� 5L   
   -   5        $$ Torles��5L      $��Shift + Delete Törlés��6!   $
��Left=�6A   $��280,643,0,0q�6\   $	��Top=�6q   $	��273��6�   $��42��6�   $��20(�6�   $��2��6�   �� 5M   
   -   5        $$ OsszesKijelol��5M      $$��Control + A Összes kilölése��6(   $
��Left=�6N   $��280,545,0,0q�6i   $	��Top=�6~   $	��273��6�   $��42��6�   $��20(�6�   $��2��6�   �� 5N   
   -   5        $$ Masolas��5N      $��F5 Másolás��6"   $
��Left=�67   $��242,643,0,0q�6R   $	��Top=�6g   $	��155��6   $��42��6�   $��20(�6�   $��1��6�   �� 5O   
   -   5        $$ 
Athelyezes��5O      $��F6 Áthelyezés��6%   $
��Left=�6=   $��462,643,0,0q�6X   $	��Top=�6m   $	��158��6�   $��42��6�   $��20(�6�   $��1��6�   5P      5Q      