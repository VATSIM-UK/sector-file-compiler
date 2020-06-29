using System;
using System.Collections.Generic;
using Compiler.Output;
using Compiler.Event;
using Compiler.Error;
using Compiler.Config;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Compiler.Input
{
    public class FileIndexFactory
    {
        public static FileIndex CreateFileIndex(
            JObject configFile,
            IEventLogger events
        ) {
            Dictionary<OutputSections, List<IFileInterface>> files = new Dictionary<OutputSections, List<IFileInterface>>();
            foreach (OutputSections section in Enum.GetValues(typeof(OutputSections)))
            {
                if (ConfigFileSectionsMapper.GetConfigSectionForOutputSection(section) == ConfigFileSectionsMapper.invalidSection) 
                {
                    events.AddEvent(
                        new UnconfiguredConfigSectionWarning(
                            section.ToString()
                        )
                    );
                    continue;
                }

                if (!configFile.ContainsKey(ConfigFileSectionsMapper.GetConfigSectionForOutputSection(section)))
                {
                    events.AddEvent(
                        new UnconfiguredConfigSectionWarning(
                            section.ToString()
                        )
                    );
                    continue;
                }

                var configSection = configFile[ConfigFileSectionsMapper.GetConfigSectionForOutputSection(section)];
                files[section] = configSection.Type == JTokenType.Array
                    ? CompileFileList((JArray)configSection)
                    : CompileFileListFromSubsections((JObject)configSection);
            }

            return new FileIndex(files);
        }

        private static List<IFileInterface> CompileFileList(JArray files)
        {
            List<IFileInterface> fileList = new List<IFileInterface>();
            foreach (var item in files)
            {
                fileList.Add(new InputFile(item.ToString()));
            }

            return fileList;
        }

        private static List<IFileInterface> CompileFileListFromSubsections(JObject subsections)
        {
            List<IFileInterface> files = new List<IFileInterface>();
            foreach (var item in subsections)
            {
                files.AddRange(CompileFileList((JArray)item.Value));
            }

            return files;
        }
    }
}
