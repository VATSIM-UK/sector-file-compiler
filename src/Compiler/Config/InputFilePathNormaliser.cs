using System.IO;

namespace Compiler.Config
{
    public class InputFilePathNormaliser
    {
        private readonly string relativeFolder;

        public InputFilePathNormaliser(string relativeFolder)
        {
            this.relativeFolder = relativeFolder;
        }

        public string NormaliseFilePath(string relativeInputFilePath)
        {
            return Path.GetFullPath(relativeFolder + Path.DirectorySeparatorChar + relativeInputFilePath);
        }
    }
}
