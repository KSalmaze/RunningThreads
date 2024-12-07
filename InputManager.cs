namespace RunningThreads;

public class InputManager
{
    private GameManager _gameManager = GameManager.Instance;
    private List<Guard> _guards;

    private int HealthUpgradeCounter = 1;
    private int GuardsUpgradeCounter = 1;
    
    public InputManager(List<Guard> guards)
    {
        _guards = guards;
    }
    
    public async Task Start()
    {
        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D0:
                    return;
                    break;
                case ConsoleKey.D1:

                    if (CheckForGold(HealthUpgradeCounter * 10))
                    {
                        HealthUpgradeCounter++;
                        await _gameManager.IncreaseMaxHealth();
                        await _gameManager.ChangeGold(HealthUpgradeCounter * 10 * -1);
                    }
                    break;
                case ConsoleKey.D2:

                    if (GuardsUpgradeCounter < 6 && CheckForGold(GuardsUpgradeCounter * 10))
                    {
                        GuardsUpgradeCounter++;
                        foreach (Guard guard in _guards)
                        {
                            await guard.Upgrade();
                        }
                    }
                    break;
            }
        }
    }

    public bool CheckForGold(int goldAmount)
    {
        if (_gameManager.Gold >= goldAmount)
        {
            return true;
        }

        return false;
    }
}