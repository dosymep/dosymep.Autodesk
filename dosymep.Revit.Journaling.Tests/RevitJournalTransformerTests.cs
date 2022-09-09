using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using dosymep.Revit.FileInfo.RevitAddins;
using dosymep.Revit.Journaling.JournalElements;

using NUnit.Framework;

namespace dosymep.Revit.Journaling.Tests {
    public class Tests {
        [Test]
        //[TestCase(2021)]
        [TestCase(2022)]
        public void RevitJournalTransformerTest(int revitVersion) {
            var dateTimeOffset = new DateTimeOffset(2022, 08, 05, 15, 11, 45,
                TimeSpan.FromHours(3));

            string revitJournalContent = new RevitJournalTransformer(revitVersion)
                .Transform(dateTimeOffset, GetJournalElements("revit_central_model_path.rvt"));

            string sourceJournalContent = File.ReadAllText($@"TestsFiles\source_journal.{revitVersion}.vb");
            Assert.AreEqual(sourceJournalContent, revitJournalContent);
        }

        private IEnumerable<JournalElement> GetJournalElements(string modelPath) {
            yield return new OpenCentralModelElement() {ModelPath = modelPath};
            yield return new SyncCentralModelElement();
            yield return new PurgeUnusedElement();
            yield return new SyncCentralModelElement();

            yield return new DynamoCommandElement() {
                ScriptPath = "@C:\\script_dynamo.dyn", ModelNodesInfo = "[{data: data}]"
            };

            yield return new ExternalCommandElement() {
                RevitAddinItem = new RevitAddinCommand() {
                    AddinId = new Guid("9725D9BF-CA8C-4EE8-B8B0-C8257B5EB6F2"),
                    FullClassName = "dosymep.RevitExternalCommand"
                },
                JournalData = new Dictionary<string, string>() {
                    {"key1", "value1"}, {"key2", "value2"}, {"key3", "value3"}
                }
            };
        }
    }
}