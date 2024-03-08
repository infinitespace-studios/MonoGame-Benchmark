using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Benchmark;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    SpriteFont Font;

    int maxLols = 0;

    Random random = new Random ();

    const int MAXLOLS = 500000;

    struct VertexPositionAndRotation {
        public Vector2[] Position = new Vector2[MAXLOLS];
        public Vector2[] Rotation = new Vector2[MAXLOLS];

        public Color[] Color = new Microsoft.Xna.Framework.Color[MAXLOLS];

        public VertexPositionAndRotation()
        {
        }

        public void Update (GameTime gameTime, int count)
        {
            for (int i=0; i < count; i++) {
                Rotation[i].X += Rotation[i].Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (Rotation[i].X > 360f)
                    Rotation[i].X = 0f;
            }
        }

    }

    VertexPositionAndRotation lols = new VertexPositionAndRotation ();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        // run at full speed
        IsFixedTimeStep = false;
        _graphics.SynchronizeWithVerticalRetrace = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        Font = Content.Load<SpriteFont> ("Arial");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if (gameTime.ElapsedGameTime < TimeSpan.FromMilliseconds(33)) {
            int c = maxLols;
            maxLols += 1000;
            for (int i = c; i < maxLols; i++) {
                   lols.Position[i] = new Vector2 (random.NextSingle () * 800f, random.NextSingle () * 600f);
                   lols.Rotation[i] = new Vector2(random.NextSingle (), random.NextSingle ());
                   lols.Color[i] = new Color (random.Next (0, 255), random.Next (0, 255), random.Next (0, 255));
            }
            Console.WriteLine ($"DEBUG: {maxLols}");
        }

        lols.Update (gameTime, maxLols);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin ();
        for (int i=0; i < maxLols; i++) {
            _spriteBatch.DrawString (Font, "LOL?", lols.Position[i], lols.Color[i], MathHelper.ToRadians (lols.Rotation[i].X), new Vector2 (.5f, .5f), 1f, SpriteEffects.None, 1f );
        }
        _spriteBatch.End ();

        _spriteBatch.Begin (SpriteSortMode.Immediate);
        _spriteBatch.DrawString (Font, $"Total: {maxLols}", new Vector2 (10, 10), Color.Red);
        _spriteBatch.End ();

        base.Draw(gameTime);
    }
}
