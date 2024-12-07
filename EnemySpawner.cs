namespace RunningThreads;

public class EnemySpawner
{
    // Singleton
    private static readonly EnemySpawner instance = new EnemySpawner();
    public static EnemySpawner Instance => instance;

    // Property s
    private int _delay = 1700;
    
    public List<Queue<Enemy>> Lanes;

    private List<(char sprite, int health, int damage, int delay, int gold)> _enemyInfos;
    
    private GameManager _gameManager = GameManager.Instance;
    
    // Constructor
    public EnemySpawner()
    {
        Lanes = new List<Queue<Enemy>>(){new Queue<Enemy>(), new Queue<Enemy>(), new Queue<Enemy>()};
        
        _enemyInfos = new List<(char sprite, int health, int damage, int delay, int gold)>();
        _enemyInfos.Add(('A', 2, 1, 1200, 2));
        _enemyInfos.Add(('C', 3,2,2000,5));
        _enemyInfos.Add(('D', 6,3,2600,8));
        _enemyInfos.Add(('O', 15,3,2800,8));
        
        _ = Task.Run(async () => await SpawnEnemys());
    }
    
    // Tasks
    private async Task SpawnEnemys()
    {
        while (_gameManager.CurrentHealth > 0)
        {
            await SpawnEnemy();
            await Task.Delay(_delay);
        }
    }
    
    private Task SpawnEnemy()
    {
        try
        {
            Random random = new Random();

            int lane = random.Next(1, 4);
            if (Lanes[lane-1].Count != 0 && Lanes[lane-1].Last().Position == 27)
            {
                return Task.CompletedTask;
            }

            var x = _enemyInfos[random.Next(0, _enemyInfos.Count)];
            
            
            Enemy enemy = new Enemy(x.sprite, lane, x.health, x.damage, x.gold, x.delay);

            Lanes[lane-1].Enqueue(enemy);

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}