using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Toolkit;

namespace Project
{
    using SharpDX.Toolkit.Graphics;
    using SharpDX.Toolkit.Input;
    public class Light
    {
        LabGame game;

        public Vector3 pointPos;
        public Vector4 pointColor;

        // ambient
        public Vector4 ambientColor;
        public float ambientIntensity;

        // diffuse
        public Vector3 diffuseDirection;
        public Vector4 diffuseColor;
        public float diffuseIntensity;


        // specular
        public Vector4 specularColor;
        public float specularIntensity;


        public Light(LabGame game)
        {
            this.game = game;

            pointPos = game.camera.pos;
            pointPos.Y -= 15;
            pointColor = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            // ambient
            ambientColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            ambientIntensity = 0.5f;
            // diffuse
            diffuseDirection = new Vector3(0.0f, 0.0f, -15.0f);
            diffuseColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            diffuseIntensity = 1.0f;
            // specular
            specularColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            specularIntensity = 1.0f;

        }
        public void Update(GameTime gameTime)
        {
            var time = (float)gameTime.TotalGameTime.TotalSeconds;
            var delta = time / 10;
            //Matrix rotationMatrix = Matrix.RotationX(delta) * Matrix.RotationY(delta * 2.0f) * Matrix.RotationZ(delta * .7f);
            //Vector3.Transform(ref pointPos, ref rotationMatrix, out pointPos);
            pointPos = game.camera.pos;
            //pointColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
