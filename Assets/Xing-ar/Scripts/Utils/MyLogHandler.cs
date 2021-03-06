using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MyLogHandler : ILogHandler
{
    private FileStream m_FileStream;
    //private StreamWriter m_StreamWriter;
    private ILogHandler m_DefaultLogHandler = Debug.unityLogger.logHandler;

    public MyLogHandler()
    {
        /*
        string filePath = Application.persistentDataPath + "/Xing-ar-Logs.txt";
        m_FileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        m_StreamWriter = new StreamWriter(m_FileStream);
        */

        // Replace the default debug log handler
        Debug.unityLogger.logHandler = this;
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        //m_StreamWriter.WriteLine(String.Format(format, args));
        //m_StreamWriter.Flush();
        m_DefaultLogHandler.LogFormat(logType, context, format, args);
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        m_DefaultLogHandler.LogException(exception, context);
    }
}