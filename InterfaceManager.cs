namespace RunningThreads;

public class InterfaceManager
{
    // Singleton
    private static readonly InterfaceManager instance = new InterfaceManager();

    public static InterfaceManager Instance => instance;

    // Construtor
    private InterfaceManager()
    {
        FrameRate = 10;
        Interface = BaseInterface;

        _ = Task.Run(async () => await AtualizarTela());
    }

    // Propriedades
    public int FrameRate; // Apenas uma atualização por frame
    
    private readonly SemaphoreSlim _interfaceSemaphore = new SemaphoreSlim(1, 1);
    private readonly object _interfaceLock = new object();

    private string[] BaseInterface = new string[]
    {
        "__",
        "  |- - - - - - - - - - - - -  ",
        "  |- - - - - - - - - - - - -  ",
        "  |- - - - - - - - - - - - -  ",
        "--                    "
    };

    public string[] Interface;
    
    // Metodos
    
    // metodo a se proteger usando semaforos, se necessario criar uma outra funcao para efetivamente alterar a tela
    public async Task AdicionarAtualizacao(char sprite, int position, int lane)
    {
        await _interfaceSemaphore.WaitAsync();
        try
        {
            AtualizarInterface(sprite, position, lane);
        }
        finally 
        {
            _interfaceSemaphore.Release();
        }
    }
    
    private void AtualizarInterface(char sprite, int position, int lane)
    {
        if (lane < 0 || lane >= Interface.Length || position < 0)
            return;

        char[] temp = Interface[lane].ToCharArray();
        
        if (position >= temp.Length)
            return;

        temp[position] = sprite;
        
        if (position < 26 && lane + 1 < BaseInterface.Length)
        {
            temp[position + 2] = BaseInterface[lane + 1][position + 2];
        }
        
        Interface[lane] = new string(temp);
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
        await _interfaceSemaphore.WaitAsync();
        try
        {
            Console.Clear();
            foreach (var line in Interface)
            {
                Console.WriteLine(line);
            }
        }
        finally
        {
            _interfaceSemaphore.Release();
        }
    }
}