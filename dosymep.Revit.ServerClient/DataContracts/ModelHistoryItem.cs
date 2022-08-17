using System;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model history.
    /// </summary>
    public class ModelHistoryItem {
        /// <summary>
        /// The date and time the submission was made to the model.
        /// </summary>
        public DateTime Date { set; get; }
        
        /// <summary>
        /// The user who made the submission.
        /// </summary>
        public string User { set; get; }
        
        /// <summary>
        /// The version of the model created by the submission.
        /// </summary>
        public int VersionNumber { set; get; }

        /// <summary>
        /// The size of the model at the time the submission was made.
        /// </summary>
        public long ModelSize { set; get; }
        
        /// <summary>
        /// The size of the auxiliary data (such as user temporary data)
        /// for the model at the time the submission was made.
        /// </summary>
        public long SupportSize { set; get; }
        
        /// <summary>
        /// The comment message created by the submission.
        /// </summary>
        public string Comment { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public long OverwrittenByHistoryNumber { set; get; }
    }
}