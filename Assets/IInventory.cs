using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    bool AddItem(Item item, int num);
    bool RemoveItem(Item item, int num);
    bool HasItem(Item item, int num);
}

public enum Item { Shoes = 0, FireGun = 1, PoisonGun = 2, Shield = 3, Mine = 4 };