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
    public int CurrentHealth => _currentHealth;
    private int _gold; // Dinheiro para o jogador gastar
    private readonly SemaphoreSlim _goldSemaphore = new SemaphoreSlim(1, 1);
    public int Gold => _gold;
    
    // Methods
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
    
    private async Task RegenHealth(){
        while (_currentHealth > 0)
        {
            if (_currentHealth < MaxHealth)
            {
                await Task.Delay(2000); // Delay para regenerar vida
                await Health(+1);
            }
        }
        
        EndGame();
    }
    
    private void EndGame()
    {
        throw new NotImplementedException();
    }
}