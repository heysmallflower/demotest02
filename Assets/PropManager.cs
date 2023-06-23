using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Random = UnityEngine.Random;

namespace GameMain
{
    public class PropManager : GameFrameworkComponent
    {
        const string PathNameRed = "SweetRed";
        const string PathNameGreen = "SweetGreen";
        protected override void Awake()
        {
            base.Awake();
            InitProp();
        }

        private void InitProp()
        {
            for (int i = 0; i < 30; i++)
            {
                float rangx = Random.Range(transform.position.x - 170 - i, transform.position.x + 170 + i);
                float rangz = Random.Range(transform.position.z - 170 - i, transform.position.z + 170 + i);
                if (i%2 == 0)
                {
                    LoadEnemy(PathNameRed, new Vector3(rangx, 0.3f, rangz)).transform.SetParent(transform,true);
                }
                else
                {
                    LoadEnemy(PathNameGreen, new Vector3(rangx, 0.3f, rangz)).transform.SetParent(transform, true);
                }
            }
        }

        public GameObject LoadEnemy(string path,Vector3 pos)
        {
            GameObject gameObject = Resources.Load<GameObject>(path);
            return Instantiate(gameObject, pos, Quaternion.identity);
        }
    }
}
