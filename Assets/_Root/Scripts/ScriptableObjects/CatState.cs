using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CatState", menuName = "Cat State", order = 51)]
public class CatState : ScriptableObject
{
    public string systemName = "";
    public string uiText = "";
    public List<ActionWithCat> actionsInThisState = null;
}
