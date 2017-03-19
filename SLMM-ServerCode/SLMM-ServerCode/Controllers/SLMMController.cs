using System;
using System.Web.Http;
using System.Threading;

using SLMM_ServerCode.Models;

namespace SLMM_ServerCode.Controllers
{
    public class SLMMController : ApiController
    {
        // I picked the top left corner as the starting point of the grid because it's the 
        // one I use most in my previous programs as it's the default one for DirectX

        // The min and max values here are to make sure the lawm mower doesn't go out of bound
        // I've set them as private so that they can't be accessed anywhere else and as consts so 
        // that they can't be changed 
        private const int minWidth = 1;
        private const int minHeight = 1;


        // This method sets is here to setup the intial dimensions of the lawn
        // The signature using integers makes sure that the user can't send any 
        // but numbers back
        [HttpPost]
        public MowerSession LawnDimensions(int x, int y)
        {
            // This creates a new mowersession object to send back to the client applications
            MowerSession session = new MowerSession();

            // This makes sure that the user doesn't send back negetive numbers
            // and if this happens then they are informed in the object that this isn't 
            // an accepted format
            if (x <= 0 || y <= 0) {
                session.Error = "Positive numbers only";
            }

            // This sets up the width and height of the lawn
            session.Width = x;
            session.Height = y;

            // This set up the X and Y positon as the minimum width and height of the lawn
            session.PositionX = minWidth;
            session.PositionY = minHeight;

            // This accesses the status string section of the object, makes a call to the mower status method
            // and then sends back the appropriate debug information to be outputted by the client
            session.Status = MowerStatus(string.Format("set the dimensions of the lawn. {0} {1}",
                                         session.Width, session.Height), session);

            // This sends back the object
            return session;
        }

        // This method sets is here to setup the intial position of the lawn
        // The signature using integers makes sure that the user can't send any 
        // but numbers back
        [HttpPost]
        public MowerSession MowerPosition(int x, int y, MowerSession session)
        {

            // This makes sure that the user doesn't send back negetive numbers
            // and if this happens then they are informed in the object that this isn't 
            // an accepted format
            if (x <= 0 || y <= 0) {
                session.Error = "Positive numbers only";
            }

            if (session.Width <= 0 || session.Height <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            // This sets up the position of the mower on the X and Y position
            // while making a call to the clamp method to make sure that it does
            // not go below the min width and height or beyond the width or height 
            // of the lawn
            session.PositionX = Clamp(x, minWidth, session.Width);
            session.PositionY = Clamp(y, minHeight, session.Height);


            // This accesses the status string section of the object, makes a call to the mower status method
            // and then sends back the appropriate debug information to be outputted by the client
            session.Status = MowerStatus("set the position of the mower", session);

            // This sends back the object
            return session;
        }

        [HttpPost]
        public MowerSession MoveLeft(MowerSession session)
        {

            // This makes sure that the width of the session is set 
            if (session.Width <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            // This moves the lawn mower left by one
            session.PositionX--;

            // This makes sure that the mower doesn't go beyond the min width or the lawn width
            session.PositionX = Clamp(session.PositionX, minWidth, session.Width);


            // This accesses the status string section of the object, makes a call to the mower status method
            // and then sends back the appropriate debug information to be outputted by the client
            session.Status = MowerStatus("moved the mower left by one", session);

            return session;
        }

        [HttpPost]
        public MowerSession MoveRight(MowerSession session)
        {
            // This makes sure that the width of the session is set 
            if (session.Width <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            // This moves the lawn mower right by one
            session.PositionX++;

            // This makes sure that the mower doesn't go beyond the min width or the lawn width
            session.PositionX = Clamp(session.PositionX, minWidth, session.Width);


            // This accesses the status string section of the object, makes a call to the mower status method
            // and then sends back the appropriate debug information to be outputted by the client
            session.Status = MowerStatus("moved the mower right by one", session);

            // This sends back the object
            return session;
        }

        [HttpPost]
        public MowerSession MoveUp(MowerSession session)
        {
            // This makes sure that the height of the session is set 
            if (session.Height <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }

            // This moves the lawn mower up by one 
            session.PositionY--;

            // This makes sure that it doesn't go beyond the min height or the lawn height
            session.PositionY = Clamp(session.PositionY, minHeight, session.Height);


            // This accesses the status string section of the object, makes a call to the mower status method
            // and then sends back the appropriate debug information to be outputted by the client
            session.Status = MowerStatus("moved the mower up by one", session);

            // This sends back the object
            return session;
        }

        [HttpPost]
        public MowerSession MoveDown(MowerSession session)
        {
            // This makes sure that the height of the session is set 
            if (session.Height <= 0) {
                session.Error = "Please set the width and height of the lawn";
            }
            
            // This moves the lawn mower down by one 
            session.PositionY++;

            // This makes sure that it doesn't go beyond the min height or the lawn height
            session.PositionY = Clamp(session.PositionY, minHeight, session.Height);


            // This accesses the status string section of the object, makes a call to the mower status method
            // and then sends back the appropriate debug information to be outputted by the client
            session.Status = MowerStatus("moved the mower down by one", session);

            // This sends back the object
            return session;
        }


        // This method creates the output messages 
        private string MowerStatus(string action, MowerSession session)
        {
         // This is sleep time for thread, I've set it as 1000 as stated in the document and 
         // made it const so that it's can be changed further down the line 
        const int threadSleepTime = 1000;

        // This is the method that actually puts the program to sleep 
        // it's there to simulate latency on a network
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