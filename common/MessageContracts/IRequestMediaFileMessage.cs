namespace MessageContracts
{
    /// <summary>
    /// Message requesting server to send the machine configuration data.
    /// </summary>
    public interface IRequestMediaFileMessage
    {
        string MediaFileLocation { get; } 
    }
}
