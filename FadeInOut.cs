using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;
    [SerializeField] private Image background;

    float aph;

    void Start()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        PlayerModel.onPlayerIsDead += OnPlayerDead;
        DontDestroyOnLoad(this);
    }

    public void GetFadeOut(string scencsNames)
    {
        StartCoroutine(FadeOut(scencsNames));
    }

    public void GetFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut(string scencsNames)
    {
        Time.timeScale = 1;
        aph = 0;
        background.raycastTarget = true;
        while (aph < 1)
        {
            aph += Time.deltaTime;
            background.color = new Color(0, 0, 0, aph);
            yield return new WaitForSeconds(0);
        }

        SceneManager.LoadScene(scencsNames);
    }

    IEnumerator FadeIn()
    {
        Time.timeScale = 1;
        aph = 1;
        while (aph > 0)
        {
            aph -= Time.deltaTime;
            background.color = new Color(0, 0, 0, aph);
            if (aph < 0.05) background.raycastTarget = false;
            yield return new WaitForSeconds(0);
        }
    }

    public void OnPlayerDead()
    {
        StartCoroutine(FadeOut("GameOverScene"));
    }
}
