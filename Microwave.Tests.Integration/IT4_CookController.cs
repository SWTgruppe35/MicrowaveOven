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
        public void StartCookingPowerTubeThrownNothing()
        {
            Assert.That(()=>_sut.StartCooking(50,60),Throws.Nothing);
        }
    }
}
