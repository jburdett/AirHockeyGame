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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOver
    {
        private MainPage parent;
        public GameOver(MainPage parent, int player)
        {
            this.parent = parent;
            InitializeComponent();
            if (player == 1)
            {
                txtwinPlayer.Text = "Winner is Player 1!!";
            }
            else if (player == 2)
            {
                txtwinPlayer.Text = "Winner is Player 2!!";
            }
            
        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            parent.Children.Add(parent.mainMenu);
            parent.Children.Remove(this);
        }
    }
}
