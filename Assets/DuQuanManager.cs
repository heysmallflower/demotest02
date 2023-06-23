using GameMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class DuQuanManager : GameFrameworkComponent
    {
        public float Length
        {
            get;
            private set;
        }
        public float times;
        float time;
        float suoduTime = 5f;
        Coroutine coroutine;
        // Update is called once per frame
        protected override void Awake()
        {
            base.Awake();
            time = times;
            Vector3 pos = GetComponent<Renderer>().bounds.size;
            Length = pos.x / 2;
        }
        void Update()
        {
            Vector3 pos = GetComponent<Renderer>().bounds.size;
            Length = pos.x / 2;
            time = time - Time.deltaTime;
            if (time <= 0)
            {
                time = times + 5;
                GameEntry.Instance.StartCoroutine(SuoDu(50));
            }
        }
        IEnumerator SuoDu(float size)
        {
            for (int i = 0; i < size; i++)
            {
                yield return null;
                transform.SetLocalScaleX(transform.localScale.x - 1);
                transform.SetLocalScaleY(transform.localScale.y - 1);
                transform.SetLocalScaleZ(transform.localScale.z - 1);
            }
            yield return null;
        }
    }
}
