//using GameFramework;
//using GameFramework.ObjectPool;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace GameMain
//{
//    public class Sweet:MonoBehaviour
//    {
//        void Start()
//        {

//        }

//        // Update is called once per frame
//        void Update()
//        {

//        }
//        private void OnTriggerStay(Collider other)
//        {
//            if (other.gameObject.CompareTag("Player") && GameEntry.playController.CurrentHealth != 100)
//            {
//                GameEntry.playController.Add(2);
//                Destroy(gameObject,2f);
//            }
//        }
//    }
//}
