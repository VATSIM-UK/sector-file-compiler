using System;
using System.IO;
using Compiler.Argument;
using Compiler.Exception;
using Compiler.Output;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class ConfigOutputFilesOptionLoader: IConfigOptionLoader
    {
        public void LoadConfig(
            CompilerArguments arguments,
            JObject config,
            string fileName
        )
        {
            ProcessFile(
                config,
                "sct_output",
                file => arguments.OutputFiles.Add(new SctOutput(new StreamWriter(file))),
                fileName
            );
            ProcessFile(
                config,
                "ese_output",
                file => arguments.OutputFiles.Add(new EseOutput(new StreamWriter(file))),
                fileName
                
            );
            ProcessFile(
                config,
                "rwy_output",
                file => arguments.OutputFiles.Add(new RwyOutput(new StreamWriter(file))),
                fileName
            );
        }

        private void ProcessFile(JObject config, string token, Action<string> addToArguments, string filename)
        {
            JToken file = config.SelectToken(token);
            if (file == null)
            {
                return;
            }

            if (!OptionValid(file))
            {
                throw new ConfigFileInvalidException(
                    $"Invalid field {token} in config file {filename} - must be a string"    
                );
            }

            addToArguments(file.ToString());
        }
        
        private bool OptionValid(JToken token)
        {
            return token.Type == JTokenType.String;
        }
    }
}