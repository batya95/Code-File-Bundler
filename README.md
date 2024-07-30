# Code File Bundler

## Description
This CLI tool allows you to bundle multiple code files into a single file. It supports various programming languages and offers several customization options.

## Features
- Bundle code files from different programming languages
- Add source file notes
- Sort files
- Remove empty lines
- Specify author information
- Create response files for easier command execution

## Supported Languages
C#, Java, Angular, React, SQL, C, C++, JavaScript, Python, and more.

## Usage
To bundle files:
bundle --output <output_file> --language [options]

Options:
- `--output` or `-o`: Specify the output file path and name
- `--language` or `-l`: Specify the programming language (required)
- `--note` or `-n`: Add source file notes
- `--auther` or `-a`: Add author information
- `--sort` or `-s`: Sort the files
- `--remove` or `-r`: Remove empty and whitespace lines

To create a response file:
create-rsp --output <rsp_file>


## Examples
Bundle C# files:
bundle --output bundle.txt --language c# --note --sort

Create a response file:
create-rsp --output mybundle.rsp


## Requirements
- .NET Core 3.1 or later

## Installation
[Provide installation instructions here]

## License
[Specify the license here]
