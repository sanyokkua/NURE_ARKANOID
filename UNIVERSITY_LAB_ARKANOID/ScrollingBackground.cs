using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNA_LAB_GAME
{
    class ScrollingBackground
    {
        public Texture2D BackGroundTexture;
        public Rectangle BackGroundRectangle;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGroundTexture, BackGroundRectangle, Color.White);
        }
        public virtual void Update() { }
    }
}
