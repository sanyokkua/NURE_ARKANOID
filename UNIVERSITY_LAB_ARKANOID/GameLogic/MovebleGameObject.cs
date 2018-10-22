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
    class MovebleGameObject : GameObject
    {
        public IBehaviore behaviore { get; private set; }
        public int X { get { return RECTANGLE.X; } }
        public int Y { get { return RECTANGLE.Y; } }

        public string TYPE { get; set; }

        public MovebleGameObject(Texture2D texture, Rectangle rectangle, IBehaviore behaviore, Song sound = null) : base(texture, rectangle, sound)
        { this.behaviore = behaviore;}
        public MovebleGameObject(Texture2D texture, int x, int y, IBehaviore behaviore, Song sound = null)
            : this(texture, new Rectangle(x, y, texture.Width, texture.Height), behaviore, sound) { SOUND = sound; }
        public MovebleGameObject(Texture2D texture, int x, int y, int width, int height, IBehaviore behaviore, Song sound = null)
            : this(texture, new Rectangle(x, y, width, height), behaviore, sound) { SOUND = sound; }
        public override void Update()
        {
            Object obj = this;
            behaviore.Update(ref obj);
        }
        public override void Intersect(GameObject obj)
        {
            Object mObj = this;
            behaviore.Intersect(ref mObj, ref obj);
        }

    }
}
