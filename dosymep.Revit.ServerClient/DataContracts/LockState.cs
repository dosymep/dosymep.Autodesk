namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The lock states.
    /// </summary>
    public enum LockState {
        /// <summary>
        /// 
        /// </summary>
        Unlocked,
        
        /// <summary>
        /// 
        /// </summary>
        Locked,
        
        /// <summary>
        /// 
        /// </summary>
        HasLockedAncestor,
        
        /// <summary>
        /// 
        /// </summary>
        HasLockedDescendent,
        
        /// <summary>
        /// 
        /// </summary>
        Unlocking,
        
        /// <summary>
        /// 
        /// </summary>
        Locking,
    }
}