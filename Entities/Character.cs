using Godot;

public partial class Character : RigidBody2D
{
	[Export] private float _baseAcceleration = 5f;
	[Export] private float _maxSpeed = 200f;

	private Vector2 _targetDirection;

	public Vector2 TargetDirection
	{
		get => _targetDirection;
		set
		{
			_targetDirection = value.Normalized();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 targetVelocity = TargetDirection * _maxSpeed;
		Vector2 deltaDirection = (targetVelocity - LinearVelocity).Normalized();

		LinearVelocity += _baseAcceleration * (float)delta * deltaDirection;
	}
}
