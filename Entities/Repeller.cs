using Godot;
using System.Collections.Generic;

public partial class Repeller : Area2D
{
    [Export] private float _repelForce = 20_000;
    [Export] private int _activeTimeMs = 100;
    [Export] private int _cooldownMs = 500;

    [Export] private Sprite2D _sprite;

    private Vector2 _repelDirection;
    private int _startTimeMs;
    private Character _character;
    private List<Projectile> _repelledProjectiles = new(5);

    public override void _Ready()
    {
        _character = GetParent<Character>();
    }


    public override void _PhysicsProcess(double delta)
    {
        if ((int)Time.GetTicksMsec() - _startTimeMs <= _activeTimeMs)
        {
            foreach (var item in GetOverlappingBodies())
            {
                if (item is not Projectile proj) continue;
                if (_repelledProjectiles.Contains(proj)) continue;

                _repelledProjectiles.Add(proj);

                proj.Faction = _character.Faction;
                proj.LinearVelocity = Vector2.Zero;
                Vector2 finalForce = _repelDirection * _repelForce;
                proj.ApplyImpulse(finalForce);
            }
        }
        else
        {
            _sprite.Visible = false;
        }
    }

    public float Repel(Vector2 point)
    {
        _repelledProjectiles.Clear();
        LookAt(point);
        _repelDirection = (point - GlobalPosition).Normalized();
        _sprite.Visible = true;
        _startTimeMs = (int)Time.GetTicksMsec();
        return _cooldownMs;
    }
}
