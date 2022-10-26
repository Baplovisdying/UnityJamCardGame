using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private Button returnToMenu;
    [SerializeField] private Button quitGame;
    [SerializeField] private GameObject plane;

    void Start()
    {
        returnToMenu.onClick.AddListener(() =>
        {
            returnToMenu.interactable = false;
            quitGame.interactable = false;
            SoundManager.instance.BGM_2.Stop();
            SoundManager.instance.BGM_3.Stop();
            FadeInOut.instance.GetFadeOut("OpenningScene");
        });
        quitGame.onClick.AddListener(() => { Application.Quit(); });

        GameModel.onGamePause += ShowAndHide;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        GameModel.onGamePause -= ShowAndHide;
    }

    void ShowAndHide(bool _isPaise)
    {
        plane.SetActive(_isPaise);
    }

}
