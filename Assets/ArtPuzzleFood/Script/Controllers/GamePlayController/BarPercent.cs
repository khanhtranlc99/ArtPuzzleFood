using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class BarPercent : MonoBehaviour
{
    public int sumPieces;
    public int currentNumb;
    public Text tvNumPieces;
    public Image amount;
    LevelData levelData;

    public void Init(LevelData param)
    {
        levelData  = param;
        sumPieces = 0;
        foreach(var item in levelData.lsGrass)
        {
            foreach (var piece in item.lsGoals)
            {
                sumPieces += 1;
            }
        }
        currentNumb = sumPieces;
        tvNumPieces.text = currentNumb + sumPieces.ToString();
    }    
    public void HandleSubtract()
    {
        currentNumb -= 1;
        tvNumPieces.text = currentNumb + sumPieces.ToString();
    }    

}
