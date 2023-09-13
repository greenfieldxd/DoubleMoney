using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class SettingsLoadingSystem : GameSystem
{
    //[SerializeField, BoxGroup("Settings")] float orthographicSize = 5f;
    [SerializeField, BoxGroup("Settings")] float fieldOfView = 60f;

    public override void OnInit()
    {
        GameLaunch();

        Bootstrap.Instance.ChangeGameState(GameStateID.Menu);
    }
    void GameLaunch()
    {
        game.MainCamera = FindObjectOfType<Camera>();
        //game.MainCamera.orthographicSize = orthographicSize;
        game.MainCamera.fieldOfView = fieldOfView;

        //screen.VersionNumber.text = "" + Application.version;

        if (player.IsFirstLaunch) return;

        player.IsFirstLaunch = true;
        player.LanguageIndex = -1;

        Extensions.SaveGame(player);
    }
}