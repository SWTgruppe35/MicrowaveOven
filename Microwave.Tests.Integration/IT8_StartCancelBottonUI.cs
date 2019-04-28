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
            _startCancelButtonSub = new Button();
            _cookControllerSub = Substitute.For<ICookController>();
            _sut = new UserInterface(_powerButton, _timeButtonSub, _startCancelButtonSub, _doorSub, _display, _light, _cookControllerSub);
            _startCancelButtonSub.Press();

        }

        [Test]
        public void StartCancelBottonSetPower()
        {
            _startCancelButtonSub
        }



        [Test]
        public void StartCancel_btn_SetTimeState()
        {

        } 

    }
}
