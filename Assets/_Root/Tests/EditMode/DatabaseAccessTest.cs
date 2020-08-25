using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class DatabaseAccessTest
{
    private IDatabaseAccess _databaseAccessInterface;
    
    [SetUp]
    public void Setup()
    {
        DatabaseAccess databaseAccess = new GameObject().AddComponent<DatabaseAccess>();
        databaseAccess.AddNewStates(NewStates());
        _databaseAccessInterface = databaseAccess.GetComponent<IDatabaseAccess>();
    }

    [TearDown]
    public void Teardown()
    {

    }

    [Test]
    public void TestActionList()
    {
        int randomState = Random.Range(0, 3);
        int randomAction = Random.Range(0, 3);
        
        Assert.AreEqual($"Action{randomAction} of State{randomState}", _databaseAccessInterface.GetCatActions(randomState)[randomAction].actionName);
    }

    [Test]
    public void TestReaction()
    {
        int randomState = Random.Range(0, 3);
        int randomAction = Random.Range(0, 3);
        
        Assert.AreEqual($"Reaction of Action{randomAction} of State{randomState}", _databaseAccessInterface.GetCatActions(randomState)[randomAction].catReaction.reactionName);
    }

    [Test]
    public void TestStatesCount()
    {
        Assert.AreEqual(3, _databaseAccessInterface.GetStatesCount());
    }

    private List<CatState> NewStates()
    {
        List<CatState> newStates = new List<CatState>();
        
        for (int i = 0; i < 3; i++)
        {
            CatState newState = ScriptableObject.CreateInstance<CatState>();
            newState.systemName = $"State{i}";
            newState.actionsInThisState = NewActions(i);
            newStates.Add(newState);
        }

        return newStates;
    }

    private List<ActionWithCat> NewActions(int stateNum)
    {
        List<ActionWithCat> newStates = new List<ActionWithCat>();
        
        for (int i = 0; i < 3; i++)
        {
            ActionWithCat newAction = ScriptableObject.CreateInstance<ActionWithCat>();
            newAction.systemName = $"Action{i} of State{stateNum}";
            newAction.catReaction = NewReaction(stateNum, i);
            newStates.Add(newAction);
        }

        return newStates;
    }

    private CatReaction NewReaction(int stateNum, int actionNum)
    {
        CatReaction newReaction = ScriptableObject.CreateInstance<CatReaction>();
        newReaction.systemName = $"Reaction of Action{actionNum} of State{stateNum}";
        return newReaction;
    }
}