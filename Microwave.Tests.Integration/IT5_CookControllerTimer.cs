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
        public void StartCookingTimerStart()
        {
            int time = 20000;
            _sut.StartCooking(50,time);
            System.Threading.Thread.Sleep(1010);

            Assert.That(_timer.TimeRemaining<time);
        }

        [Test]
        public void StopCookingTimerStop()
        {
            int time = 20000;
            _sut.StartCooking(50,1000);
            System.Threading.Thread.Sleep(1010);
            _sut.Stop();
            int time1 = _timer.TimeRemaining;
            System.Threading.Thread.Sleep(1010);
            int time2 = _timer.TimeRemaining;

            Assert.That(time1==time2);
        }
    }
}
