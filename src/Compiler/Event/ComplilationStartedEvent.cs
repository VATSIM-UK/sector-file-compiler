namespace Compiler.Event
{
    public class ComplilationStartedEvent : ICompilerEvent
    {
        public string GetMessage()
        {
            return $"Sector File Compiler version {FormatVersion()}: Starting compilation";
        }

        private string FormatVersion()
        {
            var version = typeof(ComplilationStartedEvent).Assembly.GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
