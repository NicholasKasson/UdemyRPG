using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public static ShopMenu instance;
    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;
    public Text goldText;
    public string[] currentItemsForSale;
    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue;
    public Text sellItemName, sellItemDescription, sellItemValue;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        shopMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        // if(Input.GetKeyDown(KeyCode.K) && !shopMenu.activeInHierarchy)
        // {
        //     OpenShop();
        // }

    }
    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();
        GameManager.instance.shopActive = true;
        goldText.text = GameManager.instance.currentGold.ToString();
    }
    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }
    public void OpenBuyMenu()
    {
        buyItemButtons[0].Press();
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        // selectedItem = null;
        // buyItemName.text = "";
        // buyItemDescription.text = "";
        // buyItemValue.text = "N/A Gold";
        for(int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if(currentItemsForSale[i] != "")
            {
                buyItemButtons[i].ButtonImage.gameObject.SetActive(true);
                // Debug.Log("Item " + i + itemName " loaded");
                // Debug.Log(currentItemsForSale.text[i]);
                buyItemButtons[i].ButtonImage.sprite = GameManager.instance.GetItemDetails(currentItemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            } 
            else
            {
                buyItemButtons[i].ButtonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }
    public void OpenSellMenu()
    {
        sellItemButtons[0].Press();
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);
        ShowSellItems();
        // selectedItem = null;
        // sellItemName.text = "";
        // sellItemDescription.text = "";
        // sellItemValue.text = "N/A Gold";
    }
    private void ShowSellItems()
    {
        GameManager.instance.SortItem();
        for(int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;
            if(GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].ButtonImage.gameObject.SetActive(true);
                sellItemButtons[i].ButtonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                if(GameManager.instance.numberOfItems[i] > 1)
                {
                    sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
                }
                else
                {
                    sellItemButtons[i].amountText.text = "";
                }                
            } 
            else
            {
                sellItemButtons[i].ButtonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectBuyItem(Item buyItem)
    {
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = selectedItem.itemValue + " Gold";
    }
    public void SelectSellItem(Item sellItem)
    {
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = Mathf.FloorToInt(selectedItem.itemValue * 0.65f).ToString() + " Gold";
    }
    public void BuyItem()
    {
        if(selectedItem != null)
        {
            if(GameManager.instance.currentGold >= selectedItem.itemValue)
            {
                GameManager.instance.currentGold -= selectedItem.itemValue;
                GameManager.instance.AddItem(selectedItem.itemName);
            }
        goldText.text = GameManager.instance.currentGold.ToString() + " Gold";
        }
    }
    public void SellItem()
    {
        if(selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.itemValue * 0.65f);
            GameManager.instance.RemoveItem(selectedItem.itemName);
        }
        goldText.text = GameManager.instance.currentGold.ToString() + " Gold";
        ShowSellItems();
    }
}
