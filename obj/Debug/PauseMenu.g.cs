﻿

#pragma checksum "C:\Users\Jordan\Desktop\graphicsgroup-airhockey\PauseMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D428752CBF676F359E622D6EE78EEDC"
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
    partial class PauseMenu : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 11 "..\..\PauseMenu.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ResumeGame;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 12 "..\..\PauseMenu.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.RestartGame;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 13 "..\..\PauseMenu.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoMainMenu;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

