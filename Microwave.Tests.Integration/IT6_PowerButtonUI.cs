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
    public class IT6_PowerButtonUI
    {
        private IUserInterface _sut;
        private IOutput _outputSub;
        private IDoor _doorSub;
        private IButton _powerButton;
        private IButton _timeButtonSub;
        private IButton _startCancelButtonSub;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cookControllerSub; 

        [SetUp]
        public void Setup()
        {
            _outputSub = Substitute.For<IOutput>();
            _light = new Light(_outputSub);
            _display = new Display(_outputSub);
            _doorSub = Substitute.For<IDoor>(); 
            _powerButton = new Button();
            _timeButtonSub = Substitute.For<IButton>();
            _startCancelButtonSub = Substitute.For<IButton>();
            _cookControllerSub = Substitute.For<ICookController>(); 

            _sut = new UserInterface(_powerButton, _timeButtonSub, _startCancelButtonSub, _doorSub, _display, _light, _cookControllerSub);
        }

        [Test]
        public void PowerButtonPressed()
        {
            _powerButton.Press();
            _outputSub.Received().OutputLine($"Display shows: {50} W");
        }

        [Test]
        public void PowerButtonPressedIncreasePower()
        {
            _powerButton.Press();
            _powerButton.Press();

            _outputSub.Received().OutputLine($"Display shows: {100} W");
        }

        [Test]
        public void PowerButtonPressedFifteenTimes()
        {
            for (int i = 0; i < 15; i++)
            {
                _powerButton.Press();
            }

            _outputSub.Received(2).OutputLine($"Display shows: {50} W");
        }

        [Test]
        public void PowerButtonPressedTwoTimesAndDoorIsOpenedAndClosed()
        {
            _powerButton.Press();
            _powerButton.Press();

            _doorSub.Opened += Raise.Event();
            _doorSub.Closed += Raise.Event();

            _powerButton.Press();

            _outputSub.Received(2).OutputLine($"Display shows: {50} W");
        }
    }
}
