using System;
using Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TestInteractionLogging
    {
        const string BaseAddress = "net.tcp://localhost:31461/wcfTest";

        [TestInitialize]
        public void Setup()
        {
            Mocks.StaticCounterInteractionLog.Reset();
        }

        void InvokeEchoCallback(IEcho client)
        {
            client.Echo("hi");
        }

        void DoNothingCallback(IEcho client)
        {
            // do nothing
        }

        void CheckEchoDoesNotReturnNullCallback(IEcho client)
        {
            var cid = client.Echo("hi");
            Assert.IsNotNull(cid);

            Guid guid;
            Assert.IsTrue(Guid.TryParse(cid, out guid));
        }

        [TestMethod]
        public void No_Message_Logged_When_No_Service_Called()
        {
            Tools.StartAndInvokeService<
                Services.IEcho,
                Mocks.DoNothingEchoService,
                Mocks.StaticCounterInteractionLog,
                Behaviours.Implementation.WcfInteractionState>(BaseAddress, DoNothingCallback);

            Assert.AreEqual(0, Mocks.StaticCounterInteractionLog.MessageCount);
        }

        [TestMethod]
        public void CorrelationID_Is_Assigned_In_Operation_Context()
        {
            Tools.StartAndInvokeService<
                Services.IEcho,
                Mocks.ReturnCorrelationIDEchoService,
                Mocks.StaticCounterInteractionLog,
                Behaviours.Implementation.WcfInteractionState>(BaseAddress, CheckEchoDoesNotReturnNullCallback);
        }

        [TestMethod]
        public void Request_And_Reply_Messages_Are_Logged()
        {
            Tools.StartAndInvokeService<
                Services.IEcho,
                Mocks.DoNothingEchoService,
                Mocks.StaticCounterInteractionLog,
                Behaviours.Implementation.WcfInteractionState>(BaseAddress, InvokeEchoCallback);

            Assert.AreEqual(2, Mocks.StaticCounterInteractionLog.MessageCount);
        }
    }
}
