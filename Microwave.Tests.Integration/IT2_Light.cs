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
    public class IT2_Light
    {
        private IOutput _outputSub;
        private ILight _sut;

        [SetUp]
        public void SetUp()
        {
            _outputSub = Substitute.For<IOutput>();
            _sut = new Light(_outputSub);
        }

        []
      
    }
}
