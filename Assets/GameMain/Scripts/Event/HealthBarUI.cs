using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    //��ȡui���
    public GameObject healthUIPrefab;
    public GameObject attackcdUIPrefab;
    //ui�������ɵ�λ��
    public Transform barPoint;
    public Transform cdPoint;
    //��ʾui�Ƿ�Ϊ���ÿɼ�
    public bool alwaysVisible;
    //����������õģ���ʧʱ��
    public float visibleTime;
    //ui��ʾʣ��ʱ��
    float timeLeft;
    //����ui�е�ͼƬ����
    Image healthSlider;
    Image attackSlider;
    //��ȡui���ɵ�λ�ã���������λ�ñ���һ��
    Transform UIBar;
    Transform UICd;
    //��ȡ���λ�ñ�֤ui�Ӳ�ͬ�Ƕȹۿ������仯
    Transform cam;
    //������Դ���ݶ���
    ItemDate currentStats;
    ItemController currentController;
    int curh;
    private void Awake()
    {
        currentController = GetComponent<ItemController>();
        //��ȡ��Դ�������
        currentStats = currentController.Date;
        //���Ĺ��������˺�ui�仯�ķ���
        currentController.UpdateHealthBarOnAttack += UpdateHealthBar;
        currentController.UpdateCdBarOnAttack += UpdateCDBar;
        currentController.AddHealthWidth += AddHeaBarWi;
        curh = 10;
    }
    private void OnEnable()
    {
        //��ȡ�������λ��
        cam = Camera.main.transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                //����¡������uiλ�ø���UIBar
                UIBar = Instantiate(healthUIPrefab, canvas.transform).transform;
                UICd = Instantiate(attackcdUIPrefab, canvas.transform).transform;
                //��ȡ��¡ui����Ӷ����ٻ�ȡ�Ӷ������Imae���
                healthSlider = UIBar.GetChild(0).GetComponent<Image>();
                attackSlider = UICd.GetChild(0).GetComponent<Image>();
                //�����Ƿ�Ϊ���ÿɼ�
                UIBar.gameObject.SetActive(alwaysVisible);
                UICd.gameObject.SetActive(alwaysVisible);

            }
        }
    }
    /// <summary>
    /// Ѫ��ui�仯
    /// </summary>
    /// <param name="currentHeaith"></param>
    /// <param name="maxHealth"></param>
    private void UpdateHealthBar(int currentHeaith, int maxHealth)
    {
        //Ѫ��Ϊ0
        if (currentHeaith <= 0)
        {
            curh = currentHeaith;
            //ui����
            Destroy(UIBar.gameObject);
        }
        //�ܵ�����uiǿ�ƿɼ�
        UIBar.gameObject.SetActive(true);
        //��ʼ����ʾʱ��
        timeLeft = visibleTime;
        //����ÿ�ε�Ѫui�ı仯��ֵ
        float sliderPercent = (float)currentHeaith / maxHealth;
        //����uiѪ���仯
        healthSlider.fillAmount = sliderPercent;
    }
    private void UpdateCDBar(float current, float max)
    {
        if (curh <= 0)
        {
            //ui����
            UICd.gameObject.SetActive(false);
        }
        //Ѫ��Ϊ0
        //����ÿ�ε�Ѫui�ı仯��ֵ
        float sliderPercent = current / max;
        //����uiѪ���仯
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
            //��֤Ѫ��ui�ڵ����Ϸ���λ��
            UIBar.position = barPoint.position;
            UICd.position = cdPoint.position;
            //��֤Ѫ���ķ��������������ĵ�λ��
            UIBar.forward = -cam.forward;
            UICd.forward = -cam.forward;
            //�ж���ʾʣ��ʱ���Ƿ�С�ڵ���0����ui״̬��������ɼ�
            if (timeLeft <= 0 && !alwaysVisible)
            {
                //�ر�ui
                UIBar.gameObject.SetActive(false);
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }
        }
    }
}
