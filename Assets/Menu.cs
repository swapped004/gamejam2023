using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public void startGame()
    {
        SceneManager.LoadScene(2);
    }

    public void controls()
    {
        SceneManager.LoadScene(1);
    }



}