
# Clipboard -> QR Code relay

Just a simple (if possibly over-engineered) application that runs from console, periodically queries the clipboard, and outputs a Level L QR Code to the console if a change is detected.

My intention was to create a program that would allow for easy access to small bits of information from an airgapped machine.

Also, I wanted an excuse to play with MediatR for the first time.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

If editing this, you should need only:
* Visual Studio 2019 (I used the Community Edition)
* .NET Core 3.1 SDK

If you're just running it after compilation, the following should suffice:
* .NET Core 3.1 Runtime

### Installing

Assuming you have the above prerequisites, should should need only to clone the repository:

```
https://github.com/RobertZFord/RFord.Projects.ClipboardQrCodeRelay.git
```

Then open it up in Visual Studio 2019, and compile.

## Built With

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1/)

## Authors

* **Robert Ford** - *Initial work*

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [Choose A License](https://choosealicense.com/)
* [PurpleBooth](https://gist.github.com/PurpleBooth)'s [README.md template](https://gist.github.com/PurpleBooth/109311bb0361f32d87a2)