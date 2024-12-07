namespace RunningThreads;

// Sao or guardas do castelo, dao dano nos inimigos
// de suas respectivas lanes
public class Guard
{
    private char _sprite;
    private int _lane;
    private int _delayMilliseconds;
    private int _position = 1;
    private int _damage;
    private int _level;
    
    private char[] _upgradedSprites = new char[]{'+','>','<','%','@'};
    private int[] _upgradedDamage = new int[]{1, 2, 4, 7, 10};

    private GameManager _gameManager = GameManager.Instance;
    private InterfaceManager _interfaceManager = InterfaceManager.Instance;
    private EnemySpawner _enemySpawner = EnemySpawner.Instance;
    
    public Guard(int lane, int delayMilliseconds)
    {
        _lane = lane;
        _delayMilliseconds = delayMilliseconds;
        _level = 0;
        
        _sprite = _upgradedSprites[_level];
        _damage = _upgradedDamage[_level];
        
        _ = Task.Run(async () => await GuardBehavior());
    }

    public async Task Upgrade()
    {
        if (_level + 1 < _upgradedDamage.Length)
        {
            _level++;
            _sprite = _upgradedSprites[_level];
            _damage = _upgradedDamage[_level];
            await _interfaceManager.AdicionarAtualizacao(_sprite, _position, _lane);
        }
    }
    
    private async Task GuardBehavior()
    {
        // Coloca o guarda na interface
        await _interfaceManager.AdicionarAtualizacao(_sprite, _position, _lane);
        
        while (_gameManager.CurrentHealth > 0)
        {
            try
            {
                while (!(_enemySpawner.Lanes[_lane - 1].Count > 0)) {}

                // Dar dano ao inimigo da lane
                _enemySpawner.Lanes[_lane - 1].First().Health -= _damage;
                
                // Alterar a sprite para uma indicacao visual que atirou
                _ = Task.Run(async () => await AlterarSprite());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await Task.Delay(_delayMilliseconds);
        }
    }

    private async Task AlterarSprite()
    {
        await _interfaceManager.AdicionarAtualizacao('O', _position, _lane);
        await Task.Delay(350);
        await _interfaceManager.AdicionarAtualizacao(_sprite, _position, _lane);
    }
}