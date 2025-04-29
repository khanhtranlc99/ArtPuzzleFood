using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;
using UnityEngine.Events;
using UniRx;

public class GameScene : BaseScene
{
 
    public Text tvLevel;
    public Button settinBtn;
    public Button skipBtn;
    public Transform canvas;
    public GameObject blockRaycast;
    public Button btnHome;
    public BarPercent barPercent;
    public void Init( LevelData param )
    {

        tvLevel.text = "Level " + UseProfile.CurrentLevel;
        btnHome.onClick.AddListener(HandleButtonOnClick);

        barPercent.Init(param);
    }
    public void HandleButtonOnClick()
    {

        Initiate.Fade(SceneName.HOME_SCENE, Color.black, 2f);
    }
    public void HandleButtonSkip()
    {
        GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "InterWinBox");
        void Next()
        {
            Winbox.Setup().Show();

        }
   
       
    }
    IEnumerator ChangeScene()
    {
         


        string name = "";

        name = SceneName.HOME_SCENE;
        var _asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

        while (!_asyncOperation.isDone)
        {

            yield return null;


        }
    }
    public override void OnEscapeWhenStackBoxEmpty()
    {
     
    }
}
