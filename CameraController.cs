using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    //[SerializeField] private GameObject pauseCanvas;
    //[SerializeField] private AudioSource pauseAudio;
    private bool isPaused;

    private float cameraShakePower;
    private Vector3 shakeActive;
    private bool playerDead;

    private void Start()
    {
        PlayerModel.onPosChangeAction += OnPlayerMove;
        PlayerModel.onHPLowerAction += OnPlayerHurt;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        PlayerModel.onPosChangeAction -= OnPlayerMove;
        PlayerModel.onHPLowerAction -= OnPlayerHurt;
    }

    private void Update()
    {
        HandleTimeShake();
        HandlePause();
    }

    private void LateUpdate()
    {
        HandleCameraFollow();
        HandleCameraClamp();
    }

    void HandleCameraFollow()
    {
        if (!playerDead)
            transform.position = Vector3.Lerp(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
    }

    void HandleCameraClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), -10);
    }

    public void StartCameraShake(float _amount)
    {
        cameraShakePower = _amount;
    }


    void HandleTimeShake()
    {
        if (cameraShakePower > 0)
        {
            shakeActive = new Vector3(Random.Range(-cameraShakePower, cameraShakePower), Random.Range(-cameraShakePower, cameraShakePower), 0f);
            cameraShakePower -= Time.deltaTime;
        }
        else
        {
            shakeActive = Vector3.zero;
        }
        transform.position += shakeActive;
    }

    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {

                Time.timeScale = 0;

            }
            else
            {

                Time.timeScale = 1;

            }
            GameModel.isPaused = isPaused;
        }
    }

    void PlayerDead()
    {
        playerDead = true;
    }

    void OnPlayerMove(int _Pos)
    {
        StartCameraShake(0.04f);
    }
    void OnPlayerHurt(int _Pos)
    {
        StartCameraShake(0.08f);
        StartCoroutine(TimeController(0.01f));
    }

    IEnumerator TimeController(float _slowedTime)
    {
        Time.timeScale = _slowedTime;
        while (Time.timeScale < 1)
        {
            Time.timeScale += 0.5f;
            if (Time.timeScale > 1)
            {
                Time.timeScale = 1;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
