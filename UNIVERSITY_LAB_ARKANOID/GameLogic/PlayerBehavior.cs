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

namespace XNA_LAB_GAME.GameLogic
{
    class PlayerBehavior : IBehaviore
    {
        private Vector2 position;
        private int velocity;

        public PlayerBehavior(int velocity = 3)
        {
            if (velocity > 0)
            {
                this.velocity = velocity;
            }
            else
            {
                velocity = 3;
            }
            position = new Vector2();
        }
        public void Update(ref object obj)
        {
            Rectangle rec;
            Texture2D text;
            int width;
            int height;
            try
            {
                rec = ((MovebleGameObject)obj).RECTANGLE;
                text = ((MovebleGameObject)obj).TEXTURE;
                width = ((MovebleGameObject)obj).MAX_WIDTH;
                height = ((MovebleGameObject)obj).MAX_HEIGHT;
                position.X = ((MovebleGameObject)obj).RECTANGLE.X;
                position.Y = ((MovebleGameObject)obj).RECTANGLE.Y;
            }
            catch { return; }

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left))
            {
                position.X-=velocity;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                position.X+=velocity;
            }

            rec.X = (int)position.X;
            rec.Y = (int)position.Y;
            if (rec.X <= 0) { position.X = 0; }
            if (rec.Y <= 0) { position.Y = 0; }
            if (rec.X + rec.Width >= width) { position.X = width - rec.Width; }
            if (rec.Y + rec.Height >= height) { position.Y = height - rec.Height; }
            rec.X = (int)position.X;
            rec.Y = (int)position.Y;
            ((MovebleGameObject)obj).RECTANGLE = rec;
        }
        public void Intersect(ref object obj, ref GameObject gameObject) { }
    }
}
