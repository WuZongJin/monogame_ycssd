using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.General
{
    public class Animation
    {
        #region Variables
        List<AnimationFrame> frames = new List<AnimationFrame>();
        TimeSpan timeIntoAnimation;
        int frameNum;
        int temp;
        bool isLoop = true;
        bool isFinshed = false;
        #endregion

        #region Properties
        public bool IsFinshed
        {
            get { return isFinshed; }
        }
        public bool IsLoop
        {
            get { return isLoop; }
            set { isLoop = value; }
        }
        public int FrameNum
        {
            get { return frameNum; }
            set
            {
                frameNum = value;
                temp = frameNum;
            }
        }
        TimeSpan Duration
        {
            get
            {
                double totalSeconds = 0;
                foreach (var frame in frames)
                {
                    totalSeconds += frame.Duration.TotalSeconds;
                }

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public Rectangle CurrentRectangle
        {
            get
            {
                AnimationFrame currentFrame = null;

                // See if we can find the frame
                TimeSpan accumulatedTime = new TimeSpan(0);
                foreach (var frame in frames)
                {
                    if (accumulatedTime + frame.Duration >= timeIntoAnimation)
                    {
                        
                        currentFrame = frame;
                        break;
                    }
                    else
                    {
                        //if (!isLoop)
                        //{
                        //    temp--;
                        //    currentFrame = frame;
                        //    if (temp == 0)
                        //    {
                        //        isFinshed = true;
                        //        isLoop = true;
                        //    }

                        //}
                        accumulatedTime += frame.Duration;
                    }
                }

                // If no frame was found, then try the last frame, 
                // just in case timeIntoAnimation somehow exceeds Duration
                if (currentFrame == null)
                {
                    currentFrame = frames.LastOrDefault();
                }

                // If we found a frame, return its rectangle, otherwise
                // return an empty rectangle (one with no width or height)
                if (currentFrame != null)
                {
                    if (currentFrame == frames.Last<AnimationFrame>())
                    {
                        isFinshed = true;
                    }
                    return currentFrame.SourceRectangle;
                }
                else
                {
                    return Rectangle.Empty;
                }
            }
        }
        #endregion

        #region Method
        public void AddFrame(Rectangle rectangle, TimeSpan duration)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                Duration = duration
            };

            frames.Add(newFrame);
        }

        public void Update(GameTime gameTime)
        {
            double secondsIntoAnimation =
                timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;


            double remainder = secondsIntoAnimation % Duration.TotalSeconds;

            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }
        #endregion
    }
}
