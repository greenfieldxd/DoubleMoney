using Kuhpik;
using Source.Scripts;

public class GameStartSystem : GameSystemWithScreen<MenuUIScreen>
{
    public override void OnInit()
    {
        screen.PlayButton.onClick.AddListener(OnStartGame);
    }
    void OnStartGame()
    {
        game.audioSystem.CreateSound(0);
        game.audioSystem.CreateMusic(1);

        Bootstrap.Instance.ChangeGameState(GameStateID.Game);
        CameraController.Instance.SwitchCameraWithDelay(0);
    }
}