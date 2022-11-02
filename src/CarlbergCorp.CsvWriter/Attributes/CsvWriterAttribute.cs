namespace CarlbergCorp.CsvWriter.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class CsvWriterAttribute : Attribute
{
    /// <summary>
    /// Column Header.
    /// Whats to be written as geade  in the CSV vile
    /// </summary>
    public string Header { get; set; } = string.Empty;

    /// <summary>
    /// Column sort ordering.<br/>
    /// Lower the values to the left and higher to the right.
    /// </summary>
    public int Order { get; set; } = 0;

    public CsvWriterAttribute(string header)
    {
        Header = header;
    }

    public CsvWriterAttribute(string header, int order)
    {
        Header = header;
        Order = order;
    }
}