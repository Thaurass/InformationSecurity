using System.Text;
using CommunityToolkit.Maui.Storage;

namespace FileProcessing
{
    public class SaveFile
    {
        public bool SaveResult { get; set; }
        public async Task<bool> Savedata(string FileData)
        {
            string filename = "Document.txt";
            using var stream = new MemoryStream(Encoding.Default.GetBytes(FileData));
            var fileSaveResult = await FileSaver.Default.SaveAsync(filename, stream);
            SaveResult = fileSaveResult.IsSuccessful;
            return fileSaveResult.IsSuccessful;
        }
    }
}
