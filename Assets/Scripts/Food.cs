using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Food : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Space]
    [SerializeField] ContactFilter2D contactFilter = new ContactFilter2D();

    bool foodOnCursor;
    List<RaycastHit2D> hits = new List<RaycastHit2D>();



    private void Update()
    {
        if (!foodOnCursor) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Physics2D.Raycast(mouseWorldPosition, Vector2.zero, contactFilter, hits) != 0)
        {
            transform.position = hits[0].point;
        }

        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foodOnCursor = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (hits[1].transform.tag == "Carnivore")
        {
            GameEvents.GiveFood(Diet.Carnivore);
        }
        else if (hits[1].transform.tag == "Herbivore")
        {
            GameEvents.GiveFood(Diet.Herbivore);
        }

        foodOnCursor = false;
    }
}
