using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAttach : MonoBehaviour
{
    [SerializeField]
    private Transform itemHolder;

    private bool itemAttached;

    private HookMovement hookMovement;

    private PlayerAnimation playerAnim;

    void Awake()
    {
        hookMovement = GetComponentInParent<HookMovement>();
        playerAnim = GetComponentInParent<PlayerAnimation>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
       if (target.tag == Tags.SMALL_GOLD || target.tag == Tags.MIDDLE_GOLD ||
            target.tag == Tags.LARGE_GOLD || target.tag == Tags.LARGE_STONE ||
            target.tag == Tags.MIDDLE_STONE)
        {
            if (itemAttached)
            {
                return;
            }

            itemAttached = true;

            target.transform.parent = itemHolder;
            target.transform.position = itemHolder.position;

            hookMovement.move_Speed = target.GetComponent<ItemScore>().hook_Speed;

            hookMovement.HookAttachedItem();
            
            // animate
            playerAnim.PullingItemAnimation();

        } // if target is an item

        if (target.tag == Tags.DELIVER_ITEM)
        {
            if (itemAttached)
            {
                itemAttached = false;

                Transform objChild = itemHolder.GetChild(0);

                objChild.parent = null;
                objChild.gameObject.SetActive(false);

                playerAnim.IdleAnimation();
            }
        } // deliver item
    } // on trigger enter
} // class
