using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using dosymep.Revit.Journaling.JournalElements;

using NUnit.Framework;

namespace dosymep.Revit.Journaling.Tests {
    public class Tests {
        [Test]
        public void RevitJournalTransformerTest() {
            var dateTimeOffset = new DateTimeOffset(2022, 08, 05, 15, 11, 45,
                TimeSpan.FromHours(3));

            string revitJournalContent = new RevitJournalTransformer()
                .Transform(dateTimeOffset, GetJournalElements("revit_central_model_path.rvt"));

            string sourceJournalContent = File.ReadAllText(@"TestsFiles\source_journal.vb");
            Assert.AreEqual(sourceJournalContent, revitJournalContent);
        }

        private IEnumerable<JournalElement> GetJournalElements(string modelPath) {
            yield return new OpenCentralModelElement() {ModelPath = modelPath};
            yield return new SyncCentralModelElement();
            yield return new PurgeUnusedElement();
            yield return new SyncCentralModelElement();
        }
    }
}