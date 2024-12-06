namespace RunningThreads;

// Sao or guardas do castelo, dao dano nos inimigos
// de suas respectivas lanes
public class Guard
{
    private char _sprite;
    private int _lane;
    private int _delayMilliseconds;
    private int _position;
    private int _damage;
    private int _level;
    
    private char[] _upgradedSprites = new char[]{'+','>','<','%','@'};
    private int[] _upgradedDamage = new int[]{1, 2, 4, 7, 10};

    private GameManager _gameManager = GameManager.Instance;
    private InterfaceManager _interfaceManager = InterfaceManager.Instance;
    
    public Guard(int lane,int position, int delayMilliseconds)
    {
        _lane = lane;
        _position = position;
        _delayMilliseconds = delayMilliseconds;
        _level = 0;
        
        _sprite = _upgradedSprites[_level];
        _damage = _upgradedDamage[_level];

        _ = Task.Run(async () => await GuardBehavior());
    }

    public void Upgrade()
    {
        if (_level + 1 < _upgradedDamage.Length)
        {
            _level++;
            _sprite = _upgradedSprites[_level];
            _damage = _upgradedDamage[_level];
        }
    }
    
    private async Task GuardBehavior()
    {
        while (_gameManager.CurrentHealth > 0)
        {
            // Dar dano ao inimigo da lane
            // Alterar a sprite para entender que atirou
            await Task.Delay(_delayMilliseconds);
        }
    }
}