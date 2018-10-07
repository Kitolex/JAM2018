﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public EventSystem e;
    public GameObject go;

    public void Setactive()
    {
        gameObject.SetActive(true);
        e.SetSelectedGameObject(go);
    }

    // TODO : Build settings > mettre les scenes dans l'ordre dans le projet final :3
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
