﻿

#pragma checksum "C:\Users\Jordan\Desktop\graphicsgroup-airhockey\Options.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "21E68E84A1C436FEFF39964A3A566702"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project
{
    partial class Options : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 12 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 14 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Unchecked += this.CheckBox_UnChecked;
                 #line default
                 #line hidden
                #line 14 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Checked += this.CheckBox_Checked;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 16 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.Slider_ValueChanged;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 18 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.AISlider_ValueChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 20 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.PuckSlider_ValueChanged;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 22 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.FrictionSlider_ValueChanged;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 24 "..\..\Options.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.WinSlider_ValueChanged;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


