using System;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour, IDatabaseAccess
{
    [SerializeField] private List<CatState> catStates = new List<CatState>();
    
    public void AddNewStates(List<CatState> newStates)
    {
        catStates.AddRange(newStates);
    }

    public List<Structures.ActionWithCat> GetCatActions(int catStateId)
    {
        try
        {
            return ScriptableObjectStateToStructState(catStates[catStateId]).stateActions;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error id action! Create new actions...");
            return new List<Structures.ActionWithCat>();
        }
    }

    public Structures.CatReaction GetCurrentCatReaction(int catStateId, int actionIdWithCat)
    {
        Structures.CatState theCatState = GetCatState(catStateId);

        try
        {
            return theCatState.stateActions[actionIdWithCat].catReaction;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error id action or missing cat reaction! Create new reaction...");
            return new Structures.CatReaction();
        }
    }
    
    public int GetStatesCount() => catStates.Count;

    public Structures.CatState GetCatState(int stateId)
    {
        try
        {
            return ScriptableObjectStateToStructState(catStates[stateId]);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error cat state id! Create new state...");
            return new Structures.CatState();
        }
    }

    private Structures.ActionWithCat ScriptableObjectActionToStructAction(ActionWithCat actionWithCat)
    {
        return new Structures.ActionWithCat
        {
            actionName =  actionWithCat.systemName,
            actionDescription = actionWithCat.uiText,
            resultOfactionWithCat = actionWithCat.resultForState,
            catReaction = ScriptableObjectReactionToStructReaction(actionWithCat.catReaction)
        };
    }

    private Structures.CatReaction ScriptableObjectReactionToStructReaction(CatReaction catReaction)
    {
        return new Structures.CatReaction
        {
            reactionName = catReaction.systemName,
            reactionDescription = catReaction.uiText
        };
    }

    private Structures.CatState ScriptableObjectStateToStructState(CatState catState)
    {
        List<Structures.ActionWithCat> newActions = new List<Structures.ActionWithCat>();

        foreach (var action in catState.actionsInThisState)
        {
            newActions.Add(ScriptableObjectActionToStructAction(action));
        }

        return new Structures.CatState
        {
            stateDescription = catState.uiText, stateActions = newActions
        };
    }
}
