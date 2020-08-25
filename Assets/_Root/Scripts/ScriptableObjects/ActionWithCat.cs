using UnityEngine;

[CreateAssetMenu(fileName = "New ActionWithCat", menuName = "Action With Cat", order = 53)]
public class ActionWithCat : ScriptableObject
{
    public string systemName = "";
    public string uiText = "";
    public Enums.ResultOfActionWithCat resultForState = Enums.ResultOfActionWithCat.StateNoChange;
    public CatReaction catReaction = null;
}
