   M S B A M L   `   `   `  ����B  >WpfChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfChat.ConvertersWpfChat.Converters  5      7 clr-namespace:WpfChat.ConvertersWpfChat.Converters  5          WpfChat.Pages.ChatPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6	converter clr-namespace:WpfChat.Converters     5      2local clr-namespace:WpfChat.Converters     5        N� 
Background
  ����� N� Title$ ChatPage��5       � 	Resources 5      8  2WpfChat.Converters.GonderenAdiToAlignmentConverter   GonderenToAlignment0  *WpfChat.Converters.CountToEnabledConverter  CountToEnabled�� 5   
   %:   &	        &	        5      68     5      60   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   6,   �� 5      $��5* �6   6,   .+�5   
   �� $��0��6   $
��Auto=�6(   ���#b6b8c3.��5      �� .+�5      �� $��Online Kullanıcılar��6   $
��Wrap=�63   $��Center=�6G    �� Padding$ 	0,10,0,10q�6d   �� 5      
���   �$��1q�6-   $��Stretch=�6A   �� 5      ��6   �� *BaglananlarListesi+��5      �� 5      .R�5       ]� $��5q�6$   .+�5!      A� $��15��6&   $��15��61   
��� � �6=   �� 5"      ��6$   �� 5#      5$      5&      5'      5(      �� 5+   
   -   5        $��1��5+      $��0,0,0,30q�6D   ���#e8eaf6.��5,      �� ��6   �� *GonderiList+��5-      �� 5.      .R�5/      ��  ��Name$ mBorder��6!   
���   �$��0q�6F   $��
10,5,10,10q�6�   ��6[   �� *
Gonderen+ �� 	Converter# [  ._�50      �� .+�51   "   � ��52   &   �� 53   *    �� 
TargetType" �60    �� Triggers 54   .   �� 55   2   	 �� Binding	 6>   �� 
 �� ElementName$
 mBorder�� �� Path$ HorizontalAlignment� �� Value	 Right6�   .a�56   6   ��  �� Property � �� �� Value$ 0��6O   6[   57   3   59   +   .+�5;   &   �� $��	Underline{�6K   $��Center=�6g   ��60   �� *
Gonderen+6g   5<   #   �� 5=   "   mesajTextBlock��$
��auto��6C   $
��Wrap=�6Q   $	��150��6}   $ 5q�6�   ��6e   �� *Ileti+��5>   &   �� 5?   *   " ��60    5@   .   �� 5A   2   	 6>   �� $
 mBorder��$ HorizontalAlignment�	 Right6�   .a�5B   6   ��  � ��
 �����6S   5C   3   5E   +   5G   #   5H      ��5I      �� 5J   "   " ��6(   .�5K   &   ��  �@��CornerRadius$ 8,8,8,0��6E   �� 5L   &    � �� �#d6d5d56C    5N   &   P� 5O   *    P� Property � �� P� Value$ Right=�6Q   . �5P   .   ��  �@��CornerRadius$ 8,8,0,8��6M   �� 5Q   .    � �� �#1e8dff6K   5S   +   5U   #   5X      5Y      5[      5\      ]� 5]   
   $��1��6   $��Bottom=�6$   ��. .+�5_      �� $��Gönder��6   $��80��6&   $��Right=�6Z   $��0,0,5,10q�6q   ��61   �� $ GonderCommand���5`      �� 5a      " ��6    5b      �� 5c      	 6*   �� $
 mMessageContent��$ Text.Length� 06m   .a�5d   "   ��  � {� .  6>   5e      5g      5i      �� 5j      -   5         ��Name$ mMessageContent��5j      $
��auto��6]   $
��Left=�6j   $��0,0,0,10q�6�   ��6/   �� $ 	TextIleti� �� Mode$ TwoWay=�6�   5l      5n      5o      