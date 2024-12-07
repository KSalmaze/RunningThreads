namespace RunningThreads;

public class Program
{
    static async Task Main()
    {
        Menu();

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
        Console.WriteLine("1 - Iniciar \n 2 - Como Jogar \n 0 - Sair");
        
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    return;
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Bem vindo a Running Threads!");
                    Console.WriteLine("Gameplay: Defenda o castelo!\n Os inimigos são representados por letras vindo na direção do castelo");
                    Console.WriteLine("Comandos dentro do jogo:");
                    Console.WriteLine("0 - Fechar o jogo  1 - Melhorar a vida maxima  2 - Melhorar os guardas");
                    break;
                case ConsoleKey.D0:
                    Environment.Exit(1);
                    break;
            }
        }
    }
}