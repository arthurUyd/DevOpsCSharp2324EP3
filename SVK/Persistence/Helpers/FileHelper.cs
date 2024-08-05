using System.IO;

namespace Persistence.Helpers;
public static class FileHelper
{
    public static byte[] FileToByteArray(string filePath)
    {
        return File.ReadAllBytes(filePath);
    }
}
