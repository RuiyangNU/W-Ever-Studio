using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerUI : PopupUI
{
    public bool isUIOpen = false;

    [SerializeField]
    private GameObject musicPlayerPanel;

    [SerializeField]
    private AudioManager musicManager;

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

    }

    public void PlayPrevSong()
    {
        ChoosePrevIndex();
        musicManager.PlayMusic(musicIndex);
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
