using GameMain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    Button button1;
    Button button2;
    // Start is called before the first frame update
    void Start()
    {
        button1 = transform.GetChild(0).GetComponent<Button>();
        button2 = transform.GetChild(1).GetComponent<Button>();
        button1.onClick.AddListener(TeamAAttack);
        button2.onClick.AddListener(TeamBAttack);;
    }

    private void TeamBAttack()
    {
        GameEntry.teamController.TeamBAttack();
    }

    private void TeamAAttack()
    {
        GameEntry.teamController.TeamAAttack();
    }
    private void StartGame()
    {
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
