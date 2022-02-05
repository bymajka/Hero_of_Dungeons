using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenScript : MonoBehaviour
{
    public GameObject FinalScreen;
    public GameObject myscript;
    private bool finalscreenactive = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && finalscreenactive)
        {
            AudioListener.pause = false;
            FinalScreen.SetActive(false);
            Time.timeScale = 1f;
            myscript.GetComponent<HeroScript>().enabled = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    public void FinalScreenFunc()
    {
        finalscreenactive = true;
        FinalScreen.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        myscript.GetComponent<HeroScript>().enabled = false;
    }
}
