using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Interactable
{
    private Hand player_hand = null;
    private Food_Item currItem = null;

    // Start is called before the first frame update
    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (player_hand.In_hand() != null)
        {
            Food_Item item;

            if (player_hand.In_hand().TryGetComponent<Food_Item>(out item))
            { 
                if (!currItem)
                {
                    currItem = item;
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
                    item.Put_Down(pos);
                    player_hand.Use_item();
                }     
            }  
                
        }
        else if (currItem != null)
        {
            player_hand.Pick_up_item(currItem);
            currItem = null;   
        }

    }
}
