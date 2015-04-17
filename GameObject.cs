using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;
using Windows.UI.Input;
using Windows.UI.Core;

namespace Project
{
    using SharpDX.Toolkit.Graphics;
    public enum GameObjectType
    {
        None, Player, Puck, Arena
    }

    // Super class for all game objects.
    abstract public class GameObject
    {
        public Model model;
        public LabGame game;
        public GameObjectType type = GameObjectType.None;
        public Vector3 pos;
        public Vector3 velocity;
        public Effect effect;
        public float radius;
        public string s;

        private Matrix World = Matrix.Identity;
        private Matrix WorldInverseTranspose;


        // to remove
        public BasicEffect basicEffect;

        public GameObject(LabGame game)
        {
            this.game = game;
            effect = game.Content.Load<Effect>("myShader");
        }

        public abstract void Update(GameTime gametime);

        public void UpdateWorld()
        {
            World = Matrix.Translation(pos);
            WorldInverseTranspose = Matrix.Transpose(Matrix.Invert(World));
        }
        public void Draw(GameTime gametime)
        {
            // Apply the basic effect technique and draw the object
            /*
            BasicEffect.EnableDefaultLighting(model, true);
            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                View = game.camera.View,
                Projection = game.camera.Projection,
                World = Matrix.Identity,
                VertexColorEnabled = true,
            };
            basicEffect.CurrentTechnique.Passes[0].Apply();
            model.Draw(game.GraphicsDevice, basicEffect.World, basicEffect.View, basicEffect.Projection);
            */

            // Apply the custom shader
            // world setup
            effect.Parameters["WORLD"].SetValue(World);
            effect.Parameters["PROJ"].SetValue(game.camera.Projection);
            effect.Parameters["VIEW"].SetValue(game.camera.View);
            effect.Parameters["CAM_POS"].SetValue(game.camera.pos);
            effect.Parameters["WORLD_INV_TRP"].SetValue(WorldInverseTranspose);
            // light setup
            effect.Parameters["LGHT_PNT_POS"].SetValue(game.light.pointPos);
            //effect.Parameters["LGHT_PNT_COL"].SetValue(game.light.pointColor);
            effect.Parameters["VIEW_VCTR"].SetValue(game.camera.viewVector);
            // ambient setup
            effect.Parameters["AMB_COL"].SetValue(game.light.ambientColor);
            effect.Parameters["AMB_K"].SetValue(game.light.ambientIntensity);
            // diffuse setup
            effect.Parameters["DIFF_COL"].SetValue(game.light.diffuseColor);
            effect.Parameters["DIFF_K"].SetValue(game.light.diffuseIntensity);
            // specular setup            
            effect.Parameters["SHININESS"].SetValue(200f);  // should be model.getShininess
            effect.Parameters["SPEC_COL"].SetValue(game.light.specularColor);
            effect.Parameters["SPEC_K"].SetValue(game.light.specularIntensity);

            s = this.model.Name.ToString();

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    //System.Diagnostics.Debug.WriteLine("key count: " + part.Effect.CurrentTechnique.Passes[0].Properties.Keys.Count);
                    for (int i = 0; i < part.Material.Properties.Values.ToList().Count; i++)
                    {
                        System.Diagnostics.Debug.WriteLine("key: " + part.Material.Properties.Keys.ToList()[i] + "\tvalue: " + part.Material.Properties.Values.ToList()[i]);
                    }
                    //effect.Parameters["SHININESS"].SetValue((float)(part.Material.Properties.Values.ToList()[4]));
                    //System.Diagnostics.Debug.WriteLine("color: " + color);
                    //effect.Parameters["COLOR"].SetValue(dif);

                    effect.CurrentTechnique.Passes[0].Apply();
                    part.Draw(game.GraphicsDevice);
                }
            }
            System.Diagnostics.Debug.WriteLine("custom effect: " + this.model.Name);
        }

        // These virtual voids allow any object that extends GameObject to respond to tapped and manipulation events
        public virtual void Tapped(GestureRecognizer sender, TappedEventArgs args)
        {

        }

        public virtual void OnManipulationStarted(GestureRecognizer sender, ManipulationStartedEventArgs args)
        {

        }

        public virtual void OnManipulationUpdated(GestureRecognizer sender, ManipulationUpdatedEventArgs args)
        {

        }

        public virtual void OnManipulationCompleted(GestureRecognizer sender, ManipulationCompletedEventArgs args)
        {

        }
    }
}
