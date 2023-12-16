using Godot;

public partial class GameScene : Node2D
{
    //[Export] private PackedScene _enemyScene;
    //[Export] private Timer _spawnTimer;

    [Export] private PackedScene _playerScene;
    [Export] private PackedScene _hudScene;

    private CanvasItem _endButton;

    //private int _spawnIndex = 0;
    //private Action[] _spawnActions;
    private int _enemyCount;

    //private void _OnSpawnTimerTimeout()
    //{
    //    if (_spawnIndex >= _spawnActions.Length)
    //    {
    //        return;
    //    }
    //    _spawnActions[_spawnIndex]();
    //    _spawnIndex++;
    //}

    //public override void _Ready()
    //{
    //    _spawnActions = new Action[]
    //    {
    //        GetSpawnAction(new(150, 0), 3),
    //        GetSpawnAction(new(-150, 0), 5),
    //        GetSpawnAction(new(-500, 0), 1),
    //        GetSpawnAction(new(500, 0), 5),
    //        GetSpawnAction(new(400, -200), 2),
    //        GetSpawnAction(new(400, 200), 2),
    //        GetSpawnAction(new(-400, 200), 2),
    //        GetSpawnAction(new(-400, -200), 30),
    //        GetSpawnAction(new(0, 100), 1),
    //        GetSpawnAction(new(0, 50), 1),
    //        GetSpawnAction(new(0, -50), 1),
    //        GetSpawnAction(new(0, -100), 10),
    //        GetSpawnAction(new(0, 0), 0.2f),
    //        GetSpawnAction(new(-100, 100), 0.2f),
    //        GetSpawnAction(new(-100, -100), 0.2f),
    //        GetSpawnAction(new(100, -100), 0.2f),
    //        GetSpawnAction(new(100, 100), 5),
    //        GetSpawnAction(new(450, 180), 2),
    //        GetSpawnAction(new(450, -180), 2),
    //        GetSpawnAction(new(-450, -180), 2),
    //        GetSpawnAction(new(-450, 180), 2),
    //    };
    //    _spawnTimer.Start(1);
    //}

    //private Action GetSpawnAction(Vector2 position, float waitTime) => () => Spawn(_enemyScene, position, waitTime);
    //private void Spawn(PackedScene enemyScene, Vector2 position, float waitTime)
    //{
    //    Character enemy = enemyScene.Instantiate<Character>();
    //    AddChild(enemy);

    //    enemy.Position = position;
    //    _enemyCount++;

    //    _spawnTimer.Start(waitTime);
    //}

    public override void _Ready()
    {
        Character player = _playerScene.Instantiate<Character>();
        AddChild(player);
        CanvasLayer hud = _hudScene.Instantiate<CanvasLayer>();
        AddChild(hud);

        ProgressBar playerHealthBar = GetNode<ProgressBar>("HUD/Frame/ProgressBar");
        Hurtbox playerHurtbox = GetNode<Hurtbox>("Player/Hurtbox");
        playerHurtbox.HealthChanged += (v, m) =>
        {
            playerHealthBar.MaxValue = m;
            playerHealthBar.Value = v;
        };
        playerHurtbox.Health = playerHurtbox.Health;
    }

    public void RemoveEnemy()
    {
        _enemyCount--;
        if (false)
        {
            GetNode<PlayerController>("Player/PlayerController").Frozen = true;
            _endButton.Visible = true;
        }
    }
    public void _OnRetry()
    {
        GetTree().ReloadCurrentScene();
    }
}
