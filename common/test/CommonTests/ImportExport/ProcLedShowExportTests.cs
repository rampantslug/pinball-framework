using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ImportExport;
using Xunit;

namespace CommonTests.ImportExport
{
    public class ProcLedShowExportTests
    {
        private const string ExampleLineString = "lamp:TestLed                                |  .......    ....";

        [Fact]
        public void GenerateTextString_ProducesValidYaml()
        {
            var exporter = new ProcLedShowExport(ImportExportTestHelpers.CreateTestLedShowConfiguration());
            var result = exporter.GenerateTextString();

            Assert.NotNull(result);
        }
    }
}
