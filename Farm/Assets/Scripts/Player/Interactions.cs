using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Рука")]
    [SerializeField] public Transform arm;
    [HideInInspector] public bool flag = false;
    private Rigidbody rb;
    private BoxCollider box;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
    }

    public void PickUp()
    {
        if (arm.childCount < 1)
        {
            transform.SetParent(arm);
            transform.position = arm.position;
            transform.rotation = arm.rotation;
            rb.isKinematic = true;
            flag = true;
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
