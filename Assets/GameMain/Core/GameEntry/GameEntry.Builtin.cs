using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
    public partial class GameEntry : MonoBehaviour
    {
        private static GameEntry instance;
        string path1 = "Enemy/Enemy1";
        string path2 = "Enemy/Enemy2";
        public static bool initEnd { get; private set; }
        private void Awake()
        {
            initEnd = false;
        }
        private void Start()
        {
            // ��ʼ���������
            InitBuiltinComponents();
            // ��ʼ���Զ������
            InitCustomComponents();
            initEnd = true;
        }
        public static GameEntry Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<GameEntry>();
                }
                return instance;
            }
        }
    }
}
