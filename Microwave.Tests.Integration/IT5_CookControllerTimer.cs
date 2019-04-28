using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Tests.Integration
{
    [TestFixture]
    public class IT5_CookControllerTimer
    {
        private IOutput _outputSub;
        private ITimer _timer;
        private IUserInterface _userInterfaceSub;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ICookController _sut;

        [SetUp]
        public void Setup()
        {
            _outputSub = Substitute.For<IOutput>();
            _timer = new Timer();
            _userInterfaceSub = Substitute.For<IUserInterface>();
            _display = new Display(_outputSub);
            _powerTube = new PowerTube(_outputSub);
            _sut = new CookController(_timer, _display, _powerTube, _userInterfaceSub);
        }

        [Test]
        public void TimerTickEvent()
        {
            _sut.StartCooking(50, 10);
            System.Threading.Thread.Sleep(1500);

            _outputSub.Received().OutputLine($"Display shows: {0:D2}:{9:D2}");
        }

        [Test]
        public void StartCookingTimerStart()
        {
            int time = 20;
            _sut.StartCooking(50,20);
            System.Threading.Thread.Sleep(2000);

            Assert.That(_timer.TimeRemaining<time);
        }

        [Test]
        public void StopCookingTimerStop()
        {
            int time = 20;
            _sut.StartCooking(50,1);
            System.Threading.Thread.Sleep(1010);
            _sut.Stop();
            int time1 = _timer.TimeRemaining;
            System.Threading.Thread.Sleep(1010);
            int time2 = _timer.TimeRemaining;

            Assert.That(time1==time2);
        }

        [Test]
        public void TimerExpiredEvent()
        {
            _sut.StartCooking(50, 2);
            System.Threading.Thread.Sleep(2600);

            _outputSub.Received().OutputLine($"PowerTube turned off");
        }


    }
}
