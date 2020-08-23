using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour, IInventory
{
    public Controller player;
    public Image[] Items;
    public TextMeshProUGUI[] Amounts;
    Color32 transparent = new Color32(0, 0, 0, 0);
    Color32 visible = new Color32(255, 255, 255, 255);

    public bool AddItem(Item item, int num)
    {
        int pos = (int)item;
        player.items[pos] += num;
        Amounts[pos].text = player.items[pos].ToString();
        if (player.items[pos] == 0)
            Items[pos].color = transparent;
        else
            Items[pos].color = visible;
        return true;




    }

    public bool HasItem(Item item, int num)
    {
        int pos = (int)item;
        return player.items[pos] >= num;
    }

    public bool RemoveItem(Item item, int num)
    {
        int pos = (int)item;
        if (player.items[pos] < num) return false;
        player.items[pos] -= num;
        Amounts[pos].text = player.items[pos].ToString();
        if (player.items[pos] == 0)
            Items[pos].color = transparent;
        else
            Items[pos].color = visible;
        return true;

    }

    private void Start()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Amounts[i].text = player.items[i].ToString();
            if (player.items[i] == 0)
                Items[i].color = transparent;
            else
                Items[i].color = visible;
        }
    }

    public void SetItems(Controller p)
    {
        player = p;

        if (Items.Length != 5) Items = new Image[5];
        if (Amounts.Length != 5) Amounts = new TextMeshProUGUI[5];

        for (int i = 0; i < 5; i++)
        {
            Items[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
            Amounts[i] = transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();

            Amounts[i].text = player.items[i].ToString();
            if (player.items[i] == 0)
                Items[i].color = transparent;
            else
                Items[i].color = visible;
        }
    }

}



