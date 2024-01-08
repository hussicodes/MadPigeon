using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace madpigeon.Content.Classes
{
    public class Pigeon
    {
        public static GraphicsDeviceManager graphics;
        public static SoundEffect wingSound;
        private Texture2D[] texture2D;
        private Rectangle rectangle;
        private readonly int initX;
        private readonly int initY;
        private readonly int initWidth;
        private readonly int initHeight;
        private int delayShoot;
        private bool isUp;
        private ArrayList arrayFireballs;
        public Pigeon(Texture2D MADPIGEON1, Texture2D MADPIGEON2, Texture2D MADPIGEON3)
        {
            texture2D = new Texture2D[3];
            texture2D[0] = MADPIGEON1;
            texture2D[1] = MADPIGEON2;
            texture2D[2] = MADPIGEON3;
            arrayFireballs = new ArrayList();
            initWidth = 60;
            initHeight = 45;
            delayShoot = 0;
            isUp = false;
            initX = (graphics.PreferredBackBufferWidth / 4 - (initWidth / 2));
            initY = (4 * (graphics.PreferredBackBufferHeight / 10)) - (initHeight / 2);
            rectangle = new Rectangle(initX, initY, initWidth, initHeight);


        }

        public void ResetPosition()
        {
            rectangle.X = initX;
            rectangle.Y = initY;

        }

        public bool IsOnFloor()
        {
            return rectangle.Y > 485;
            
        }
        public void GoUp()
        {
            rectangle.Y -= 10;
        }

        public void GoDown()
        {
            rectangle.Y += 5;
        }


        public Texture2D[] Texture2D
        {
            get
            {
                return texture2D;
            }
            set
            {
                this.texture2D = value;
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                this.rectangle = value;
            }
        }

        public Pigeon(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }

        public Pigeon(Texture2D texture2D)
        {
        }

        public bool IsUp
        {
            get
            {
                return isUp;
            }
            set
            {
                this.isUp = value;
            }
        }
        public int DelayShoot
        {
            get
            {
                return delayShoot;
            }
            set
            {
                this.delayShoot = value;
            }
        }
        public ArrayList arrayFireBalls
        {
            get
            {
                return arrayFireballs;
            }
            set
            {
                this.arrayFireBalls = value;
            }
        }
    }
}
