using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using SharpDX;

namespace Project
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PauseMenu : Page
    {
        private MainPage parent;
        public PauseMenu(MainPage parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void ResumeGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            parent.Resume();
        }

        private void RestartGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            parent.Restart();
        }

        private void GoMainMenu(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            parent.ToMainMenu();
        }
    }
}
