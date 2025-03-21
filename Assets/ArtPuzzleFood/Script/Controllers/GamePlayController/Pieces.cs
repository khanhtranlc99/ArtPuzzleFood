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
}
