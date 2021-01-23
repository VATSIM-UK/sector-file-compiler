using Compiler.Argument;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public interface IConfigOptionLoader
    {
        /**
         * From config, load into compiler arguments.
         */
        public void LoadConfig(
            CompilerArguments arguments,
            JObject config,
            string fileName
        );
    }
}