   M S B A M L   `   `   `  ����R  NSupermarket Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Supermarket_Application.ViewModels"Supermarket_Application.ViewModels  5   	   3    -Supermarket_Application.Views.ViewReceiptView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:Supermarket_Application.ViewModels     5   	     � Title$  ViewReceiptView��5   	   $	��450��6!   $	��800��6.    � DataContext 5
      9   3Supermarket_Application.ViewModels.ReceiptViewModel  5   
   .��5      � .+�5   
   �� ���	#FFDD2343$��Italic$�6L   $��Cascadia Mono)�6_   ��6   �� *
Receipts+��5      �� 5      .R�5      �� .+�5      9�  9� 
IsExpanded .  ��6#   �� *
DateIssued+.��5      �� ��6'   �� *ReceiptDetails+ �� View 5   "   �� 5   &   .P�5   *   �� $��
Product ID��69    �� Width$	 100��6x    �� DisplayMemberBinding 6M   �� *	ProductID+6x   �� 5   *   $��Quantity��69   $	 100��6u    6K   �� *
Quantity+6u   �� 5   *   $��Subtotal��69   $	 100��6u    6K   �� *
Subtotal+6u   5   '   5      5      �� 5      $��16(�5   $   $
��Bold"�62   
���  ��$��Right=�5    $   $��10q�6@   ��5   $   �� *TotalAmount+ �� StringFormat$ Total: {0:C}��5    @   5!      5"      5$      5%      5&      