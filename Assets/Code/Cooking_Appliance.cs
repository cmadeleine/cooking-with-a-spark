using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking_appliance : Interactable
{
    //whether the appliance is plugged in or not
    //when electricity is implemented, the default should be false
    bool is_powered = true;

    Food_Item cooking_item = null;

    //public so you can watch in editor, and updating progress bar
    public int cook_progress = 0;

    //how much progress is made per fixed_update (50/s)
    public int cook_speed = 1;

    //when cook_progress reaches these numbers, the food's state changes
    public int processed = 1000;
    public int ruined = 1250;

    //THE FOLLOWING SHOULD BE REMOVED IN FAVOR OF VISION SELECTION WITH RAYCAST
    private Hand player_hand = null;

    private void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
    }

    void Update()
    {
       
    }


    private void FixedUpdate()
    {
        if (cooking_item && is_powered) {
            increment_cook();
        }
    }

    public override void Interact()
    {
        Debug.Log("appliance use detected");
        if (cooking_item == null && player_hand.In_hand() != null)
        {
            cooking_item = player_hand.Use_item() as Food_Item;
            Debug.Log("beginning cooking");
            start_cooking(cooking_item);
        }
        else
        {
            Debug.Log("ending cooking");
            stop_cooking();
        }
    }


    //increments cook_progress by cook_speed
    //checks progress and changes cooking_item.state
    private void increment_cook()
    {
        cook_progress += cook_speed;

        if (cook_progress >= ruined)
        {
            Debug.Log("food ruined!");
            cooking_item.Ruin_food();
        }
        else if (cook_progress >= processed)
        {
            Debug.Log("food cooked!");
            cooking_item.Process_food();
        }
    }


    //called by player, giving the food item in their hand to the appliance to cook
    public void start_cooking(Food_Item to_cook)
    {
        //TODO check the Food_Type of the item
        cooking_item = to_cook;
        //TODO move the object into position above the stove / in the toaster
    }

    //called by player, taking out the item and resetting progress
    //should return Food_Item
    public void stop_cooking()
    {

        cook_progress = 0;

        Food_Item temp_item = cooking_item;
        cooking_item = null;

        //return temp_item;
        player_hand.item = temp_item;
    }

}
