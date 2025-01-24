using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using dosymep.AutodeskApps;
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
        ITransformer<string, DynamoCommandElement>,
        ITransformer<string, ExternalCommandElement>,
        ITransformer<string, SaveAsFileCommandElement> {
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
                .AppendLine(string.Format(RevitJournalTemplates.Init, _revitVersion, dateTimeOffset))
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

            builder.AppendLine(RevitJournalTemplates.CentralOpen);

            if(visitable.Detach) {
                builder.AppendLine(RevitJournalTemplates.CentralOpenDetachCheckBox);
            }

            if(visitable.CreateLocal) {
                builder.AppendLine(RevitJournalTemplates.CentralOpenAsLocalCheckBox);
            }

            if(visitable.WithAudit) {
                builder.AppendLine(RevitJournalTemplates.CentralOpenAuditCheckBox);
            }

            builder.AppendFormat(RevitJournalTemplates.CentralModelName, visitable.ModelPath);
            
            builder.AppendLine();
            if(visitable.WorksetOption == WorksetsOption.Custom) {
                builder.AppendFormat(RevitJournalTemplates.CentralWorksetConfig, visitable.WorksetOption, "1");
                builder.AppendLine();
                builder.Append(RevitJournalTemplates.CentralAcceptCustomWorksets);
            } else {
                builder.AppendFormat(RevitJournalTemplates.CentralWorksetConfig, visitable.WorksetOption, "1");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Sync central model transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(SyncCentralModelElement visitable) {
            var builder = new StringBuilder();

            builder.AppendLine(GetVersionString(RevitJournalTemplatesOld.FileSync,
                RevitJournalTemplates.FileSync));

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

            builder.AppendLine(string.Format(GetVersionString(RevitJournalTemplatesOld.FileSyncComment,
                RevitJournalTemplates.FileSyncComment), visitable.Comment));

            builder.Append(GetVersionString(RevitJournalTemplatesOld.FileSyncAccept,
                RevitJournalTemplates.FileSyncAccept));
            
            return builder.ToString();
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
        public string Transform(DynamoCommandElement visitable) {
            var builder = new StringBuilder();

            builder.Append(RevitJournalTemplates.DynamoCommandExecute);
            WriteJournalData(builder, visitable.JournalData);

            return builder.ToString();
        }

        /// <summary>
        /// External command transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(ExternalCommandElement visitable) {
            var builder = new StringBuilder();

            builder.AppendFormat(RevitJournalTemplates.ExecuteExternalCommand,
                visitable.RevitAddinItem.AddinId,
                visitable.RevitAddinItem.FullClassName);
            WriteJournalData(builder, visitable.JournalData);

            return builder.ToString();
        }
        
        /// <summary>
        /// Save as command transformer.
        /// </summary>
        /// <param name="visitable">Visitable object.</param>
        /// <returns>Returns transformation object.</returns>
        public string Transform(SaveAsFileCommandElement visitable) {
            var builder = new StringBuilder();

            builder.AppendLine(RevitJournalTemplates.SaveAsFile);
            
            builder.AppendFormat(RevitJournalTemplates.SaveAsFileOptions,
                visitable.MaxBackupCount,
                visitable.ThumbnailViewId,
                visitable.RegenerateThumbnail ? 1 : 0,
                visitable.CompactFile ? 1 : 0,
                visitable.WorksetOption);

            builder.AppendLine();
            builder.AppendFormat(RevitJournalTemplates.SaveAsFileNameOption, visitable.ModelPath);

            builder.AppendLine();
            builder.AppendFormat(RevitJournalTemplates.SaveAsEnableWorksharing, visitable.EnableWorksharing ? 1 : 0);
            
            builder.AppendLine();
            builder.AppendFormat(RevitJournalTemplates.SaveAsMakeThisFileCentalModel, visitable.MakeThisFileCentalModel ? 1 : 0);

            if(visitable.ReplaceExistingFile) {
                if(IsRsnFile(visitable.ModelPath)) {
                    builder.AppendLine();
                    builder.AppendFormat(
                        RevitJournalTemplates.SaveAsReplaceCentralFile, Path.GetFileName(visitable.ModelPath));
                } else {
                    builder.AppendLine();
                    builder.AppendFormat(
                        RevitJournalTemplates.SaveAsReplaceWorksharingFile, Path.GetFileName(visitable.ModelPath));
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines whether the path is RSN path.
        /// </summary>
        /// <param name="modelPath">Model path.</param>
        /// <returns>true if path is RSN path, otherwise fals.</returns>
        protected static bool IsRsnFile(string modelPath) {
            return modelPath.StartsWith("RSN:", StringComparison.InvariantCultureIgnoreCase);
        }

        private static void WriteJournalData(StringBuilder builder, IDictionary<string, string> journalData) {
            if(journalData.Count > 0) {
                builder.AppendLine();
                builder.AppendLine(RevitJournalTemplates.ExternalCommandJournalData);
                builder.AppendFormat("    , {0} _", journalData.Count);
                builder.AppendLine();
                builder.Append("    , ");
                builder.Append(string.Join(Environment.NewLine + "    , ",
                    journalData.Select(item => $"\"{item.Key}\", \"{item.Value}\" _")));
            }
        }

        private string GetVersionString(string oldValue, string newValue) {
            return _revitVersion < 2022 ? oldValue : newValue;
        }
    }
}