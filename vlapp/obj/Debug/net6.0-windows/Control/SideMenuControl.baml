   M S B A M L   `   `   `  ????@  <vlapp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:vlapp.Controlvlapp.Control  5      _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aeHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf 5	      #    vlapp.Control.SideMenuControl   5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:vlapp.Control     5      XwpfHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf     5	      -   5          >?Name$
  this??5        ? 	Resources 5
       ?? 
TargetType ?? Property ?? Value ?? 
TargetType ??Name ?? Padding J? SnapsToDevicePixels	 ? ColumnDefinitions
 ?? RelativeSource ?? Mode ?? AncestorType ?? Triggers ?? Binding ?? Value??%?  '??       '??      ?? 5   
   " ??6   .?5      ??  ? ??$ 0q?60   ?? 5       ? ?? ?#ffffff6+   ?? 5       ? ??$ Stretch=?64   ?? 5       ? ??$ Stretch=?62   ?? 5       ?@??SelectionMode$ Single=?6.   ?? 5       ? |?$ False??6*   5      ?? 5   
   " ??6   .?5      ??  ? ?? ?#ffffff6+   ?? 5       ? ??$ 0q?60   ?? 5       ?@??
IsSelected 6+   ?? *IsItemSelected+?? 5       ? ??$ 74??6'   ?? 5       ? ?? 5      ?? 5      " ??6&   .R?5      ?? $ Bd??6!   #??z??6-   #??z??6Y   #??z??6?   # z??6?   $
 true??6?   ._?5&      ? 	 5'   "   ?? 5(   &   $??5 ?67   ?? 5)   &   $??50 ?67   ?? 5*   &   $
??Auto ?67   .+?5.   "   ?? ??5/   &   ?? 50   *   " ??60   .?51   .   ??  ? ?? ?#6200ee6K   ?? 54   .    ? y?$ Hidden=?6K   ?? 57   .    ? ?? 6G   ?? *Height+
 ?? $ FindAncestor=?
 Border?? 59   .    ? ??$ 5??6F   ?? 5:   .    ? |?$ False??6J   ?? 5;   .    ? ??$
 Left=?6T    5>   .   ?? 5@   2    6>   ?? *IsMouseOver+
 ?? $ FindAncestor=? ListBoxItem True6?   .a?5A   6   ??  ? y?$ Visible=?6S   5B   3   ?? 5C   2    6>   ?? *
IsSelected+
 ?? $ FindAncestor=? ListBoxItem True6?   .a?5D   6   ??  ? y?$ Visible=?6S   5E   3   5H   +   5J   #   ?? 5t   "   $??2??6,   ??5u   &   ?? 5v   *   " ??60   .?5w   .   ??  ? ??$ Stretch=?6T   ?? 5x   .    ? ??$ Center=?6R   ?? 5y   .    ? ??
 ?   ?6K   ?? 5z   .    ? ?? 6E   ?? *
MenuText+5{   +   5}   #   5      5?      5?      5?      5?      5?      .??5?      ?? -   5         ??Name$ 	MyListBox??5?      $??1??6   ??6.   ?? *
ItemList+ ?? SelectedIndex 6O   ?? *SelectedIndex+ ?? ElementName$
 this??5?      5?      