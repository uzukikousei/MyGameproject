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
            // û�г�������
            if (player.HasKitchenObject())
            {
                // �������������Ŷ���
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // �������û�ж���

            }
        }
        else
        {
            // �г�������
            if(player.HasKitchenObject())
            {
                // ����������˶���
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //�������������,��ȷ�������ǲ�����
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    // ����õĲ����������������
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // ��̨����һ����
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            // ����޷���Ӿͻٵ�
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // �������û�ж���
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
   
}
