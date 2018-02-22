﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour {

    public Button backToStart;
    public Button offline;
    public Button online;

    /**
     *Method to go back to main menu when back button is clicked
     * @author Lawrence Howes-Yarlett
     */
    public void BackToStartPress()
    {
        SceneManager.LoadScene(0);
    }

    /**
     *Method to go to the offline game set up scene
     * @author Lawrence Howes-Yarlett
     */
    public void OfflinePress()
    {
        //SceneManager.LoadScene(//offline setup scene index);
    }

    /**
     *Method to go to the online game set up scene
     * @author Lawrence Howes-Yarlett
     */
    public void OnlinePress()
    {
        //SceneManager.LoadScene(//online setup scene index);
    }
}
