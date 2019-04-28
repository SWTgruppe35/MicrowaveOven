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
using NUnit.Framework.Internal;

namespace Microwave.Tests.Integration
{
    [TestFixture()]
    class IT9_DoorUI
    {
        private IUserInterface _sut;
        private IButton _powerbtn;
        private IButton _timerbtn;
        private IButton _start_cancelbtn;
        private IDisplay _display;
        private ILight _light;
        private IDoor _door;
        private IOutput _outputSub;
        private ICookController _controllerSub;

        [SetUp]
        public void setup()
        {
            _powerbtn = new Button();
            _timerbtn = new Button();
            _outputSub = Substitute.For<IOutput>();
            _display = new Display(_outputSub);
            _start_cancelbtn = new Button();
            _light = new Light(_outputSub);
            _door = new Door();
            _controllerSub = Substitute.For<ICookController>();
            _sut = new UserInterface(_powerbtn, _timerbtn, _start_cancelbtn, _door, _display, _light, _controllerSub);
        }

        [Test]
        public void ReadyDoorOpensLightTurnsOn()
        {
            _door.Open();

            _outputSub.Received().OutputLine($"Light is turned on");

        }

        [Test]
        public void DoorOpenClosedLightIsOff()
        {
            _door.Open();
            _door.Close();

            _outputSub.Received().OutputLine($"Light is turned off");
        }

        [Test]
        public void SetPowerDoorOpened()
        {
            _powerbtn.Press();
            _door.Open();

            _outputSub.Received().OutputLine($"Light is turned on");
            _outputSub.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void SetTimeDoorOpened()
        {
            _powerbtn.Press();
            _timerbtn.Press();

            _door.Open();

            _outputSub.Received().OutputLine($"Light is turned on");
            _outputSub.Received().OutputLine($"Display cleared");

        }

        [Test]
        public void CookingDoorOpened()
        {
            _powerbtn.Press();
            _timerbtn.Press();
            _start_cancelbtn.Press();

            _door.Open();

            _outputSub.Received().OutputLine($"Light is turned on");
            _outputSub.Received().OutputLine($"Display cleared");
        }
    }
}
