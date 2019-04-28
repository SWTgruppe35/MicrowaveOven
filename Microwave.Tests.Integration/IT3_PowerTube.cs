using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Tests.Integration
{
    [TestFixture]
    public class IT3_PowerTube
    {
        private IOutput _outputSub;
        private IPowerTube _sut;

        [SetUp]
        public void SetUp()
        {
            _outputSub = Substitute.For<IOutput>();
            _sut = new PowerTube(_outputSub);
        }

        [TestCase(50)]
        [TestCase(500)]
        public void TurnOnPowerTubeIsCalledWithParameters(int power)
        {
            _sut.TurnOn(power);
            _outputSub.Received(1).OutputLine($"PowerTube works with {power} W");
        }

        [Test]
        public void TurnOnAndOffPowerTubeIsCalled()
        {
            _sut.TurnOn(50);
            _sut.TurnOff();
            _outputSub.Received(1).OutputLine($"PowerTube works with {50} W");
            _outputSub.Received(1).OutputLine($"PowerTube turned off");
        }

    }
}
