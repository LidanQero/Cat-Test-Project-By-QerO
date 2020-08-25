using System.Collections.Generic;

public interface IDatabaseAccess
{
    List<Structures.ActionWithCat> GetCatActions(int catStateId);
    Structures.CatReaction GetCurrentCatReaction(int catStateId, int actionIdWithCat);
    int GetStatesCount();
    Structures.CatState GetCatState(int stateId);
}
