using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace Microwave.Tests.Integration
{
    [TestFixture]
    public class IT1_Display
    {
        private IOutput _outputSub;
        private IDisplay _sut;

        [SetUp]
        public void SetUp()
        {
            _outputSub = Substitute.For<IOutput>();
            _sut = new Display(_outputSub);
        }

        [Test]
        public void ShowTimeIsCalledWithParameters()
        {
            _sut.ShowTime(10,10);

            _outputSub.Received(1).OutputLine($"Display shows: {10:D2}:{10:D2}");
        }

        [Test]
        public void ShowPowerIsCalledWithParameters()
        {
            _sut.ShowPower(10);

            _outputSub.Received(1).OutputLine($"Display shows: {10} W");
        }

        [Test]
        public void ClearIsCalled()
        {
            _sut.Clear();

            _outputSub.Received(1).OutputLine("Display cleared");
        }
    }
}
