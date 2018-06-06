using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace ReduxSimple.Samples.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> ReadFileAsync(string filepath)
        {
            var uri = new Uri("ms-appx:///" + filepath);
            var sampleFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            return await FileIO.ReadTextAsync(sampleFile);
        }
    }
}
