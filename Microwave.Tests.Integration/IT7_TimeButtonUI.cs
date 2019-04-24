using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Interfaces;
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
            
        }
    }
}
