using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayHeath : MonoBehaviour
    {
        //Ѫ��ui���
        Image healthSlider;
        private void Awake()
        {
            healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        }
        private void Update()
        {
            UpdateHealth();
        }
        void UpdateHealth()
        {
            //����Ѫ��ui��ʾ������ֵ
            float sliderPercent = GameEntry.playController.CurrentHealth / GameEntry.playController.MaxHealth;
            //Ѫ��ui������仯
            healthSlider.fillAmount = sliderPercent;
        }
    }
}
