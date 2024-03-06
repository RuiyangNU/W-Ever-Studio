using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerUI : PopupUI
{
    public bool isUIOpen = false;

    [SerializeField]
    private GameObject musicPlayerPanel;

    [SerializeField]
    private AudioManager musicManager;

    [SerializeField]
    private TextMeshProUGUI musicText;

    [SerializeField]
    private Slider musicVolumn;

    public int musicIndex = 0;

    public void ChangeUI()
    {
        if (isUIOpen)
        {
            CloseUI();
        }
        else
        {
            OpenUI();
        }
    }


    public override void CloseUI()
    {
        isUIOpen = false;
        musicPlayerPanel.SetActive(false);
    }

    public override void OpenUI()
    {
        isUIOpen = true;
        musicPlayerPanel.SetActive(true);
    }

    public override void UpdateUI()
    {
        //DO SOMETHING

    }


    public void ChoosePrevIndex()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < 0)
        {
            musicIndex = musicManager.musicSounds.Length - 1;
        }
    }

    public void ChooseNextIndex()
    {
        musicIndex = musicIndex + 1;
        musicIndex %= musicManager.musicSounds.Length;
    }

    public void PlayNextSong()
    {
        ChooseNextIndex();
        musicManager.PlayMusic(musicIndex);
        musicText.text = musicManager.musicSounds[musicIndex].name;

    }

    public void PlayPrevSong()
    {
        ChoosePrevIndex();
        musicManager.PlayMusic(musicIndex);
        musicText.text = musicManager.musicSounds[musicIndex].name;
    }

    public void ChangeVolumn()
    {
        float volumn = musicVolumn.value;

        musicManager.musicSource.volume = volumn;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
