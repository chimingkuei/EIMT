   M S B A M L   `   `   `  ���� I  EOpticalScaling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:OpticalScalingOpticalScaling      OpticalScaling.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  (localclr-namespace:OpticalScaling  -     � 
ResizeMode$  CanMinimize=� � Title$ OpticalScaling��$	��700��$
��1000�����	#FF3E6FB3 � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$	 Log��$
��Left=�$	��172��$��	5,490,0,0q�$	��Top=�$	��790�����	#FFE6F0FF � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       'J� �     �� " ��.��� # �@��VerticalScrollBarVisibility$
 Auto=���  � ��$ 0q���  �@��
IsReadOnly . ��  � ��$ Bottom=��� " J�.���  �@��Margin$ 0q���  � ��$ 16(�.@��� $��一般��   BaseTabcontrol#��[  .���� -    ��Name$ richTextBoxGeneral�����	#FF43B5BF�� $��警告��#��[  .���� -   $ richTextBoxWarning�����	#FF43AABF�� $��錯誤��#��[  .���� -   $ richTextBoxError�����	#FF43A0BF�� $��除錯��#��[  .���� -   $ richTextBoxDebug�����	#FF4396BF�� $
��Left=�$��5,8,0,0q�$	��Top=�$	��478��$	��978�����	#FFE6F0FF.@��� $��	主畫面��#��[  .��� .+�� 	 � Orientation$	 
Horizontal=�$��82,26,66,250q�.+��� -   
 ��Name$
 Open_Source_Folder��  
BaseButton#��[ $��開啟影像資料夾��$��18(����	#FF3E6FB3$	��150��$��40��$��	50,30,0,0q��� -    ��Name$ Source_Folder��  BaseTextBox1#��[ $
��Left=� �� TextWrapping$
 Wrap=�$	��Top=�$��30��$	��532��$��	40,33,0,0q�$��Center=�$��Center=��� -	   $
 Open_Output_Folder��#��[ $��開啟輸出資料夾��$��18(����	#FF3E6FB3$	��150��$��40��$��	50,30,0,0q��� -
   $ Output_Folder��  BaseTextBox2#��[ $
��Left=�$
 Wrap=�$	��Top=�$��30��$	��532��$��	40,33,0,0q�$��Center=�$��Center=�� $	 
Horizontal=�$��242,174,191,70q�.+��� -    ��Name$ Source_OR_Label��$��原始光學解析度:��$��18(�$��Center=�$��Center=�$��	50,20,0,0q�$  Xceed.Wpf.Toolkit.DoubleUpDown  -     Name$ 	Source_OR���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X��  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	Increment$
 0.01X�,  &Xceed.Wpf.Toolkit.Primitives.InputBase   TextAlignment$ Center=�$��20(�$	��100��$��40��$��	50,20,0,0q�	� -    	�Name$ Color��  BaseRadioButton#��[ $��	50,20,0,0q�$��18(�$��25��$��Center=�$��Center=�.��彩色�� -   $ Output_OR_Label��$��輸出光學解析度:��$��18(�$��Center=�$��Center=�$��	50,20,0,0q�  -   $ 	Output_OR��$ 0X�$
 0.01X�$ Center=�$��20(�$	��100��$��40��$��	50,20,0,0q�	� -   $
 Gray��#��[ $��	50,20,0,0q�$��18(�$��25��$��Center=�$��Center=�.��黑白�� -   $ Thread_Num_Label��$��多執行緒數量:��$��18(�$��Center=�$��Center=�$��	68,20,0,0q�%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$ 
Thread_Num���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 1X�   Maximum$ 10X�$ Center=�$��20(�$	��100��$��40��$��	50,20,0,0q��� -   $
 
OR_Scaling��#��[ $��254,382,568,24q�$��縮放光學解析度��$��18(����	#FF3E6FB3$	��150��$��40���� -   $
 Save_Config��#��[ $��592,382,230,24q�$��儲存參數��$��18(����	#FF3E6FB3$	��150��$��40��