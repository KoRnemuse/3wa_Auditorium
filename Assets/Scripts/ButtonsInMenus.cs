using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsInMenus : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1); //Load first scene
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0); //Load Title Scene
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit(); // Quit the Application
    }

    public void NextLvl()
    {
        //We check if there's another scene in build setting before going to the next level
        if (SceneManager.sceneCountInBuildSettings - 1 > SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load the scene which has scene index +1 than the current scene
        }
        else
        {
            SceneManager.LoadScene(0); //Load Title Scene
        }
    }
}
