using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//스크롤 뷰의 Content에 달아서 사용
public class Infinite_Scroll : MonoBehaviour
{
    //선언=========================================================================================
    public GameObject scroll_cell_num0;//0번 셀
    public bool is_pooling;//풀링 유무
    public float content_x, right_x;//Content의 x좌표, Content의 순환 크기
    private float view_width, cell_width;//Viewport의 넓이, Cell의 넓이

    void Start()
    {
        //초기화===================================================================================
        right_x = (-1 * this.GetComponent<RectTransform>().rect.width) + scroll_cell_num0.GetComponent<RectTransform>().rect.width;
        view_width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        cell_width = scroll_cell_num0.GetComponent<RectTransform>().rect.width;
    }

    void Update()
    {
        //매 프레임 초기화=========================================================================
        content_x = this.GetComponent<RectTransform>().localPosition.x;
        //좌측 끝까지 이동시 우측 끝으로 이동======================================================
        if (content_x > 0)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector2(right_x + content_x, 0);
        }
        //우측 끝까지 이동시 좌측 끝으로 이동======================================================
        else if (content_x <= right_x)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector2(content_x - right_x, 0);
        }
        //풀링 ture================================================================================
        if(is_pooling == true)
        {
            pooling();
        }
    }
    //풀링 함수(순환 시에 Content를 순환 방향으로 드래그 할 경우 좌표가 튀는 현상 발생)============
    void pooling()
    {
        //모든 자식 수 만큼 탐색===================================================================
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //Viewport 안에 있는 Cell만 활성화=====================================================
            if ((-1 * content_x) - cell_width * (i) < this.transform.GetChild(i).GetComponent<RectTransform>().position.x &&
                (-1 * content_x) + view_width - cell_width * (i) > this.transform.GetChild(i).GetComponent<RectTransform>().position.x)
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
}
