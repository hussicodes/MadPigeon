using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework.Audio;

namespace madpigeon.Content.Classes
{
    public class GameController
    {
        public const int START_STATE = 0;
        public const int PLAY_STATE = 1;
        public const int LOSE_STATE = 2;
        public const int PAUSE_STATE = 3;
        



        private MouseState previousMouseState;
        private int upDistance;
        private int horizontalDistanceCounter;
        private ArrayList arrayPipes;
        private int gameState;
        public static SoundEffect dieSound;
        public static SoundEffect hitSound;
        private int score;
        private bool point;


        public GameController()
        {
            gameState = 0;
            score = 0;
            point = true;
            previousMouseState = new MouseState();
            upDistance = 0;
            arrayPipes = new ArrayList();
            horizontalDistanceCounter = 0;
            gameState = START_STATE;
        }

        public void MovePipes()
        {
            foreach(Pipe pipe in arrayPipes.ToArray())
            {
                //This will make it move it move it, you got to - MOVE IT!
                pipe.Move();

                //This will make it remove it remove it, you got to - REMOVE IT!

                if(pipe.TopPipeRectangle.Right < 0)
                {
                    arrayPipes.Remove(pipe);
                }
            }
        }

        public void VerifyIfIncreaseScore(Pigeon pigeon)
        {
            foreach(Pipe pipe in arrayPipes.ToArray())
            {
                if (pipe.State == Pipe.FRONT_STATE)
                {
                    if(point && pigeon.Rectangle.X >= pipe.TopPipeRectangle.X && pigeon.Rectangle.Y > pipe.TopPipeRectangle.Y && pigeon.Rectangle.Y < pipe.BottomPipeRectangle.Y)
                    {
                        score++;
                        point = false;
                    }
                    if(pigeon.Rectangle.X >= pipe.TopPipeRectangle.Right)
                    {
                        point = true;
                        pipe.State = Pipe.BACK_STATE;
                    }
                }
            }
        }



        public void AddPipes()
        {
            horizontalDistanceCounter++;
            if(horizontalDistanceCounter >= Pipe.horizontalDistanceBetween)
            {
                arrayPipes.Add(new Pipe());
                horizontalDistanceCounter = 0;
            }
        }
        public void RaisePigeonOnClick(Pigeon pigeon)
        {
            if(previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                pigeon.IsUp = true;
                Pigeon.wingSound.Play();
            }
            previousMouseState = Mouse.GetState();
            if (upDistance < 10 && pigeon.IsUp)
            {
                upDistance++;
                pigeon.GoUp();
            }
            else
            {
                upDistance = 0;
                pigeon.IsUp = false;
                pigeon.GoDown();
            }

        }

        public void VerifyLoseForImpactPipe(Pigeon pigeon)
        {
            foreach (Pipe pipe in arrayPipes.ToArray())
            {
                if (pigeon.Rectangle.Intersects(pipe.TopPipeRectangle) || pigeon.Rectangle.Intersects(pipe.BottomPipeRectangle))
                {
                    hitSound.Play();
                    dieSound.Play();
                    //Loss upon impact
                    gameState = LOSE_STATE;
                }
            }
        }
        
        public void ResumeGame()
        {
            gameState = PLAY_STATE;

        }


        public void VeryifyLoseForImpactFloor(Pigeon pigeon)
        {
            if(pigeon.IsOnFloor())
            {
                dieSound.Play();
                gameState = LOSE_STATE;
            }
        }


        public ArrayList ArrayPipes
        {
            get
            {
                return arrayPipes;
            }
            set
            {
                this.arrayPipes = value;
            }
        }


      

        public int GameState
        {
            get
            {
                return gameState;
            }
            set
            {
                this.gameState = value;
            }
        }


        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                this.score = value;
            }
        }

    }
}
