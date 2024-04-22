using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;



    private void Start()
    {
        GameInput.instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStartChanged += KitchenGameManager_OnStartChanged;

        UpdateVisual();

        Show();
    }

    private void KitchenGameManager_OnStartChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Interact);
        keyInteractAlternateText.text = GameInput.instance.GetBingdingText(GameInput.Binding.InteractAlternate);
        keyPauseText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Pause);
        keyGamepadInteractText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Gamepad_Interact);
        keyGamepadInteractAlternateText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Gamepad_InteractAlternate);
        keyGamepadPauseText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Gamepad_Pause);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
