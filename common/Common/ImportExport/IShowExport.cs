namespace Common.ImportExport
{
    /// <summary>
    /// Implementers of this interface should build a string that can be directly written to a text file.
    /// The implementation should include all required formatting.
    /// </summary>
    public interface IShowExport
    {
        string GenerateTextString();
    }
}
