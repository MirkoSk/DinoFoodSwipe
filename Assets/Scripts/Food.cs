using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Food : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Space]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ContactFilter2D contactFilter = new ContactFilter2D();


    FoodType foodType;
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
        Animal animal = hits[1].transform.GetComponent<Animal>();
        if (animal != null && animal.Diet == foodType.Diet)
        {
            GameEvents.GiveFood(1);
            Destroy(gameObject);
        }
        else if (animal != null && animal.Diet != foodType.Diet)
        {
            GameEvents.GiveFood(-1);
            Destroy(gameObject);
        }

        foodOnCursor = false;
    }



    public void Initialize(FoodType foodType)
    {
        this.foodType = foodType;
        if (foodType.Sprite != null) spriteRenderer.sprite = foodType.Sprite;
        spriteRenderer.color = foodType.Tint;
    }
}
