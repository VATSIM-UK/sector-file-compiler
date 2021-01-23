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
        ) {
            ProcessFile(
                config,
                "sct_output",
                file => arguments.OutputFiles.Add(new SctOutput(MakeOutputWriter(arguments, file, fileName))),
                fileName
            );
            ProcessFile(
                config,
                "ese_output",
                file => arguments.OutputFiles.Add(new EseOutput(MakeOutputWriter(arguments, file, fileName))),
                fileName
                
            );
            ProcessFile(
                config,
                "rwy_output",
                file => arguments.OutputFiles.Add(new RwyOutput(MakeOutputWriter(arguments, file, fileName))),
                fileName
            );
        }


        /**
         * Makes an output writer.
         */
        private OutputWriter MakeOutputWriter(CompilerArguments arguments, string file, string configFilePath)
        {
            return OutputWriterFactory.Make(
                arguments,
                NormaliseFilePath(configFilePath, file),
                new OutputFileStreamFactory()
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
        
        private string GetFolderForConfigFile(string pathToConfigFile)
        {
            return Path.GetFullPath(Path.GetDirectoryName(pathToConfigFile) ?? string.Empty);
        }
        
        private string NormaliseFilePath(string configFilePath, string relativeInputFilePath)
        {
            return Path.GetFullPath(
                GetFolderForConfigFile(configFilePath) + Path.DirectorySeparatorChar + relativeInputFilePath
            );
        }
        
        private bool OptionValid(JToken token)
        {
            return token.Type == JTokenType.String;
        }
    }
}