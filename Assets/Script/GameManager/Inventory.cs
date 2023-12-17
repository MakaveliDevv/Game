using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> collectedWeapons = new List<GameObject>();
    public Transform[] weaponSlot;
    public Image[] weaponImageSlot;

    public bool CanCollectObject() { return collectedWeapons.Count < weaponSlot.Length; }
    public bool CollectWeapon(GameObject prefab) 
    {
        if (!CanCollectObject()) return false; 

        GameObject collectedWeapon = Instantiate(prefab, weaponSlot[collectedWeapons.Count].position, Quaternion.identity);
        collectedWeapon.transform.SetParent(weaponSlot[collectedWeapons.Count]);
        collectedWeapons.Add(collectedWeapon);

        // string tag = prefab.tag;
        // for (int i = 0; i < weaponImageSlot.Length; i++)
        // {
        //     // Check if the tag of the image slot is the same as the game object
        //     if(weaponImageSlot[i].gameObject.CompareTag(tag)) 
        //     {
        //         // Set the slot active and exit the loop
        //         weaponImageSlot[i].gameObject.SetActive(true);
        //         return true;
        //     }
        // }
        // return false;
        return true;
    }

    public int GetCollectedWeaponCount() 
    {
        return collectedWeapons.Count;
    }

    public GameObject GetCollectedWeapon(int index) 
    {
        if (index >= 0 && index < collectedWeapons.Count)
        {
            return collectedWeapons[index];
        }
        else
        {
            return null;
        }
    }

    public void RemoveFromInventory(int index) 
    {
        if(index >= 0 && index < collectedWeapons.Count) 
        {   
            Destroy(collectedWeapons[index]);
            collectedWeapons.RemoveAt(index);
        }
    }
}
