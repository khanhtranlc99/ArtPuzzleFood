using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
 

public class LevelData : MonoBehaviour
{
    public int id;
    public Grass currentGrass;
    public List<Grass> lsGrass;
    public List<Pieces> pieces;
    public List<Goals> lsDataGoalsPost;
    PlayerContain playerContain;
    public Grass GetGrass
    {
        get
        {
            foreach (Grass grass in lsGrass)
            {
                if(!grass.isDone)
                {
                    return grass;
                }
            }
            return null;
        }
    }

   public void Init(PlayerContain paramContain)
    {
        playerContain = paramContain;
        foreach (var grass in lsGrass)
        {
            foreach (var goat in grass.lsGoals)
            {
                goat.Init();
                lsDataGoalsPost.Add(goat);
                var pieceTemp = SimplePool2.Spawn(playerContain.pieces).GetComponent<Pieces>();
              //  pieceTemp.transform.SetParent(playerContain.postScroll,false);
                pieceTemp.thumnail.sprite = goat.thumnails.sprite;
                pieceTemp.controller = paramContain.hScrollController;
                pieceTemp.id = goat.id;
            //    pieceTemp.firstIndex = goat.id;
                pieceTemp.goals = goat;
                pieces.Add(pieceTemp);
         
            }
        }
        pieces.Shuffle(); //duyet lai index
        for(int i = 0; i < pieces.Count; i++)
        {
            pieces[i].transform.SetParent(playerContain.postScroll, false);
            pieces[i].firstIndex = i;
        }
        //foreach (var item in pieces)
        //{
        //    item.transform.SetParent(playerContain.postScroll, false);
        //}


        StartCoroutine(HandleOff());
    }
    private IEnumerator HandleOff()
    {
        yield return new WaitForEndOfFrame();
        playerContain.postScroll.GetComponent<GridLayoutGroup>().enabled = false;
        playerContain.postScroll.GetComponent<ContentSizeFitter>().enabled = false;
        foreach (var item in pieces)
        {
            item.firstPos = item.transform.localPosition;
            item.startSize = item.thumnail.gameObject.GetComponent<RectTransform>().sizeDelta;            
        }
        foreach (var item in lsDataGoalsPost)
        {
            item.thumnails.color = new Color32(0, 0, 0, 0);
            
        }
        currentGrass = GetGrass;
        currentGrass.gameObject.SetActive(true);
        currentGrass.HandleFadeIn();
    }
    public void HandleFillIndex(Pieces param)
    {
        param.isDone = true;
        param.goals.isComplete = true;
        for (int i = lsDataGoalsPost.Count - 1; i >= 0; i--)
        {
            if (param.goals == lsDataGoalsPost[i])
            {
                lsDataGoalsPost.RemoveAt(i);
            }
        }
        for (int i = pieces.Count -1; i >= 0; i--)
        {
            if(param == pieces[i])
            {
                pieces.RemoveAt(i);
            }
        }
        foreach (var item in pieces)
        {
            item.firstPos = item.transform.localPosition;
 
        }
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].firstIndex = i;
        }
        if(currentGrass.HandleCheckDone)
        {
            currentGrass = GetGrass;
            if(currentGrass != null)
            {
                currentGrass.gameObject.SetActive(true);
                currentGrass.HandleFadeIn();
            }
            else
            {
                Winbox.Setup().Show();
                Debug.LogError("Win");
            }    
  
        }
    }
    [Button]
    private void FillIdGoats()
    {
        var tempLsGoats = new List<Goals>();
        foreach (var grass in lsGrass)
        {
            grass.lsGoals.Clear();
            for (int i = 0; i < grass.transform.childCount; i++)
            {
                // Lấy child thứ i
                Goals childTransform = grass.transform.GetChild(i).gameObject.GetComponent<Goals>();
                // Thêm GameObject của child vào list
                grass.lsGoals.Add(childTransform);
            }
        }
        foreach (var grass in lsGrass)
        {
            foreach (var goat in grass.lsGoals)
            {
                tempLsGoats.Add(goat);
            }
        }
        for (int i = 0; i < tempLsGoats.Count; i++)
        {
            tempLsGoats[i].id = i;
            tempLsGoats[i].thumnails = tempLsGoats[i].GetComponent<Image>();
            tempLsGoats[i].grass = tempLsGoats[i].transform.parent.GetComponent<Grass>();
            tempLsGoats[i].gameObject.name = "Goat_" + i;
            tempLsGoats[i].thumnails.color = new Color32(255,255,255,80);
        }
    }
}
 