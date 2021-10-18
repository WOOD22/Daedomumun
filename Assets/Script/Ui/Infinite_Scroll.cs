using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스크롤 뷰의 Content에 달아서 사용
public class Infinite_Scroll : MonoBehaviour
{
    //선언=========================================================================================
    public GameObject scroll_cell_num0;//0번 셀
    public bool is_pooling;//풀링 유무
    public bool is_dial;
    public float content_x;//Content의 x좌표
    private float view_width, cell_width;//Viewport의 넓이, Cell의 넓이

    void Start()
    {
        //초기화===================================================================================
        view_width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        cell_width = scroll_cell_num0.GetComponent<RectTransform>().rect.width;
    }
    void Update()
    {
        //매 프레임 초기화=========================================================================
        content_x = -1 * this.GetComponent<RectTransform>().localPosition.x;
        loop();
        //풀링 ture================================================================================
        if (is_pooling == true)
        {
            pooling();
        }
        if(Input.GetMouseButtonDown(0))
        {

        }
        if (Input.GetMouseButtonUp(0))
        {
            Dial();
        }
    }

    void Dial()
    {
        this.GetComponent<RectTransform>().localPosition = new Vector2(-1 * (Mathf.Floor(content_x / 100) * 100 + cell_width / 2), 0);
    }

    //풀링 함수(순환 시에 Content를 순환 방향으로 드래그 할 경우 좌표가 튀는 현상 발생)============
    void pooling()
    {
        //모든 자식 수 만큼 탐색===================================================================
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //Viewport 안에 있는 Cell만 활성화=====================================================
            if (content_x - cell_width < this.transform.GetChild(i).GetComponent<RectTransform>().localPosition.x &&
                content_x + view_width > this.transform.GetChild(i).GetComponent<RectTransform>().localPosition.x)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
            //나머지 비활성화======================================================================
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void loop()
    {
        //좌측으로 이동============================================================================
        if (content_x < this.transform.GetChild(0).GetComponent<RectTransform>().localPosition.x)
        {
            this.transform.GetChild(this.transform.childCount - 1).GetComponent<RectTransform>().localPosition = new Vector2(this.transform.GetChild(0).GetComponent<RectTransform>().localPosition.x - cell_width, 0);
            this.transform.GetChild(this.transform.childCount - 1).SetSiblingIndex(0);
        }
        //우측으로 이동============================================================================
        else if (content_x + cell_width > (this.transform.GetChild(this.transform.childCount - 1).GetComponent<RectTransform>().localPosition.x)) 
        {
            this.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector2(this.transform.GetChild(this.transform.childCount - 1).GetComponent<RectTransform>().localPosition.x + cell_width, 0);
            this.transform.GetChild(0).SetSiblingIndex(this.transform.childCount - 1);
        }
    }
}
