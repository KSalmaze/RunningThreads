namespace RunningThreads;

// Classe para os inimigos 
public class Enemy
{
    private char _sprite; // Sprite a ser mostrada na irteface
    public int Lane; // Indica qual dos 3 caminhos o inimigos esta
    private int _delayMilliseconds; // Delay entre as acoes do inimigo
    public int Position; // Posicao horizontal do inimigo
    public int Health; // Vida atual
    public int Damage; // Dano
    public int Gold; // Quantidade de ouro dada ao jogador quando o inimigo for derrotado
    
    private InterfaceManager _interfaceManager; // Referencia a interface
    private GameManager _gameManager; // Referencia so game manager
    private EnemySpawner _enemySpawner; // Referencia ao spawner de inimigos
    
    // Contrutor
    public Enemy(char sprite, int lane, int health = 3, int damage = 1,int gold = 2,int delayMilliseconds = 500)
    {
        _sprite = sprite;
        Lane = lane;
        Position = 27; // Vai para a ultima posicao da lane
        _delayMilliseconds = delayMilliseconds;
        _interfaceManager = InterfaceManager.Instance;
        _gameManager = GameManager.Instance;
        _enemySpawner = EnemySpawner.Instance;
        Gold = gold;
        Health = health;
        Damage = damage;
        
        // Inicia paralelamente a task que controla o comportamento do inimigo
        _ = Task.Run(async () => await EnemyController());
    }

    // Atualiza a representacao do inimigo na interface
    private async Task UpdatePositionOnInterface()
    {
        await _interfaceManager.AdicionarAtualizacao(_sprite, Position, Lane);
    }

    // Move o imigo de posicao
    private async Task Mover()
    {
        Position -= 2;
        await UpdatePositionOnInterface();
    }
    
    // Controla o comportamento do inimigo
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
        // remover da lista de inimigos
        _enemySpawner.Lanes[Lane - 1].Dequeue();
    }

    // Verificar se o inimigo pode se mover
    private bool CanMove()
    {
        // Verificar se ha algum inimigo na posicao na proxima posicao
        if (_interfaceManager.Interface[Lane][Position - 2 ] != '-')
        {
            return false;
        }
        
        return true;
    }
}