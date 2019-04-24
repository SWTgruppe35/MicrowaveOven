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
    class IT7_TimeButtonUI
    {
        private IUserInterface _sut;
        private IButton _powerbtn;
        private IButton _timerbtn;
        private IButton _start_cancelbtnSub;
        private IDisplay _display;
        private ILight _light;
        private IDoor _doorSub;
        private IOutput _outputSub;
        private ICookController _controllerSub;

        [SetUp]
        public void Setup()
        {
            _powerbtn=new Button();
            _timerbtn = new Button();
            _outputSub = Substitute.For<IOutput>();
            _display=new Display(_outputSub);
            _start_cancelbtnSub = Substitute.For<IButton>();
            _light=new Light(_outputSub);
            _doorSub = Substitute.For<IDoor>();
            _controllerSub = Substitute.For<ICookController>();
            _sut=new UserInterface(_powerbtn,_timerbtn,_start_cancelbtnSub,_doorSub, _display, _light, _controllerSub);

            _powerbtn.Press();
        }

        [Test]
        public void TimeBtnPressedDisplayShowsRightTime()
        {
            _timerbtn.Press();

            _outputSub.Received().OutputLine($"Display shows: {1:D2}:{0:D2}");

        }

        [Test]
        public void TimeBtnPressed5TimesDisplayShowsRightTime()
        {
            _timerbtn.Press();
            _timerbtn.Press();
            _timerbtn.Press();
            _timerbtn.Press();
            _timerbtn.Press();

            _outputSub.Received().OutputLine($"Display shows: {5:D2}:{0:D2}");

        }

        [Test]
        public void DoorOpenedTimeResetDisplayShowsClearedLightTurnsOn()
        {
            _timerbtn.Press();
            _timerbtn.Press();
            _timerbtn.Press();
            _timerbtn.Press();
            _timerbtn.Press();

            _doorSub.Opened += Raise.Event();

            _outputSub.Received().OutputLine($"Light is turned on");
            _outputSub.Received().OutputLine($"Display cleared");
        }
    }
}
