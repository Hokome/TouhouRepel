using Godot;

public partial class Thrower : Node2D
{

	[Export] private float _throwForce = 1200;

	[Export] private PackedScene _projectile;

	public void Throw(Vector2 direction)
	{
		var projectile = _projectile.Instantiate<RigidBody2D>();
		GetTree().Root.GetChild(0).AddChild(projectile);
		projectile.GlobalPosition = GlobalPosition;

		projectile.ApplyForce(direction * _throwForce);
	}
}
