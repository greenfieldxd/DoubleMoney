using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Extensions;
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
        game.mainCamera = FindObjectOfType<Camera>();
        //game.MainCamera.orthographicSize = orthographicSize;
        game.mainCamera.fieldOfView = fieldOfView;

        //screen.VersionNumber.text = "" + Application.version;

        if (player.isFirstLaunch) return;

        player.isFirstLaunch = true;
        player.languageIndex = -1;

        OtherExtensions.SaveGame(player);
    }
}