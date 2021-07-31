namespace Compiler.Config
{
    public class ConfigPath
    {
        public string SectionRoot { get; }
        public ConfigFileSection Section { get; }

        public ConfigPath(string sectionRoot, ConfigFileSection section)
        {
            this.SectionRoot = sectionRoot;
            this.Section = section;
        }

        public override string ToString()
        {
            return $"{SectionRoot}.{Section.JsonPath}";
        }
    }
}
