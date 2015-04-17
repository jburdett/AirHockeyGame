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
    class Arena : GameObject
    {
        public Arena(LabGame game) 
            :base(game)
        {
            this.game = game;
            type = GameObjectType.Arena;
            pos = new SharpDX.Vector3(0, 0, 0);
            radius = 0;

            model = game.Content.Load<Model>("arena");
        }

        // Frame update.
        public override void Update(GameTime gameTime)
        {
            UpdateWorld();
        }
    }
}
