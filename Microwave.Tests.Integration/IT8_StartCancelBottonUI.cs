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
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace Microwave.Tests.Integration
{
    [TestFixture]
    class IT8_UserInterfaceDoor
    {
        private IUserInterface _sut;
        private IOutput _outputSub;
        private IDoor _doorSub;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
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
            _timeButton = new Button();
            _startCancelButton = new Button();
            _cookControllerSub = Substitute.For<ICookController>();
            _sut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _doorSub, _display, _light, _cookControllerSub);
            

        }

        [Test]
        public void StartCancelButtonSetPowerState()
        {
            _powerButton.Press();
            _startCancelButton.Press();
            _outputSub.Received().OutputLine($"Display shows: {50} W");
        }
        
        [Test]
        public void StartCancel_btn_pressed_In_SetTimeState()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _outputSub.Received().OutputLine($"Display cleared");
            _outputSub.Received().OutputLine($"Light is turned on");
            _cookControllerSub.Received().StartCooking(50,60);
        } 
        
        [Test]
        public void StartCancelButtonSetCooking()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            _outputSub.Received().OutputLine($"Display shows: {50} W");
            _cookControllerSub.Received().Stop();
        }




    }
}
