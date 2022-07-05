using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCharacter : MonoBehaviour
{
    [Header("Дальность взаимодействия")]
    [SerializeField] private float maxDistance;
    public Image image;
    public Button buttonE;
    public Button buttonT;
    private Ray ray;
    private RaycastHit hit;

    [Header("Радиус взаимодействия")]
    [SerializeField] private float radius;

    private void Ray()
    {
        ray = new Ray(transform.position, transform.forward);
    }

    private void DrawRay()
    {
        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue);
        }
        
        if(hit.transform == null)
        {
            image.enabled = false;
            buttonE.image.enabled = false;
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }

    private void Interact() // Метод взаимодействия с действиями 
    {
        if (hit.transform != null && hit.transform.GetComponent<Interactions>())
        {
            buttonE.image.enabled = true;
            image.enabled = true;
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }
    }

    private void Radius() // Метод взаимодействия с объектами для передвижения
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if(rigidbody)
            {
                buttonE.image.enabled = true;
                image.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rigidbody.GetComponent<Interactions>().PickUp();
                }
            }
            
        }
    }

    public void ButtonE()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.GetComponent<Interactions>().PickUp();
            }

        }
    }

    public void ButtonT()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.GetComponent<Interactions>().Release();
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Update()
    {
        Ray();
        DrawRay();
        Interact();
        Radius();
    }
}
