using Godot;

public partial class Thrower : Node2D
{
	[Export] private float _throwForce = 1200;

	[Export] private PackedScene _projectile;

	private Character _character;

	public override void _Ready()
	{
		_character = GetParent<Character>();
	}

	public void Throw(Vector2 direction)
	{
		var projectile = _projectile.Instantiate<Projectile>();
		GetTree().Root.GetChild(0).AddChild(projectile);
		projectile.Faction = _character.Faction;
		projectile.GlobalPosition = GlobalPosition;

		projectile.ApplyForce(direction * _throwForce);
	}
}
