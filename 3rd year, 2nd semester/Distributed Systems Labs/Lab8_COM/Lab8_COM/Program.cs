using System;
using System.Runtime.InteropServices;
using System.Threading;

// Examen:
// Trebuie sa fie structura
// Structura sa fie publica
// Membrii sa fie publici
public struct SystemTime
{
    public ushort year;
    public ushort month;
    public ushort weekday;
    public ushort day;
    public ushort hour;
    public ushort minute;
    public ushort second;
    public ushort millisecond;
}

public class LibWrap
{
    [DllImport("Kernel32.dll")]
    public static extern void GetLocalTime(out SystemTime st);
}

public class App
{
    public static void Main()
    {
        SystemTime st = new SystemTime();        
        while (true)
        {
            LibWrap.GetLocalTime(out st);

            string minutes = String.Format("{0,2:D2}", st.minute);
            string seconds = String.Format("{0,2:D2}", st.second);
            string miliseconds = String.Format("{0,3:D3}", st.millisecond);

            Console.Write("{0}:{1}:{2}.{3}", st.hour, minutes, seconds, miliseconds);
            Thread.Sleep(TimeSpan.FromMilliseconds(25));
            Console.Clear();
            
        }
    }
}