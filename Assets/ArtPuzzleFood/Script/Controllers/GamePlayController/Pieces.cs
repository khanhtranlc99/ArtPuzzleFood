using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


public class Pieces : MonoBehaviour, IPointerDownHandler
{
    public int id;
    public Vector2 startPoint;
    public Vector2 startSize;
    public HScrollController controller;
    public Vector2 firstPos;
    private bool isCanDrag;
    public bool isDragging;
    public int firstIndex;
    public Image thumnail;
    public RectTransform draggedItemRect;
    public bool isDone;
    public Goals goals;
    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        controller.currentClickScroll = this;

    }
    public void ActiveDrag(bool isActive)
    {
        if (isActive && this.controller.isCanDrag)
        {
            this.transform.parent = this.controller.parentDrag;
            isCanDrag = true;
            isDragging = true;
            controller.ActiveDrag(this.firstIndex);

            var level = GamePlayController.Instance.playerContain.levelData;
            for (int i = 0; i < level.lsDataGoalsPost.Count; i++)
            {
                if (this.id == level.lsDataGoalsPost[i].id)
                {
                    this.thumnail.GetComponent<RectTransform>().DOSizeDelta(new Vector2(level.lsDataGoalsPost[i].thumnails.rectTransform.sizeDelta.x, level.lsDataGoalsPost[i].thumnails.rectTransform.sizeDelta.y), 0.2f);
                }
            }

        }
        Debug.LogError("isActive_" + isActive);
        Debug.LogError("isCanDrag_" + this.controller.isCanDrag);
    }
    public void ReturnScroll()
    {
        this.transform.parent = this.controller.parentElement;
        this.transform.SetSiblingIndex(this.firstIndex);
        this.thumnail.GetComponent<RectTransform>().DOSizeDelta(this.startSize, 0.3f);
        controller.ReturnScroll(this.firstIndex, () =>
        {
            this.transform.DOLocalMove(firstPos, 0.3f);
         //   controller.isCanDrag = true;
            GamePlayController.Instance.gameScene.blockRaycast.SetActive(false);
        });
    }
    void Update()
    {
        if (isDragging && controller.isCanDrag)
        {
            Vector2 localPosition = Vector2.zero;
            Vector2 sceenPoint = Input.mousePosition + 300 * Vector3.up;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(

                draggedItemRect,

                sceenPoint,

                controller.camera,

                out localPosition);


            draggedItemRect.position = Vector3.Lerp(draggedItemRect.position, draggedItemRect.TransformPoint(localPosition), 10 * Time.deltaTime);

            //for (int i = 0; i < GamePlayController.Instance.playerContain.levelData.lsDataGoalsPost.Count; i++)
            //{
            //    if (GamePlayController.Instance.playerContain.levelData.lsDataGoalsPost[i].id == this.id)
            //    {
            //       // GamePlayController.Instance.playerContain.levelData.lsDataGoalsPost[i].CheckCompletePiece();
            //    }
            //}
            CheckComplete();


        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isCanDrag && this.isDone == false)
            {
                isDragging = false;
                isCanDrag = false;
                ReturnScroll();
                controller.scroll.enabled = true;
            }
        }
    }
    private void CheckComplete()
    {
        if (goals.transform.parent.gameObject.activeSelf )
        {
            float distance = Vector3.Distance(this.transform.position, goals.transform.position);
            if (distance < 0.5f)
            {
             
                controller.currentClickScroll = null;
                goals.CheckComplete();
                isDragging = false;
                isCanDrag = false;
                controller.scroll.enabled = true;
           
                GamePlayController.Instance.playerContain.levelData.HandleFillIndex(this);
                GamePlayController.Instance.gameScene.blockRaycast.SetActive(false);
                Debug.LogError("123");
                StartCoroutine(ResetContentSize());
                SimplePool2.Despawn(this.gameObject);



            }
        }
    }    

    private IEnumerator ResetContentSize()
    {
        controller.gridLayoutGroup.enabled = true;
        controller.contentSizeFitter.enabled = true;
        yield return new WaitForSeconds(0.1f);
        controller.gridLayoutGroup.enabled = false;
        controller.contentSizeFitter.enabled = false;
    }
}
