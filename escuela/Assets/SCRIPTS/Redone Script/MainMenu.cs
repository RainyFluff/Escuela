using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NextSlide()
    {
        SceneManager.LoadScene("goodscene");
    }
   //Jobbar på en main menu, inte klar ännu
}
