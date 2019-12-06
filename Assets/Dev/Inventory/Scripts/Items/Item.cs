using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

//    private int numberOfStack = 0;

    public virtual void Use()
    {
        // Use the item
        // Something might happen
        
        Debug.Log("Using " + name);
    }
}

[System.Serializable]
public class ItemStack
{
    public Item item;
    public int stacks;

    public ItemStack(Item item, int stacks)
    {
        this.item = item;
        this.stacks = stacks;
    }

    public ItemStack(Item item)
    {
        this.item = item;
        stacks = 1;
    }

    public bool RemoveOneStack()
    {
        if (stacks > 0)
        {
            stacks--;
            return true;
        }

        return false;
    }

    public int RemoveMultipleStack(int stacks)
    {
        /*
         * Return consumed stacks
         */
        for (int i = 0; i < stacks; i++)
        {
            if (!RemoveOneStack())
            {
                //Debug.Log("Vous ne pouvez pas utiliser plus de " + i + " stack pour l'item " + item.name);
                return i;
            }
        }
        return stacks;
    }

    public void AddStack()
    {
        stacks++;
    }

    public void AddStacks(int stacks)
    {
        this.stacks += stacks;
    }
    
    
}
