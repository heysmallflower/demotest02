using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Date", menuName = "Character Stats/ItemDate")]
public class ItemDate : ScriptableObject
{
    //状态属性
    [Header("属性状态")]
    //最大生命值
    public int maxHealth;
    //当前生命值
    public int currentHealth;
    //基础防御
    public int baseDefence;
    //最大防御
    public int currentDefence;
    //速度
    public int speed;
    public int attackNum;
}
