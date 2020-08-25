using System;
using System.Collections.Generic;
using UnityEngine;

public class CatBehavior
{
    private int _currentIdOfCatState;
    private IDatabaseAccess _databaseAccess;

    private Structures.CatState _currentCatState;
    private Structures.CatReaction _currentCatReaction;
    private List<Structures.ActionWithCat> _currentActions = new List<Structures.ActionWithCat>();

    public CatBehavior(int catStateId, IDatabaseAccess newDatabaseAccess)
    {
        _databaseAccess = newDatabaseAccess;

        _currentIdOfCatState = TestCatState(catStateId);
        _currentCatState = _databaseAccess.GetCatState(_currentIdOfCatState);
        _currentActions = _databaseAccess.GetCatActions(_currentIdOfCatState);
    }

    public void ActionWithCat(int actionId)
    {
        Structures.ActionWithCat actionWithCat = TrySetNewCurrentAction(actionId);

        _currentCatReaction = _databaseAccess.GetCurrentCatReaction(_currentIdOfCatState, actionId);
        
        if (actionWithCat.resultOfactionWithCat == Enums.ResultOfActionWithCat.StateUp)
            _currentIdOfCatState++;
        else if (actionWithCat.resultOfactionWithCat == Enums.ResultOfActionWithCat.StateDown)
            _currentIdOfCatState--;
        else if (actionWithCat.resultOfactionWithCat == Enums.ResultOfActionWithCat.StateDoubleDown)
            _currentIdOfCatState -= 2;
        
        _currentIdOfCatState = TestCatState(_currentIdOfCatState);
        _currentCatState = _databaseAccess.GetCatState(_currentIdOfCatState);
        
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

        if (testValue > _databaseAccess.GetStatesCount() - 1)
            return _databaseAccess.GetStatesCount() - 1;

        return testValue;
    }

    public List<Structures.ActionWithCat> GetCurrentActions() => _currentActions;
    public Structures.CatReaction GetCurrentCatReaction() => _currentCatReaction;
    public Structures.CatState GetCurrentCatState() => _currentCatState;
    public int GetCurrentIdOfCatState() => _currentIdOfCatState;
    public int GetStatesCount => _databaseAccess.GetStatesCount();
}