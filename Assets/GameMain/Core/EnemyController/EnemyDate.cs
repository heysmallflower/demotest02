using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Date", menuName = "Character Stats/EnemyDate")]
public class EnemyDate : ScriptableObject
{
    //状态属性
    [Header("属性状态")]
    //最大生命值
    public int maxHealth;
    //当前生命值
    public int currentHealth;
}
