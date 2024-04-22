# MSBT Function Map (MFM) to YAML

[![License](https://img.shields.io/badge/License-MIT-blue.svg?logo=github&logoColor=5751ff&labelColor=2A2C33&color=5751ff&style=for-the-badge)](https://github.com/ArchLeaders/MfmToYaml/blob/master/License.md) [![Downloads](https://img.shields.io/github/downloads/ArchLeaders/MfmToYaml/total?label=downloads&logo=github&logoColor=37c75e&labelColor=2A2C33&color=37c75e&style=for-the-badge)](https://github.com/ArchLeaders/MfmToYaml/releases) [![Latest](https://img.shields.io/github/v/tag/ArchLeaders/MfmToYaml?label=Release&logo=github&logoColor=324fff&color=324fff&labelColor=2A2C33&style=for-the-badge)](https://github.com/ArchLeaders/MfmToYaml/releases/latest)

Simple CLI tool to convert [Aeon](https://gitlab.com/AeonSake/)'s MSBT function map format to the YAML format used by [MessageStudio](https://github.com/EPD-Libraries/MessageStudio).

- [MSBT Function Map (MFM) to YAML](#msbt-function-map-mfm-to-yaml)
- [Usage](#usage)
  - [Single Input/Output](#single-inputoutput)
    - [Examples](#examples)
  - [Multiple Inputs/Outputs](#multiple-inputsoutputs)
    - [Example](#example)
- [Install](#install)
  - [Windows (x64)](#windows-x64)
  - [Windows (Arm64)](#windows-arm64)
  - [Linux (x64)](#linux-x64)
  - [Linux (arm64)](#linux-arm64)
  - [MacOS (x64)](#macos-x64)
  - [MacOS (Arm64)](#macos-arm64)
  - [Build From Source](#build-from-source)
- [Help](#help)

# Usage

The tool can take any number of inputs immediately followed by `-o` or `--output` to specify a custom output path for the input (see examples).

## Single Input/Output

```powershell
MfmToYaml <input> [-o|--output OUTPUT]
```

### Examples

```powershell
MfmToYaml "D:\TotK.mfm"
```

```powershell
MfmToYaml "D:\TotK.mfm" -o "D:\Output\TotK.yml"
```

## Multiple Inputs/Outputs

```powershell
MfmToYaml <input> [-o|--output OUTPUT] <input> [-o|--output OUTPUT] ...
```

### Example

```powershell
MfmToYaml "D:\TotK.mfm" "D:\BotW.mfm"
```

```powershell
MfmToYaml "D:\TotK.mfm" -o "D:\Output\TotK.yml" "D:\BotW.mfm"
```

```powershell
MfmToYaml "D:\TotK.mfm" -o "D:\Output\TotK.yml" "D:\BotW.mfm" -o "D:\Output\BotW.yml" 
```

# Install

## Windows (x64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for Windows x64.
- Download the [latest win-x64 release](https://github.com/ArchLeaders/MfmToYaml/releases/latest/download/MfmToYaml-win-x64.zip)

## Windows (Arm64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for Windows Arm64.
- Download the [latest arm-x64 release](https://github.com/ArchLeaders/MfmToYaml/releases/latest/download/MfmToYaml-win-arm64.zip)

## Linux (x64)

- Install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) x64 runtime for your linux distribution.
- Download the [latest linux-x64 release](https://github.com/ArchLeaders/MfmToYaml/releases/latest/download/MfmToYaml-linux-x64.zip)

## Linux (arm64)

- Install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) Arm64 runtime for your linux distribution.
- Download the [latest linux-arm64 release](https://github.com/ArchLeaders/MfmToYaml/releases/latest/download/MfmToYaml-linux-arm64.zip)

## MacOS (x64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for MacOS x64.
- Download the [latest osx-x64 release](https://github.com/ArchLeaders/MfmToYaml/releases/latest/download/MfmToYaml-osx-x64.zip)

## MacOS (Arm64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for MacOS Arm64.
- Download the [latest osx-arm64 release](https://github.com/ArchLeaders/MfmToYaml/releases/latest/download/MfmToYaml-osx-arm64.zip)

## Build From Source

```powershell
git clone "https://github.com/ArchLeaders/MfmToYaml"
dotnet build MfmToYaml
```

# Help

<a href="https://github.com/ArchLeaders/MfmToYaml/issues">
  <img src="https://img.shields.io/github/issues/ArchLeaders/MfmToYaml?style=for-the-badge&logoColor=c71b42&color=c71b42&labelColor=2A2C33&logo=github&label=Issues" alt="ArchLeaders' Website"/>
</a> &nbsp;
<a href="https://discord.gg/cbA3AWwfJj">
  <img src="https://img.shields.io/discord/825161394663456799?style=for-the-badge&logoColor=37C75E&color=37C75E&labelColor=2A2C33&logo=discord&label=discord" alt="ArchLeaders' Discord"/>
</a>

*If you need any help or found an issue, please create an [issue on GitHub](https://github.com/ArchLeaders/MfmToYaml/issues) or contact me on [Discord](https://discord.gg/8Saj6tTkNB) @ [archleaders](https://discord.com/users/728823685015797770).*