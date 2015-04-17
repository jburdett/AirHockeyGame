using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Toolkit;
namespace Project
{
    public class Camera
    {
        public Matrix View;
        public Matrix Projection;
        public LabGame game;
        public Vector3 pos;
        public Vector3 oldPos;
        private float cameraTilt;

        public Vector3 cameraTarget;
        public Vector3 viewVector;

        // Ensures that all objects are being rendered from a consistent viewpoint
        public Camera(LabGame game) {
            pos = new Vector3(0, 0, 30);
            cameraTarget = new Vector3(0, 0, 0);
            View = Matrix.LookAtRH(pos, cameraTarget, Vector3.UnitY);
            Projection = Matrix.PerspectiveFovRH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, 0.01f, 1000.0f);
            this.game = game;
            cameraTilt = 5;
        }

        // If the screen is resized, the projection matrix will change
        public void Update()
        {
            if (game.tilt)
            {
                pos.X = (float)game.accelerometerReading.AccelerationX * cameraTilt;
                pos.Y = (float)game.accelerometerReading.AccelerationY * cameraTilt;
            }

            Projection = Matrix.PerspectiveFovRH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, 0.1f, 100.0f);
            View = Matrix.LookAtRH(pos, cameraTarget, Vector3.UnitY);
            viewVector = cameraTarget - pos;

        }
    }
}
