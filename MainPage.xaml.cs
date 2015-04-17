// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using SharpDX;

namespace Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public readonly LabGame game;
        public MainMenu mainMenu;
        public GameOver gameOver;
        public Options options;
        public PauseMenu pause;
        public MainPage()
        {
            InitializeComponent();
            game = new LabGame(this);
            game.Run(this);
            mainMenu = new MainMenu(this);
            options = new Options(this);
            pause = new PauseMenu(this);
            this.Children.Add(mainMenu);
        }

        // TASK 1: Update the game's score
        public void UpdateScore(int score1, int score2)
        {
            txtScore1.Text = "Player 1: " + score1.ToString();
            txtScore2.Text = "Player 2: " + score2.ToString();
        }

        public void setTilt(bool val)
        {
            game.tilt = val;
        }

        // TASK 2: Starts the game.  Not that it seems easier to simply move the game.Run(this) command to this function,
        // however this seems to result in a reduction in texture quality on some machines.  Not sure why this is the case
        // but this is an easy workaround.  Not we are also making the command button invisible after it is clicked
        public void StartGame()
        {
            this.Children.Remove(mainMenu);
            game.started = true;
        }

        public void PauseGame() 
        {
            this.Children.Add(pause);
            game.paused = true ;
        }

        public void GameOverPage(int player)
        {

            gameOver = new GameOver(this, player);
            this.Children.Add(gameOver);
            game.started = false;

            game.resetGame();

        }

        public void Quit()
        {
            game.Exit();
            game.Dispose();
            App.Current.Exit();
        }

        public void setSensitivity(float s)
        {
            float maxChange = 0.02f;
            game.sensitivity = s*maxChange + LabGame.defaultSensitivity;
        }

        internal void setAI(float p)
        {
            game.AIspeed = p / 100 * LabGame.defaultAI;
        }

        public void setPuckSpeed(float speed)
        {
            game.puckSpeed = speed * LabGame.defaultPuckSpeed;
        }

        internal void setFriction(float f)
        {
            float maxChange = 0.005f;
            game.friction = f * maxChange + LabGame.defaultFriction;
        }

        internal void setWinScore(int score)
        {
            game.winScore = score;
        }

        internal void Resume()
        {
            this.Children.Remove(pause);
            game.paused = false;
        }

        internal void Restart()
        {
            this.Children.Remove(pause);
            game.paused = false;

            game.resetGame();
        }

        internal void ToMainMenu()
        {
            this.Children.Remove(pause);
            this.Children.Add(mainMenu);
            game.paused = false;
            game.started = false;

            game.resetGame();
        }

        private void PauseMenu(object sender, RoutedEventArgs e)
        {
            this.Children.Add(pause);
            game.paused = true;
        }

    }
}
