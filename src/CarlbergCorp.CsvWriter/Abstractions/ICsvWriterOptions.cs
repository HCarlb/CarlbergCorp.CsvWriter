using System.Globalization;
using System.Text;

namespace CarlbergCorp.CsvWriter.Abstractions;

/// <summary>
/// Options for the CsvWriter contained in a separate class.
/// </summary>
public interface ICsvWriterOptions
{
    /// <summary>
    /// Default=InvariantCulture.
    /// </summary>
    CultureInfo CultureInfo { get; set; }

    /// <summary>
    /// Text file encoding of the written file.
    /// Default=UTF8.
    /// </summary>
    Encoding Encoding { get; set; }

    /// <summary>
    /// Full filename including path and extensions.
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    /// Separator sign for the csv file.
    ///   Usually either<br/>
    ///   Comma(,)<br/>
    ///   SemiColon(;)<br/>
    /// <i>or</i><br/>
    ///   Colon(:)<br/>
    /// are used as separators, but not limited to.
    /// </summary>
    string Separator { get; set; }
}