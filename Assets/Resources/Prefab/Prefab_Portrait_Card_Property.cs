using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Prefab_Portrait_Card_Property : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Prefab_Portrait_Card의 Text 및 내부 속성=====================================================
    public Text name;
    public Text st_STR, st_DEX, st_CON;
    public Text st_INT, st_WIS, st_WIL;

    public Student student = new Student();

    //Text변경 필요할 때만 업데이트================================================================
    void Update()
    {
        if (student.name != name.text)
        {
            name.text = student.name;
        }
        if (student.stat.st_STR != float.Parse(st_STR.text))
        {
            st_STR.text = Mathf.Floor(student.stat.st_STR).ToString();
        }
        if (student.stat.st_DEX != float.Parse(st_DEX.text))
        {
            st_DEX.text = Mathf.Floor(student.stat.st_DEX).ToString();
        }
        if (student.stat.st_CON != float.Parse(st_CON.text))
        {
            st_CON.text = Mathf.Floor(student.stat.st_CON).ToString();
        }
        if (student.stat.st_INT != float.Parse(st_INT.text))
        {
            st_INT.text = Mathf.Floor(student.stat.st_INT).ToString();
        }
        if (student.stat.st_WIS != float.Parse(st_WIS.text))
        {
            st_WIS.text = Mathf.Floor(student.stat.st_WIS).ToString();
        }
        if (student.stat.st_WIL != float.Parse(st_WIL.text))
        {
            st_WIL.text = Mathf.Floor(student.stat.st_WIL).ToString();
        }
    }
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
                if (results[i].gameObject.name == "Portrait_Card_Slot")
                {
                    prev_parent = (results[i].gameObject);
                    student.training = results[i].gameObject.transform.parent.name.Substring(21);
                    this.transform.SetParent(prev_parent.transform);
                }
                else if (results[i].gameObject.name == "Unit_Scroll_View")
                {
                    prev_parent = list_parent;
                    this.transform.SetParent(prev_parent.transform);
                    student.training = "NONE";
                }
                else
                {
                    this.transform.SetParent(prev_parent.transform);
                }
            }
        }
    }
}
