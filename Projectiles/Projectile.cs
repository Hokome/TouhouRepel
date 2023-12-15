using Godot;

public partial class Projectile : RigidBody2D
{
	public static readonly Color PlayerFactionColor = new("7cd6de");
	public static readonly Color EnemyFactionColor = new("de7c7c");

	[Export] private int _damagePerKpxs;
	[Export] private Timer _timer;

	private int faction;

	public int DamagePerKms => _damagePerKpxs;
	public int Faction
	{
		get => faction;
		set
		{
			faction = value;
			_timer.Stop();
			_timer.Start();
			GetNode<Sprite2D>("Sprite2D").Modulate = value == 0 ? PlayerFactionColor : EnemyFactionColor;
		}
	}


	private void _OnTimerTimeout()
	{
		QueueFree();
	}

}
