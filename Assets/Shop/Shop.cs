using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour, IInventory
{
    private Controller player;
    int[] shopItems;
    public TextMeshProUGUI[] AmountsShop;
    public TextMeshProUGUI[] AmountsPlayer;
    public TextMeshProUGUI Money;

    void Start()
    {
        player = Controller.thePlayer;
        player.money = 100; // FIXME this is for debug!!!!
        Money.text = player.money + "$";
        shopItems = new int[5];
        for (int i = 0; i < 5; i++)
        {
            shopItems[i] = 10;
            AmountsShop[i].text = "10";
            AmountsPlayer[i].text = player.items[i].ToString();
        }
    }


    public bool AddItem(Item item, int num)
    {
        shopItems[(int)item] += num;
        return true;
    }

    public bool HasItem(Item item, int num)
    {
        return shopItems[(int)item] >= num;
    }

    public bool RemoveItem(Item item, int num)
    {
        if (shopItems[(int)item] >= num)
        {
            shopItems[(int)item] -= num;
            return true;
        }
        return false;
    }


    public void BuyShoes()
    {
        if (player.money < 5)
        {
            Money.text = player.money + "$ Not enough money!";
            return;
        }
        if (shopItems[0] < 1)
        {
            Money.text = player.money + "$ Not enough shoes to buy!";
            return;
        }
        player.money -= 5;
        Money.text = player.money + "$";
        shopItems[0]--;
        player.items[0]++;
        AmountsShop[0].text = shopItems[0].ToString();
        AmountsPlayer[0].text = player.items[0].ToString();
    }

    public void SellShoes()
    {
        if (player.items[0] < 1)
        {
            Money.text = player.money + "$ Not enough shoes to sell!";
            return;
        }
        player.money += 5;
        Money.text = player.money + "$";
        shopItems[0]++;
        player.items[0]--;
        AmountsShop[0].text = shopItems[0].ToString();
        AmountsPlayer[0].text = player.items[0].ToString();
    }

    public void BuyShield()
    {
        if (player.money < 15)
        {
            Money.text = player.money + "$ Not enough money!";
            return;
        }
        if (shopItems[3] < 1)
        {
            Money.text = player.money + "$ Not enough shields to buy!";
            return;
        }
        player.money -= 15;
        Money.text = player.money + "$";
        shopItems[3]--;
        player.items[3]++;
        AmountsShop[3].text = shopItems[3].ToString();
        AmountsPlayer[3].text = player.items[3].ToString();
    }

    public void SellShield()
    {
        if (player.items[3] < 1)
        {
            Money.text = player.money + "$ Not enough shields to sell!";
            return;
        }
        player.money += 15;
        Money.text = player.money + "$";
        shopItems[3]++;
        player.items[3]--;
        AmountsShop[3].text = shopItems[4].ToString();
        AmountsPlayer[3].text = player.items[4].ToString();
    }

    public void BuyFire()
    {
        if (player.money < 15)
        {
            Money.text = player.money + "$ Not enough money!";
            return;
        }
        if (shopItems[1] < 1)
        {
            Money.text = player.money + "$ Not enough fire guns to buy!";
            return;
        }
        player.money -= 15;
        Money.text = player.money + "$";
        shopItems[1]--;
        player.items[1]++;
        AmountsShop[1].text = shopItems[1].ToString();
        AmountsPlayer[1].text = player.items[1].ToString();
    }

    public void SellFire()
    {
        if (player.items[1] < 1)
        {
            Money.text = player.money + "$ Not enough fire guns to sell!";
            return;
        }
        player.money += 15;
        Money.text = player.money + "$";
        shopItems[1]++;
        player.items[1]--;
        AmountsShop[1].text = shopItems[1].ToString();
        AmountsPlayer[1].text = player.items[1].ToString();
    }

    public void BuyPoison()
    {
        if (player.money < 25)
        {
            Money.text = player.money + "$ Not enough money!";
            return;
        }
        if (shopItems[2] < 1)
        {
            Money.text = player.money + "$ Not enough poison guns to buy!";
            return;
        }
        player.money -= 25;
        Money.text = player.money + "$";
        shopItems[2]--;
        player.items[2]++;
        AmountsShop[2].text = shopItems[2].ToString();
        AmountsPlayer[2].text = player.items[2].ToString();
    }

    public void SellPoison()
    {
        if (player.items[2] < 1)
        {
            Money.text = player.money + "$ Not enough poison guns to sell!";
            return;
        }
        player.money += 25;
        Money.text = player.money + "$";
        shopItems[2]++;
        player.items[2]--;
        AmountsShop[2].text = shopItems[2].ToString();
        AmountsPlayer[2].text = player.items[2].ToString();
    }

    public void BuyMine()
    {
        if (player.money < 6)
        {
            Money.text = player.money + "$ Not enough money!";
            return;
        }
        if (shopItems[4] < 1)
        {
            Money.text = player.money + "$ Not enough bombs to buy!";
            return;
        }
        player.money -= 6;
        Money.text = player.money + "$";
        shopItems[4]--;
        player.items[4]++;
        AmountsShop[4].text = shopItems[4].ToString();
        AmountsPlayer[4].text = player.items[4].ToString();
    }

    public void SellMine()
    {
        if (player.items[4] < 1)
        {
            Money.text = player.money + "$ Not enough bombs to sell!";
            return;
        }
        player.money += 6;
        Money.text = player.money + "$";
        shopItems[4]++;
        player.items[4]--;
        AmountsShop[4].text = shopItems[4].ToString();
        AmountsPlayer[4].text = player.items[4].ToString();
    }

    public void ExitShop()
    {
        player.ExitShop();
    }

}