using System.Globalization;
using System.Text;
using CarlbergCorp.CsvWriter.Abstractions;

namespace CarlbergCorp.CsvWriter;

/// <summary>
/// Options for the CsvWriter contained in a separate class.
/// </summary>
public sealed class CsvWriterOptions : ICsvWriterOptions
{
    public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;
    public Encoding Encoding { get; set; } = new UTF8Encoding(false);

    public string FileName { get; set; } = string.Empty;

    public string Separator { get; set; } = ",";

    public CsvWriterOptions(string fileName)
    {
        FileName = fileName;
    }

    public CsvWriterOptions()
    {
    }

    public CsvWriterOptions(string fileName, Encoding encoding, CultureInfo cultureInfo, string separator = ",")
    {
        FileName = fileName;
        Encoding = encoding;
        Separator = separator;
        CultureInfo = cultureInfo;
    }

    public CsvWriterOptions(string fileName, Encoding encoding, string separator = ",")
    {
        FileName = fileName;
        Encoding = encoding;
        Separator = separator;
    }
}