namespace CarlbergCorp.CsvWriter.Abstractions;

public interface ICsvWriter
{
    /// <summary>
    /// Options for the CsvWrite
    /// </summary>
    ICsvWriterOptions? Options { get; set; }

    /// <summary>
    /// Options factory to create new Options objects
    /// </summary>
    /// <returns></returns>
    ICsvWriterOptions CreateOptions();

    /// <summary>
    /// Writes data asyncronously using options.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Task WriteCsvAsync<T>(T[] data, ICsvWriterOptions options) where T : class;

    /// <summary>
    /// Writes data asyncronously.
    /// Note that <b>Options</b> propery on the CsvWriter must be set before write or exception will be thrown.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    Task WriteCsvAsync<T>(T[] data) where T : class;
}