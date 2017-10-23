using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Configuration;
using Xunit;

namespace CommonTests
{
    public class ConfigurationTests
    {
        [Fact]
        public void ToFile_IgnoresCertainProperties()
        {
            var testConfig = new MachineConfiguration();

            // Create a switch and set some of the values we dont want saved
            var sw = new Switch
            {
                Number = 5, 
                LastChangeTimeStamp = DateTime.Now,
            };
            sw.SetState(true);

            testConfig.Switches.Add(sw);
            testConfig.PlayfieldImage = "test";
            testConfig.Images = new List<string>(){"test"};
            testConfig.Videos = new List<string>() { "test" };
            testConfig.Sounds = new List<string>() { "test" };

            Assert.False(true, "Fix Below");
            //  testConfig.ToFile("testFile.txt");

            var fileContents = File.ReadAllText("testFile.txt");

            //
            // Check file contents for unwanted properties
            //

            // Device stuff
            Assert.DoesNotContain("Number", fileContents);
            Assert.DoesNotContain("LastChangeTimeStamp", fileContents);
            Assert.DoesNotContain("PlayfieldImage", fileContents);
            Assert.DoesNotContain("State", fileContents);
            Assert.DoesNotContain("IsActive", fileContents);
            Assert.DoesNotContain("_state", fileContents);
            Assert.DoesNotContain("StateString", fileContents);

            // Mode stuff

            // Media stuff
            Assert.DoesNotContain("Images", fileContents);
            Assert.DoesNotContain("Videos", fileContents);
            Assert.DoesNotContain("Sounds", fileContents);

        }


    }
}
