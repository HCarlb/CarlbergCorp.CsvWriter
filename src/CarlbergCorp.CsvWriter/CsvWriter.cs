using CarlbergCorp.CsvWriter.Abstractions;
using CarlbergCorp.CsvWriter.Attributes;

namespace CarlbergCorp.CsvWriter;

public sealed class CsvWriter : ICsvWriter
{
    private record Column(int ColumnOrder, string ColumnName, string PropertyName);

    public ICsvWriterOptions? Options { get; set; }

    public ICsvWriterOptions CreateOptions()
    {
        return new CsvWriterOptions();
    }

    public async Task WriteCsvAsync<T>(T[] data) where T : class
    {
        ArgumentNullException.ThrowIfNull(Options, nameof(Options));

        await WriteCsvAsync<T>(data, Options);
    }

    public async Task WriteCsvAsync<T>(T[] data, ICsvWriterOptions options) where T : class
    {
        Options = options;

        // Get column info from type
        var columns = GetColumnInfo<T>();

        // Get a streamwriter to be able to write to file.
        using var file = new StreamWriter(options.FileName, false, options.Encoding);

        try
        {
            // Write the header to file
            var headerRow = string.Join(options.Separator, columns.Select(x => x.ColumnName));
            await file.WriteLineAsync(headerRow);

            foreach (T item in data)
            {
                var texts = new List<string>();
                foreach (var col in columns)
                {
                    // Get the property by name from object
                    var property = typeof(T).GetProperty(col.PropertyName);
                    ArgumentNullException.ThrowIfNull(property, nameof(property));

                    // Get the value from property. Allways return some sort of string and add that to the list.
                    string propertyValue = GetPropertyStringValue(property.GetValue(item), options.CultureInfo);
                    texts.Add(propertyValue);
                }

                // Write each row to file
                await file.WriteLineAsync(string.Join(options.Separator, texts));
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            file.Close();
        }
    }

    private static List<Column> GetColumnInfo<T>()
    {
        List<Column>? columns = null;
        foreach (var property in typeof(T).GetProperties())
        {
            foreach (var attribute in property.GetCustomAttributes(false)
                                              .Cast<CsvWriterAttribute>())
            {
                (columns ??= new()).Add(new Column(attribute.Order, attribute.Header, property.Name));
            }
        }

        ArgumentNullException.ThrowIfNull(columns, nameof(columns));

        return columns.OrderBy(x => x.ColumnOrder)
                      .ToList();
    }

    private static string GetPropertyStringValue(dynamic? propertyValue, IFormatProvider cultureInfo, string numberFormat = "0.######")
    {
        return propertyValue switch
        {
            null => string.Empty,
            string => (string)propertyValue,
            int => (string)propertyValue.ToString(cultureInfo),
            double or float or decimal => (string)propertyValue.ToString(numberFormat, cultureInfo),
            DateTime or DateOnly => (string)propertyValue.ToString("yyyy-MM-dd"),
            _ => (string)propertyValue.ToString(),
        };
    }
}