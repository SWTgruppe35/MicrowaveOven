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

        [TestCase(1)]
        [TestCase(100)]
        public void TurnOnPowerTubeIsCalledWithParameters(int power)
        {
            _sut.TurnOn(power);
            _outputSub.Received(1).OutputLine($"PowerTube works with {power} %");
        }

        [Test]
        public void TurnOnAndOffPowerTubeIsCalled()
        {
            _sut.TurnOn(10);
            _sut.TurnOff();
            _outputSub.Received(1).OutputLine($"PowerTube works with {10} %");
            _outputSub.Received(1).OutputLine($"PowerTube turned off");
        }

    }
}
