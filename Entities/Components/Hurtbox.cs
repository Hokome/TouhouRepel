using Godot;

public partial class Hurtbox : Area2D
{
    [Export] private int _maxHealth = 20;
    private Character _character;

    [Signal] public delegate void DeathEventHandler();
    [Signal] public delegate void HealthChangedEventHandler(float val, float max);

    private int _health;
    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Min(value, MaxHealth);
            EmitSignal(SignalName.HealthChanged, Health, MaxHealth);
            if (Health < 1 && MaxHealth > 0)
                EmitSignal(SignalName.Death);
        }
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            EmitSignal(SignalName.HealthChanged, Health, MaxHealth);
        }
    }

    public override void _Ready()
    {
        _character = GetParent<Character>();

        MaxHealth = _maxHealth;
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
