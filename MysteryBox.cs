using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : Interactable
{
    //All weapons available from the box
    public GameObject[] weapons;
    //The weapon the box lands on that is collectable
    private GameObject shownWeapon;
    private GameObject collectableWeapon;

    //Used to find the correct length the weapon sghould be changing before landing on the final weapon
    public float timeInSeconds;

    //GameObject that shows the weapon in the box
    public Transform weaponHolder;

    private Animator animator;
    public bool weaponCollect;

    private string buyInfo;

    private void Start()
    {
        //To easily swap info back to normal state
        buyInfo = "Hold 'F' for a Random Weapon (Cost " + price + ")";


        animator = GetComponent<Animator>();
        info = buyInfo;
    }


    public override void Interact(Player player)
    {
        //If hasnt been used yet, start animation and is no longer interacable
        //After weapon animation, make interactable again to pickup weapon - cahnge info to weapon name or grab gun w/e
        //If pickuped up end animation else animation will auto end after duration and animation will reenable the interactable

        //If hasnt been opened yet
        if (weaponCollect == false)
        {
            //If player can afford it
            if (player.GetPoints() >= price)
            {
                //If there is already a weapon in the box, destroy it
                if(shownWeapon != null)
                {
                    Destroy(shownWeapon);
                }

                //Spend points
                player.SpendPoints(price);

                //Open box
                animator.SetTrigger("Start");
                //disable trigger to make it not interactable

                //Randomize Weapons - in courotine
                //Stop on a weapon
                //Re enable trigger to make it interacable
                StartCoroutine(CycleWeapons());
            }
        }
        
        if(weaponCollect)
        {
            player.PickUpGun(collectableWeapon);
            Destroy(shownWeapon);
            info = buyInfo;
            //End animation
            animator.SetTrigger("End");
        }
        
    }


    IEnumerator CycleWeapons()
    {
        float timeElapsed = 0;

        while(timeElapsed < timeInSeconds)
        {
            int random = Random.Range(0, weapons.Length);
            shownWeapon = Instantiate(weapons[random], weaponHolder);
            collectableWeapon = weapons[random];

            yield return new WaitForSeconds(0.2f);
            timeElapsed += 0.2f;

            if(timeElapsed >= timeInSeconds)
            {
                break;
            }
            Destroy(shownWeapon);
        }

        info = "Hold 'F' to pickup " + shownWeapon.GetComponent<Gun>().name;

        //info = "Get " + weaponHolder.GetComponent<Gun>().GetName();
    }
}
