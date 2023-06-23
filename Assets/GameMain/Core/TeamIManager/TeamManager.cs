using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using static UnityEditor.PlayerSettings;
using Random = System.Random;

namespace GameMain
{
    public enum TeamIndex
    {
        A, B, C, D, E
    }
    public class TeamManager : GameFrameworkComponent
    {
        GList gList;
        const string PathName = "Enemy/Enemy";
        public Transform teamApos;
        public Transform teamBpos;
        public Transform teamCpos;
        public Transform teamDpos;
        public Transform teamEpos;
        public float teamAF;
        public float teamBF;
        public float teamCF;
        public float teamDF;
        public float teamEF;
        public List<EnemyController> teamA
        {
            get;
            private set;
        }
        public List<EnemyController> teamB
        {
            get;
            private set;
        }
        public List<EnemyController> teamC
        {
            get;
            private set;
        }
        public List<EnemyController> teamD
        {
            get;
            private set;
        }
        public List<EnemyController> teamE
        {
            get;
            private set;
        }
        Random random;
        Coroutine aiLoad;
        protected override void Awake()
        {
            base.Awake();
            teamAF = 0;
            teamBF = 0;
            teamCF = 0;
            teamDF = 0;
            teamEF = 0;
            teamA = new List<EnemyController>();
            teamB = new List<EnemyController>();
            teamC = new List<EnemyController>();
            teamD = new List<EnemyController>();
            teamE = new List<EnemyController>();
            random = new Random();
            UIPackage.AddPackage("DaTaoShao");
            aiLoad = StartCoroutine(InitAI());
        }
        private IEnumerator InitAI()
        {
            for (int i = 0; i < 14; i++)
            {
                yield return null;
                int r = random.Next(1, 5);
                string aiName = PathName + r.ToString();
                GameObject gameObject = LoadEnemy(aiName);
                switch (gameObject.GetComponent<EnemyController>().teamIndex)
                {
                    case TeamIndex.A:
                        gameObject.transform.SetParent(teamApos, false);
                        gameObject.transform.position = teamApos.position;
                        //gameObject.transform.parent = teamApos;
                        Debug.Log(gameObject.transform.position);
                        break;
                    case TeamIndex.B:
                        gameObject.transform.SetParent(teamBpos, false);
                        gameObject.transform.position = teamBpos.position;
                        break;
                    case TeamIndex.C:
                        gameObject.transform.SetParent(teamCpos, false);
                        gameObject.transform.position = teamCpos.position;
                        break;
                    case TeamIndex.D:
                        gameObject.transform.SetParent(teamDpos, false);
                        gameObject.transform.position = teamDpos.position;
                        break;
                    default:
                        gameObject.transform.SetParent(teamEpos, false);
                        gameObject.transform.position = teamEpos.position;
                        break;
                }
                yield return null;
            }
        }
        public GameObject LoadEnemy(string path)
        {
            GameObject gameObject = Resources.Load<GameObject>(path);
            return Instantiate(gameObject, teamApos.position, Quaternion.identity);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                ShowGameEnd();
            }
        }
        List<string> namse = new List<string> { "teamA", "teamB", "teamC", "teamD", "teamE" };
        List<float> fens = new List<float>();
        public void ShowGameEnd()
        {
            fens.Add(teamAF);
            fens.Add(teamBF);
            fens.Add(teamCF);
            fens.Add(teamDF);
            fens.Add(teamEF);
            GComponent view = UIPackage.CreateObject("DaTaoShao", "GameEndCom").asCom;
            gList = view.GetChildAt(0).asList;
            GRoot.inst.AddChild(view);
            gList.itemRenderer = RenderListItem;
            gList.numItems = 5;
            Time.timeScale = 0;
        }
        void RenderListItem(int index, GObject obj)
        {
            GComponent gComponent = obj.asCom;
            gComponent.GetChild("TeamName").asTextField.text = namse[index];
            gComponent.GetChild("TeamFen").asTextField.text = fens[index].ToString();
        }
        public void AddF(TeamIndex teamIndex, int value)
        {
            switch (teamIndex)
            {
                case TeamIndex.A:
                    teamAF += value;
                    break;
                case TeamIndex.B:
                    teamBF += value;
                    break;
                case TeamIndex.C:
                    teamCF += value;
                    break;
                case TeamIndex.D:
                    teamDF += value;
                    break;
                default:
                    teamEF += value;
                    break;
            }
        }
        public void GameOver(TeamIndex teamIndex)
        {
            switch (teamIndex)
            {
                case TeamIndex.A:
                    teamA.RemoveAt(teamA.Count - 1);
                    break;
                case TeamIndex.B:
                    teamA.RemoveAt(teamA.Count - 1);
                    break;
                case TeamIndex.C:
                    teamA.RemoveAt(teamA.Count - 1);
                    break;
                case TeamIndex.D:
                    teamA.RemoveAt(teamA.Count - 1);
                    break;
                default:
                    teamA.RemoveAt(teamA.Count - 1);
                    break;
            }
            if (teamB.Count <= 0 && teamC.Count <= 0 && teamD.Count <= 0 && teamE.Count <= 0)
            {
                ShowGameEnd();
            }
        }
    }
}
