using UnityEngine;

namespace Helpers
{
    public class HandleException : MonoBehaviour
    {
        void Awake()
        {
            Application.logMessageReceived += LogCaughtException;
            DontDestroyOnLoad(gameObject);
        }

        void LogCaughtException(string logText, string stackTrace, LogType logType)
        {
            if (logType != LogType.Log)
            {
                // add your exception logging code here
            }
        }
    }
}