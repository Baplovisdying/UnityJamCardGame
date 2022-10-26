using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScence : MonoBehaviour
{
    [SerializeField] private Button starButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject fadePrefab;
    [SerializeField] private GameObject soundPrefab;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true, 120);
        Application.targetFrameRate = 120;
        Time.timeScale = 1;
        starButton.onClick.AddListener(StarGame);
        quitButton.onClick.AddListener(() => { Application.Quit(); });

        if (FadeInOut.instance == null)
        {
            Instantiate(fadePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            FadeInOut.instance.GetFadeIn();
        }
        if (SoundManager.instance == null)
        {
            Instantiate(soundPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            SoundManager.instance.BGM_1.Play();
        }


        PlayerModel.ReLoad();
        GameModel.ReLoad();
    }

    void StarGame()
    {
        starButton.interactable = false;
        quitButton.interactable = false;
        FadeInOut.instance.GetFadeOut("GameScene");
    }
}
