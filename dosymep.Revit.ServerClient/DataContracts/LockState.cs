namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The lock states.
    /// </summary>
    public enum LockState {
        /// <summary>
        /// Unlocked
        /// </summary>
        Unlocked,
        
        /// <summary>
        /// Locked
        /// </summary>
        Locked,
        
        /// <summary>
        /// HasLockedAncestor
        /// </summary>
        HasLockedAncestor,
        
        /// <summary>
        /// HasLockedDescendent
        /// </summary>
        HasLockedDescendent,
        
        /// <summary>
        /// Unlocking
        /// </summary>
        Unlocking,
        
        /// <summary>
        /// Locking
        /// </summary>
        Locking,
    }
}