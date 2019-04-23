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
        private IOutput outputSub;
        private IDisplay display;

        [SetUp]
        public void SetUp()
        {
            outputSub = Substitute.For<IOutput>();
            display = new Display(outputSub);
        }

        [Test]
        public void ShowTimeIsCalledWithParameters()
        {
            display.ShowTime(10,10);

            outputSub.Received(1).OutputLine($"Display shows: {10:D2}:{10:D2}");
        }

        [Test]
        public void ShowPowerIsCalledWithParameters()
        {
            display.ShowPower(10);

            outputSub.Received(1).OutputLine($"Display shows: {10} W");
        }

        [Test]
        public void ClearIsCalled()
        {
            display.Clear();

            outputSub.Received(1).OutputLine("Display cleared");
        }
    }
}
