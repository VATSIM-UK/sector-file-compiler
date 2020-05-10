using System;
using System.Collections.Generic;
using Compiler.Output;
using Compiler.Event;
using Compiler.Error;
using Compiler.Config;

namespace Compiler.Input
{
    public class FileIndexFactory
    {
        public static FileIndex CreateFileIndex(
            Dictionary<string, List<string>> configFile,
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

                List<IFileInterface> sectionFileList = new List<IFileInterface>();
                if (!configFile.ContainsKey(ConfigFileSectionsMapper.GetConfigSectionForOutputSection(section)))
                {
                    events.AddEvent(
                        new UnconfiguredConfigSectionWarning(
                            section.ToString()
                        )
                    );
                    continue;
                }

                foreach (string filePath in configFile[ConfigFileSectionsMapper.GetConfigSectionForOutputSection(section)])
                {
                    sectionFileList.Add(new InputFile(filePath));
                }

                files[section] = sectionFileList;
            }

            return new FileIndex(files);
        }
    }
}
