using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Grass : MonoBehaviour
{
   public List<Goals> lsGoals;
    public CanvasGroup canvasGroup;
    public bool isDone;

    public void HandleFadeIn()
    {
        foreach(var item in lsGoals)
        {
            item.thumnails.DOColor(new Color32(255,255,255,100),0.5f);
        }
    }
    public bool HandleCheckDone
    {
        get
        {
          
            foreach (var item in lsGoals)
            {
                if(item.isComplete == false)
                {
                    return false;
                }
            }
            isDone = true;
            return true;
        }
  
    }
}
