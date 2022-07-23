using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Interactions : MonoBehaviour
{
    [Header("Рука")]
    public Transform armSmall;
    public Transform armLarge;
    public bool flag = false;
    private Rigidbody rb;

    [Header("Rig's")]
    public RigBuilder rbBuilder;

    public enum sizeItem
    {
        small = 0,
        large = 1
    }
    public sizeItem size = sizeItem.small;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        if (size == sizeItem.small)
        {
            if (armSmall.childCount < 1 && armLarge.childCount < 1)
            {
                rbBuilder.layers[2].active = false;
                rbBuilder.layers[1].active = true;
                transform.SetParent(armSmall);
                transform.position = armSmall.position;
                transform.rotation = armSmall.rotation;
                rb.isKinematic = true;
                flag = true;
            }
        }
        else if(size == sizeItem.large)
        {
            if (armLarge.childCount < 1 && armSmall.childCount < 1)
            {
                
                rbBuilder.layers[1].active = false;
                rbBuilder.layers[2].active = true;
                transform.SetParent(armLarge);
                transform.position = armLarge.position;
                transform.rotation = armLarge.rotation;
                rb.isKinematic = true;
                flag = true;
            }
        }
    }

    public void Throw()
    {
        if(flag == true)
        {
            transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * 100);
            flag = false;
        }
    }

    public void Release()
    {
        if (flag == true)
        {
            rbBuilder.layers[1].active = false;
            rbBuilder.layers[2].active = false;
            transform.parent = null;
            rb.isKinematic = false;
            flag = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Release();
        }
    }

    
}
