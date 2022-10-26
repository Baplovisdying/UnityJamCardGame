using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScence : MonoBehaviour
{
    [SerializeField] private Button returnToMenu;
    [SerializeField] private Button quitGame;

    void Start()
    {
        Time.timeScale = 1;
        FadeInOut.instance.GetFadeIn();
        returnToMenu.onClick.AddListener(() =>
        {
            returnToMenu.interactable = false;
            quitGame.interactable = false;
            FadeInOut.instance.GetFadeOut("OpenningScene");
        });
        quitGame.onClick.AddListener(() => { Application.Quit(); });
        SoundManager.instance.BGM_2.Stop();
        SoundManager.instance.BGM_3.Stop();
        SoundManager.instance.Win.Play();
    }
}
