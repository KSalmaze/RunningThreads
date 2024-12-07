namespace RunningThreads;

public class Enemy
{
    private char _sprite;
    public int Lane;
    private int _delayMilliseconds;
    public int Position;
    public int Health;
    public int Damage;
    public int Gold;
    
    private InterfaceManager _interfaceManager;
    private GameManager _gameManager;
    private EnemySpawner _enemySpawner;
    
    public Enemy(char sprite, int lane, int health = 3, int damage = 1,int gold = 2,int delayMilliseconds = 500)
    {
        _sprite = sprite;
        Lane = lane;
        Position = 27; // Vai para a ultima posicao da lane
        _delayMilliseconds = delayMilliseconds;
        _interfaceManager = InterfaceManager.Instance;
        _gameManager = GameManager.Instance;
        _enemySpawner = EnemySpawner.Instance;
        Health = health;
        Damage = damage;

        _ = Task.Run(async () => await EnemyController());
    }

    private async Task UpdatePositionOnInterface()
    {
        await _interfaceManager.AdicionarAtualizacao(_sprite, Position, Lane);
    }

    private async Task Mover()
    {
        Position -= 2;
        await UpdatePositionOnInterface();
    }
    
    private async Task EnemyController()
    { 
        while (Health > 0)
        {
            // Se a vida for menor que 33% tirar do caps lock
            if (Position > 3) // Implementar um CanMove
            {
                if (CanMove())
                {
                    await Mover();
                }
            }
            else
            {
                await _gameManager.Health(-Damage);
            }
            await Task.Delay(_delayMilliseconds);
        }
        
        // Retirar o inimigo da interface
        await _interfaceManager.AdicionarAtualizacao('-', Position, Lane);
        // Dar o gold ao jogador
        await _gameManager.ChangeGold(Gold);
        _enemySpawner.Lanes[Lane - 1].Dequeue();
    }

    private bool CanMove()
    {
        if (_interfaceManager.Interface[Lane][Position - 2 ] != '-')
        {
            return false;
        }
        
        return true;
    }
}