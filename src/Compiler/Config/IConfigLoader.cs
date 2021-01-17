using Compiler.Argument;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public interface IConfigLoader
    {
        /**
         * From config, load
         */
        public void LoadConfig(
            CompilerArguments arguments,
            ConfigInclusionRules inclusionRules,
            JObject config,
            string fileName
        );
    }
}