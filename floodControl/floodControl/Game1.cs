#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace floodControl
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playingPieces;
        Texture2D backgroundScreen;
        Texture2D titleScreen;

        GameBoard board;
        Vector2 gameBoardDisplayOrigin = new Vector2(70, 89);
        int playerScore = 0;
        enum gameState { TitleScreen, Playing };
        gameState state = gameState.Playing;
        Rectangle emptyPiece = new Rectangle(1, 247, 40, 40);
        const float minTimeSunceLastInput = 0.25f;
        float timeSinceLastInput = 0.0f;

        public Game1()
            : base()
        {
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			this.graphics.PreferredBackBufferWidth = 800;
			this.graphics.PreferredBackBufferHeight = 600;
			graphics.IsFullScreen = false;
			this.graphics.ApplyChanges();
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
            board = new GameBoard();
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

            playingPieces = Content.Load<Texture2D>("Textures/Tile_Sheet");
            backgroundScreen = Content.Load<Texture2D>("Textures/Background");
            titleScreen = Content.Load<Texture2D>("Textures/TitleScreen");

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (state == gameState.TitleScreen)
            {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            }
            else if (state == gameState.Playing)
            {
                spriteBatch.Draw(backgroundScreen, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                //Debug.Write("lol");
                for (int x = 0; x < GameBoard.w; ++x)
                {
                    for (int y = 0; y < GameBoard.h; ++y)
                    {
                        int px = (int)gameBoardDisplayOrigin.X + x * GamePiece.w;
                        int py = (int)gameBoardDisplayOrigin.Y + y * GamePiece.h;

                        spriteBatch.Draw(playingPieces,
                                         new Rectangle(px, py, GamePiece.w, GamePiece.h), emptyPiece, Color.White);
                        spriteBatch.Draw(playingPieces,
                                         new Rectangle(px, py, GamePiece.w, GamePiece.h), board.GetRect(x, y), Color.White);
                        Window.Title = playerScore.ToString();
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
