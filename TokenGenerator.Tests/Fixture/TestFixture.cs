using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using TokenGenerator;

namespace TokenGenerator.Tests.Fixture
{
    class TestFixture : WebApplicationFactory<Startup>
    {
        public TestFixture()
        {
        }
    }
}
