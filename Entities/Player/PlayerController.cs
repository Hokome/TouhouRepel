using Godot;

public partial class PlayerController : Node2D
{
    [Export] private Repeller _repeller;
    [Export] private Thrower _thrower;
    private Character _character;

    public bool Frozen { get; set; }

    private Vector2 _cursorPosition;
    private Vector2 _moveDirection;
    private float _cooldown;

    public Vector2 Aim => (_cursorPosition - GlobalPosition).Normalized();

    private void MovementInput(InputEventKey eventKey)
    {
        Vector2 inputDirection = eventKey.PhysicalKeycode switch
        {
            Key.W => Vector2.Up,
            Key.A => Vector2.Left,
            Key.S => Vector2.Down,
            Key.D => Vector2.Right,
            _ => Vector2.Zero,
        };

        if (eventKey.IsPressed() && !eventKey.IsEcho())
            _moveDirection += inputDirection;
        else if (eventKey.IsReleased())
            _moveDirection -= inputDirection;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
            MovementInput(eventKey);

        if (Frozen)
        {
            _character.TargetDirection = Vector2.Zero;
            return;
        }

        _moveDirection = _moveDirection.Clamp(-Vector2.One, Vector2.One);

        // Move direction is not normalized to avoid breaking input.
        _character.TargetDirection = _moveDirection;

        if (@event.IsActionPressed("repel", false, true))
        {
            if (_cooldown <= 0f)
                _cooldown = _repeller.Repel(_cursorPosition);
        }
        //if (@event.IsActionPressed("throw", false, true))
        //	_thrower.Throw(Aim);
    }

    public override void _Ready()
    {
        _character = GetParent<Character>();
    }


    public override void _Process(double delta)
    {
        _cursorPosition = GetGlobalMousePosition();
        _cooldown -= (float)delta * 1000f;
    }

    private void _OnHurtboxDeath()
    {
        GetTree().ReloadCurrentScene();
    }
}
