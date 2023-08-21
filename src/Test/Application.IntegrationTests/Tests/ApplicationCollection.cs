using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Tests
{
    [CollectionDefinition(nameof(ApplicationCollection))]
    public class ApplicationCollection : ICollectionFixture<WebApiFixture>
    {
    }
}
