using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public enum Team
    {
        A, B
    }
    public class TeamController : GameFrameworkComponent
    {
        const string PathName = "DogPBR";
        public Transform posA;
        public Transform posB;
        public Button button1;
        public Button button2;
        Dictionary<Team, List<ItemController>> teamsValue;
        public List<ItemController> teamA
        {
            get;
            private set;
        }
        public List<ItemController> teamB
        {
            get;
            private set;
        }
        protected override void Awake()
        {
            base.Awake();
            teamA = new List<ItemController>();
            teamB = new List<ItemController>();
        }
        private void Start()
        {
            InitMode();
            TeamAAttack();
            InitHealth();
        }

        private void InitHealth()
        {
            foreach (var item in teamA)
            {
                item.InitAddHealth((int)(item.Date.maxHealth * 0.3));
            }
            foreach (var item in teamB)
            {
                item.InitAddHealth((int)(item.Date.maxHealth * 0.3));
            }
        }

        void InitMode()
        {
            for (int i = 0; i < 12; i++)
            {
                if (teamA.Count < 6)
                {
                    if (teamA.Count == 0)
                    {
                        GameObject temp = Load(PathName, posA.position);
                        temp.transform.Rotate(new Vector3(0, 180, 0), Space.World);
                        teamA.Add(temp.GetComponent<ItemController>());
                        temp.GetComponent<ItemController>().teamName = Team.A;
                        temp.GetComponent<ItemController>().index = 1;
                    }
                    else if (teamA.Count <= 2)
                    {
                        GameObject temp = Load(PathName, teamA[i - 1].gameObject.transform.position + new Vector3(6, 0, 0));
                        temp.transform.Rotate(new Vector3(0, 180, 0), Space.World);
                        teamA.Add(temp.GetComponent<ItemController>());
                        temp.GetComponent<ItemController>().teamName = Team.A;
                        temp.GetComponent<ItemController>().index = i + 1;
                    }
                    else
                    {
                        GameObject temp = Load(PathName, teamA[i - 3].gameObject.transform.position + new Vector3(0, 0, 6));
                        temp.transform.Rotate(new Vector3(0, 180, 0), Space.World);
                        teamA.Add(temp.GetComponent<ItemController>());
                        temp.GetComponent<ItemController>().teamName = Team.A;
                        temp.GetComponent<ItemController>().index = i + 1;
                    }

                }
                else
                {
                    if (teamB.Count == 0)
                    {
                        GameObject temp = Load(PathName, posB.position);
                        teamB.Add(temp.GetComponent<ItemController>());
                        temp.GetComponent<ItemController>().teamName = Team.B;
                        temp.GetComponent<ItemController>().index = i - 6 + 1;
                    }
                    else if (teamB.Count <= 2)
                    {
                        GameObject temp = Load(PathName, teamB[i - 7].gameObject.transform.position + new Vector3(6, 0, 0));
                        teamB.Add(temp.GetComponent<ItemController>());
                        temp.GetComponent<ItemController>().teamName = Team.B;
                        temp.GetComponent<ItemController>().index = i - 6 + 1;
                    }
                    else
                    {
                        GameObject temp = Load(PathName, teamB[i - 9].gameObject.transform.position - new Vector3(0, 0, 6));
                        teamB.Add(temp.GetComponent<ItemController>());
                        temp.GetComponent<ItemController>().teamName = Team.B;
                        temp.GetComponent<ItemController>().index = i - 6 + 1;
                    }
                }
            }
        }
        private void Update()
        {
            InspectItemState();

        }
        void InspectItemState()
        {
            if (teamA.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    ItemController temp = teamA[i].gameObject.GetComponent<ItemController>();
                    if (temp.isDead && !teamA[i + 3].gameObject.GetComponent<ItemController>().isDead)
                    {
                        swap(teamA, i, i + 3);
                        teamA[i].gameObject.GetComponent<ItemController>().SwapInfo(temp, teamA[i + 3].gameObject.GetComponent<ItemController>().pos);
                    }
                }
            }

            if (teamB.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    ItemController temp = teamB[i].gameObject.GetComponent<ItemController>();
                    if (temp.isDead && !teamB[i + 3].gameObject.GetComponent<ItemController>().isDead)
                    {
                        swap(teamB, i, i + 3);
                        teamB[i].gameObject.GetComponent<ItemController>().SwapInfo(temp, teamB[i + 3].gameObject.GetComponent<ItemController>().pos);
                    }
                }
            }

        }
        public GameObject Load(string path, Vector3 pos, bool isRotation = false)
        {
            GameObject gameObject = Resources.Load<GameObject>(path);
            return Instantiate(gameObject, pos, Quaternion.identity);
        }
        public void TeamAAttack()
        {
            for (int i = 0; i < 3; i++)
            {
                ItemController temp = teamA[i].gameObject.GetComponent<ItemController>();
                temp.FindAttackTarget(teamB[i].gameObject, null);
                teamB[i].gameObject.GetComponent<ItemController>().FindAttackTarget(null, temp.gameObject);
            }
        }
        public void TeamBAttack()
        {
            for (int i = 0; i < 3; i++)
            {
                ItemController temp = teamB[i].gameObject.GetComponent<ItemController>();
                temp.FindAttackTarget(teamA[i].gameObject, null);
                teamA[i].gameObject.GetComponent<ItemController>().FindAttackTarget(null, temp.gameObject);
            }
        }
        private void swap<T>(List<T> list, int index1, int index2) where T : ItemController
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
