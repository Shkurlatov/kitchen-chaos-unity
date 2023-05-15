using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

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
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(SoundEffectsClick);
        musicButton.onClick.AddListener(MusicClick);
        closeButton.onClick.AddListener(CloseClick);

        moveUpButton.onClick.AddListener(MoveUpClick);
        moveDownButton.onClick.AddListener(MoveDownClick);
        moveLeftButton.onClick.AddListener(MoveLeftClick);
        moveRightButton.onClick.AddListener(MoveRightClick);
        interactButton.onClick.AddListener(InteractClick);
        interactAlternateButton.onClick.AddListener(InteractAlternateClick);
        pauseButton.onClick.AddListener(PauseClick);
        gamepadInteractButton.onClick.AddListener(GamepadInteractClick);
        gamepadInteractAlternateButton.onClick.AddListener(GamepadInteractAlternateClick);
        gamepadPauseButton.onClick.AddListener(GamepadPauseClick);
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

    private void SoundEffectsClick()
    {
        SoundManager.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void MusicClick()
    {
        MusicManager.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void CloseClick()
    {
        Hide();
        onCloseButtonAction();
    }

    private void MoveUpClick()
    {
        RebindBinding(GameInput.Binding.Move_Up);
    }

    private void MoveDownClick()
    {
        RebindBinding(GameInput.Binding.Move_Down);
    }

    private void MoveLeftClick()
    {
        RebindBinding(GameInput.Binding.Move_Left);
    }

    private void MoveRightClick()
    {
        RebindBinding(GameInput.Binding.Move_Right);
    }

    private void InteractClick()
    {
        RebindBinding(GameInput.Binding.Interact);
    }

    private void InteractAlternateClick()
    {
        RebindBinding(GameInput.Binding.InteractAlternate);
    }

    private void PauseClick()
    {
        RebindBinding(GameInput.Binding.Pause);
    }
    
    private void GamepadInteractClick()
    {
        RebindBinding(GameInput.Binding.Gamepad_Interact);
    }

    private void GamepadInteractAlternateClick()
    {
        RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
    }

    private void GamepadPauseClick()
    {
        RebindBinding(GameInput.Binding.Gamepad_Pause);
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
