   M S B A M L   `   `   `  ����   
FajlKezelo    FajlKezelo.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Fájlkezelő��$��751.119��$
��1500�� � WindowState$ 	Maximized=�.���  � ColumnDefinitions �� $
��363* ��� $��1129* �.+�&   System.Windows.Controls.DataGrid  -     Name$ 
FajlLista1��$
��Left=�$��
29,144,0,0q�$	��Top=�$	��349��$	��600��   GridLinesVisibility$
 None=�   AutoGenerateColumns .     CanUserReorderColumns .     
IsReadOnly .    CanUserResizeColumns . 	   HeadersVisibility$	 Column=�$
 �� IsSynchronizedWithCurrentItem$

 TrueY� �� SelectedItem$ FajlLista1SelectedItem��$��2��   EnableRowVirtualization .  !   EnableColumnVirtualization .  ���� *
FajlLista1+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedIndex �� *KijeloltElem1+   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .�-   ��  �� Property � �� �� Value �� *FajlLista1Betuszin+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Fájlnév��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$	 250 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ FajlNev�  $ Kiterjesztés�� . $	 100  �� $ Kiterjesztes�  $ Méret�� . $	 100  �� $ Meret�  $ Dátum�� . $	 130  �� $ Datum� �� StringFormat$ {0:yyyy/MM/dd hh:mm}��   	CellStyle �� *  $System.Windows.Controls.DataGridCell"   �� Triggers ��  �� Binding ��  �� RelativeSource ��   �� AncestorType"   $	 Tag�  -   $ 
FajlLista2��$
��Left=�$��364.895,144,0,0q�$	��Top=�$	��349��$	��600��$
 None=� .   .   .  . $	 Column=�$

 TrueY�$��1��$ FajlLista1SelectedItem�� .   .  ���� *
FajlLista2+$ TwoWay=�$ PropertyChanged=� �� *KijeloltElem2+����  �� "  .�-   ��  � �� �� *FajlLista2Betuszin+   $ 	Fájlnév�� . $	 250  �� $ FajlNev�  $ Kiterjesztés�� . $	 100  �� $ Kiterjesztes�  $ Méret�� . $	 100  �� $ Meret�  $ Dátum�� . $	 130  �� $ Datum�$ {0:yyyy/MM/dd hh:mm}�� �� "   ��  ��  �� "   $	 Tag�.a���  � ��
 ������� -   ! ��Name$! AktualisMappa1��$
��Left=�$��
100,99,0,0q�$	��Top=�$��15(�$	��524��$��2������ *AktualisMappa1+$ TwoWay=�$ PropertyChanged=����� *AktualisMappa1Kijeloles+$ TwoWay=�$ PropertyChanged=��� -   $! AktualisMappa2��$
��Left=�$��
445,99,0,0q�$	��Top=�$��15(�$	��519��$��1������ *AktualisMappa2+$ TwoWay=�$ PropertyChanged=����� *AktualisMappa2Kijeloles+$ TwoWay=�$ PropertyChanged=��� -   " ��Name$" 	Meghajto1��$
��Left=�$��
29,109,0,0q�$	��Top=�$��60��# �� Text$
# C://������ $ 	Meghajtok� �� *	Meghajto1+�� -	   $" 	Meghajto2��$
��Left=�$��365,109,0,0q�$	��Top=�$��60��$
# C://��$��1������ *	Meghajtok+ �� *	Meghajto2+�� -
   $ ��Name$$ Kijelol��$��Space Kijelölés��$
��Left=�$��
29,545,0,0q�$	��Top=�$	��176��$��42��$��20(��� -   $$ 	Megnyitas��$��Enter Megnyitás��$
��Left=�$��242,545,0,0q�$	��Top=�$	��155��$��42��$��20(�$��1���� -   $$ Lomtarba��$��Delete Lomtárba��$
��Left=�$��
29,643,0,0q�$	��Top=�$	��176��$��42��$��20(��� -   $$ Torles��$��Shift + Delete Törlés��$
��Left=�$��280,643,0,0q�$	��Top=�$	��273��$��42��$��20(�$��2���� -   $$ OsszesKijelol��$$��Control + A Összes kilölése��$
��Left=�$��280,545,0,0q�$	��Top=�$	��273��$��42��$��20(�$��2���� -   $$ Masolas��$��F5 Másolás��$
��Left=�$��242,643,0,0q�$	��Top=�$	��155��$��42��$��20(�$��1���� -   $$ 
Athelyezes��$��F6 Áthelyezés��$
��Left=�$��462,643,0,0q�$	��Top=�$	��158��$��42��$��20(�$��1��