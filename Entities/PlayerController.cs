using Godot;

public partial class PlayerController : Node2D
{
	[Export] private Character _character;
	[Export] private Thrower _thrower;

	private Vector2 _cursorPosition;
	private Vector2 _moveDirection;

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

		_moveDirection = _moveDirection.Clamp(-Vector2.One, Vector2.One);

		// Move direction is not normalized to avoid breaking input.
		_character.TargetDirection = _moveDirection;

		if (@event.IsActionPressed("throw", false, true))
		{
			_thrower.Throw((_cursorPosition - _thrower.GlobalPosition).Normalized());
		}
	}

	public override void _Process(double delta)
	{
		_cursorPosition = GetGlobalMousePosition();
	}
}
