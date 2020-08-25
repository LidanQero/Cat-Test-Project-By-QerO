using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int defaultCateState = 0;
    [SerializeField] private GameObject uiLogic = null;
    [SerializeField] private GameObject databaseAcсess = null;
    
    private IUiLogic _uiLogic;
    private CatBehavior _catBehavior;
    
    void Start()
    {
        if (UiLogicOrDatabaseAccessDoNotExist())
            return;
        
        SetupGame();
    }

    private bool UiLogicOrDatabaseAccessDoNotExist()
    {
        bool value = false;
        
        if (uiLogic == null)
        {
            value = true;
            Debug.LogError($"UiLogic does not exist!");
        }
        
        if (databaseAcсess == null)
        {
            value = true;
            Debug.LogError($"DatabaseAccess does not exist!");
        }

        return value;
    }

    private void SetupGame()
    {
        IDatabaseAccess newDatabaseAccess = databaseAcсess.GetComponent<IDatabaseAccess>();

        _catBehavior = new CatBehavior(defaultCateState, newDatabaseAccess);
        
        _uiLogic = uiLogic.GetComponent<IUiLogic>();
        _uiLogic.InitUi();
        
        _uiLogic.ActionWithCatCase += ActionWithCat;
        _uiLogic.CatReactionComplete += ShowCurrentCatState;
        
        ShowCurrentCatState();
    }
    
    private void ActionWithCat(int actionId)
    {
        _catBehavior.ActionWithCat(actionId);
        _uiLogic.ShowCatReaction(_catBehavior.GetCurrentCatReaction());
    }

    private void ShowCurrentCatState()
    {
        _uiLogic.ShowCurrentCatState(_catBehavior.GetCurrentCatState(), _catBehavior.GetCurrentIdOfCatState(), _catBehavior.GetStatesCount);
        _uiLogic.ShowCurrentActions(_catBehavior.GetCurrentActions());
    }

    private void OnValidate()
    {
        if (uiLogic && uiLogic.GetComponent<IUiLogic>() == null)
        {
            uiLogic = null;
            Debug.LogError($"Cant find UiLogic interface!");
        }

        if (databaseAcсess && databaseAcсess.GetComponent<IDatabaseAccess>() == null)
        {
            databaseAcсess = null;
            Debug.LogError($"Cant find DatabaseAccess interface!");
        }
    }
}
