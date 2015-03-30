using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XNA_LAB_GAME.GameLogic;

namespace XNA_LAB_GAME
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static int SCREEN_WIDTH = 1366;
        static int SCREEN_HEIGHT = 768;
        static bool IS_FULL_SCREEN = true;

        static int NUMBER_OF_PLATFORMS = 100;
        static int PLATFORM_WIDTH = 100;
        static int PLATFORM_HEIGHT = 25;

        static int PLAYER_WIDTH = 120;
        static int PLAYER_HEIGHT = 15;
        static int PLAYER_SPEED = 15;
        int PLAYER_START_POSITION_X = SCREEN_WIDTH / 2;
        int PLAYER_START_POSITION_Y = (int)(SCREEN_HEIGHT * 0.9);

        static int BALL_RADIUS = 35;
        static float BALL_SPEED = 5f;
        int BALL_START_POSITION_X = (int)(SCREEN_HEIGHT * 0.9);
        int BALL_START_POSITION_Y = (int)(SCREEN_HEIGHT * 0.9) - BALL_RADIUS;

        bool gameIsPaused = true;
        bool gameIsOver = false;

        List<MovebleGameObject> blocks;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Song gameOverSound;

        MovebleGameObject ball;
        MovebleGameObject player;

        MainScrolling scrollingBackground_1;
        MainScrolling scrollingBackground_2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = IS_FULL_SCREEN;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SCREEN_WIDTH = GraphicsDevice.Viewport.Width;
            SCREEN_HEIGHT = GraphicsDevice.Viewport.Height;

            font = Content.Load<SpriteFont>("SpriteFont_GAME");
            gameOverSound = Content.Load<Song>("gameover");

            Texture2D ballTexture = Content.Load<Texture2D>("ball");
            Song ballSound = Content.Load<Song>("destroy");
            ball = new MovebleGameObject(ballTexture, BALL_START_POSITION_X, BALL_START_POSITION_Y,
                BALL_RADIUS, BALL_RADIUS, new BallBehavior(BALL_SPEED), ballSound);
            ball.MAX_WIDTH = SCREEN_WIDTH;
            ball.MAX_HEIGHT = SCREEN_WIDTH;
            ball.TYPE = "ball";

            Texture2D playerTexture = Content.Load<Texture2D>("cosmic");
            player = new MovebleGameObject(playerTexture, PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y, PLAYER_WIDTH,
                PLAYER_HEIGHT, new PlayerBehavior(PLAYER_SPEED));
            player.MAX_WIDTH = SCREEN_WIDTH;
            player.MAX_HEIGHT = SCREEN_HEIGHT;
            player.TYPE = "player";

            LoadBlocks();

            Texture2D scrollingBack1 = Content.Load<Texture2D>("scroll (1)");
            Texture2D scrollingBack2 = Content.Load<Texture2D>("scroll (2)");
            scrollingBackground_1 = new MainScrolling(scrollingBack1, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT));
            scrollingBackground_2 = new MainScrolling(scrollingBack2, new Rectangle(0, SCREEN_HEIGHT, SCREEN_WIDTH, SCREEN_HEIGHT));
        }

        protected void LoadBlocks()
        {
            Texture2D platformTexture = Content.Load<Texture2D>("block_image_good");
            Texture2D platformTexture1 = Content.Load<Texture2D>("block_image_middle");
            Texture2D platformTexture2 = Content.Load<Texture2D>("block_image_worse");

            Song sound = Content.Load<Song>("bang");
            blocks = new List<MovebleGameObject>();
            int line = SCREEN_WIDTH / PLATFORM_WIDTH;
            int max = NUMBER_OF_PLATFORMS / line;
            int x, y;
            Random random = new Random();
            PlatformBehavior plB = new PlatformBehavior(0);
            for (int i = 0; i <= max; i++)
            {
                y = i * PLATFORM_HEIGHT + 1;
                for (int j = 0; j <= line; j++)
                {
                    x = j * PLATFORM_WIDTH + 1;
                    blocks.Add(new MovebleGameObject(platformTexture, x, y, PLATFORM_WIDTH - 1, PLATFORM_HEIGHT - 1, plB, sound));
                }
            }
            foreach (MovebleGameObject obj in blocks){
                obj.TYPE = "block";
                obj.OTHER_TEXTURES.Add(platformTexture1);
                obj.OTHER_TEXTURES.Add(platformTexture2);

                int k = random.Next(0, 2);
                MovebleGameObject.STATE state = GameObject.STATE.GOOD;
                if (k == 0)
                {
                    state = GameObject.STATE.GOOD;
                }
                if (k == 1)
                {
                    state = GameObject.STATE.MIDDLE;
                }
                if (k == 2)
                {
                    state = GameObject.STATE.WORSE;
                }
                obj.ObjectState = state;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                System.Threading.Thread.Sleep(200);
                gameIsPaused = gameIsPaused ? false : true;
            }
            if (!gameIsPaused)
            {
                if (scrollingBackground_1.BackGroundRectangle.Y + scrollingBackground_1.BackGroundRectangle.Height <= 0)
                {
                    scrollingBackground_1.BackGroundRectangle.Y = scrollingBackground_2.BackGroundRectangle.Y + scrollingBackground_2.BackGroundRectangle.Height;
                }
                if (scrollingBackground_2.BackGroundRectangle.Y + scrollingBackground_2.BackGroundRectangle.Height <= 0)
                {
                    scrollingBackground_2.BackGroundRectangle.Y = scrollingBackground_1.BackGroundRectangle.Y + scrollingBackground_1.BackGroundRectangle.Height;
                }
                scrollingBackground_1.Update();
                scrollingBackground_2.Update();
                VerifyPlatforms();
                ball.Intersect(player);
                ball.Update();
                player.Update();
            }
            if (((BallBehavior)ball.behaviore).velocity.X == 0)
            {
                if (gameOverSound != null && !gameIsOver)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(gameOverSound);
                }
                gameIsOver = true;
            }
            base.Update(gameTime);
        }
        protected void VerifyPlatforms()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                
                if (blocks.ElementAt(i).IS_VISIBLE)
                {
                    blocks.ElementAt(i).Update();
                    blocks.ElementAt(i).Intersect(ball);
                    ball.Intersect(blocks.ElementAt(i));
                }
                else
                {
                    ball.Intersect(blocks.ElementAt(i));
                    blocks.Remove(blocks.ElementAt(i));
                    if (blocks.Count == 0)
                    {
                        gameIsOver = true;
                    }
                }
                

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            scrollingBackground_1.Draw(spriteBatch);
            scrollingBackground_2.Draw(spriteBatch);

            if (!gameIsOver)
            {
                DrawBlocks(spriteBatch);
                ball.Draw(spriteBatch);
                player.Draw(spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(SCREEN_WIDTH / 2 - 50, SCREEN_HEIGHT / 2), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected void DrawBlocks(SpriteBatch sb)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks.ElementAt(i).Draw(sb);
            }
        }
    }
}
