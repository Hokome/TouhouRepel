using Godot;

[Tool]
public partial class Wall : StaticBody2D
{
    private Vector2 _size = new(32, 32);
    [Export]
    public Vector2 Size
    {
        get => _size;
        set
        {
            _size = value;
            CollisionShape2D shape = GetNode<CollisionShape2D>("CollisionShape2D");
            Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");

            shape.Shape = new RectangleShape2D() { Size = value };
            sprite.Scale = value / sprite.Texture.GetSize();
        }
    }
}
