using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2.Entities
{
    public class Pipe
    {
        public Texture2D texture;
        public Vector2 position;
        public bool scored = false;

        public void Update()
        {
            this.position.X -= 1.7f;
        }

        public Pipe()
        {
            this.texture = Statics.CONTENT.Load<Texture2D>("Textures/pipes");
            this.position = new Vector2(400, Statics.RANDOM.Next(-150, 10));
        }

        public Rectangle TopBound { get { return new Rectangle((int)this.position.X, (int)this.position.Y, 40, 220); } }
        public Rectangle BottomBound { get { return new Rectangle((int)this.position.X, (int)this.position.Y, 40, 220); } }

        public void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.texture, this.position, Color.White);
        }
    }
}
