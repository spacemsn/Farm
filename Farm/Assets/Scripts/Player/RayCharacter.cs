using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCharacter : MonoBehaviour
{
    [Header("????????? ??????????????")]
    [SerializeField] private float maxDistance;
    public Image imageE;
    public Image imageT;
    public Button buttonE;
    public Button buttonT;
    private Ray ray;
    private RaycastHit hit;

    [Header("?????? ??????????????")]
    [SerializeField] private float radius;
    [SerializeField] private bool isGizmos = true;

    [Header("?????????")]
    public Transform inventoryPanel;
    public List<Slot> slots = new List<Slot>();
    [SerializeField] bool isOpenPanel;

    private CharacterMove characterMove;

    private void Start()
    {
        characterMove = GetComponent<CharacterMove>();

        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<Slot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<Slot>());
            }
        }
    }

    private void Ray()
    {
        ray = new Ray(transform.position + new Vector3(0, 1f, 0), transform.forward);
    }

    private void DrawRay()
    {
        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue);
        }
        
        if(hit.transform == null)
        {
            if (characterMove.move == CharacterMove.Move.PC)
            {
                imageE.enabled = false;
                imageT.enabled = false;

            }
            else if (characterMove.move == CharacterMove.Move.Android)
{
                buttonE.image.enabled = false;
                buttonT.image.enabled = false;
            }
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }

    private void Interact() // ????? ?????????????? ? ?????????? 
    {
        if (hit.transform != null && hit.transform.GetComponent<Interactions>())
        {
            if (characterMove.move == CharacterMove.Move.PC)
            {
                imageE.enabled = true;
                imageT.enabled = true;

            }
            else if (characterMove.move == CharacterMove.Move.Android)
            {
                buttonE.image.enabled = true;
                buttonT.image.enabled = true;
            }
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }
    }

    private void Radius() // ????? ?????????????? ? ????????? ??? ????????????
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if(rigidbody)
            {
                if (characterMove.move == CharacterMove.Move.PC)
                {
                    imageE.enabled = true;
                    imageT.enabled = true;

                }
                else if (characterMove.move == CharacterMove.Move.Android)
                {
                    buttonE.image.enabled = true;
                    buttonT.image.enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rigidbody.GetComponent<Interactions>().PickUp();
                }
                if(Input.GetKeyDown(KeyCode.P))
                {
                    AddItem(rigidbody.gameObject.GetComponent<ItemPrefab>().item, rigidbody.gameObject.GetComponent<ItemPrefab>().amount, rigidbody.gameObject.GetComponent<ItemPrefab>().icon);
                    Destroy(rigidbody.gameObject);
                }
            }
            
        }
    }

    private void AddItem(Item _item, int _amount, Sprite _icon)
    {
        foreach (Slot slot in slots)
        {
            if(slot.item == _item)
            {
                slot.amount += _amount;
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.gameObject.GetComponent<Image>().sprite = _icon;
                return;
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
        if (isGizmos == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + new Vector3(0, 1f, 0), radius);
        }
    }

    private void Update()
    {
        Ray();
        DrawRay();
        Interact();
        Radius();

        if (characterMove.move == CharacterMove.Move.PC)
        {
            buttonE.gameObject.SetActive(false);
            buttonT.gameObject.SetActive(false);
            imageE.gameObject.SetActive(true);
            imageT.gameObject.SetActive(true);
            
        }
        else if (characterMove.move == CharacterMove.Move.Android)
        {
            imageE.gameObject.SetActive(false);
            imageT.gameObject.SetActive(false);
            buttonE.gameObject.SetActive(true);
            buttonT.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpenPanel == true)
            {
                inventoryPanel.gameObject.SetActive(false);
                isOpenPanel = false;
            }
            else if (isOpenPanel == false)
            {
                inventoryPanel.gameObject.SetActive(true);
                isOpenPanel = true;
            }
        }
    }
}
