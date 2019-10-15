using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    //If interacable require points
    public int price;

    public Text interactText;

    //Used for Canvas Text to show what you are interacting with
    //Ex. Interacting with msytery box "Spend x points on 'mystery weapon'"
    //Ex2. "Spend x point 'to open door'"
    //Might be better to put in a different class
    protected string info;

    private bool interacting = false;

    //Use Interactable by specific player - player given in the trigger method
    public virtual void Interact(Player player)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
           
            interactText.text = info;
            interactText.gameObject.SetActive(true);
        }
    }

    //When something is in the area
    //Also enable text on screen (Ex. "Spend x points to get mystery weapon")
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {   
            Player player = other.GetComponent<Player>();
            //Enable text/Change text based on info



            if(player.GetAction() && !interacting)
            {
                interacting = true;
                StartCoroutine(GetActionButton(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.gameObject.SetActive(false);
    }

    IEnumerator GetActionButton(Player player)
    {
        float timeHeld = 0f;

        while(player.HasActionButtonPressed())
        {
            //If player holds action button for one second, you interact with the object
            if(timeHeld > 1f)
            {
                Interact(player);
                interactText.gameObject.SetActive(false);
                break;
            }

            yield return new WaitForEndOfFrame();
            timeHeld += Time.deltaTime;
        }

        interacting = false;
    }

    // TO DO
    //Make sure Player Object has Tag "Player"
    //Change all Player variables in this class to what the actual Player Class name is -- I Think its called PlayerMovement - This should also be changed should only handle movment another class should handle attributes such as HP, Points and others
    //Add private int points variable
    //Add public void GainPoints(), public void SpendPoints(), public int GetPoints() Methods
    //               Earn Points             Use Points          Getter to Get the Players Points


}
