using Compiler.Argument;
using Compiler.Exception;
using Compiler.Transformer;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class ConfigTokenReplaceOptionLoader: IConfigOptionLoader
    {
        public void LoadConfig(CompilerArguments arguments, JObject config, string fileName)
        {
            JToken replaceToken = config.SelectToken("replace");
            if (replaceToken == null)
            {
                return;
            }

            if (replaceToken.Type != JTokenType.Array)
            {
                throw new ConfigFileInvalidException($"Invalid replace option - must be an array of objects");
            }

            foreach (var replacement in (JArray) replaceToken)
            {
                if (replacement.Type != JTokenType.Object)
                {
                    throw new ConfigFileInvalidException($"Invalid replace item - must be an object");
                }

                JObject replacementObject = (JObject) replacement;

                
                // Check theres a token to be replaced
                JToken toReplaceToken = replacementObject.SelectToken("token");
                if (toReplaceToken == null || toReplaceToken.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException($"Invalid replace token - must be a string");
                }
                

                // Check theres a type of replacement
                JToken typeToken = replacementObject.SelectToken("type");
                if (typeToken == null || typeToken.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException($"Invalid replace type - must be a string");
                }
                switch (typeToken.ToString())
                {
                    case "date":
                        ProcessDateReplacement(arguments, replacementObject, toReplaceToken.ToString());
                        break;
                    case "version":
                        ProcessVersionReplacement(arguments, toReplaceToken.ToString());
                        break;
                    default:
                        throw new ConfigFileInvalidException("Invalid replace type - must be date or version");
                }
            }
        }

        /**
         * Check there's valid format and create a date replacement token
         */
        private void ProcessDateReplacement(CompilerArguments arguments, JObject replacement, string tokenToReplace)
        {
            JToken formatToken = replacement.SelectToken("format");
            if (formatToken == null)
            {
                throw new ConfigFileInvalidException($"Missing date format in replace");
            }
            
            if (formatToken.Type != JTokenType.String)
            {
                throw new ConfigFileInvalidException($"Invalid date format in replace, must be a string");
            }

            arguments.TokenReplacers.Add(new TokenDateReplacer(tokenToReplace, formatToken.ToString()));
        }
        
        /**
         * Add a version replacement token
         */
        private void ProcessVersionReplacement(CompilerArguments arguments, string tokenToReplace)
        {
            arguments.TokenReplacers.Add(new TokenBuildVersionReplacer(arguments, tokenToReplace));
        }
    }
}