using System.Collections.Generic;

public class Structures
{
    public struct ActionWithCat
    {
        public string actionName;
        public string actionDescription;
        public Enums.ResultOfActionWithCat resultOfactionWithCat;
        public CatReaction catReaction;
    }
    
    public struct CatReaction
    {
        public string reactionName;
        public string reactionDescription;
    }
    
    public struct CatState
    {
        public string stateName;
        public string stateDescription;
        public List<ActionWithCat> stateActions;
    }
}