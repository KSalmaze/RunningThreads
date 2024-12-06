using System;
using System.Numerics;
using RunningThreads;

public class Program
{
    static void Main(string[] args)
    {
        
        // Menu - 1 Jogar - 2 Jogar Modo debug - 3 Como Jogar - Esc Quit

        InterfaceManager interfaceManager = InterfaceManager.Instance;
        interfaceManager.UpdateInterface();

        char[] a = interfaceManager.BaseInterface[1].ToCharArray();
        //a[27] = 'A';
        //a[3] = 'A';
        interfaceManager.BaseInterface[2] = new string (a);

        interfaceManager.UpdateInterface();
        
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            
        }
    }
}
