namespace RunningThreads;

public class Program
{
    static async Task Main()
    {
        //Menu();
        // Menu - 1 Jogar - 2 Jogar Modo debug - 3 Como Jogar - Esc Quit

        InterfaceManager interfaceManager = InterfaceManager.Instance;
        interfaceManager.FrameRate = 5;
        
        EnemySpawner enemySpawner = EnemySpawner.Instance;
        Guard guarda1 = new Guard(1,1000);
        Guard guarda2 = new Guard(2,1000);
        Guard guarda3 = new Guard(3,1000);
        
        // Comeca a ler as entradas do usuario
        InputManager inputManager = new InputManager([guarda1, guarda2, guarda3]);
        await inputManager.Start();
    }

    public static void Menu()
    {
        
    }
}