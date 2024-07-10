using UniRx;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public IReadOnlyReactiveProperty<bool> HasChangedOpeningDialogue => _hasChangedOpeningDialogue;
    private readonly BoolReactiveProperty _hasChangedOpeningDialogue = new BoolReactiveProperty(false);

    public IReadOnlyReactiveProperty<bool> IsOpeningDialogue => _isOpeningDialogue;
    private readonly BoolReactiveProperty _isOpeningDialogue = new BoolReactiveProperty(true);

    private int _openingDialogueCount = 0;

    /// <summary>
    /// ダイアログの開閉があった際に呼ばれる
    /// </summary>
    /// <param name="isOpen">開いたか閉じたか</param>
    public void ControllDialogueLayer(bool isOpen)
    {
        if (isOpen)
        {
            _hasChangedOpeningDialogue.SetValueAndForceNotify(true);
            _openingDialogueCount++;
            if (_openingDialogueCount == 1)
            {
                _isOpeningDialogue.SetValueAndForceNotify(true);
                Time.timeScale = 0;
            }
        }
        else
        {
            _hasChangedOpeningDialogue.SetValueAndForceNotify(false);
            _openingDialogueCount--;
            if (_openingDialogueCount == 0)
            {
                _isOpeningDialogue.SetValueAndForceNotify(false);
                Time.timeScale = 1;
            }
        }

        Debug.Log("現在開いているダイアログの数は " +  _openingDialogueCount);
    }
}
