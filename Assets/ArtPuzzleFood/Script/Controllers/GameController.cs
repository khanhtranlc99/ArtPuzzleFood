using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif



public class GameController : MonoBehaviour
{

    public static GameController Instance;

    public MoneyEffectController moneyEffectController;
    public UseProfile useProfile;
    public DataContain dataContain;
    public MusicManagerGameBase musicManager;
    public AdmobAds admobAds;

    public AnalyticsController AnalyticsController;
    public IapController iapController;
    public HeartGame heartGame;
    [HideInInspector] public SceneType currentScene;

    public StartLoading startLoading;

    protected void Awake()
    {
        Instance = this;
        Init();

        DontDestroyOnLoad(this);

        GameController.Instance.useProfile.IsRemoveAds = true;


#if UNITY_IOS

    if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == 
    ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
    {

        ATTrackingStatusBinding.RequestAuthorizationTracking();

    }

#endif

    }

    private void Start()
    {
        //   musicManager.PlayBGMusic();

    }

    public void Init()
    {
        Application.targetFrameRate = 60;
        SetUp();
    }

    public void SetUp()
    {
        admobAds.Init();
        musicManager.Init();
        iapController.Init();
        MMVibrationManager.SetHapticsActive(useProfile.OnVibration);
        startLoading.Init();
        heartGame.Init();

    }

    public void LoadScene(string sceneName)
    {
        Initiate.Fade(sceneName.ToString(), Color.black, 2f);
    }

    public GameObject objHack;
    public TMP_InputField inputField;
    private int numberValue;
    public void HandleOnOff()
    {
        if (objHack.activeSelf == true)
        {
            objHack.SetActive(false);
        }
        else
        {
            objHack.SetActive(true);
        }
    }


    public void HandleNextLevel()
    {

        string inputText = inputField.text;

        // chuyển thành số
        if (int.TryParse(inputText, out numberValue))
        {
            UseProfile.LevelEggChest = numberValue;
            Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);
        }


    }


    public void HandleUnLockAll()
    {


        var temp = JsonConvert.DeserializeObject<List<int>>(UseProfile.ListSave);

        var Newdata = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        UseProfile.ListSave = JsonConvert.SerializeObject(Newdata);

        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);
    }


    public void HandlePlusHint()
    {

        UseProfile.Hint_Booster += 10;
        List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        giftRewardShows.Add(new GiftRewardShow() { amount = 10, type = GiftType.Hint_Booster });
        PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { });

    }




    

}
public enum SceneType
{
    StartLoading = 0,
    MainHome = 1,
    GamePlay = 2
}