using Godot;

public partial class MainMenu : Control
{

    [Export] private CanvasItem _infoBackground;

    public void _OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Environment/GameScene.tscn");
    }
    public void _OnInfoButtonPressed() => ToggleInfo();
    public void _OnOkButtonPressed() => ToggleInfo();

    private void ToggleInfo()
    {
        _infoBackground.Visible = !_infoBackground.Visible;
    }
}
