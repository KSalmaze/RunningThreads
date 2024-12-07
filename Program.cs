using System;
using System.Numerics;
using RunningThreads;

public class Program
{
    static async Task Main(string[] args)
    {
        
        // Menu - 1 Jogar - 2 Jogar Modo debug - 3 Como Jogar - Esc Quit

        InterfaceManager interfaceManager = InterfaceManager.Instance;
        interfaceManager.FrameRate = 5;
        
        EnemySpawner enemySpawner = EnemySpawner.Instance;
        Guard guarda1 = new Guard(1,1000);
        Guard guarda2 = new Guard(2,1000);
        Guard guarda3 = new Guard(3,1000);
        
        // Inicia a thread de maneira pararela
        //Task.Run(async () => await interfaceManager.UpdateInterface()); 
        
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            
        }
    }
}
