# sector-file-compiler
Compiler and validator for the [VATSIM UK Sector File](https://github.com/VATSIM-UK/uk-sector-file).

![Build Test and Coverage](https://github.com/AndyTWF/sector-file-compiler/workflows/Build%20Test%20and%20Coverage/badge.svg?branch=main)
[![codecov](https://codecov.io/gh/AndyTWF/sector-file-compiler/branch/main/graph/badge.svg)](https://codecov.io/gh/AndyTWF/sector-file-compiler)

## What Does It Do?

The compiler parses a number of input files, validates their contents and then compiles the overall result into one big file for circulation and use in VATSIM controller clients. It can also apply transformations to the output, such as removing comment lines and replacing tokens.

## Compiling

The solution is best compiled using Visual Studio 2019. The required test runners and dependencies can then be installed via the dotnet CLI.

# Using The Compiler

To run the compiler, you need to use the CompilerCli executable.

## Input Configuration

The input for the compiler is determined through a configuration file in JSON format.
This file is used to specify which files should be compiled, which sections of the output they should appear in and the order in which they should appear.
All file paths are relative to the config file.

Each section may have an array of files or, alternatively, an object specifying arrays of files for logical subsections. For example,
the VOR section could have subsections for VORs within each FIR.

### Compiler configuration sections

#### SCT2
`sct_header` - A comment block or copyright notice to go at the top of the SCT2 output.

`sct_colour_defs` - A place at the top of the SCT2 to define colours

`sct_info` - The \[INFO\] section of the SCT2.

`sct_airport` - The \[AIPORT\] section of the SCT2.

`sct_runway` - The \[RUNWAY\] section of the SCT2.

`sct_vor` - The \[VOR\] section of the SCT2.

`sct_ndb` - The \[NDB\] section of the SCT2.

`sct_fixes` - The \[FIXES\] section of the SCT2.

`sct_geo` - The \[GEO\] section of the SCT2.

`sct_low_airway` - The \[LOW AIRWAY\] section of the SCT2.

`sct_high_airway` - The \[HIGH AIRWAY\] section of the SCT2.

`sct_artcc` - The \[ARTCC\] section of the SCT2.

`sct_artcc_high` - The \[ARTCC HIGH\] section of the SCT2.

`sct_artcc_low` - The \[ARTCC LOW\] section of the SCT2.

`sct_sid` - The \[SID\] section of the SCT2.

`sct_star` - The \[STAR\] section of the SCT2.

`sct_labels` - The \[LABELS\] section of the SCT2.

`sct_regions` - The \[REGIONS\] section of the SCT2.

#### ESE (EuroScope Only)
`ese_header` - A comment block or copyright notice to go at the top of the ESE output.

`ese_preamble` - Any other information to appear at the top of the ESE.

`ese_positions` - The \[POSITIONS\] section of the ESE.

`ese_freetext` - The \[FREETEXT\] section of the ESE.

`ese_sidsstars` - EuroScope only. The \[SIDSSTARS\] section of the ESE.

`ese_airspace` - The \[AIRSPACE\] section of the ESE.

### RWY (EuroScope Only)
`rwy_active_runways` - The default active runway setup (.rwy file)

### Example compiler configuration

```JSON
{
  "ese_header": [
    "Copyright.txt"
  ],
  "ese_preamble": [
    "preamble.txt"
  ],
  "sidsstars": [
    "Airports/EGBB/Sids.txt",
    "Airports/EGBB/Stars.txt",
  ],
  "positions": [
      "Airports/EGBB/Positions.txt"
  ],
  "freetext": [
      "Airports/EGBB/Freetext.txt"
  ],
  "airspace": [
      "Airports/EGBB/Airspace.txt"
  ],
}

```

## Command Line Flags

### Required Flags

`--config-file` - Takes a single argument. Path to a compiler configuration JSON file. If this argument is specified multiple
times, then the compiler will attempt to merge the configs together.

`--out-file-ese` - Takes a single argument. Where the output file for the EuroScope ESE should be generated.

`--out-file-sct` - Takes a single argument. Where the output file for the SCT should be generated.

### Optional Flags

`--ignore-validation` - If set, any validation failures will not halt compilation.

`--strip-comments` - If set, any comments in the input will be removed. This does not apply to files named in the header parts of the compiler configuration.

`--strip-newlines` - If set, any lines that are empty (i.e. just newlines) in the input will be removed.

`--build-version` - Takes a single argument. Specifies the version of the build to replace the `{VERSION}` token in the input.

`--force-contiguous-routes` - If set, forces SIDs and STARs in the SCT to have contiguous routes (no gaps).

`--display-input-files` - If set, adds a comment line in the output at the point where a new input file was started.

### Token Replacements

There are a number of tokens that may be added to comments in the input files, which will be replaced in the output.

`{YEAR}` - The year at the time of compilation.

`{VERSION}` - A user generated version for the sector file, e.g. the AIRAC date. Defaults to current date and time.

`{BUILD}` - The date on which the build occured.
