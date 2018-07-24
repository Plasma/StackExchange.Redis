using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Redis
{
    /// <summary>
    /// Temporary class to provide central debugging information
    /// </summary>
    public static class DebugHelper
    {
        const int MaxLogSize = 100;

        static readonly object Lock = new object();
        static readonly List<string> Logs = new List<string>();

        /// <summary>
        /// Return the debug log data available
        /// </summary>
        /// <returns>The log data in a single string</returns>
        public static string GetLogDump()
        {
            var builder = new StringBuilder();

            lock (Lock) {
                builder.AppendLine($"Dumping {Logs.Count} Log(s) (Time Now: {DateTime.UtcNow})");

                foreach (var log in Logs)
                    builder.AppendLine(log);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Write the following log line to debug info
        /// <paramref name="message">The log message to write</paramref>
        /// </summary>
        public static void AddLog(string message)
        {
            lock (Lock) {
                // Insert message
                Logs.Insert(0, $"{DateTime.UtcNow}: {message}");

                // Trim list
                while(Logs.Count > MaxLogSize)
                    Logs.RemoveAt(Logs.Count - 1);
            }
        }
    }
}
