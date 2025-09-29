using Crystal;
using DG.Tweening;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateGame
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4
}

public class GamePlayController : Singleton<GamePlayController>
{
    public StateGame stateGame;
    public PlayerContain playerContain;
    public GameScene gameScene;
    public TutorialFunController tutorial_Level_1;
 
 
    
    protected override void OnAwake()
    {
        //  GameController.Instance.currentScene = SceneType.GamePlay;

     
        Init();

    }

    public void Init()
    {

   
        playerContain.Init();
        gameScene.Init(playerContain.levelData);
        UseProfile.FirstLoading = true;
        tutorial_Level_1.Init();

        tutorial_Level_1.StartTut();



    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            var temp = JsonConvert.DeserializeObject<List<int>>(UseProfile.ListSave);
            Debug.LogError(temp);
            if (temp == null)
            {
                var Newdata = new List<int>() { 1, 2 };
                UseProfile.ListSave = JsonConvert.SerializeObject(Newdata);
            }
            else
            {
                temp.Add(UseProfile.LevelEggChest + 1);
                UseProfile.ListSave = JsonConvert.SerializeObject(temp);
            }
            UseProfile.LevelEggChest += 1;
            Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);

        }    


    }



}
