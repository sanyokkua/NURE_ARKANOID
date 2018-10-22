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
    class GameObject
    {
        public Rectangle RECTANGLE { get; set; }
        public Texture2D TEXTURE { get; set; }

        public List<Texture2D> OTHER_TEXTURES = new List<Texture2D>();
        public int MAX_WIDTH { get; set; }
        public int MAX_HEIGHT { get; set; }
        public bool IS_VISIBLE { get; set; }
        public Song SOUND;
        public enum STATE { GOOD, MIDDLE, WORSE };
        
        public STATE ObjectState { get; set; }

        public GameObject(Texture2D texture, Rectangle rectangle, Song sound = null)
        {
            IS_VISIBLE = true;
            this.TEXTURE = texture;
            RECTANGLE = rectangle;
            SOUND = sound;
            ObjectState = STATE.GOOD;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IS_VISIBLE)
            {
                spriteBatch.Draw(TEXTURE, RECTANGLE, Color.White);
            }
        }
        public virtual void Update() { }
        public virtual void Intersect(GameObject obj) { }
    }
}
