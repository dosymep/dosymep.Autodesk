using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using dosymep.Autodesk;
using dosymep.Revit.Journaling.JournalElements;

namespace dosymep.Revit.Journaling {
    /// <summary>
    /// Journal transform.
    /// </summary>
    public class RevitJournalTransformer :
        ITransformer,
        ITransformer<string, JournalElement>,
        ITransformer<string, OpenCentralModelElement>,
        ITransformer<string, SyncCentralModelElement>,
        ITransformer<string, PurgeUnusedElement>,
        ITransformer<string, ExternalCommandElement> {
        private readonly int _revitVersion;

        /// <summary>
        /// Creates revit journal transformer.
        /// </summary>
        /// <param name="revitVersion">Revit version.</param>
        public RevitJournalTransformer(int revitVersion) {
            _revitVersion = revitVersion;
        }

        /// <summary>
        /// Consistently transforms journal elements.
        /// </summary>
        /// <param name="journalElements">Journal elements.</param>
        /// <returns>Returns a string representation of the Revit Journal.</returns>
        public string Transform(IEnumerable<JournalElement> journalElements) {
            return Transform(DateTimeOffset.Now, journalElements);
        }

        /// <summary>
        /// Consistently transforms journal elements.
        /// </summary>
        /// <param name="journalElements">Journal elements.</param>
        /// <returns>Returns a string representation of the Revit Journal.</returns>
        public string Transform(params JournalElement[] journalElements) {
            return Transform(DateTimeOffset.Now, journalElements.AsEnumerable());
        }

        /// <summary>
        /// Consistently transforms journal elements. (for internal use)
        /// </summary>
        /// <param name="dateTimeOffset">Date time generated revit journal.</param>
        /// <param name="journalElements">Journal elements.</param>
        /// <returns>Returns a string representation of the Revit Journal.</returns>
        public string Transform(DateTimeOffset dateTimeOffset, params JournalElement[] journalElements) {
            return Transform(dateTimeOffset, journalElements.AsEnumerable());
        }

        /// <summary>
        /// Consistently transforms journal elements. (for internal use)
        /// </summary>
        /// <param name="dateTimeOffset">Date time generated revit journal.</param>
        /// <param name="journalElements">Journal elements.</param>
        /// <returns>Returns a string representation of the Revit Journal.</returns>
        public string Transform(DateTimeOffset dateTimeOffset, IEnumerable<JournalElement> journalElements) {
            var builder = new StringBuilder()
                .AppendLine(string.Format(RevitJournalTemplates.Init, dateTimeOffset))
                .AppendLine(RevitJournalTemplates.InitDebug)
                .AppendLine(string.Join(Environment.NewLine,
                    journalElements.Select(item => item.Reduce<string, JournalElement>(this))
                        .Where(item => !string.IsNullOrEmpty(item))))
                .AppendLine(RevitJournalTemplates.ExitApplication);

            return builder.ToString();
        }

        /// <summary>
        /// Default transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(JournalElement visitable) {
            return null;
        }

        /// <summary>
        /// Open central model transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(OpenCentralModelElement visitable) {
            var builder = new StringBuilder();

            if(visitable.Detach) {
                builder.AppendLine(RevitJournalTemplates.CentralOpenDetachCheckBox);
            }

            if(visitable.CreateLocal) {
                builder.AppendLine(RevitJournalTemplates.CentralOpenAsLocalCheckBox);
            }

            if(visitable.WithAudit) {
                builder.AppendLine(RevitJournalTemplates.CentralOpenAuditCheckBox);
            }

            return string.Format(RevitJournalTemplates.CentralOpen,
                builder.ToString().Trim(),
                visitable.ModelPath,
                visitable.KeepWorkset ? "1" : "0");
        }

        /// <summary>
        /// Sync central model transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(SyncCentralModelElement visitable) {
            var builder = new StringBuilder();

            if(visitable.Compact) {
                builder.AppendLine(GetVersionString(RevitJournalTemplatesOld.FileSyncCompactFile,
                    RevitJournalTemplates.FileSyncCompactFile));
            }

            if(visitable.BorrowedElements) {
                builder.AppendLine(GetVersionString(RevitJournalTemplatesOld.FileSyncBorrowedElements,
                    RevitJournalTemplates.FileSyncBorrowedElements));
            }

            if(visitable.UserCreatedWorksets) {
                builder.AppendLine(GetVersionString(RevitJournalTemplatesOld.FileSyncUserСreatedWorksets,
                    RevitJournalTemplates.FileSyncUserСreatedWorksets));
            }

            if(visitable.SaveLocalFile) {
                builder.AppendLine(GetVersionString(RevitJournalTemplatesOld.FileSyncSaveLocalFile,
                    RevitJournalTemplates.FileSyncSaveLocalFile));
            }

            var fileSync = GetVersionString(RevitJournalTemplatesOld.FileSync,
                RevitJournalTemplates.FileSync);
            return string.Format(fileSync, builder.ToString().Trim(), visitable.Comment);
        }

        /// <summary>
        /// Purge unused transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(PurgeUnusedElement visitable) {
            return string.Join(Environment.NewLine,
                Enumerable.Range(0, visitable.TryCount).Select(_ => RevitJournalTemplates.PurgeUnused));
        }

        /// <summary>
        /// External command transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(ExternalCommandElement visitable) {
            throw new NotImplementedException();
        }

        private string GetVersionString(string oldValue, string newValue) {
            return _revitVersion < 2022 ? oldValue : newValue;
        }
    }
}