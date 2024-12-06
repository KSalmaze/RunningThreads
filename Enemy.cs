namespace RunningThreads;

public class Enemy
{
    private char _sprite;
    public int Lane;
    private int _delayMilliseconds;
    private int _position;
    public int Health;
    public int Damage;
    public int Gold;
    
    private InterfaceManager _interfaceManager;
    private GameManager _gameManager;
    
    public Enemy(char sprite, int lane, int health = 3, int damage = 1,int gold = 2,int delayMilliseconds = 500)
    {
        _sprite = sprite;
        Lane = lane;
        _position = 27; // Vai para a ultima posicao da lane
        _delayMilliseconds = delayMilliseconds;
        _interfaceManager = InterfaceManager.Instance;
        _gameManager = GameManager.Instance;
        Health = health;
        Damage = damage;

        _ = Task.Run(async () => await EnemyController());
    }

    private async Task UpdatePositionOnInterface()
    {
        _position -= 2;
        await _interfaceManager.AdicionarAtualizacao(_sprite, _position, Lane);
    }
    
    private async Task EnemyController()
    { 
        while (Health > 0)
        {
            if (_position > 3) // Implementar um CanMove
            {
                await UpdatePositionOnInterface();
            }
            else
            {
                await _gameManager.Health(-Damage);
            }
            await Task.Delay(_delayMilliseconds);
        }
        
        // retirar ele da interface
        // Dar o gold ao jogador
        await _gameManager.ChangeGold(Gold);
    }
}