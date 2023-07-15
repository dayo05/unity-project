using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO.Ports;
using System.Text;
using JetBrains.Annotations;

public class SerialData
{
    public float pitch;
    public float yaw;
}
public class SerialIO : MonoBehaviour
{
    private ConcurrentStack<SerialData> cStack = new();

    public string SerialData;

    public SerialData latest = new();
    // Start is called before the first frame update
    void Start()
    {

        new Thread(() =>
        {
            var serialIO = new SerialPort(SerialData);
            serialIO.BaudRate = 115200;
            serialIO.Parity = Parity.None;
            serialIO.StopBits = StopBits.One;
            serialIO.DataBits = 8;
            serialIO.Handshake = Handshake.None;
            serialIO.RtsEnable = true;
            var sb = new StringBuilder();

            while (true)
            {
                if (!serialIO.IsOpen)
                    serialIO.Open();

                if (serialIO.BytesToRead == 0) continue;
                var s = serialIO.ReadExisting();
                sb.Append(s);
                if (!s.Contains('\n')) continue;
                var b = sb.ToString().Split('\n');
                var ss = b[^2];
                Console.WriteLine(ss);
                sb = new StringBuilder(b[^1]);
                var sss = ss.Split(' ');
                latest.pitch = float.Parse(sss[0]);
                latest.yaw = float.Parse(sss[1]);
            }
        }).Start();
    }

    public SerialData GetSerialInput()
    {
        cStack.TryPeek(out var g);
        cStack.Clear();
        return g;
    }
}
