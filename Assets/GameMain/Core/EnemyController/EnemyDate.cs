using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Date", menuName = "Character Stats/EnemyDate")]
public class EnemyDate : ScriptableObject
{
    //״̬����
    [Header("����״̬")]
    //�������ֵ
    public int maxHealth;
    //��ǰ����ֵ
    public int currentHealth;
}
