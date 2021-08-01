using Compiler.Argument;
using Compiler.Exception;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class EmptyFileOptionLoader: IConfigOptionLoader
    {
        public void LoadConfig(
            CompilerArguments arguments,
            JObject config,
            string fileName
        ) {
            JToken emptyFolderToken = config.SelectToken("empty_folder");
            if (emptyFolderToken == null)
            {
                return;
            }

            if (emptyFolderToken.Type != JTokenType.String)
            {
                throw new ConfigFileInvalidException("empty_folder option must be a string");
            }

            switch ((string) emptyFolderToken)
            {
                case "warn":
                    arguments.EmptyFolderAction = CompilerArguments.EmptyFolderWarning;
                    break;
                case "error":
                    arguments.EmptyFolderAction = CompilerArguments.EmptyFolderError;
                    break;
                case "ignore":
                    break;
                default:
                    throw new ConfigFileInvalidException("Invalid option for empty_folder");
            }
        }
    }
}
