using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2.Entities
{
    public class Scroll
    {
        public Vector2 Position;
        public Texture2D texture;

        public int aniTimer = 10;
        public double aniElapsed = 0;
        public int decalX = 0;

        public Scroll()
        {
            this.texture = Statics.CONTENT.Load<Texture2D>("Textures/scroll");
            this.Position = new Vector2(500, 600);
        }

        public void Update()
        {
           // aniElapsed += Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;

            if (aniElapsed > aniTimer)
            {
                this.decalX += 2;
                if (decalX > 12)
                    decalX = 0;
                aniElapsed = 0;
            }
        }

        public void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.texture,this.Position, new Rectangle(this.decalX,0,Statics.GAME_WIDTH,12), Color.White);
        }
    }
}
