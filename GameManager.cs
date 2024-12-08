namespace RunningThreads;

public class GameManager
{
    // Singleton
    private static readonly GameManager instance = new GameManager();
    public static GameManager Instance => instance;

    // Constructor
    private GameManager()
    {
        MaxHealth = 10;
        _currentHealth = MaxHealth;
        _gold = 0;

        _ = Task.Run(async () => await RegenHealth());
    }
    
    // Property s
    public int MaxHealth; // Vida maxima atual
    private int _currentHealth; // Adicionar get e set com verificacao
    private readonly SemaphoreSlim _healthSemaphore = new SemaphoreSlim(1, 1);
    public int CurrentHealth => _currentHealth; // Geter de health
    private int _gold; // Dinheiro para o jogador gastar
    private readonly SemaphoreSlim _goldSemaphore = new SemaphoreSlim(1, 1);
    public int Gold => _gold; // Geter de gold
   
    // Methods
    // Funcao para alterar a vida
    public async Task Health(int operation)
    {
        await _healthSemaphore.WaitAsync();
        try
        {
            _currentHealth += operation;
        }
        finally
        {
            _healthSemaphore.Release();
        }
    }
    
    // Funcao para alterar a quantidade de ouro
    public async Task ChangeGold(int operation)
    {
        await _goldSemaphore.WaitAsync();
        try
        {
            _gold += operation;
        }
        finally
        {
            _goldSemaphore.Release();
        }
    }
    
    // Regenera a vida com o tempo
    private async Task RegenHealth(){
        while (_currentHealth > 0)
        {
            if (_currentHealth < MaxHealth)
            {
                await Task.Delay(2000); // Delay para regenerar vida
                await Health(+1);
            }
        }
        
        await EndGame();
    }

    // Funcao para aumentar a vida maxima 
    public async Task IncreaseMaxHealth()
    {
        MaxHealth += 10;
        await Health(+10);
    }
    
    private async Task EndGame()
    {
        try
        {
            InterfaceManager.Instance.StopInterface();
            Console.Clear();
            Console.WriteLine("Que pena, seu castelo foi tomado pelos inimigos :(");
            Task.Delay(3000).Wait();
            Environment.Exit(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}