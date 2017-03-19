using Microsoft.VisualStudio.TestTools.UnitTesting;

using SLMM_ServerCode.Models;
using SLMM_ServerCode.Controllers;

namespace SLMM_ServerCode.Tests
{
    [TestClass]
    public class SLMMControllerTest
    {

        //This method tests to see if the Lawn Dimensions method works correctly
        [TestMethod]
        public void LawnDimensions()
        {
            // This section details with the intial setup of the test
            SLMMController controller = new SLMMController();
            MowerSession session = controller.LawnDimensions(10, 10);

            //This assert checks to see if the width entered intially in the method is the same width that comes 
            // out
            Assert.AreEqual(10, session.Width);

            // This assert checks to see if the height entered initially in the method is the same height that comes
            // out
            Assert.AreEqual(10, session.Height);
        }


        // This method tests to see if the MowerPosition method correctly works 
        [TestMethod]
        public void MowerPosition()
        {

            // This section details with the intial setup of the test
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);

            MowerSession session = controller.MowerPosition(2, 5, postback);

            // This checks to see if the actual Position X given initally is the same 
            // as the one that is actually assigned
            Assert.AreEqual(2, session.PositionX);

            // This checks to see if the actual Position Y given initally is the same 
            // as the one that is actually assigned
            Assert.AreEqual(5, session.PositionY);
        }

        // This method makes sure that the MoveLeft Method actually moves the mower to the left
        [TestMethod]
        public void MoveLeft()
        {
            // This section details with the intial setup of the test
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);
            MowerSession session = controller.MoveLeft(postback);

            // This tests for two things first it makes sure that the method comes back with the correct  
            // information but it is also check to see if the clamp method in the controller is making 
            // sure that the mower does not go out of bounds
            Assert.AreEqual(session.PositionX, 1);
            Assert.AreEqual(session.PositionY, 1);
        }

        // This method makes sure that the MoveLeft Method actually moves the mower to the right
        [TestMethod]
        public void MoveRight()
        {
            // This section details with the intial setup of the test
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);
            MowerSession session = controller.MoveRight(postback);

            // This check is to make sure that the user moves to the correct position 
            Assert.AreEqual(session.PositionX, 2);
            Assert.AreEqual(session.PositionY, 1);
        }

        // This method makes sure that the MoveLeft Method actually moves the mower up
        [TestMethod]
        public void MoveUp()
        {
            // This section details with the intial setup of the test
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);
            MowerSession session = controller.MoveUp(postback);

            // This tests for two things first it makes sure that the method comes back with the correct  
            // information but it is also check to see if the clamp method in the controller is making 
            // sure that the mower does not go out of bounds
            Assert.AreEqual(session.PositionX, 1);
            Assert.AreEqual(session.PositionY, 1);
        }

        // This method makes sure that the MoveLeft Method actually moves the mower down
        [TestMethod]
        public void MoveDown()
        {
            // This section details with the intial setup of the test
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);
            MowerSession session = controller.MoveDown(postback);

            // This check is to make sure that the user moves to the correct position 
            Assert.AreEqual(session.PositionX, 1);
            Assert.AreEqual(session.PositionY, 2);
        }
    }
}
