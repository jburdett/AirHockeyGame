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

using SharpDX;
using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using Windows.UI.Input;
using Windows.UI.Core;
using Windows.Devices.Sensors;
using Windows.UI.Xaml;

namespace Project
{
    // Use this namespace here in case we need to use Direct3D11 namespace as well, as this
    // namespace will override the Direct3D11.
    using SharpDX.Toolkit.Graphics;
    using SharpDX.Toolkit.Input;

    public class LabGame : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        public List<GameObject> gameObjects;
        private Stack<GameObject> addedGameObjects;
        private Stack<GameObject> removedGameObjects;
        private KeyboardManager keyboardManager;
        public KeyboardState keyboardState;
        private Player player1;
        private Player player2;
        public Puck puck;
        private Arena arena;
        public AccelerometerReading accelerometerReading;
        public GameInput input;
        public int score1;
        public int score2;
        public MainPage mainPage;

        // TAS K4: Use this to represent difficulty
        public float difficulty;
        public float tiltSensitivity;

        // Represents the camera's position and orientation
        public Camera camera;
        // Represents the light's position and orientation
        public Light light;

        // Random number generator
        public Random random;

        // World boundaries that indicate where the edge of the screen is for the camera.
        public float boundaryLeft;
        public float boundaryRight;
        public float boundaryTop;
        public float boundaryBottom;

        public double screenWidth;

        // Option variables
        public bool started = false;
        public bool tilt = false;
        public static float defaultSensitivity = 0.02f;
        public float sensitivity = defaultSensitivity;
        public static float defaultAI = 1;
        public float AIspeed = defaultAI;
        public static float defaultPuckSpeed = 1;
        public float puckSpeed = defaultPuckSpeed;
        public static float defaultFriction = 0.995f;
        public float friction = defaultFriction;
        public int winScore = 7;
        public bool paused = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabGame" /> class.
        /// </summary>
        public LabGame(MainPage mainPage)
        {
            // Creates a graphics manager. This is mandatory.
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            // Setup the relative directory to the executable directory
            // for loading contents with the ContentManager
            Content.RootDirectory = "Content";

            // Create the keyboard manager
            keyboardManager = new KeyboardManager(this);
            random = new Random();
            input = new GameInput();

            // Set boundaries.
            boundaryLeft = -19f;
            boundaryRight = 19f;
            boundaryTop = 9;
            boundaryBottom = -9f;

            tiltSensitivity = 0.01f;

            // Initialise event handling.
            input.gestureRecognizer.Tapped += Tapped;
            input.gestureRecognizer.ManipulationStarted += OnManipulationStarted;
            input.gestureRecognizer.ManipulationUpdated += OnManipulationUpdated;
            input.gestureRecognizer.ManipulationCompleted += OnManipulationCompleted;

            this.mainPage = mainPage;

            score1 = 0;
            score2 = 0;
            difficulty = 1;
        }

        protected override void LoadContent()
        {
            // Initialise game object containers.
            gameObjects = new List<GameObject>();
            addedGameObjects = new Stack<GameObject>();
            removedGameObjects = new Stack<GameObject>();

            // Create game objects.
            player1 = new Player(this, new Vector3(boundaryLeft+2,0,0), PlayerNumber.P1);
            player2 = new Player(this, new Vector3(boundaryRight-2,0,0), PlayerNumber.P2);
            puck = new Puck(this);
            arena = new Arena(this);
            gameObjects.Add(player1);
            gameObjects.Add(player2);
            gameObjects.Add(puck);
            gameObjects.Add(arena);

            screenWidth = Windows.UI.Xaml.Window.Current.Bounds.Width;
            // Create an input layout from the vertices

            base.LoadContent();
        }

        protected override void Initialize()
        {
            Window.Title = "Arcade Air Hockey";
            camera = new Camera(this);
            light = new Light(this);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (started && !paused)
            {
                keyboardState = keyboardManager.GetState();
                flushAddedAndRemovedGameObjects();
                if (tilt)
                {
                    accelerometerReading = input.accelerometer.GetCurrentReading();
                }
                camera.Update();
                light.Update(gameTime);
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].Update(gameTime);
                }

                mainPage.UpdateScore(score1, score2);
                if (score1 >= winScore)
                {
                    mainPage.GameOverPage(1);
                }
                if (score2 >= winScore)
                {
                    mainPage.GameOverPage(2);
                }

                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                    this.Dispose();
                    App.Current.Exit();
                }
                // Handle base.Update
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            if (started)
            {
                // Clears the screen with the Color.CornflowerBlue
                GraphicsDevice.Clear(Color.White);

                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].Draw(gameTime);
                }
            }
            // Handle base.Draw
            base.Draw(gameTime);
        }
        // Count the number of game objects for a certain type.
        public int Count(GameObjectType type)
        {
            int count = 0;
            foreach (var obj in gameObjects)
            {
                if (obj.type == type) { count++; }
            }
            return count;
        }

        // Add a new game object.
        public void Add(GameObject obj)
        {
            if (!gameObjects.Contains(obj) && !addedGameObjects.Contains(obj))
            {
                addedGameObjects.Push(obj);
            }
        }

        // Remove a game object.
        public void Remove(GameObject obj)
        {
            if (gameObjects.Contains(obj) && !removedGameObjects.Contains(obj))
            {
                removedGameObjects.Push(obj);
            }
        }

        // Process the buffers of game objects that need to be added/removed.
        private void flushAddedAndRemovedGameObjects()
        {
            while (addedGameObjects.Count > 0) { gameObjects.Add(addedGameObjects.Pop()); }
            while (removedGameObjects.Count > 0) { gameObjects.Remove(removedGameObjects.Pop()); }
        }

        public void OnManipulationStarted(GestureRecognizer sender, ManipulationStartedEventArgs args)
        {
            // Pass Manipulation events to the game objects.
            foreach (var obj in gameObjects)
            {
                obj.OnManipulationStarted(sender, args);
            }
        }

        public void Tapped(GestureRecognizer sender, TappedEventArgs args)
        {
            // Pass Manipulation events to the game objects.
            foreach (var obj in gameObjects)
            {
                obj.Tapped(sender, args);
            }
        }

        public void OnManipulationUpdated(GestureRecognizer sender, ManipulationUpdatedEventArgs args)
        {
            foreach (var obj in gameObjects)
            {
                if (obj.basicEffect != null) { obj.basicEffect.View = camera.View; }
                obj.OnManipulationUpdated(sender, args);
            }
        }

        public void OnManipulationCompleted(GestureRecognizer sender, ManipulationCompletedEventArgs args)
        {
            foreach (var obj in gameObjects)
            {
                obj.OnManipulationCompleted(sender, args);
            }
        }

        public void resetGame(){
            //reset the score to 0
            score1 = 0;
            score2 = 0;

            gameObjects = new List<GameObject>();
            player1 = new Player(this, new Vector3(boundaryLeft + 2, 0, 0), PlayerNumber.P1);
            player2 = new Player(this, new Vector3(boundaryRight - 2, 0, 0), PlayerNumber.P2);
            puck = new Puck(this);
            arena = new Arena(this);
            gameObjects.Add(player1);
            gameObjects.Add(player2);
            gameObjects.Add(puck);
            gameObjects.Add(arena);
        }
    }
}
