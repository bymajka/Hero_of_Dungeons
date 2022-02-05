using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject myscript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause = true;
            myscript.GetComponent<HeroScript>().enabled = false;
        }
    }
    
}
