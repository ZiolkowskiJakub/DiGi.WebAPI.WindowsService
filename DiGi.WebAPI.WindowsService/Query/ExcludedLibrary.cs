using System.IO;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        public static bool ExcludedLibrary(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            string name = Path.GetFileName(path);

            return name.StartsWith("System.") || name.StartsWith("Microsoft.") || name.StartsWith("mscorlib");
        }
    }
}