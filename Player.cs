using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool actionButtonPressed;
    public Transform weaponHolder;

    public int points = 100000;

    //An Array consisting of 2 guns or more depending on how many guns the player should be able to hold
    public GameObject[] guns = new GameObject[2];
    //This would be the gun being held in guns
    private int activeGun;
    private GameObject equippedGun;

    private void Start()
    {
        equippedGun = Instantiate(guns[0], weaponHolder);
    }

    private void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            SwapGun();
        }
    }

    public int GetPoints()
    {
        return points;
    }

    public void SpendPoints(int price)
    {
        points -= price;
    }


    //Returns true wihle button is held down
    public bool HasActionButtonPressed()
    {
        if (Input.GetKey("f"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Gets the frame action button was pressed
    public bool GetAction()
    {
        if(Input.GetKeyDown("f"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //If there is an empty slot in guns[] put the new weapon there, else replace activeGun with newGun
    public void PickUpGun(GameObject newGun)
    {
        if(guns[1] == null)
        {
            //Set NewGun
            guns[1] = newGun;   

            Destroy(equippedGun);


            //Swap to newGun
            activeGun = 1;
            equippedGun = Instantiate(guns[activeGun], weaponHolder);        
        }
        else
        {
            //Destroy currently held gun
            Destroy(equippedGun);
            //Set gunslot to new gun
            guns[activeGun] = newGun;
            //Instantiate new gun in hand
            equippedGun = Instantiate(guns[activeGun], weaponHolder);
        }
    }

    //Method to swap replace activeGun with newGun
    private void SwapGun()
    {
        //Check if there is a gun to swap too
        //get next gun interger but dont allow the number to go above the number of guns equipabble
        int nextGun = activeGun + 1;
        nextGun %= guns.Length;

        if (guns[nextGun] != null)
        {
            //Destroy currently held gun
            Destroy(equippedGun);

            activeGun = nextGun;

            //Spawn otherGun
            equippedGun = Instantiate(guns[activeGun], weaponHolder);
            
            //Problem -- newGun will have full ammo again
            //I think ammo should be held by the player not the gun maybe
            
            //Tip -- have a starting animation so when gun is instantiated animation plays to look like gun is being grabbed 
            //      off your back or something
        }
        
    }
}
