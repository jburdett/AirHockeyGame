using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Toolkit;
using Windows.UI.Input;
using Windows.UI.Core;

namespace Project
{
    using SharpDX.Toolkit.Graphics;
    using SharpDX.Toolkit.Input;
    public enum PlayerNumber
    {
        P1, P2
    }

    // Player class.
    public class Player : GameObject
    {
        //private float speed = 0.006f;
        private PlayerNumber pnum;
        public int mass;
        private bool holding;
        private float boundaryLeft;
        private float boundaryRight;
        private Vector3 idlePos;

        public Player(LabGame game, Vector3 pos, PlayerNumber pnum)
            : base(game)
        {
            this.game = game;
            type = GameObjectType.Player;

            model = game.Content.Load<Model>("paddle");

            this.pos = pos;
            this.pnum = pnum;
            holding = false;
            velocity = new Vector3(0, 0, 0);
            radius = 1;

            if (pnum == PlayerNumber.P1)
            {
                boundaryLeft = game.boundaryLeft;
                boundaryRight = 0;
            }
            else
            {
                boundaryRight = game.boundaryRight;
                boundaryLeft = 0;
                idlePos = pos - new Vector3(2, 0, 0);
            }

            //Value of mass is arbitrary, need to do some tuning to make it realistic.
            mass = 5;
        }

        // Frame update.
        public override void Update(GameTime gameTime)
        {

            if (pnum == PlayerNumber.P2)
            {
                AIUpdate(gameTime);
            }

            pos.X += velocity.X;
            pos.Y += velocity.Y;
            // TODO uncomment this assignment for accelerometer (also uncomment other assignment) @Nick
            if (game.tilt && !holding && pnum != PlayerNumber.P2)
            {
                velocity.X = (float)game.accelerometerReading.AccelerationX * game.tiltSensitivity;
                velocity.Y = (float)game.accelerometerReading.AccelerationY * game.tiltSensitivity;
            }

            // Keep within the boundaries.
            if (pos.X < boundaryLeft+radius) {
                pos.X = boundaryLeft+radius;
                velocity.X *= -0.3f;
                velocity.Y *= 0.3f;
            }
            if (pos.X > boundaryRight-radius) { 
                pos.X = boundaryRight-radius; 
            }
            if (pos.Y > game.boundaryTop-radius) { 
                pos.Y = game.boundaryTop-radius;
                velocity.X *= 0.3f;
                velocity.Y *= -0.3f;
            }
            if (pos.Y < game.boundaryBottom+radius) { 
                pos.Y = game.boundaryBottom+radius;
                velocity.X *= 0.3f;
                velocity.Y *= -0.3f;
            }

            UpdateWorld();
        }

        private void AIUpdate(GameTime gameTime)
        {
            // Decision making for the player two AI
            Puck puck = game.puck;

            if (puck.pos.X + 1 > pos.X)
            {
                // the puck is behind P2
                // this line works but causes jerky movement in P2
                velocity.X = puck.velocity.Length() * 0.9f;
            
            }

            else if (puck.velocity.X < 0 && puck.pos.X < 4)
            {
                // Puck is heading away from the P2
                // attempt to return to the idle position
                float diffX = pos.X - idlePos.X;
                float diffY = pos.Y - idlePos.Y;
                float dist = (float)Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

                if (dist > 1.5)
                {

                    velocity.X = -diffX * 0.05f;
                    velocity.Y = - diffY * 0.05f;

                }
            }
            else if (/*puck.velocity.Length() < 1 && */puck.pos.X > 0)
            {

                // puck is slowly moving within range of P2
                // try to strike the puck

                float diffX;
                float diffY;
                float dist;


                if (puck.velocity.Length() > 1)
                {
                    diffX = pos.X - (puck.pos.X + puck.velocity.X * puck.velocity.X);
                    diffY = pos.Y - (puck.pos.Y + puck.velocity.Y * puck.velocity.Y);
                    dist = (float)Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));
                }
                else
                {
                    diffX = pos.X - (puck.pos.X + puck.velocity.X * 2);
                    diffY = pos.Y - (puck.pos.Y + puck.velocity.Y * 2);
                    dist = (float)Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

                }


                velocity.X = -diffX * 0.075f;
                velocity.Y = -diffY * 0.075f;
            }
            else
            {
                // puck is moving quickly toward P2 
                // try to get between the puck and the goal

            }

            velocity *= game.AIspeed;
        
            
        }

        // React to getting hit by an enemy bullet.
        public void Hit()
        {
            game.Exit();
        }

        public override void Tapped(GestureRecognizer sender, TappedEventArgs args)
        {
        }

        public override void OnManipulationStarted(GestureRecognizer sender, ManipulationStartedEventArgs args)
        {
            holding = true;
        }

        public override void OnManipulationUpdated(GestureRecognizer sender, ManipulationUpdatedEventArgs args)
        {
            if (pnum == PlayerNumber.P1)
            {
                velocity.X = (float)args.Delta.Translation.X * game.sensitivity;
                velocity.Y = (float)args.Delta.Translation.Y * -game.sensitivity;
                pos.X += velocity.X;
                pos.Y += velocity.Y;
            }
        }

        public override void OnManipulationCompleted(GestureRecognizer sender, ManipulationCompletedEventArgs args)
        {
            holding = false;
        }
    }
}