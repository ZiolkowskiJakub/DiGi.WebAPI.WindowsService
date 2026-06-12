using System.IO;

namespace DiGi.WebAPI.WindowsService
{
    public static partial class Query
    {
        /// <summary>
        /// Determines whether the specified library path should be excluded based on standard system and Microsoft naming conventions.
        /// </summary>
        /// <param name="path">The file path of the library to check for exclusion.</param>
        /// <returns>True if the library is a system or Microsoft assembly; otherwise, false.</returns>
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