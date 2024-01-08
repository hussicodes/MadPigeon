using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace madpigeon.Content.Classes
{
    public class Scene
    {
        public static GraphicsDeviceManager graphics;
        private Texture2D backgroundTexture;
        private Texture2D floorTexture;
        private Rectangle backgroundRectangle;
        private Rectangle floorRectangle;
        private Rectangle backgroundRectangle2;
        private Rectangle floorRectangle2;

        public Scene()
        {
            backgroundRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            floorRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            backgroundRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            floorRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void Move()
        {
            MoveBackground();
            MoveFloor();
        }
        private void MoveBackground()
        {
            if (backgroundRectangle.X <= -graphics.PreferredBackBufferWidth)
                backgroundRectangle.X = backgroundRectangle2.Right;
            if (backgroundRectangle2.X <= -graphics.PreferredBackBufferWidth)
                backgroundRectangle2.X = backgroundRectangle.Right;
            backgroundRectangle.X -= 1;
            backgroundRectangle2.X -= 1;
        }

        private void MoveFloor()
        {
            if (floorRectangle.X <= -graphics.PreferredBackBufferWidth)
                floorRectangle.X = floorRectangle2.Right;
            if (floorRectangle2.X <= -graphics.PreferredBackBufferWidth)
                floorRectangle2.X = floorRectangle.Right;
            floorRectangle.X -= 5;
            floorRectangle2.X -= 5;
        }

        public Texture2D BackgroundTexture
        {
            get
            {
                return backgroundTexture;
            }
            set
            {
                this.backgroundTexture = value;
            }
        }
        public Texture2D FloorTexture
        {
            get
            {
                return floorTexture;
            }
            set
            {
                this.floorTexture = value;
            }
        }
        public Rectangle BackgroundRectangle
        {
            get
            {
                return backgroundRectangle;
            }
            set
            {
                this.backgroundRectangle = value;
            }
        }
        public Rectangle FloorRectangle
        {
            get
            {
                return floorRectangle;
            }
            set
            {
                this.floorRectangle = value;
            }
        }

        public Rectangle BackgroundRectangle2
        {
            get
            {
                return backgroundRectangle2;
            }
            set
            {
                this.backgroundRectangle2 = value;
            }
        }
        public Rectangle FloorRectangle2
        {
            get
            {
                return floorRectangle2;
            }
            set
            {
                this.floorRectangle2 = value;
            }
        }
    }



}