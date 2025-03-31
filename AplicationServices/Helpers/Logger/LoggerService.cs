using AplicationServices.Application.Contracts.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Helpers.Logger
{
    public class LoggerService : ILoggerServices
    {
        private readonly string logFilePath;

        // Constructor para inicializar la ruta del archivo de log
        public LoggerService()
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");

            // Asegurarse de que el archivo exista, si no, crearlo.
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
        }

        public void LogInfo(string message)
        {
            LogToFile("INFO", message);
        }

        public void LogWarn(string message)
        {
            LogToFile("WARN", message);
        }

        public void LogDebug(string message)
        {
            LogToFile("DEBUG", message);
        }

        public void LogError(string message)
        {
            LogToFile("ERROR", message);
        }

        // Método privado para escribir el log en el archivo de texto
        private void LogToFile(string logLevel, string message)
        {
            string logMessage = $"{DateTime.Now:G} [{logLevel}] {message}{Environment.NewLine}";

            // Escribe el mensaje de log en el archivo
            File.AppendAllText(logFilePath, logMessage);
        }
    }
}
