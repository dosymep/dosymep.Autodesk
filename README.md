# dosymep.Autodesk

[![JetBrains Rider](https://img.shields.io/badge/JetBrains-Rider-blue.svg)](https://www.jetbrains.com/rider)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://en.wikipedia.org/wiki/MIT_License)

A comprehensive set of .NET libraries for interacting with Autodesk products, including Revit and Navisworks. This repository provides tools for file analysis, journaling, and server communication.

## Key Features

- **File Information**: Read metadata from Revit (`.rvt`, `.rfa`) and Navisworks files without opening them in the host application.
- **Revit Journaling**: Generate and manipulate Revit journal files for automation.
- **Revit Server Client**: Interface with Revit Server via its REST API.
- **Autodesk Apps Core**: Shared interfaces and logic for Autodesk-related applications.

## Libraries

- `dosymep.AutodeskApps`: Core shared logic and interfaces.
- `dosymep.AutodeskApps.FileInfo`: Base library for Autodesk file metadata.
- `dosymep.Revit.FileInfo`: Extract information from Revit files (version, preview, etc.).
- `dosymep.Navisworks.FileInfo`: Extract information from Navisworks files.
- `dosymep.Revit.Journaling`: Create and transform Revit journals.
- `dosymep.Revit.ServerClient`: Client library for Revit Server interaction.

## Contributing

Contributions are welcome! Please refer to [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## License

This project is licensed under the MIT License – see the [LICENSE.md](LICENSE.md) file for details.
