using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    /**
     * Add Item Types Here
     *
     * Note: You can convert the enum value to a string by appending ToString()
     * Example: Inventory.ItemTypes.PAPER.ToString()
     */
    public enum ItemTypes
    {
        PAPER,
        STICK
    }
    
    public enum Status
    {
        OK,
        DO_NOT_HAVE,
        NOT_ENOUGH
    }
    
    public Dictionary<ItemTypes, int> items = new();
    
    /**
     * Adds items into the inventory
     *
     * Example:
     * inventory.addItems(new()
     * {
     *     {Inventory.ItemTypes.PAPER, 1},
     *     {Inventory.ItemTypes.STICK, 2}
     * });
     * 
     */
    public void addItems(Dictionary<ItemTypes, int> a)
    {
        foreach (KeyValuePair<ItemTypes, int> entry in a)
        {
            if (items.ContainsKey(entry.Key))
            {
                items[entry.Key] += entry.Value;
            }
            else
            {
                items.Add(entry.Key, entry.Value);
            }
        }
    }
    
    /**
     * Removes items from the inventory
     * 
     * Example:
     * string missingItem = "";
     * inventory.removeItems(new()
     * {
     *     {Inventory.ItemTypes.PAPER, 1},
     *     {Inventory.ItemTypes.STICK, 2}
     * }, out missingItem);
     *
     * The function will return a (Status) enum value.
     *
     * If an error has occured (i.e. not enough items), an error status will be returned and the out string
     * will contain the missing item name.
     *
     * Otherwise it will return an OK status and missingItem will be an empty string;
     */
    public Status removeItems(Dictionary<ItemTypes, int> a, out string s)
    {
        foreach (KeyValuePair<ItemTypes, int> entry in a)
        {
            int amount;
            s = entry.Key.ToString();
            if (!items.TryGetValue(entry.Key, out amount)) {return Status.DO_NOT_HAVE;}
            if (amount < entry.Value) return Status.NOT_ENOUGH;
        }
        
        foreach (KeyValuePair<ItemTypes, int> entry in a)
        {
            items.TryAdd(entry.Key, -entry.Value);
        }

        foreach (KeyValuePair<ItemTypes, int> entry in items)
        {
            if (entry.Value <= 0)
            {
                items.Remove(entry.Key);
            }
        }

        s = "";
        return Status.OK;
    }
}
