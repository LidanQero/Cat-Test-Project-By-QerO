using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiLogic : MonoBehaviour, IUiLogic
{
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private TMP_Text outputText = null;
    [SerializeField] private Slider catStateSlider = null;

    private List<InputButton> _inputButtons = new List<InputButton>();
    private bool _waitReactionEnd;

    public void InitUi()
    {
        if (MissingElements())
        {
            Debug.LogError("Missing output or buttons!");
            return;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            _inputButtons.Add(new InputButton
            {
                button = buttons[i], Text = buttons[i].GetComponentInChildren<TMP_Text>()
            });

            var i1 = i;
            _inputButtons[i].button.onClick.AddListener(delegate { UseButton(i1); });
        }
        
        HideAllButtons();
    }

    private bool MissingElements()
    {
        return (outputText == null) || (buttons.Count == 0);
    }

    public void ShowCurrentActions(List<Structures.ActionWithCat> actionsWithCat)
    {
        for (int i = 0; i < _inputButtons.Count; i++)
        {
            if (i < actionsWithCat.Count)
            {
                if (_inputButtons[i].button)
                    _inputButtons[i].button.gameObject.SetActive(true);
                if (_inputButtons[i].Text)
                    _inputButtons[i].Text.text = actionsWithCat[i].actionDescription;
            }
        }
    }

    public void ShowCatReaction(Structures.CatReaction catReaction)
    {
        _waitReactionEnd = true;

        outputText.text = catReaction.reactionDescription;

        HideAllButtons();
        
        if (_inputButtons[0] == null)
            return;
        
        _inputButtons[0].button.gameObject.SetActive(true);
        _inputButtons[0].Text.text = "Далее";
    }

    public void ShowCurrentCatState(Structures.CatState catState, int catStateId, int catStateCount)
    {
        _waitReactionEnd = false;

        outputText.text = catState.stateDescription;

        if (catStateSlider)
        {
            catStateSlider.maxValue = catStateCount - 1;
            catStateSlider.value = catStateId;
        }
    }

    private void HideAllButtons()
    {
        foreach (var inputButton in _inputButtons)
        {
            inputButton.button.gameObject.SetActive(false);
        }
    }

    private void UseButton(int buttonId)
    {
        if (_waitReactionEnd)
        {
            CatReactionComplete?.Invoke();
        }
        else
        {
            ActionWithCatCase?.Invoke(buttonId);
        }
    }

    public event Delegates.DelegateWithOneInt ActionWithCatCase;
    public event Delegates.StandardDelegate CatReactionComplete;
}
