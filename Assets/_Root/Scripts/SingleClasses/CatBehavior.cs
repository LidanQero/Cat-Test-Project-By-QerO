using System;
using System.Collections.Generic;
using UnityEngine;

public class CatBehavior
{
    private int _currentIdOfCatState;
    private readonly IDatabaseAccess _databaseAccess;

    private Structures.CatState _currentCatState;
    private Structures.CatReaction _currentCatReaction;
    private List<Structures.ActionWithCat> _currentActions;

    public CatBehavior(int catStateId, IDatabaseAccess newDatabaseAccess)
    {
        _databaseAccess = newDatabaseAccess;

        _currentIdOfCatState = TestCatState(catStateId);
        _currentCatState = _databaseAccess.GetCatState(_currentIdOfCatState);
        _currentActions = _databaseAccess.GetCatActions(_currentIdOfCatState);
    }

    public void ActionWithCat(int actionId)
    {
        SetActualCatReaction(actionId);
        ChangeCatState(actionId);
        ChangeCatActions();
    }

    private void SetActualCatReaction(int actionId)
    {
        _currentCatReaction = _databaseAccess.GetCurrentCatReaction(_currentIdOfCatState, actionId);
    }

    private void ChangeCatState(int actionId)
    {
        Structures.ActionWithCat actionWithCat = TrySetNewCurrentAction(actionId);
        
        switch (actionWithCat.resultOfactionWithCat)
        {
            case  Enums.ResultOfActionWithCat.StateUp:
                _currentIdOfCatState += 1;
                break;
            case  Enums.ResultOfActionWithCat.StateDoubleUp:
                _currentIdOfCatState += 2;
                break;
            case  Enums.ResultOfActionWithCat.StateDown:
                _currentIdOfCatState -=1;
                break;
            case  Enums.ResultOfActionWithCat.StateDoubleDown:
                _currentIdOfCatState -= 2;
                break;
            case Enums.ResultOfActionWithCat.StateNoChange:
                break;
            default:
                Debug.LogError("Missing action result type!");
                break;
        }
        
        _currentIdOfCatState = TestCatState(_currentIdOfCatState);
        _currentCatState = _databaseAccess.GetCatState(_currentIdOfCatState);
    }

    private void ChangeCatActions()
    {
        _currentActions = _databaseAccess.GetCatActions(_currentIdOfCatState);
    }

    private Structures.ActionWithCat TrySetNewCurrentAction(int actionId)
    {
        try
        {
            return _currentActions[actionId];
        }
        catch (Exception e)
        {
            Debug.LogError($"Incorrect action ID # {actionId}. Create empty action...");
            return new Structures.ActionWithCat();
        }
    }

    private int TestCatState(int testValue)
    {
        if (testValue < 0)
            return 0;

        int statesCount = _databaseAccess.GetStatesCount() - 1;
        if (testValue > statesCount)
            return statesCount;

        return testValue;
    }

    public List<Structures.ActionWithCat> GetCurrentActions() => _currentActions;
    public Structures.CatReaction GetCurrentCatReaction() => _currentCatReaction;
    public Structures.CatState GetCurrentCatState() => _currentCatState;
    public int GetCurrentIdOfCatState() => _currentIdOfCatState;
    public int GetStatesCount => _databaseAccess.GetStatesCount();
}