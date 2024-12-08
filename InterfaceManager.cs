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
        _gameManager = GameManager.Instance;

        _ = Task.Run(async () => await AtualizarTela());
    }

    // Propriedades
    public int FrameRate; // Apenas uma atualização por frame
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    
    private readonly SemaphoreSlim _interfaceSemaphore = new SemaphoreSlim(1, 1);

    private string[] BaseInterface = new string[]
    {
        "__",
        "  |- - - - - - - - - - - - -  ",
        "  |- - - - - - - - - - - - -  ",
        "  |- - - - - - - - - - - - -  ",
        "--                    "
    };

    public string[] Interface;
    
    private GameManager _gameManager;
    
    // Metodos
    
    // metodo a se proteger usando semaforos, se necessario criar uma outra funcao para efetivamente alterar a tela
    public async Task AdicionarAtualizacao(char sprite, int position, int lane)
    {
        //Console.WriteLine(" New request");
        
        await _interfaceSemaphore.WaitAsync();
        try
        {
            AtualizarInterface(sprite, position, lane);
            if (position < 26)
            {
                AtualizarInterface('-', position + 2, lane);
            }
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
        
        //Console.Write(position >= temp.Length);
        if (position >= temp.Length)
            return;

        temp[position] = sprite;
        
        Interface[lane] = new string(temp);
    }

    public void StopInterface()
    {
        _cts.Cancel();
    }
    
    public async Task AtualizarTela()
    {
        while (!_cts.Token.IsCancellationRequested)
        {
            await Task.Delay(1000 / FrameRate, _cts.Token);
            Console.WriteLine("Atualizando a tela");
            await UpdateInterface(_cts.Token);
        }
    }

    public async Task UpdateInterface(CancellationToken ct = default)
    {
        await _interfaceSemaphore.WaitAsync();
        try
        {
            if (ct.IsCancellationRequested)
            {
                return;
            }
            
            Console.Clear();
            foreach (var line in Interface)
            {
                Console.WriteLine(line);
            }
            // Print de infos
            Console.WriteLine($"Vida: {_gameManager.CurrentHealth} Gold: {_gameManager.Gold}");
        }
        finally
        {
            _interfaceSemaphore.Release();
        }
    }
}