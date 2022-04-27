using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeclarableDataGrid.Tests
{   
    public class DeclarableDataGridTests
    {
        [Fact]
        public void Test()
        {
            new object().Should().NotBeNull();
        }
    }
}
