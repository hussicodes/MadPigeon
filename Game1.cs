using madpigeon.Content.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;

namespace madpigeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        GameController gameController;
        MouseState previousMouseState;
        Scene scene;
        Pigeon pigeon;
        SpriteFont scoreFont;
        private Texture2D buttonTexture;
        private Texture2D startBackgroundTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1000,
                PreferredBackBufferHeight = 650,

            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            Scene.graphics = graphics;
            Pigeon.graphics = graphics;
            Pipe.graphics = graphics;
            scene = new Scene();
            pigeon = new Pigeon(
            Content.Load<Texture2D>("MADPIGEON1"),
            Content.Load<Texture2D>("MADPIGEON2"),
            Content.Load<Texture2D>("MADPIGEON3")
            );
            gameController = new GameController();
            previousMouseState = new MouseState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            scene.BackgroundTexture = Content.Load<Texture2D>("background1");
            scene.FloorTexture = Content.Load<Texture2D>("floor");
            pigeon.Texture2D[0] = Content.Load<Texture2D>("MADPIGEON5");
            pigeon.Texture2D[1] = Content.Load<Texture2D>("MADPIGEON5");
            pigeon.Texture2D[2] = Content.Load<Texture2D>("MADPIGEON5");
            Pipe.topPipeTexture = Content.Load<Texture2D>("toppipe");
            Pipe.bottomPipeTexture = Content.Load<Texture2D>("bottompipe");
            buttonTexture = Content.Load<Texture2D>("startButtonTexture");
            startBackgroundTexture = Content.Load<Texture2D>("startBackgroundTexture");


            Pigeon.wingSound = Content.Load<SoundEffect>("wing");
            GameController.dieSound = Content.Load<SoundEffect>("pdie");
            GameController.hitSound = Content.Load<SoundEffect>("phit");

            scoreFont = Content.Load<SpriteFont>("scorefont");



        }

        protected override void Update(GameTime gameTime)
        {




            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameController.GameState)
            {


                case GameController.PLAY_STATE:

                    scene.Move();
                    gameController.RaisePigeonOnClick(pigeon);
                    gameController.AddPipes();
                    gameController.MovePipes();
                    gameController.VerifyLoseForImpactPipe(pigeon);
                    gameController.VeryifyLoseForImpactFloor(pigeon);
                    gameController.VerifyIfIncreaseScore(pigeon);
                    if (previousMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)

                    {
                        gameController.GameState = GameController.PAUSE_STATE;
                    }
                    previousMouseState = Mouse.GetState();

                    break;


                case GameController.LOSE_STATE:
                    //You Lose

                    if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)

                    {

                        gameController.ArrayPipes.Clear();
                        pigeon.ResetPosition();
                        gameController.Score = 0;
                        if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            gameController.ResumeGame();
                        }


                        base.Draw(gameTime);

                        //This will make the game start again if you click, wowie
                    }
                    previousMouseState = Mouse.GetState();
                    break;

                case GameController.PAUSE_STATE:
                    if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameController.GameState = GameController.PLAY_STATE;
                    }
                    previousMouseState = Mouse.GetState();
                    break;




            }






            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            




            spriteBatch.Begin();
            switch (gameController.GameState)
            {
                case GameController.START_STATE:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Draw(startBackgroundTexture, graphics.GraphicsDevice.Viewport.Bounds, Color.White);

                   

                   
                    Rectangle startButtonRect = new Rectangle(
                        GraphicsDevice.Viewport.Width / 2 - buttonTexture.Width / 4,
                        50,
                        buttonTexture.Width,
                        buttonTexture.Height);
                    spriteBatch.Draw(buttonTexture, startButtonRect, Color.Red);

                  
                    string buttonText = "Start";
                    Vector2 buttonTextSize = scoreFont.MeasureString(buttonText);
                    Vector2 buttonTextPosition = new Vector2(
                        startButtonRect.X + (startButtonRect.Width - buttonTextSize.X) / 2,
                        startButtonRect.Y + (startButtonRect.Height - buttonTextSize.Y) / 2);
                    spriteBatch.DrawString(scoreFont, buttonText, buttonTextPosition, Color.Black);

                   
                    if (startButtonRect.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameController.GameState = GameController.PLAY_STATE;
                    }
                    break;

                case GameController.PLAY_STATE:
                    spriteBatch.Draw(scene.BackgroundTexture, scene.BackgroundRectangle, Color.White);
                    spriteBatch.Draw(scene.BackgroundTexture, scene.BackgroundRectangle2, Color.White);
                    foreach (Pipe pipe in gameController.ArrayPipes)
                    {
                        spriteBatch.Draw(Pipe.topPipeTexture, pipe.TopPipeRectangle, Color.White);
                        spriteBatch.Draw(Pipe.bottomPipeTexture, pipe.BottomPipeRectangle, Color.White);
                    }
                    spriteBatch.Draw(scene.FloorTexture, scene.BackgroundRectangle, Color.White);
                    spriteBatch.Draw(scene.FloorTexture, scene.BackgroundRectangle2, Color.White);
                    spriteBatch.Draw(pigeon.Texture2D[0], pigeon.Rectangle, Color.White);
                    spriteBatch.DrawString(scoreFont, gameController.Score.ToString(), new Vector2((graphics.PreferredBackBufferWidth / 2) - (scoreFont.MeasureString(gameController.Score.ToString()).X / 2), 0), Color.DarkGreen);
                    break;

                

                default:
                   
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}