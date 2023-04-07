# Captain Key

## Disclaimer

Captain Key is strictly for educational purposes and should not be used for any malicious activity. The author of this
code is not responsible for any harm caused by its usage.
<p align="center">
  <img src="https://i.ibb.co/2hMyfQS/ck1.png" alt="Image" width="400" height="400" />
</p>`

## About

Captain Key written in C# using the user32.dll library. With Captain Key you can capture pressed keys and log them to a
file.

Currently you can only write on a local file.

## Usage

To use Captain Key, simply create an instance of the `Capture` class and pass a log file path as an argument. Captain
Key will continuously run in the background and log any pressed keys until the `StopLogging` method is called.

```csharp
// create an instance of Capture class
var capture = new Capture("log.txt");

// to stop the logging thread and close the file
capture.StopLogging();
```
