using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private bool musicStatus = true;
    private bool effectsStatus = true;

    public AudioMixer mixer;
    public Button musicButton;
    public Button effectsButton;
    public GameObject soundSystem;
    public GameObject settingsMenu;
    public GameObject[] settingsTabs;
    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite effectsOn;
    public Sprite effectsOff;

    private void Start()
    {
        if (!musicStatus)
        {
            mixer.SetFloat("MusicVolume", -80f);
        }
        if (!effectsStatus)
        {
            mixer.SetFloat("EffectsVolume", -80f);
        }
    }

    public void OpenSettings()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        settingsMenu.SetActive(true);
        settingsTabs[0].SetActive(true);
        settingsTabs[1].SetActive(false);
        settingsTabs[2].SetActive(false);
    }

    public void CloseSettings()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        settingsMenu.SetActive(false);
    }

    public bool GetSettingsStatus()
    {
        return settingsMenu.activeInHierarchy;
    }

    public void ChangeMusic()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        if (musicStatus)
        {
            musicButton.image.sprite = musicOff;
            musicStatus = false;
            mixer.SetFloat("MusicVolume", -80f);
        }
        else
        {
            musicButton.image.sprite = musicOn;
            musicStatus = true;
            mixer.SetFloat("MusicVolume", 0f);
        }
    }

    public void ChangeEffects()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        if (effectsStatus)
        {
            effectsButton.image.sprite = effectsOff;
            effectsStatus = false;
            mixer.SetFloat("EffectsVolume", -80f);
        }
        else
        {
            effectsButton.image.sprite = effectsOn;
            effectsStatus = true;
            mixer.SetFloat("EffectsVolume", 0f);
        }
    }

    public bool GetMusicStatus()
    {
        return musicStatus;
    }

    public bool GetEffectsStatus()
    {
        return effectsStatus;
    }

    public void SetVolume(bool savedMusic, bool savedEffects)
    {
        if (!savedMusic)
        {
            musicButton.image.sprite = musicOff;
            musicStatus = false;
        }
        if (!savedEffects)
        {
            effectsButton.image.sprite = effectsOff;
            effectsStatus = false;
        }
    }

    public void OpenLanguageTab()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        settingsTabs[0].SetActive(false);
        settingsTabs[1].SetActive(true);
    }

    public void OpenAuthorsTab()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        settingsTabs[0].SetActive(false);
        settingsTabs[2].SetActive(true);
    }
    
    public void ReturnToMain()
    {
        soundSystem.GetComponent<SoundSystem>().PlayClick();
        settingsTabs[0].SetActive(true);
        settingsTabs[1].SetActive(false);
        settingsTabs[2].SetActive(false);
    }
}