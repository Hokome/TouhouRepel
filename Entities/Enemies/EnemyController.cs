using Godot;

public partial class EnemyController : Node2D
{
    [Export] private float _rotationSpeed = 45f;
    [Export] private Thrower _thrower;
    [Export] private Timer _timer;

    private Character _character;
    private Vector2 _aim = Vector2.Up;

    public override void _Ready()
    {
        _character = GetParent<Character>();
        _timer.Timeout += Shoot;
    }

    private void Shoot()
    {
        _thrower.Throw(_aim);
    }

    public override void _Process(double delta)
    {
        _aim = _aim.Rotated(Mathf.DegToRad(_rotationSpeed * (float)delta));
    }
}
