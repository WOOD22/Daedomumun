using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag_And_Drop : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //드래그 기능==================================================================================
    GameObject prev_parent;
    GameObject list_parent;
    string open_page;
    GraphicRaycaster tagray;
    PointerEventData tagped;

    void Start()
    {
        open_page = GameObject.Find("GameManager").GetComponent<Current_Page>().open_page_name;
        list_parent = GameObject.Find("Canvas").transform.Find(open_page).transform.Find("Unit_Scroll_View").transform.Find("Viewport").transform.Find("Content").gameObject;
        tagray = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        tagped = new PointerEventData(null);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    //드래그 시작시
    void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
    {

    }
    //드래그 시 Pool의 자식으로 할당===============================================================
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        prev_parent = this.transform.parent.gameObject;
        this.transform.SetParent(GameObject.Find("Canvas").transform.Find("Pool"));
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        tagped.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        tagray.Raycast(tagped, results);

        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                //Portrait_Card_Slot이 비어있을 경우 자식으로 할당=================================
                if (results[i].gameObject.name == "Portrait_Card_Slot" && results[i].gameObject.transform.childCount == 0)
                {
                    prev_parent = (results[i].gameObject);
                    this.transform.SetParent(prev_parent.transform);
                }
                //Portrait_Card_Slot가 채워져 있을 경우 교체=======================================
                else if (results[i].gameObject.name == "Portrait_Card_Slot" && results[i].gameObject.transform.childCount != 0 && results[i].gameObject.transform.GetChild(0).gameObject.activeSelf == true)
                {
                    results[i].gameObject.transform.GetChild(0).transform.SetParent(list_parent.transform);
                    prev_parent = (results[i].gameObject);
                    this.transform.SetParent(prev_parent.transform);
                }
                else if (results[i].gameObject.name == "Unit_Scroll_View")
                {
                    prev_parent = list_parent;
                    this.transform.SetParent(prev_parent.transform);
                }
                else
                {
                    this.transform.SetParent(prev_parent.transform);
                }
            }
        }
    }
}
