using Compiler.Argument;

namespace Compiler.Transformer
{
    public class BuildVersionTokenReplacer: ITransformer
    {
        private readonly CompilerArguments arguments;

        public BuildVersionTokenReplacer(CompilerArguments arguments)
        {
            this.arguments = arguments;
        }

        public string Transform(string data)
        {
            return data.Replace(arguments.VersionToken, arguments.BuildVersion);
        }
    }
}