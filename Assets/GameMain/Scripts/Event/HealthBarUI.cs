using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    //获取ui组件
    public GameObject healthUIPrefab;
    public GameObject attackcdUIPrefab;
    //ui最终生成的位置
    public Transform barPoint;
    public Transform cdPoint;
    //表示ui是否为长久可见
    public bool alwaysVisible;
    //如果不是永久的，消失时间
    public float visibleTime;
    //ui显示剩余时间
    float timeLeft;
    //访问ui中的图片对象
    Image healthSlider;
    Image attackSlider;
    //获取ui生成的位置，来与最终位置保持一致
    Transform UIBar;
    Transform UICd;
    //获取相机位置保证ui从不同角度观看发生变化
    Transform cam;
    //创建资源数据对象
    ItemDate currentStats;
    ItemController currentController;
    int curh;
    private void Awake()
    {
        currentController = GetComponent<ItemController>();
        //获取资源数据组件
        currentStats = currentController.Date;
        //订阅攻击产生伤害ui变化的方法
        currentController.UpdateHealthBarOnAttack += UpdateHealthBar;
        currentController.UpdateCdBarOnAttack += UpdateCDBar;
        currentController.AddHealthWidth += AddHeaBarWi;
        curh = 10;
    }
    private void OnEnable()
    {
        //获取主相机的位置
        cam = Camera.main.transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                //将克隆出来的ui位置给予UIBar
                UIBar = Instantiate(healthUIPrefab, canvas.transform).transform;
                UICd = Instantiate(attackcdUIPrefab, canvas.transform).transform;
                //获取克隆ui里的子对象，再获取子对象里的Imae组件
                healthSlider = UIBar.GetChild(0).GetComponent<Image>();
                attackSlider = UICd.GetChild(0).GetComponent<Image>();
                //设置是否为永久可见
                UIBar.gameObject.SetActive(alwaysVisible);
                UICd.gameObject.SetActive(alwaysVisible);

            }
        }
    }
    /// <summary>
    /// 血条ui变化
    /// </summary>
    /// <param name="currentHeaith"></param>
    /// <param name="maxHealth"></param>
    private void UpdateHealthBar(int currentHeaith, int maxHealth)
    {
        //血量为0
        if (currentHeaith <= 0)
        {
            curh = currentHeaith;
            //ui销毁
            Destroy(UIBar.gameObject);
        }
        //受到攻击ui强制可见
        UIBar.gameObject.SetActive(true);
        //初始化显示时间
        timeLeft = visibleTime;
        //计算每次掉血ui的变化的值
        float sliderPercent = (float)currentHeaith / maxHealth;
        //更改ui血条变化
        healthSlider.fillAmount = sliderPercent;
    }
    private void UpdateCDBar(float current, float max)
    {
        if (curh <= 0)
        {
            //ui销毁
            UICd.gameObject.SetActive(false);
        }
        //血量为0
        //计算每次掉血ui的变化的值
        float sliderPercent = current / max;
        //更改ui血条变化
        attackSlider.fillAmount = sliderPercent;
    }
    private void AddHeaBarWi(float add)
    {
        Vector2 vector = UIBar.gameObject.GetComponent<RectTransform>().sizeDelta;
        Vector2 newSize = vector + new Vector2(add, 0);
        UIBar.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = newSize;
        UIBar.gameObject.GetComponent<RectTransform>().sizeDelta = newSize;
    }

    private void LateUpdate()
    {
        if (UIBar != null)
        {
            //保证血条ui在敌人上方的位置
            UIBar.position = barPoint.position;
            UICd.position = cdPoint.position;
            //保证血条的方向是摄像机反向的的位置
            UIBar.forward = -cam.forward;
            UICd.forward = -cam.forward;
            //判断显示剩余时间是否小于等于0并且ui状态不是永结可见
            if (timeLeft <= 0 && !alwaysVisible)
            {
                //关闭ui
                UIBar.gameObject.SetActive(false);
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }
        }
    }
}
