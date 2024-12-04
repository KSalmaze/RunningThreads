namespace RunningThreads;

public class InterfaceManager
{
    public int FrameRate; // Apenas uma atualização por frame

    public readonly string[] BaseInterface = new string[]
    {
        "__",
        "  | - - - - - - - - - - - -",
        "  | - - - - - - - - - - - -",
        "  | - - - - - - - - - - - -",
        "--"
    };
        /*"__ \n" +
        " | - - - - - - - - - - - - \n" +
        " | - - - - - - - - - - - - \n" +
        " | - - - - - - - - - - - - \n" +
        "-- \n";*/
    
    public InterfaceManager()
    {
        FrameRate = 10;
    }

    public void AdicionarAtualizacao(string sprite, int position, int lane)
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
        foreach (var line in BaseInterface)
        {
            Console.WriteLine(line);
        }
    }
}