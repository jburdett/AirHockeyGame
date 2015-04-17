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
    public class Puck : GameObject
    {
        private int mass;

        public Puck(LabGame game)
            : base(game)
        {
            this.game = game;
            type = GameObjectType.Puck;
            model = game.Content.Load<Model>("puck");
            pos = new SharpDX.Vector3(0, 0, 0);

            mass = 1;
            radius = 1;
        }

        // Frame update.
        public override void Update(GameTime gameTime)
        {
            if (game.tilt)
            {
                velocity.X += (float)game.accelerometerReading.AccelerationX * game.tiltSensitivity;
                velocity.Y += (float)game.accelerometerReading.AccelerationY * game.tiltSensitivity;
            }
            Vector3 oldPos = pos;
            pos.X += velocity.X * game.puckSpeed;
            pos.Y += velocity.Y * game.puckSpeed;

            velocity.X *= game.friction;
            velocity.Y *= game.friction;

            calc_rebound_wall(this);
            // Keep within the boundaries.
            if (pos.X < game.boundaryLeft+radius) { pos.X = game.boundaryLeft+radius; }
            if (pos.X > game.boundaryRight-radius) { pos.X = game.boundaryRight-radius; }
            if (pos.Y > game.boundaryTop-radius) { pos.Y = game.boundaryTop-radius; }
            if (pos.Y < game.boundaryBottom+radius) { pos.Y = game.boundaryBottom+radius; }

            checkForCollisions(oldPos);
            check_for_score(this);

            UpdateWorld();
        }

        //Checking for collision between the puck and the arena and the paddles
        private void checkForCollisions(Vector3 oldPos)
        {
            foreach (var obj in game.gameObjects)
            {
                float del_x;
                float del_y;
                float dist;

                del_x = this.pos.X - obj.pos.X;
                del_y = this.pos.Y - obj.pos.Y;
                dist = (float)Math.Sqrt(del_x * del_x + del_y * del_y);

                //if the puck collide with player
                if (dist <= this.radius+obj.radius)
                {
                    if (obj.type == GameObjectType.Player)
                    {
                        //if collide with P1, pass P1 into physics function
                        handleCollision((Player) obj, new Vector3(del_x, del_y, 0));

                        Vector3 displacementvec = new Vector3(del_x, del_y, 0);
                        displacementvec.Normalize();
                        displacementvec *= (radius+obj.radius);
                        pos = obj.pos + displacementvec;
                    }
                }
            }
        }

        public void handleCollision(Player player, Vector3 collision)
        {
            float speedConserved = 0.7f;
            Vector3 oldVelocity = velocity;
            float speed = velocity.Length();
            collision.Normalize();
            velocity = collision * speed * speedConserved + player.velocity * speedConserved;
            float playerSpeed = player.velocity.Length();
            player.velocity = collision * -playerSpeed * speedConserved + oldVelocity * speedConserved;
        }

        public void check_for_score(Puck puck)
        {
            if (puck.pos.X <= -18 && (puck.pos.Y < 4 && puck.pos.Y > -4))
            {
                //player on the right hand side +1
                game.score2 += 1;

                //reset the speed of the puck
                puck.velocity.X = 0;
                puck.velocity.Y = 0;

                //reset the position of the puck
                puck.pos.X = -11;
                puck.pos.Y = 0;
            }
            if (puck.pos.X >= 18 && (puck.pos.Y < 4 && puck.pos.Y > -4))
            {
                //player on the left hand side +1
                game.score1 += 1;

                //reset the speed of the puck
                puck.velocity.X = 0;
                puck.velocity.Y = 0;

                //reset the position of the puck
                puck.pos.X = 11;
                puck.pos.Y = 0;
            }
        }

        
        public void calc_rebound_wall(Puck puck)
        {
            if (puck.pos.Y > game.boundaryTop - radius || puck.pos.Y < game.boundaryBottom + radius)
            {
                puck.velocity.Y *= -0.95f;
            }
            if (puck.pos.X > game.boundaryRight - radius || puck.pos.X < game.boundaryLeft + radius)
            {
                puck.velocity.X *= -0.95f;
            }
        }
    }
}
