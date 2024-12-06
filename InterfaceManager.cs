namespace RunningThreads;

public class InterfaceManager
{
    // Singleton
    private static readonly InterfaceManager instance = new InterfaceManager();

    public static InterfaceManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    // Construtor
    private InterfaceManager()
    {
        FrameRate = 10;
        Interface = BaseInterface;

        _ = Task.Run(async () => await AtualizarTela());
    }

    // Propriedades
    public int FrameRate; // Apenas uma atualização por frame

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
    public void AdicionarAtualizacao(char sprite, int position, int lane)
    {
        Console.WriteLine("Atualizacao chamada");
        
        char[] temp = Interface[lane].ToCharArray();
        temp[position] = sprite;
        
        if (position < 26)
        {
            temp[position + 2] = BaseInterface[lane + 1][position + 2];
        }
        
        Console.WriteLine($"Nova linha: \n {temp}");
        Interface[lane] = temp.ToString();
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
        //Console.Clear(); // Limpa a tela
        foreach (var line in Interface)
        {
            Console.WriteLine(line);
        }
    }
}