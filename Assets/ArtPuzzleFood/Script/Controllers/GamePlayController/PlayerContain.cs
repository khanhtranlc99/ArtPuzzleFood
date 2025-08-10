using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using UnityEngine.UI;
public class PlayerContain : MonoBehaviour
{
    public LevelData levelData;
    public Transform postLevel;
    public HScrollController hScrollController;
    public Pieces pieces;
    public Transform postScroll;
    public Material _shaderChange;
    public Material _colorChange;
    public BoosterHint boosterHint;

    public ScrollRect scrollView;
    public RectTransform viewPort;
    public RectTransform content;
    public void Init()
    {
        string pathLevel = StringHelper.PATH_CONFIG_LEVEL_TEST;
        levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.LevelEggChest)));
        levelData.transform.SetParent(postLevel, false);
        levelData.Init(this);
        boosterHint.Init();
    }

   


}
