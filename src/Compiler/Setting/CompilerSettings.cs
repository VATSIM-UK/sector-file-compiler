namespace Compiler.Setting
{
    public class CompilerSettings
    {
        // The place to find the configuration JSON file.
        private string configFilePath;

        public string ConfigFilePath
        {
            get
            {
                return this.configFilePath;
            }

            set
            {
                this.configFilePath = value;
            }
        }
    }
}
