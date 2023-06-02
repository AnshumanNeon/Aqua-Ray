using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> usableItems = new List<Item>() { Capacity = 4 };
    public List<Item> inventoryItems = new List<Item>();
    public float switchTime, radius;
    public LayerMask layer;

    int currentItemIndex;
    float timeSinceLastSwitch;
    Item currentItem;

    // Start is called before the first frame update
    void Start()
    {
        currentItemIndex = 0;
        if(usableItems.Count > 0) currentItem = usableItems[currentItemIndex];
        timeSinceLastSwitch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedItem = currentItemIndex;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItemIndex = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentItemIndex = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentItemIndex = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentItemIndex = 3;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        for(int i = 0; i < colliders.Length; i++)
        {
            ItemPickup item = colliders[i].gameObject.GetComponent<ItemPickup>();
            if(item != null && item.item != null && Input.GetKeyDown(KeyCode.E))
            {
                AddItem(item.item);
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            currentItem.Use();
        }

        if(previousSelectedItem != currentItemIndex)
        {
            currentItem = usableItems[currentItemIndex];
            timeSinceLastSwitch = 0;
        }

        timeSinceLastSwitch += Time.deltaTime;
    }

    void AddItem(Item item)
    {
        if(usableItems.Count < 9)
        {
            usableItems.Add(item);
            item.PickUp();
        }
        else if(usableItems.Count == 9)
        {
            inventoryItems.Add(item);
            item.PickUp();
        }

        currentItem = usableItems[currentItemIndex];
    }
}
