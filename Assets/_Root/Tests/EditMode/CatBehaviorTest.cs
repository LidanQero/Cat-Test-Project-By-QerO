using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CatBehaviorTest
{
    private CatBehavior _catBehavior;
    private TestDatabase _testDatabase;

    [SetUp]
    public void Setup()
    {
        _testDatabase = new TestDatabase();
        _catBehavior = new CatBehavior(0, _testDatabase);
    }

    [TearDown]
    public void Teardown()
    {

    }

    [Test]
    public void TestCatStateId()
    {
        int stateOutOfRange = 100;
        _catBehavior = new CatBehavior(stateOutOfRange, new TestDatabase());
        Assert.AreNotEqual(stateOutOfRange, _catBehavior.GetCurrentIdOfCatState());
    }
    
    [Test]
    public void TestCatState()
    {
        Assert.AreEqual("state0", _catBehavior.GetCurrentCatState().stateName);
    }

    [Test]
    public void TestCatStateAfterUpAction()
    {
        _catBehavior.ActionWithCat(0);
        Assert.AreEqual("state1", _catBehavior.GetCurrentCatState().stateName);
    }

    [Test]
    public void TestCatStateAfterDoubleUpAction()
    {
        _catBehavior.ActionWithCat(0);
        _catBehavior.ActionWithCat(0);
        Assert.AreEqual("state1", _catBehavior.GetCurrentCatState().stateName);
    }
    
    [Test]
    public void TestCatStateAfterDownAction()
    {
        _catBehavior.ActionWithCat(1);
        Assert.AreEqual("state0", _catBehavior.GetCurrentCatState().stateName);
    }
    
    [Test]
    public void TestCatStateAfterUpAndDownActions()
    {
        _catBehavior.ActionWithCat(0);
        _catBehavior.ActionWithCat(1);
        Assert.AreEqual("state0", _catBehavior.GetCurrentCatState().stateName);
    }

    [Test]
    public void TestCatReactionAfterAction()
    {
        _catBehavior.ActionWithCat(1);
        Assert.AreEqual(_catBehavior.GetCurrentCatReaction().reactionName, "reaction1");
    }

    [Test]
    public void TestCatReactionAfterDoubleAction()
    {
        _catBehavior.ActionWithCat(1);
        _catBehavior.ActionWithCat(0);
        Assert.AreEqual(_catBehavior.GetCurrentCatReaction().reactionName, "reaction0");
    }
    
    [Test]
    public void TestCatActionsList()
    {
        Assert.AreEqual(_testDatabase.GetCatActions(0), _catBehavior.GetCurrentActions());
    }

    private class TestDatabase : IDatabaseAccess
    {
        private List<Structures.ActionWithCat> _actionsWithCats = new List<Structures.ActionWithCat>();
        private List<Structures.CatReaction> _catReactions = new List<Structures.CatReaction>();
        private List<Structures.CatState> _catStates = new List<Structures.CatState>();

        public TestDatabase()
        {
            Structures.CatState state0 = new Structures.CatState
            {
                stateName = "state0"
            };
            Structures.CatState state1 = new Structures.CatState
            {
                stateName = "state1"
            };

            _catStates.Add(state0);
            _catStates.Add(state1);

            Structures.CatReaction reaction0 = new Structures.CatReaction
            {
                reactionName = "reaction0"
            };
            Structures.CatReaction reaction1 = new Structures.CatReaction
            {
                reactionName = "reaction1"
            };
            
            _catReactions.Add(reaction0);
            _catReactions.Add(reaction1);

            Structures.ActionWithCat action0 = new Structures.ActionWithCat
            {
                resultOfactionWithCat = Enums.ResultOfActionWithCat.StateUp
            };
            Structures.ActionWithCat action1 = new Structures.ActionWithCat
            {
                resultOfactionWithCat = Enums.ResultOfActionWithCat.StateDown
            };

            _actionsWithCats.Add(action0);
            _actionsWithCats.Add(action1);
        }

        public List<Structures.ActionWithCat> GetCatActions(int catState) => _actionsWithCats;
        public Structures.CatReaction GetCurrentCatReaction(int catStateId, int actionIdWithCat) => _catReactions[actionIdWithCat];
        
        public int GetStatesCount()
        {
            return _catStates.Count;
        }
        
        public Structures.CatState GetCatState(int stateId)
        {
            try
            {
                return _catStates[stateId];
            }
            catch (Exception e)
            {
                Debug.LogError($"Invalid cat state ID!");
                return _catStates[0];
            }
        }
    }
}
