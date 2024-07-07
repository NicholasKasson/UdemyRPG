using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Image ButtonImage;
    public Text amountText;
    public int buttonValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Press()
    {
        if(GameMenu.instance.theMenu.activeInHierarchy)
        {
            if(GameManager.instance.itemsHeld[buttonValue] != "")
            {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }
        if(ShopMenu.instance.shopMenu.activeInHierarchy)
        {
            if(ShopMenu.instance.buyMenu.activeInHierarchy)
            {
                ShopMenu.instance.SelectBuyItem(GameManager.instance.GetItemDetails(ShopMenu.instance.currentItemsForSale[buttonValue]));
            }
            if(ShopMenu.instance.sellMenu.activeInHierarchy)
            {
                ShopMenu.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
            
        }
    }
}
