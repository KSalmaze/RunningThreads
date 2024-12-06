namespace RunningThreads;

public class InterfaceManager
{
    private static readonly InterfaceManager instance = new InterfaceManager();

    private InterfaceManager()
    {
        FrameRate = 10;

        _ = Task.Run(async () => await AtualizarTela());
    }
    
    public static InterfaceManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    public int FrameRate; // Apenas uma atualização por frame

    public string[] BaseInterface = new string[]
    {
        "__",
        "  |- - - - - - - - - - - - -  ",
        "  |- - - - - - - - - - - - -  ",
        "  |- - - - - - - - - - - - -  ",
        "--                    "
    };

    public void AdicionarAtualizacao(char sprite, int position, int lane)
    {
        // Coloca na fila de processos, uma chamada dessa lista por frame
    }

    public async Task AtualizarTela()
    {
        while (true)
        {
            await Task.Delay(1000 / FrameRate);
            Console.WriteLine("Atualizando a tela");
            await UpdateInterface();
        }
    }

    public async Task UpdateInterface()
    {
        Console.Clear(); // Limpa a tela
        foreach (var line in BaseInterface)
        {
            Console.WriteLine(line);
        }
    }
}