using Godot;

public partial class PlayerController : Node2D
{
	[Export] private Character _character;
	[Export] private Thrower _thrower;

	private Vector2 _moveDirection;

	public override void _Input(InputEvent @event)
	{
		#region Movement Input
		if (@event.IsActionPressed("move_up", false, true))
			_moveDirection += Vector2.Up;
		if (@event.IsActionPressed("move_down", false, true))
			_moveDirection += Vector2.Down;
		if (@event.IsActionPressed("move_left", false, true))
			_moveDirection += Vector2.Left;
		if (@event.IsActionPressed("move_right", false, true))
			_moveDirection += Vector2.Right;

		if (@event.IsActionReleased("move_up", true))
			_moveDirection -= Vector2.Up;
		if (@event.IsActionReleased("move_down", true))
			_moveDirection -= Vector2.Down;
		if (@event.IsActionReleased("move_left", true))
			_moveDirection -= Vector2.Left;
		if (@event.IsActionReleased("move_right", true))
			_moveDirection -= Vector2.Right;
		#endregion

		// Move direction is not normalized to avoid breaking input.
		_character.TargetDirection = _moveDirection;

		if (@event.IsActionPressed("throw", false, true))
		{
			_thrower.Throw(Vector2.Right * 800);
		}
	}

}
