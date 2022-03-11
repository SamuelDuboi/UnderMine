using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public float maxTime = 300.0f;
    public float timeLeft;

    public GameObject ingameCanvas;
    public GameObject pauseCanvas;
    public Text timerText;

    public Image timeImage;
    public TimeManager instance;
    private bool doOnce;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        ResetTime();    
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 60.0f)
        {
            timerText.text = "" + Mathf.Round(timeLeft);
        }
        else
        {
            if(Mathf.Round(timeLeft % 60) <= 9)
            {
                timerText.text = "" + Mathf.Floor(timeLeft / 60) + " : 0" + Mathf.Round(timeLeft % 60);
            }
            else
            {
                timerText.text = "" + Mathf.Floor(timeLeft / 60) + " : " + Mathf.Round(timeLeft % 60);
            }
        }
        if(timeLeft<10&& !doOnce)
        {
            StartCoroutine(warningRed());
            doOnce = true;
        }
        if(timeLeft <= 0)
        {
            StopMine();
        }
    }

    public void ResetTime()
    {
        timeLeft = maxTime;
    }

    public void StopMine()
    {
        // TODO : Save
        SceneManager.LoadScene("HubScene");
    }

    public void PauseMineAction()
    {
        ingameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeMineAction()
    {
        pauseCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void QuitMineAction()
    {
        StopMine();
        Time.timeScale = 1;
    }

    IEnumerator warningRed()
    {
        for (int i = 0; i < 10; i++)
        {
            timeImage.color -= new Color(0, 0.1f, 0.1f,0);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(warningWhite());
    }
    IEnumerator warningWhite()
    {
        for (int i = 0; i < 10; i++)
        {
            timeImage.color += new Color(0f, 0.1f, 0.1f,0);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(warningRed());
    }
}
