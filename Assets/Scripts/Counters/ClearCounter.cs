using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; 

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // 没有厨房对象
            if (player.HasKitchenObject())
            {
                // 如果玩家手里拿着东西
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // 玩家手里没有东西

            }
        }
        else
        {
            // 有厨房对象
            if(player.HasKitchenObject())
            {
                // 玩家手里拿了东西
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //玩家手里拿了碗,先确定手里是不是碗
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    // 玩家拿的不是碗而是其他东西
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // 柜台上有一个碗
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            // 如果无法添加就毁掉
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // 玩家手里没有东西
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
   
}
