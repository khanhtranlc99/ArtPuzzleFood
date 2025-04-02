using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class ButtonLevel : MonoBehaviour
{
    public int idLevel;
    public GameObject blind;
    public Button btnClick;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        if(UseProfile.CurrentLevel >= idLevel)
        {
            blind.gameObject.SetActive(false);
        }
        else
        {
            blind.gameObject.SetActive(true);
        }
        btnClick.onClick.AddListener(HandleButtonOnClick);
    }    
    public void HandleButtonOnClick()
    {
        UseProfile.LevelEggChest = idLevel;
     Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);
    }
    IEnumerator ChangeScene()
    {
        UseProfile.LevelEggChest = this.idLevel;
   
       
        string name = "";
    
        name = SceneName.GAME_PLAY;
        var _asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

        while (!_asyncOperation.isDone)
        {
        
            yield return null;


        }
    }
}
