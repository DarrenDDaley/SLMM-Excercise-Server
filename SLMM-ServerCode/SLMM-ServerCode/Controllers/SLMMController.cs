using System;
using System.Web.Http;
using System.Threading;

using SLMM_ServerCode.Models;

namespace SLMM_ServerCode.Controllers
{
    public class SLMMController : ApiController
    {
        public SLMMController()
        {
        }

        private const int minWidth = 1;
        private const int minHeight = 1;

        private readonly int threadSleepTime = 1000;

        [HttpPost]
        public MowerSession LawnDimensions(int x, int y)
        {
            MowerSession session = new MowerSession();

            if (x <= 0 || y <= 0) {
                session.Error = "Positive numbers only";
            }

            session.Width = x;
            session.Height = y;

            session.PositionX = minWidth;
            session.PositionY = minHeight;

            session.Status = MowerStatus(string.Format("set the dimensions of the lawn. {0} {1}",
                                         session.Width, session.Height), session);

            return session;
        }

        [HttpPost]
        public MowerSession MowerPosition(int x, int y, MowerSession session)
        {
            if (x <= 0 || y <= 0) {
                session.Error = "Positive numbers only";
            }

            if (session.Width <= 0 || session.Height <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            session.PositionX = Clamp(x, minWidth, session.Width);
            session.PositionY = Clamp(y, minHeight, session.Height);
            session.Status = MowerStatus("set the position of the mower", session);

            return session;
        }

        [HttpPost]
        public MowerSession MoveLeft(MowerSession session)
        {
            if (session.Width <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            session.PositionX--;
            session.PositionX = Clamp(session.PositionX, minWidth, session.Width);
            session.Status = MowerStatus("moved the mower left by one", session);

            return session;
        }

        [HttpPost]
        public MowerSession MoveRight(MowerSession session)
        {
            if (session.Width <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            session.PositionX++;
            session.PositionX = Clamp(session.PositionX, minWidth, session.Width);
            session.Status = MowerStatus("moved the mower right by one", session);

            return session;
        }

        [HttpPost]
        public MowerSession MoveUp(MowerSession session)
        {
            if (session.Height <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            session.PositionY--;
            session.PositionY = Clamp(session.PositionY, minHeight, session.Height);
            session.Status = MowerStatus("moved the mower up by one", session);

            return session;
        }

        [HttpPost]
        public MowerSession MoveDown(MowerSession session)
        {
            if (session.Height <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            session.PositionY++;
            session.PositionY = Clamp(session.PositionY, minHeight, session.Height);
            session.Status = MowerStatus("moved the mower down by one", session);

            return session;
        }


        // This method creates the output messages 
        private string MowerStatus(string action, MowerSession session)
        {
            Thread.Sleep(threadSleepTime);

            // I used the Format method to cleanly construct the string
            return string.Format("Time: {0} - Action: {1} - Position: X {2}, Y {3}.",
                                   DateTime.Now.ToString("HH:mm:ss"), action, session.PositionX, session.PositionY);
        }

        // This method makes sure the X and Y positions are kept within the min and max bounds 
        // It's based on a native method that is in the XNA Math library
        private static int Clamp(int value, int min, int max) {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}