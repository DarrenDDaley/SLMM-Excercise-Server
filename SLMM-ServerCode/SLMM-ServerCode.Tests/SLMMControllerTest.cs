using Microsoft.VisualStudio.TestTools.UnitTesting;

using SLMM_ServerCode.Models;
using SLMM_ServerCode.Controllers;

namespace SLMM_ServerCode.Tests
{
    [TestClass]
    public class SLMMControllerTest
    {
        [TestMethod]
        public void LawnDimensions()
        {
            SLMMController controller = new SLMMController();
            MowerSession session = controller.LawnDimensions(10, 10);

            Assert.AreEqual(10, session.Width);
            Assert.AreEqual(10, session.Height);
        }

        [TestMethod]
        public void MowerPosition()
        {
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);

            MowerSession session = controller.MowerPosition(2, 5, postback);

            Assert.AreEqual(2, session.PositionX);
            Assert.AreEqual(5, session.PositionY);
        }

        [TestMethod]
        public void MoveLeft()
        {
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);

            MowerSession session = controller.MoveLeft(postback);

            Assert.AreEqual(session.PositionX, 1);
            Assert.AreEqual(session.PositionY, 1);
        }

        [TestMethod]
        public void MoveRight()
        {
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);

            MowerSession session = controller.MoveRight(postback);

            Assert.AreEqual(session.PositionX, 2);
            Assert.AreEqual(session.PositionY, 1);
        }

        [TestMethod]
        public void MoveUp()
        {
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);

            MowerSession session = controller.MoveUp(postback);

            Assert.AreEqual(session.PositionX, 1);
            Assert.AreEqual(session.PositionY, 1);
        }

        [TestMethod]
        public void MoveDown()
        {
            SLMMController controller = new SLMMController();
            MowerSession postback = controller.LawnDimensions(10, 10);

            MowerSession session = controller.MoveDown(postback);

            Assert.AreEqual(session.PositionX, 1);
            Assert.AreEqual(session.PositionY, 2);
        }
    }
}
