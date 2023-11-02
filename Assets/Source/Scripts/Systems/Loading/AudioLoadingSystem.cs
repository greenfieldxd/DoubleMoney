using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLoadingSystem : GameSystemWithScreen<SoundUIScreen>
{
    [SerializeField, BoxGroup("Prefab")] List<GameObject> audioList;

    [SerializeField, BoxGroup("Component")] AudioMixerGroup audioMixer;

    [SerializeField, BoxGroup("Debug"), ReadOnly] AudioSource musicSource;

    public AudioSource MusicSource => musicSource;

    public override void OnInit()
    {
        game.audioSystem = this;

        screen.SoundButton.onClick.AddListener(OnUpdateSound);

        UpdateAudioMixer(player.isSoundOff);
    }
    public void UpdateSound()
    {
        player.isSoundOff = !player.isSoundOff;

        UpdateAudioMixer(player.isSoundOff);
        OtherExtensions.SaveGame(player);
    }
    public void CreateSound(int index, float time = 1f)
    {
        GameObject sound = Instantiate(audioList[index]);
        Destroy(sound, time);
    }
    public void CreateMusic(int index)
    {
        if (musicSource) Destroy(musicSource.gameObject);
        if (index < 0) return;

        musicSource = Instantiate(audioList[index]).GetComponent<AudioSource>();
        musicSource.Play();
    }
    public void YandexInterstitialStart()
    {
        if (player.isSoundOff) return;

        UpdateAudioMixer(true);
    }
    public void YandexInterstitialEnd()
    {
        if (player.isSoundOff) return;

        UpdateAudioMixer(false);
    }
    void OnUpdateSound()
    {
        UpdateSound();
        CreateSound(0);

        OtherExtensions.TransformPunchScale(screen.SoundButton.transform);
    }
    void UpdateAudioMixer(bool isStatus)
    {
        audioMixer.audioMixer.SetFloat("Music", isStatus ? -80 : 0);
        audioMixer.audioMixer.SetFloat("Sound", isStatus ? -80 : 0);
        audioMixer.audioMixer.SetFloat("UI", isStatus ? -80 : 0);

        screen.SoundImage.sprite = screen.SoundSpriteList[player.isSoundOff ? 0 : 1];
        screen.SoundImage.SetNativeSize();
    }
}