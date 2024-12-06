using System;
using System.Numerics;
using RunningThreads;

public class Program
{
    static async Task Main(string[] args)
    {
        
        // Menu - 1 Jogar - 2 Jogar Modo debug - 3 Como Jogar - Esc Quit

        InterfaceManager interfaceManager = InterfaceManager.Instance;
        interfaceManager.FrameRate = 15;

        Enemy inimigoTeste = new Enemy('A',1, delayMilliseconds:1000);
        Enemy inimigoTeste1 = new Enemy('V',2, delayMilliseconds:2000);
        await Task.Delay(2200);
        Enemy inimigoTeste2 = new Enemy('T',3, delayMilliseconds:1500);
        Enemy inimigoTeste3 = new Enemy('L',2, delayMilliseconds:2000);
        
        // Inicia a thread de maneira pararela
        Task.Run(async () => await interfaceManager.UpdateInterface()); 
        
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            
        }
    }
}
