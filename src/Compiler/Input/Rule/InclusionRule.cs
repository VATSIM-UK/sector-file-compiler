using System.Collections.Generic;
using System.Linq;
using Compiler.Event;
using Compiler.Input.Filter;
using Compiler.Input.Generator;
using Compiler.Input.Sorter;
using Compiler.Input.Validator;
using Compiler.Output;

namespace Compiler.Input.Rule
{
    public class InclusionRule: IInclusionRule
    {
        public IFileListGenerator ListGenerator { get; }
        public IEnumerable<IFileFilter> Filters { get; }
        public IEnumerable<IFileValidator> Validators { get; }
        public IEnumerable<IFileSetValidator> FilesetValidators { get; }
        public IEnumerable<IFileSorter> Sorters { get; }
        public IRuleDescriptor Descriptor { get; }
        public InputDataType DataType { get; }
        public OutputGroup Group { get; }

        public InclusionRule(
            IFileListGenerator fileListGenerator,
            IEnumerable<IFileFilter> filters,
            IEnumerable<IFileValidator> validators,
            IEnumerable<IFileSetValidator> filesetValidators,
            IEnumerable<IFileSorter> sorters,
            IRuleDescriptor descriptor,
            InputDataType dataType,
            OutputGroup outputGroup
        )
        {
            ListGenerator = fileListGenerator;
            Filters = filters;
            Validators = validators;
            FilesetValidators = filesetValidators;
            Sorters = sorters;
            Descriptor = descriptor;
            DataType = dataType;
            Group = outputGroup;
        }
        
        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude(
            SectorDataFileFactory dataFileFactory,
            IEventLogger eventLogger
        )
        {
            return BuildSectorDataFiles(
                SortFiles(
                    ValidateFileSet(
                        ValidateFiles(
                            FilterFiles(),
                            eventLogger
                        ),
                        eventLogger
                    )
                ),
                dataFileFactory
            );
        }

        public OutputGroup GetOutputGroup()
        {
            return Group;
        }

        private IEnumerable<string> FilterFiles()
        {
            return ListGenerator.GetPaths()
                .Where(path => Filters.All(filter => filter.Filter(path)));
        }

        private IEnumerable<string> ValidateFiles(IEnumerable<string> files, IEventLogger eventLogger)
        {
            return files.Where(path => Validators.All(validator => validator.Validate(path, Descriptor, eventLogger)));
        }

        private IEnumerable<string> SortFiles(IEnumerable<string> files)
        {
            if (!Sorters.Any())
            {
                return files;
            }

            using var enumerator = Sorters.GetEnumerator();
            enumerator.MoveNext();
            var sortedFiles = files.OrderBy(path => path, enumerator.Current);
            while (enumerator.MoveNext())
            {
                sortedFiles = sortedFiles.ThenBy(path => path, enumerator.Current);
            }

            return sortedFiles;
        }

        private IEnumerable<string> ValidateFileSet(IEnumerable<string> files, IEventLogger logger)
        {
            var validateFileSet = files.ToList();
            foreach (var fileSetValidator in FilesetValidators)
            {
                fileSetValidator.Validate(validateFileSet, Descriptor, logger);
            }

            return validateFileSet;
        }

        private IEnumerable<AbstractSectorDataFile> BuildSectorDataFiles(
            IEnumerable<string> files,
            SectorDataFileFactory dataFileFactory
        ) {
            return files.Select(path => dataFileFactory.Create(path, DataType));
        }
    }
}
