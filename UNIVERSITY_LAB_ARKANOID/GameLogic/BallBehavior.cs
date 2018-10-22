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
    class BallBehavior : IBehaviore
    {
        public Vector2 velocity;
        public BallBehavior(float speed)
        {
            velocity = new Vector2(speed);
        }
        void IBehaviore.Update(ref Object obj)
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
            }
            catch { return; }

            rec.X += (int)velocity.X;
            rec.Y += (int)velocity.Y;

            if (rec.X <= 0) { 
                velocity.X = -velocity.X;
                rec.X = 0;
            }
            if (rec.Y <= 0) { 
                velocity.Y = -velocity.Y;
                rec.Y = 0;
            }
            if (rec.X + rec.Width >= width) { 
                velocity.X = -velocity.X;
                rec.X = width - rec.Width - 1;
            }
            if (rec.Y + rec.Height + 1 >= height) { 
                velocity.Y = -velocity.Y;
                rec.Y = height - rec.Height - 1;
            }
            if (rec.Y > height - rec.Height - 5)
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            ((MovebleGameObject)obj).RECTANGLE = rec;
        }
        void IBehaviore.Intersect(ref Object obj, ref GameObject gameObject)
        {
            Rectangle rec;
            Rectangle recOther;
            MovebleGameObject block = ((MovebleGameObject)gameObject); 
            try
            {
                rec = ((MovebleGameObject)obj).RECTANGLE;
                recOther = gameObject.RECTANGLE;
            }
            catch (Exception ex)
            {
                return;
            }
            if (rec.Intersects(gameObject.RECTANGLE))
            {
                velocity.Y = -velocity.Y;
                
                if (velocity.X > 0)
                {
                    rec.X += 1;
                }
                else
                {
                    rec.X -= 1;
                }
                if (velocity.Y > 0)
                {
                    rec.Y += 1;
                }
                else
                {
                    rec.Y -= 1;
                }

                Song sound = ((MovebleGameObject)obj).SOUND;
                if (sound != null && block.ObjectState!=GameObject.STATE.WORSE)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(sound);
                }
            }
            ((MovebleGameObject)obj).RECTANGLE = rec;
        }
    }
}
