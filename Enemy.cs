namespace RunningThreads;

public class Enemy
{
    private char _sprite;
    public int Lane;
    private int _delayMilliseconds;
    private int _position;
    
    private InterfaceManager _interfaceManager;
    
    public Enemy(char sprite, int lane, int delayMilliseconds = 500)
    {
        _sprite = sprite;
        Lane = lane;
        _position = 27; // Vai para a ultima posicao da lane
        _delayMilliseconds = delayMilliseconds;
        _interfaceManager = InterfaceManager.Instance;

        _ = Task.Run(async () => await EnemyController());
    }

    private async Task UpdatePositionOnInterface()
    {
        _position -= 2;
        _interfaceManager.AdicionarAtualizacao(_sprite, _position, _delayMilliseconds);
    }
    
    private async Task EnemyController()
    { 
        while (_position > 3)// (health > 0)
        {
            if (_position > 3)
            {
                _ = Task.Run(async () => await UpdatePositionOnInterface());
            }
            else
            {
                // Ataque
            }
            await Task.Delay(_delayMilliseconds);
        }
    }
}