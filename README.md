# sector-file-compiler
Compiler and validator for the [VATSIM UK Sector File](https://github.com/VATSIM-UK/uk-sector-file).

![Build Test and Coverage](https://github.com/VATSIM-UK/sector-file-compiler/workflows/Build%20Test%20and%20Coverage/badge.svg?branch=main)
[![codecov](https://codecov.io/gh/VATSIM-UK/sector-file-compiler/branch/main/graph/badge.svg)](https://codecov.io/gh/VATSIM-UK/sector-file-compiler)
[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)](http://commitizen.github.io/cz-cli/)
[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)
![release](https://img.shields.io/github/v/release/VATSIM-UK/sector-file-compiler)
![license](https://img.shields.io/github/license/VATSIM-UK/sector-file-compiler)
![issues](https://img.shields.io/github/issues/VATSIM-UK/sector-file-compiler)
![pull requests](https://img.shields.io/github/issues-pr/VATSIM-UK/sector-file-compiler)
![language](https://img.shields.io/github/languages/top/VATSIM-UK/sector-file-compiler)

## What Does It Do?

The compiler parses a number of input files, validates their contents and then compiles the overall result into one big file for circulation and use in VATSIM controller clients. It can also apply transformations to the output, such as removing comment lines and replacing tokens.

## Compiling

The solution is best compiled using Visual Studio 2019 or compatible .NET IDE. The required test runners and dependencies can then be installed via the dotnet CLI.

# Using The Compiler

To run the compiler, you need to use the CompilerCli executable.

## Command Line Arguments

### Required

`--config-file` - Takes a single argument. Path to a compiler configuration JSON file. If this argument is specified multiple
times, then the compiler will attempt to merge the configs together.

### Optional

`--check-config` - If set, only runs the configuration checking step to ensure that the compiler config is correct.

`--lint` - If set, only runs the configuration check and linting steps.

`--validate` - If set, only runs the configuration check, linting and post-validation steps. Does not output files.

`--skip-validation` - If set, the compiler will skip the post-parse validation phase of compilation.
If running in full compilation mode, will still produce output.

`--strip-comments` - If set, any comments in the input will be removed. If an empty line is leftover, it will be discarded.

`--build-version` - Takes a single argument. Specifies the version of the build to replace the `{VERSION}` token in the input.

`--no-wait` - Prompts the compiler not to wait for a keypress when compilation has finished.

## Input Configuration

The compiler configuration file, at the highest level, is a JSON object with a number of keys.

### Compiler Options

Options for the compiler can be specified using the `options` key in the config file. The options are as follows:

#### SCT Output

The `sct_output` option takes a single string. It provides the path, relative to the config
file, where the `.sct` file should be written.

#### ESE Output

The `ese_output` option takes a single string. It provides the path, relative to the config
file, where the `.ese` file should be written.

#### RWY Output

The `rwy_output` option takes a single string. It provides the path, relative to the config
file, where the `.rwy` file should be written.

#### Token Replacement

The `replace` option takes an array of objects. Each object provides details of tokens
within comments in the output that should be replaced on compilation. There are two types
of replacements.

The date replacement type replaces the specified token with a date in the given format. For example,
the following config would replace the `{YEAR}` token with a four digit year.

```json
{
    "token": "{YEAR}",
    "type": "date",
    "format": "yyyy"
}
```

The version replacement type replaces the specified token with the build version, which
can be specified on the commandline, as follows:

```json
{
    "token": "{VERSION}",
    "type": "version"
}
```

#### Empty Folders

The `empty_folder` option takes a single string. It determines what the compiler should do in the event that
it comes across a Folder inclusion rule with no files found. Valid values are:

- `ignore` - Default, do nothing.
- `warn` - Raise a warning message in the output, but continue compilation.
- `error` - Raise a fatal error that halts compilation.

### Include Files

The input files that the compiler should parse are specified in the `include` key of the compiler
configuration. There are three subsections.

#### The Airports Subsection

This subsection is an object of objects. The key of each parent object specifies
a folder (relative to the config file) to look in for airport files.

Valid keys in each airports object are as follows:

```json
{
  "active_runways": {},
  "airspace": {},
  "basic": {},
  "fixes": {},
  "freetext": {},
  "ground_network": {},
  "ownership": {},
  "positions": {},
  "positions_mentor": {},
  "sid_airspace": {},
  "star_airspace": {},
  "runways": {},
  "sectors": {},
  "sids": {},
  "stars": {},
  "smr": {
    "geo": {},
    "regions": {},
    "labels": {}
  },
  "ground_map": {
    "geo": {},
    "regions": {},
    "labels": {}
  },
  "vrps": {}
}
```

### The Enroute Subsection

The `enroute` subsection contains data in relation to enroute positions and is an object. **Each
key may be an array of objects, or a single object.**

```json
{
  "sector_lines": {},
  "ownership": {},
  "positions": []
}
```

### The Misc Subsection

The `misc` subsection contains other misc data. **Each
key may be an array of objects, or a single object.**

```json
{
  "agreements": [],
  "freetext": {},
  "colours": {},
  "info": {},
  "file_headers": {},
  "pre_positions": {},
  "fixes": {},
  "ndbs": {},
  "vors": {},
  "danger_areas": {},
  "artcc_low": {},
  "artcc_high": {},
  "lower_airways": [],
  "upper_airways": [],
  "sid_airspace": {},
  "star_airspace": [],
  "geo": {},
  "regions": {}
}
```

There are two main ways to specify files:

#### File Lists

This rule includes files as listed in the configuration file.

```json
{
    "type": "files",
    "files": [
        "Basic.txt"
    ],
    "ignore_missing": true,
    "except_where_exists": "Basic2.txt",
    "exclude_directory": "EGAC"
}
```
There are two optional flags available for the file list rule:

- `ignore_missing` (default: `false`) instructs the compiler to simply ignore the input file
if it cannot be found (would usually cause an error). This is useful for airports where not all airports have all files.
- `except_where_exists`instructs the compiler to skip the files, if another file is present.
This is particularly useful for SMRs and Ground Maps.
- `exclude_directory` useful for processing airport ownership data. Excludes a particular directory from
the rule.
  
#### Folders

This rule includes all files within a given folder.

```json
{
    "type": "folder",
    "folder": "Ownership/Alternate",
    "recursive": true,
    "pattern":  "SomeRegex",
    "exclude": [
      "EUR Islands.txt"
    ]
}
```

There are three optional flags available for the folder rule:

- `recursive` (default: `false`) will cause the compiler to include all files in any subfolders
contained within the main folder.
- `exclude` will cause the compiler to ignore any files with a particular name. Conversely, specifying `include`
will only include files with a certain name.
- `pattern` allows you to provide a regular expression, with only files matching the pattern being included.

## Comment Annotations

There are a number of annotations that may be applied to comments, which have an effect on the output
of the compiler. These annotations will be removed during compilation.

### Preserving comments

The `@preserveComment` annotation will force the compiler to keep the comments even if the `--strip-comments` option is specified.

#### Example Usage

```
; @preserveComment This comment will be preserved
; This comment will not
```
