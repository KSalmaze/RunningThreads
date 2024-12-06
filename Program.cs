using System;
using System.Numerics;
using RunningThreads;

public class Program
{
    static void Main(string[] args)
    {
        
        // Menu - 1 Jogar - 2 Jogar Modo debug - 3 Como Jogar - Esc Quit

        InterfaceManager interfaceManager = InterfaceManager.Instance;
        interfaceManager.FrameRate = 5;

        //char[] a = interfaceManager.Interface[1].ToCharArray();
        //a[27] = 'A';
        //a[3] = 'A';
        //interfaceManager.Interface[2] = new string (a);

        Enemy inimigoTeste = new Enemy('A',1, 1000);
        
        interfaceManager.UpdateInterface();
        
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            
        }
    }
}
