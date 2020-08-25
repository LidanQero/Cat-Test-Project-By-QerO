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
        Structures.ActionWithCat newAction;
        newAction.actionName = actionWithCat.systemName;
        newAction.actionDescription = actionWithCat.uiText;
        newAction.resultOfactionWithCat = actionWithCat.resultForState;
        newAction.catReaction = ScriptableObjectReactionToStructReaction(actionWithCat.catReaction);

        return newAction;
    }

    private Structures.CatReaction ScriptableObjectReactionToStructReaction(CatReaction catReaction)
    {
        Structures.CatReaction newReaction;
        newReaction.reactionName = catReaction.systemName;
        newReaction.reactionDescription = catReaction.uiText;

        return newReaction;
    }

    private Structures.CatState ScriptableObjectStateToStructState(CatState catState)
    {
        List<Structures.ActionWithCat> newActions = new List<Structures.ActionWithCat>();

        foreach (var action in catState.actionsInThisState)
        {
            newActions.Add(ScriptableObjectActionToStructAction(action));
        }

        Structures.CatState newState = new Structures.CatState
        {
            stateName = catState.systemName, stateDescription = catState.uiText, stateActions = newActions
        };

        return newState;
    }
}
