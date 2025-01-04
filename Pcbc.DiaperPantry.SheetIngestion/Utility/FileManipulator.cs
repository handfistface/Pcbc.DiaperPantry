using System.Text;

namespace Pcbc.DiaperPantry.SheetIngestion.Utility
{
    public interface IFileManipulator
    {
        string ReadFile(string fileName);
    }

    public class FileManipulator : IFileManipulator
    {
        public string ReadFile(string fileName)
        {
            var toRead = new FileInfo(fileName);
            if (!toRead.Exists)
                throw new FileNotFoundException(toRead.FullName);
            using var reader = toRead.OpenRead();
            byte[] data = new byte[reader.Length];
            reader.Read(data, 0, (int)reader.Length);
            return Encoding.UTF8.GetString(data);
        }
    }
}
