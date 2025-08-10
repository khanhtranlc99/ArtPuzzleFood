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
using Newtonsoft.Json;

public class GameScene : BaseScene
{
    
    public Text tvLevel;
    public Button settinBtn;
    public Button nextBtn;
    public Button homeBtn;
    public Transform canvas;
    public GameObject blockRaycast;
    public Button btnHome;
    public BarPercent barPercent;
    public CanvasGroup canvasGroupMain;


    public void Init( LevelData param )
    {
        nextBtn.gameObject.transform.localScale = Vector3.zero;
        nextBtn.gameObject.SetActive(false);
        tvLevel.text = "Level " + UseProfile.CurrentLevel;
        btnHome.onClick.AddListener(HandleButtonOnClick);
        nextBtn.onClick.AddListener(HandleButtonNext);
      
        barPercent.Init(param);
    }
    public void HandleButtonOnClick()
    {
        GameController.Instance.musicManager.PlayClickSound();
        Initiate.Fade(SceneName.HOME_SCENE, Color.black, 2f);
    }
    public void HandleButtonNext()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "InterWinBox");
        void Next()
        {
            
            var temp = JsonConvert.DeserializeObject<List<int>>(UseProfile.ListSave);
            Debug.LogError(temp);
            if(temp == null)
            {
                var Newdata = new List<int>() { 1} ;
                UseProfile.ListSave = JsonConvert.SerializeObject(Newdata);
            }    
            else
            {
                temp.Add(UseProfile.CurrentLevel);
                UseProfile.ListSave = JsonConvert.SerializeObject(temp);
            }
            UseProfile.CurrentLevel += 1;
            Initiate.Fade(SceneName.HOME_SCENE, Color.black, 2f);
        }
   
       
    }
    public IEnumerator WaitFadeCanvas( )
    {

        yield return canvasGroupMain.DOFade(0,0.85f).WaitForCompletion();
         
    }
    public void HandleShowButton()
    {
        nextBtn.gameObject.SetActive(true);
        nextBtn.gameObject.transform.DOScale(Vector3.one,0.5f);
        GameController.Instance.musicManager.PlayWinSound();
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
