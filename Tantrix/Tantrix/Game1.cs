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

namespace Tantrix
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState previousMouse;
        bool movingCamera;
        Tile clickedOnTile;

        public static Texture2D tilebackground;
        public static Texture2D straightpiece;
        public static Texture2D swoopypiece;
        public static Texture2D curlypiece;
        public static Texture2D tilebagtexture;

        Board board;

        public static float height = 100.0f;
        public static float width = 86.85f; //this might be wrong...
        public static Vector2 centre = new Vector2(height / 2, width / 2);

        public static int screenHeight = 700;
        public static int screenWidth = 800;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            board = new Board();

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
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

            tilebackground = Content.Load<Texture2D>("art//background");
            straightpiece = Content.Load<Texture2D>("art//straight");
            curlypiece = Content.Load<Texture2D>("art//curly");
            swoopypiece = Content.Load<Texture2D>("art//swoopy");
            tilebagtexture = Content.Load<Texture2D>("art//tilebag");

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (movingCamera)
                {
                    board.moveCamera(ms.X, ms.Y, previousMouse.X, previousMouse.Y);
                }
                else
                {
                    if (previousMouse == null || previousMouse.LeftButton == ButtonState.Released)
                    {
                        clickedOnTile = board.getTileOnScreen(ms.X, ms.Y);
                        if (clickedOnTile == null) { movingCamera = true; }
                    }
                    if (clickedOnTile != null)
                    {
                        clickedOnTile.SetLocation(new Vector2(ms.X, ms.Y));
                    }
                }
            }
            else if (ms.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
            {
                if (clickedOnTile != null)
                {
                    board.PlaceTile(clickedOnTile, ms.X, ms.Y);
                    clickedOnTile = null;
                }
                else if (movingCamera)
                {
                    movingCamera = false;
                }
            }
            else
            {
                clickedOnTile = null;
            }

            if (clickedOnTile != null && /*Keyboard.GetState().IsKeyDown(Keys.Right)) //*/ms.RightButton == ButtonState.Pressed && previousMouse.RightButton == ButtonState.Released)
            {
                clickedOnTile.Rotate();
            }

            previousMouse = ms;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            board.Draw(spriteBatch, clickedOnTile);

            if (clickedOnTile != null)
            {
                clickedOnTile.Draw(spriteBatch, false, true);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
