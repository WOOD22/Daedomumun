using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Infinite_Scroll : MonoBehaviour, IDragHandler
{
    public GameObject scroll_view;
    public GameObject scroll_cell_num0;
    public float content_x, right_x;

    void Start()
    {
        content_x = this.GetComponent<RectTransform>().localPosition.x;
        right_x = (-1 * this.GetComponent<RectTransform>().rect.width) + (2 * scroll_cell_num0.GetComponent<RectTransform>().rect.width);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(content_x > 0)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector2(right_x + content_x, 0);
        }
        else if(content_x < right_x)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector2(right_x + content_x - right_x, 0);
        }
    }
}
