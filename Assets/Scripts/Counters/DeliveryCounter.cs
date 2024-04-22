using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{


    public static DeliveryCounter Instance { get; private set; }


    private void Awake()
    {
        Instance = this; 
    }
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // 如果是一个碗就摧毁
                DeliveryManager.instance.DeliverRecipe(plateKitchenObject);

                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
