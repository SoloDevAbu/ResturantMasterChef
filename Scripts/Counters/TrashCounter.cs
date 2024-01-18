using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public event EventHandler OnPlayerTrashedObject;

    public override void Interact(Player player) {
        if(player.HasKitchenObejct()) {
            player.GetKitchenObject().DestroySelf();

            OnPlayerTrashedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
