using System;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model lock data.
    /// </summary>
    public class ModelLockData {
        /// <summary>
        /// The age of the model lock.
        /// </summary>
        public TimeSpan Age { set; get; }
        
        /// <summary>
        /// The time stamp of the model lock.
        /// </summary>
        public DateTime TimeStamp { set; get; }
        
        /// <summary>
        /// The type of the model lock.
        /// </summary>
        public ModelLockType ModelLockType { set; get; }
        
        /// <summary>
        /// The combination of the model lock options
        /// </summary>
        public ModelLockOptions ModelLockOptions { set; get; }
        
        /// <summary>
        /// The user who locks the model.
        /// </summary>
        public string UserName { set; get; }
        
        /// <summary>
        /// The model path.
        /// </summary>
        public string ModelPath { set; get; }
    }
}