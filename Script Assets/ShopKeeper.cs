using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public bool canShop;
    public bool hasActivated;
    // Bool exists to switch active state while entering the designated area, allowing the player to shop when near a shopkeeper.
    public string[] itemsForSale = new string[40];
    // Listed defines and items available for the player to buy, can be limited so the player unlocks items at appropriate time in order to not be overpowered relative to their level or area. 

    // Start is called before the first frame update
    void Start()
    {
        canShop = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(canShop && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !ShopMenu.instance.shopMenu.activeInHierarchy)
        {
            ShopMenu.instance.currentItemsForSale = itemsForSale;
            ShopMenu.instance.OpenShop();
        }    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(hasActivated)
        {
            canShop = true;
        }
        else
        {
            hasActivated = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canShop = false;
    }
}
