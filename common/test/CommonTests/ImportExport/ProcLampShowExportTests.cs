using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Common.ImportExport;
using Xunit;


namespace CommonTests.ImportExport
{
    public class ProcLampShowExportTests
    {
        private const string ExampleLineString = "lamp:TestLed                                |  .......    ....";

        [Fact]
        public void GetLedLine_GeneratesCorrectFormat()
        {
            var exporter = new ProcLampShowExport(ImportExportTestHelpers.CreateTestLedShowConfiguration());
            var result = exporter.GetLedLine(ImportExportTestHelpers.CreateLedInShow());
            
            Assert.Equal(ExampleLineString, result);
        }
    }
}