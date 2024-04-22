using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI instance {  get; private set; }



    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private Text soundEffectsText;
    [SerializeField] private Text musicText;
    [SerializeField] private Text moveUpText;
    [SerializeField] private Text moveDownText;
    [SerializeField] private Text moveLeftText;
    [SerializeField] private Text moveRightText;
    [SerializeField] private Text interactText;
    [SerializeField] private Text interactAlternateText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Text gamepadInteractText;
    [SerializeField] private Text gamepadInteractAlternateText;
    [SerializeField] private Text gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransfrom;


    private Action onCloseButtonAction;
    
    private void Awake()
    {
        instance = this;


        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });
        moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        gamepadInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        gamepadInteractAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });
        gamepadPauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        UpdateVisual();

        HidePressToRebindKey();
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "ÒôÐ§£º" + Mathf.Round(SoundManager.instance.GetVolume() * 10f);
        musicText.text = "ÒôÀÖ£º" + Mathf.Round(MusicManager.instance.GetVolume() * 10f);


        moveUpText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.instance.GetBingdingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.instance.GetBingdingText(GameInput.Binding.Gamepad_Pause);
    }
    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransfrom.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransfrom.gameObject.SetActive(false);
    }
    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.instance.RebindBinding(binding,() =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        }
        );
    }
}
