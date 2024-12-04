using System;
using RunningThreads;

public class Program
{
    static void Main(string[] args)
    {
        // Menu - 1 Jogar - 2 Jogar Modo debug - 3 Como Jogar - 4 Quit
        
        InterfaceManager interfaceManager = new InterfaceManager();

        interfaceManager.UpdateInterface();
    }
}
