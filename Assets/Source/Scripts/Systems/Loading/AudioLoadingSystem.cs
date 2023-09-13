using Kuhpik;
using NaughtyAttributes;
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
        game.AudioSystem = this;

        screen.SoundButton.onClick.AddListener(OnUpdateSound);

        UpdateAudioMixer();
    }
    public void UpdateSound()
    {
        player.IsSoundOff = !player.IsSoundOff;

        UpdateAudioMixer();
        Extensions.SaveGame(player);
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
    void OnUpdateSound()
    {
        UpdateSound();
        CreateSound(0);

        Extensions.TransformPunchScale(screen.SoundButton.transform);
    }
    void UpdateAudioMixer()
    {
        audioMixer.audioMixer.SetFloat("Music", player.IsSoundOff ? -80 : 0);
        audioMixer.audioMixer.SetFloat("Sound", player.IsSoundOff ? -80 : 0);
        audioMixer.audioMixer.SetFloat("UI", player.IsSoundOff ? -80 : 0);

        screen.SoundImage.sprite = screen.SoundSpriteList[player.IsSoundOff ? 0 : 1];
        screen.SoundImage.SetNativeSize();
    }
}