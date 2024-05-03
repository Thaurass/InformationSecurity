using System.Text.RegularExpressions;

namespace FileProcessing;

public class LoadFile
{
    public string FirstLine { get; set; }

    public async Task<FileResult> GetFile()
    {
        var customFileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { ".txt" } },
                { DevicePlatform.Android, new[] { ".txt" } },
                { DevicePlatform.WinUI, new[] { ".txt" } },
                { DevicePlatform.Tizen, new[] { ".txt" } },
                { DevicePlatform.macOS, new[] { ".txt" } },
            });

        PickOptions options = new()
        {
            PickerTitle = "Выберите файл с расширением txt",
            FileTypes = customFileType,
        };

        var result = await PickAndShow(options);
        return result;
    }

    private async Task<FileResult> PickAndShow(PickOptions options)
    {
        try
        {
            string pattern = @"[^|][|]\d+";
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                StreamReader f = new StreamReader(result.FullPath);
                string first_line = f.ReadLine();
                f.Close();
                if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) && Regex.IsMatch(first_line, pattern, RegexOptions.IgnoreCase))
                {
                    FirstLine= first_line;
                    return result;
                }
                else
                {
                    FirstLine = "first_line";
                }
            }
            else
            {
                FirstLine = "first_line";
            }
        }
        catch (Exception ex)
        {
            //await MainPage.DisplayError(ex.Message);
        }

        return null;
    }
}

