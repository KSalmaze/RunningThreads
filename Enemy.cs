using System.Runtime.Intrinsics.Arm;

namespace RunningThreads;

public class Enemy
{
    public char Sprite;
    public int Lane;
    private int _delayMilliseconds;
    private int _position;
    
    private InterfaceManager _interfaceManager;
    
    public Enemy(char sprite, int lane, int delayMilliseconds = 500)
    {
        Sprite = sprite;
        Lane = lane;
        _position = 27; // Vai para a ultima posicao da lane
        _delayMilliseconds = delayMilliseconds;

        _ = Task.Run(async () => await EnemyController());
    }

    private void UpdatePositionOnInterface()
    {
        _position -= 2;
        _interfaceManager.AdicionarAtualizacao(Sprite, _position, _delayMilliseconds);
    }
    
    private async Task EnemyController()
    { 
        while (_position < 3)// (health > 0)
        {
            if (_position < 3)
            {
                UpdatePositionOnInterface();
            }
            else
            {
                // Ataque
            }
            await Task.Delay(_delayMilliseconds);
        }
    }
}