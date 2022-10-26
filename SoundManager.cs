using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    [SerializeField] public AudioSource BGM_1;
    [SerializeField] public AudioSource BGM_2;
    [SerializeField] public AudioSource BGM_3;

    [SerializeField] public AudioSource BookOpen;
    [SerializeField] public AudioSource CardSelect;
    [SerializeField] public AudioSource Impact;
    [SerializeField] public AudioSource Move;

    [SerializeField] public AudioSource GetHit;
    [SerializeField] public AudioSource Win;
    [SerializeField] public AudioSource Lose;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(this);
    }

}
