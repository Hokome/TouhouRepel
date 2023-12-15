using Godot;

public partial class Hurtbox : Area2D
{
	[Export] private int _maxHealth = 20;
	[Export] private ProgressBar _healthBar;
	private Character _character;

	[Signal] public delegate void DeathEventHandler();

	private int _health;
	public int Health
	{
		get => _health;
		set
		{
			_health = value;
			if (_healthBar != null)
				_healthBar.Value = _health;
			if (Health < 1)
				EmitSignal(SignalName.Death);
		}
	}

	public override void _Ready()
	{
		_character = GetParent<Character>();

		if (_healthBar != null)
			_healthBar.MaxValue = _maxHealth;
		Health = _maxHealth;
	}
	private void _OnBodyEntered(Node2D body)
	{
		if (body is not Projectile projectile) return;
		if (projectile.Faction == _character.Faction) return;

		float projSpeed = projectile.LinearVelocity.Length();
		int damage = Mathf.FloorToInt(projectile.DamagePerKms * projSpeed * 0.001f);
		if (damage > 0)
			Health -= damage;
	}
}
