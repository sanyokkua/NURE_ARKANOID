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
using XNA_LAB_GAME.GameLogic;

namespace XNA_LAB_GAME.GameLogic
{
    class PlatformBehavior: IBehaviore
    {
        private Vector2 velocity;
        private Vector2 position;

        public PlatformBehavior(float velocity = 0)
        {
            if (velocity < 0 || velocity > 1) { velocity = 0.0f; }
            this.velocity = new Vector2(velocity);
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
            }
            catch { return; }
            rec.Y += (int)(velocity.Y);
            ((MovebleGameObject)obj).RECTANGLE = rec;
        }

        public void Intersect(ref object obj, ref GameObject gameObject)
        {
            MovebleGameObject mObj;
            try
            {
                mObj = ((MovebleGameObject)obj);
            }
            catch (Exception ex)
            {
                return;
            }
            if (mObj.RECTANGLE.Intersects(gameObject.RECTANGLE))
            {
                if (mObj.ObjectState == GameObject.STATE.GOOD)
                {
                    mObj.ObjectState = GameObject.STATE.MIDDLE;
                    mObj.TEXTURE = mObj.OTHER_TEXTURES.ElementAt(0);
                }
                else if (mObj.ObjectState == GameObject.STATE.MIDDLE)
                {
                    mObj.ObjectState = GameObject.STATE.WORSE;
                    mObj.TEXTURE = mObj.OTHER_TEXTURES.ElementAt(1);
                }
                else
                {
                    mObj.IS_VISIBLE = false;
                    Song sound = mObj.SOUND;
                    if (sound != null)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(sound);
                    }
                }
            }
        }
    }
}
