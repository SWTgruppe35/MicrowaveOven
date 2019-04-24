using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Tests.Integration
{
    [TestFixture]
    public class IT4_CookController
    {
        private IOutput _outputSub;
        private ITimer _timerSub;
        private IUserInterface _userInterfaceSub;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ICookController _sut;

        [SetUp]
        public void Setup()
        {
            _outputSub = Substitute.For<IOutput>();
            _timerSub = Substitute.For<ITimer>();
            _userInterfaceSub = Substitute.For<IUserInterface>();
            _display = new Display(_outputSub);
            _powerTube = new PowerTube(_outputSub);
            _sut = new CookController(_timerSub, _display, _powerTube, _userInterfaceSub);
        }

        [Test]
        public void PowerTubeStopped()
        {
            _sut.StartCooking(50,2000);
            _sut.Stop();

            _outputSub.Received().OutputLine($"PowerTube turned off");
        }

        [Test]
        public void PowerTubeStarted()
        {
            _sut.StartCooking(50, 2000);
            _sut.Stop();

            _outputSub.Received().OutputLine($"PowerTube works with {50} %");
        }

        [Test]
        public void TimerStarted()
        {
            _sut.StartCooking(50, 2000);
            _sut.Stop();

            _timerSub.Received(1).Start(2000);
        }

        [Test]
        public void TimerStopped()
        {
            _sut.StartCooking(50, 2000);
            _sut.Stop();

            _timerSub.Received(1).Stop();
        }

        [Test]
        public void StartCooking_PowerTube_ThrownNothing()
        {
            Assert.That(()=>_sut.StartCooking(50,60),Throws.Nothing);
        }

        [Test]
        public void Powertube_Stopped_After_TimerExpireEvent()
        {
            _sut.StartCooking(50,2000);
            _timerSub.Expired += Raise.Event();

            _outputSub.Received(1).OutputLine($"PowerTube turned off");

        }

        [Test]
        public void Powertube_NotStopped_After_TimerExpireEvent()
        {
            _timerSub.Expired += Raise.Event();

            _outputSub.DidNotReceiveWithAnyArgs().OutputLine("");
        }

        [Test]
        public void Outputs_TimeRemaining_After_TimerTickEvent()
        {
            _sut.StartCooking(50,2);

            _timerSub.TimeRemaining.Returns(2);
            _timerSub.TimerTick += Raise.Event();

            _outputSub.Received().OutputLine($"Display shows: {0:D2}:{2:D2}");
        }

    }
}
