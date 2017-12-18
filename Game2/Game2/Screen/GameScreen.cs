using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2.Screen
{
    public class GameScreen : Screen
    {
        public Texture2D background;
        public Entities.Bird Bird;
        public Entities.Scroll Scroll;
        public List<Entities.Pipe> Pipe;
        public int pipeTimer = 2500;
        public double pipeElapsed = 0;
        public bool GameOver = false;
        public int wynik = 0;
        private SpriteFont Font;
        private int score = 0;

        public GameScreen()
        {

        }
        public override void Update()
        {
            if (!Bird.dead)
            {
                wynik++;
            }
            pipeCreator();
            if (!Bird.dead)
            {

                for (int i = Pipe.Count - 1; i > -1; i--)
                {
                    if (Pipe[i].position.X < 30)
                        Pipe.RemoveAt(i);
                    else
                    {
                        Pipe[i].Update();
                        if (!Pipe[i].scored && Bird.Position.X > Pipe[i].position.X + 30)
                        {
                            Pipe[i].scored = true;
                            score++;
                        }
                        if (Bird.Bound.Intersects(Pipe[i].TopBound) || (Bird.Bound.Intersects(Pipe[i].BottomBound)))
                        {
                            Bird.dead = true;

                        }
                    }
                }
                Bird.Update();
                Scroll.Update();
            }

            if(Bird.dead && Statics.input.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.R))
            {
                this.Reset();
                wynik = 0;
            }
            base.Update();
        }

        public void pipeCreator()
        {
            pipeElapsed += 30;

            if (pipeElapsed > pipeTimer)
            {
                Pipe.Add(new Entities.Pipe());
                pipeElapsed = 0;
            }
        }
        

        public override void Draw()
        {
            Statics.SPRITEBATCH.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null);

            Statics.SPRITEBATCH.Draw(this.background,Vector2.Zero,Color.White);
            Scroll.Draw();
            Bird.Draw();
            Statics.SPRITEBATCH.DrawString(Font, "Wynik :" + this.wynik.ToString(), new Vector2(10, 10), Color.White);
            //Statics.SPRITEBATCH.DrawString(Font, "Score", new Vector2(100, 100), Color.Black);
            foreach (var item in Pipe)
            {
                item.Draw();
            }
            Statics.SPRITEBATCH.End();
            base.Draw();
        }

        public void Reset()
        {
            Bird = new Entities.Bird();
            Scroll = new Entities.Scroll();
            Pipe = new List<Entities.Pipe>();
            Pipe.Add(new Entities.Pipe());
            score = 0;
            GameOver = false;
        }

        public override void LoadContent()
        {
            background = Statics.CONTENT.Load<Texture2D>("Textures/background");
            //font = Statics.CONTENT.Load<SpriteFont>("Score");

            Font = Statics.CONTENT.Load<SpriteFont>("Fonts");
            Reset();
            base.LoadContent();
        }
    }
}
