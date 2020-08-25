using System.Collections.Generic;

public interface IUiLogic
{
    void InitUi();
    void ShowCurrentCatState(Structures.CatState catState, int catStateId, int catStateCount);
    void ShowCurrentActions(List<Structures.ActionWithCat> actionWithCat);
    void ShowCatReaction(Structures.CatReaction catReaction);

    event Delegates.DelegateWithOneInt ActionWithCatCase;
    event Delegates.StandardDelegate CatReactionComplete;
}
