using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayHeath : MonoBehaviour
    {
        //血量ui组件
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
            //计算血条ui显示进度数值
            float sliderPercent = GameEntry.playController.CurrentHealth / GameEntry.playController.MaxHealth;
            //血条ui填充条变化
            healthSlider.fillAmount = sliderPercent;
        }
    }
}
