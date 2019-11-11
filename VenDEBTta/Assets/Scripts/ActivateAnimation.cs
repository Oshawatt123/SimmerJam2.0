using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    public Animator anim;

    public string trigger;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger(trigger);
    }
}
