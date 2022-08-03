namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The server's roles.
    /// </summary>
    public enum ServerRole {
        /// <summary>
        /// Host
        /// </summary>
        Host,
        
        /// <summary>
        /// Accelerator
        /// </summary>
        Accelerator,
        
        /// <summary>
        /// Admin
        /// </summary>
        Admin,
        
        /// <summary>
        /// Cloud
        /// </summary>
        Cloud,
        
        /// <summary>
        /// NotARevitServer
        /// </summary>
        NotARevitServer,
    }
}