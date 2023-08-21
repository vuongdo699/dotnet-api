using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.DomainTests.Chemicals
{
    public class ChemicalTests
    {
        //Sample Test
        [Fact]
        public void CreateChemical_NotValid()
        {
            // Arrange
            var mocker = new Fixture();

            // Actual

            // Assert
            Assert.True(true);
        }
    }
}
