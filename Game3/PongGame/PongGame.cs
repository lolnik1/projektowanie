using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongGame.Classes;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PongGame
{
    
    public class PongGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ClassBall ball;
        ClassRacket P1;
        ClassRacket P2;
        SpriteFont font;
        Random rnd;

        int scoreP1 = 0;
        int scoreP2 = 0;
        int ballSize = 10;
        int racketwidth = 10;
        int racketheight = 100;

        public PongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

    
        protected override void Initialize()
        {
            

            base.Initialize();
        }

   
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("font");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = new ClassBall(GraphicsDevice, spriteBatch, this, ballSize);
            P1 = new ClassRacket(GraphicsDevice, spriteBatch, this, racketwidth, racketheight, 10, GraphicsDevice.Viewport.Height / 2 - racketheight / 2);
            P2 = new ClassRacket(GraphicsDevice, spriteBatch, this, racketwidth, racketheight, GraphicsDevice.Viewport.Width - 10 - racketwidth, GraphicsDevice.Viewport.Height / 2 - racketheight / 2);

            Components.Add(ball);
            Components.Add(P1);
            Components.Add(P2);
            ball.ResetBall();     
        }

  
        protected override void UnloadContent()
        {
            
        }


        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !ball.gameRun)
            {
                ball.gameRun = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.R) && ball.gameRun)
            {
                ball.gameRun = false;
                ball.ResetBall();
            }

            P1.posY = Mouse.GetState().Y;
            rnd = new Random();

            if(P1.posY > 400)
            {
                P1.posY = 400;   
            }
            if (P1.posY < 0)
            {
                P1.posY = 0;
            }

            if (P2.posY > 400)
            {
                P2.posY = 400;
            }
            if (P2.posY < 0)
            {
                P2.posY = 0;
            }

            int odbicie = rnd.Next(1, 100);
            if (ball.posX > 400)
            {
                if (odbicie >= 85)
                {
                    P2.posY = ball.posY;

                    if (P1.posY > 400)
                    {
                        P1.posY = 400;
                    }
                    if (P1.posY < 0)
                    {
                        P1.posY = 0;
                    }

                    if (P2.posY > 400)
                    {
                        P2.posY = 400;
                    }
                    if (P2.posY < 0)
                    {
                        P2.posY = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        P2.posY = ball.posY + 1;
                    }

                }
            }
            else
            P2.posY = Mouse.GetState().Y;

            //ball goes to P2
            if (ball.dirX > 0)
            {
                if (ball.posY >= P2.posY && ball.posY + ballSize < P2.posY + racketheight && ball.posX + ballSize >= P2.posX)
                {
                    ball.dirX = -ball.dirX;
                }
                else if (ball.posX >= GraphicsDevice.Viewport.Width - ballSize)
                {
                    scoreP1++;
                    ball.gameRun = false;
                    ball.ResetBall();
                }
            }
            //ball goes to P1
            else if(ball.dirX < 0)
            {
                if (ball.posY >= P1.posY && ball.posY + ballSize <= P1.posY + racketheight && ball.posX <= P1.posX + racketwidth)
                {
                    ball.dirX = -ball.dirX;
                }
                else if(ball.posX <= 0)
                {
                    scoreP2++;
                    ball.gameRun = false;
                    ball.ResetBall();
                }
            }
        

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(51,51,51));

            spriteBatch.Begin();

            spriteBatch.DrawString(font, scoreP1.ToString(), new Vector2(50,50), new Color(45,45,45,20));
            spriteBatch.DrawString(font, scoreP2.ToString(), new Vector2(GraphicsDevice.Viewport.Width - 250, 50), new Color(45, 45, 45, 20));

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
