using System.Runtime.Intrinsics.Arm;

namespace RunningThreads;

public class Enemy
{
    public char Sprite;
    public int Lane;
    private int _delayMilliseconds;
    private int _position;
    
    public Enemy(char sprite, int lane, int delayMilliseconds = 1000)
    {
        Sprite = sprite;
        Lane = lane;
        _position = 12;
    }

    private void UpdatePositionOnInterface()
    {
        // Acessar a interface e atualizar a tela
    }
    
    private async Task EnemyController()
    {
        while (_position < 0)
        {
            UpdatePositionOnInterface();
            await Task.Delay(_delayMilliseconds);
        }
    }
}