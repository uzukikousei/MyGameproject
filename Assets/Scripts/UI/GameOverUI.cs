using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;


    private void Start()
    {
        KitchenGameManager.Instance.OnStartChanged += KitchenGameObject_OnStartChanged;

        Hide();
    }

    private void KitchenGameObject_OnStartChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();

            recipeDeliveredText.text = DeliveryManager.instance.GetSuccessfulRecipeAmount().ToString();
        }
        else
        {
            Hide();
        }
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
