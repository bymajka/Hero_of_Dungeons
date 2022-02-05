using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject myscript;

    public void continueGame()
    {
        Time.timeScale = 1f;
        PausePannel.SetActive(false);
        AudioListener.pause = false;
        myscript.GetComponent<HeroScript>().enabled = true;
    }

    public void Exit()
    {
        AudioListener.pause = false;
        PausePannel.SetActive(false);
        Time.timeScale = 1f;
        myscript.GetComponent<HeroScript>().enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
