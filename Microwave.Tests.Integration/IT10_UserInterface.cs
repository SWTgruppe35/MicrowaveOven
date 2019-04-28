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
    public class IT10_Userinterface
    {
        private IUserInterface _sut;
        private IOutput _outputSub;
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDisplay _display;
        private ILight _light;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private ICookController _cookController;

        [SetUp]
        public void Setup()
        {
            _outputSub = Substitute.For<IOutput>();
            _light = new Light(_outputSub);
            _display = new Display(_outputSub);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _timer = new Timer();
            _powerTube = new PowerTube(_outputSub);
            _cookController = new CookController(_timer, _display, _powerTube, _sut);

            _sut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _cookController);


        }

        [Test]
        public void StartCookingAssertPowertube()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _outputSub.Received().OutputLine($"PowerTube works with 50 W");
        }

        [Test]
        public void StartCookingAssertTimerStarted()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            System.Threading.Thread.Sleep(5010);

            _outputSub.Received(5).OutputLine(Arg.Is<string>(str => str.Contains($"Display shows: 00:")));
        }


    }
}
