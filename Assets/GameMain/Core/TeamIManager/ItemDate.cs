using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Date", menuName = "Character Stats/ItemDate")]
public class ItemDate : ScriptableObject
{
    //״̬����
    [Header("����״̬")]
    //�������ֵ
    public int maxHealth;
    //��ǰ����ֵ
    public int currentHealth;
    //��������
    public int baseDefence;
    //������
    public int currentDefence;
    //�ٶ�
    public int speed;
    public int attackNum;
}
