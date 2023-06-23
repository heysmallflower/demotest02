using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System;
using UnityEngine.SceneManagement;

public class StarGame : MonoBehaviour
{
    // Start is called before the first frame update
    GComponent view;
    private void Awake()
    {

    }
    void Start()
    {
        UIPackage.AddPackage("DaTaoShao");
        UIPanel panel = gameObject.GetComponent<UIPanel>();
        view = panel.ui;
        view.GetChild("n0").onClick.Set(StartGame);
    }

    private void StartGame(EventContext context)
    {
        SceneManager.LoadScene("Test");
    }
}
