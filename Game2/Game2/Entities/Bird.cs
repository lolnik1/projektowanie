using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2.Entities
{
    public class Bird
    {
        public Texture2D[] Textures;
        public float Rotation;
        public int texturePosition;
        public float YSpeed;
        public Vector2 Position;

        public int jumpTimer = 500;
        public double jumpElapsed = 0;

        public int aniTimer = 100;
        public double aniElapsed = 0;
        public int textureAdd = 1;

        public bool canJump = true;
        public bool dead = false;

        public Bird()
        {
            Textures = new Texture2D[1];
            this.Textures[0] = Statics.CONTENT.Load<Texture2D>("Textures/bird");
            YSpeed = 0;
            this.Position = new Vector2(100, 200);
        }

        public void Update()
        {
            if (this.Position.Y < 500)
            {
                YSpeed += 0.2f;

               //   jumpElapsed += Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;

                if (jumpElapsed > jumpTimer)
                {
                    canJump = true;
                    jumpElapsed = 0;
                }

                //  aniElapsed += Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;

                if (aniElapsed > aniTimer)
                {
                    this.texturePosition += this.textureAdd;
                    if (this.texturePosition == 2 || this.texturePosition == 0)
                        this.textureAdd = this.textureAdd * -1;
                    jumpElapsed = 0;
                }

                if (Statics.input.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space) && canJump)
                {
                    YSpeed = -6;
                }

                Rotation = (float)Math.Atan2(YSpeed, 10);

                this.Position.Y += YSpeed;

                if(this.Position.Y > 500)
                {
                    dead = true;
                }
            }
        }

        public Rectangle Bound { get { return new Rectangle((int)this.Position.X - 20, (int)this.Position.Y - 20, 40, 40); }  }

        public void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.Textures[this.texturePosition], this.Position, null, Color.White,this.Rotation, new Vector2(20,20), 1f, SpriteEffects.None, 0f);
        }
    }
}
