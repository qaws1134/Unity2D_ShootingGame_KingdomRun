﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform rootSlot;
    public Store store;
    private List<Slot> slots;
 
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<Slot>();

        int slotCnt = rootSlot.childCount;

        for(int i =0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();

            slots.Add(slot);
        }

        store.onSlotClick += BuyItem;
    }
   
    void BuyItem(ItemProperty item)
    {
        var emptySlot = slots.Find(t =>
        {
            return t.item == null || t.item.itemName == string.Empty;
        });
        if (emptySlot != null)
        {
            int player_gold = PlayerPrefs.GetInt("player_gold");
            if (item.price < player_gold)
            {
                emptySlot.SetItem(item);
                emptySlot.text.enabled = false;
                PlayerPrefs.SetInt("player_gold", player_gold - item.price);
                store.printGold();
            }
        }
    }

}
