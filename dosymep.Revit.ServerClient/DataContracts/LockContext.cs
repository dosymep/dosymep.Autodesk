namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The lock context.
    /// </summary>
    public class LockContext {
        /// <summary>
        /// The context of the admin lock on the folder/model,
        /// describing the use of the admin lock
        /// such as copying or moving a folder from one server to another.
        /// </summary>
        public string Context { set; get; }
    }
}