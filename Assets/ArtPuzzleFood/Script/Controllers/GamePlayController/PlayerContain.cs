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
        StartCoroutine(LoadLevelFromAssetBundle());
    }

    private IEnumerator LoadLevelFromAssetBundle()
    {
        string bundleName = string.Format("level_{0}", UseProfile.LevelEggChest);
        string prefabName = string.Format("Level_{0}", UseProfile.LevelEggChest);
        
        // Tạo đường dẫn asset bundle theo platform
        string bundlePath = "";
        #if UNITY_ANDROID
            bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath,  bundleName);
        #elif UNITY_IOS
            bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath,  bundleName);
        #else
            bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, bundleName);
        #endif
        Debug.Log($"bundlePath: {bundlePath}");
        // Thử load từ asset bundle trước
        if (System.IO.File.Exists(bundlePath))
        {
            Debug.Log($"File tồn tại: {bundlePath}");
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            if (bundle != null)
            {
                Debug.Log($"Load level {UseProfile.LevelEggChest} từ asset bundle: {bundlePath}");
                // Load prefab từ bundle
                GameObject levelPrefab = bundle.LoadAsset<GameObject>(prefabName);
                if (levelPrefab != null)
                {
                    // Instantiate prefab và lấy component LevelData
                    GameObject levelObj = Instantiate(levelPrefab);
                    levelData = levelObj.GetComponent<LevelData>();
                    
                    if (levelData != null)
                    {
                        levelData.transform.SetParent(postLevel, false);
                        levelData.Init(this);
                        boosterHint.Init();
                        bundle.Unload(false);
                        Debug.Log($"Đã load level {UseProfile.LevelEggChest} từ asset bundle: {bundlePath}");
                        yield break;
                    }
                    else
                    {
                        Debug.LogError($"Prefab không có component LevelData: {prefabName}");
                        DestroyImmediate(levelObj);
                    }
                }
                else
                {
                    Debug.LogError($"Không thể load prefab từ asset bundle: {bundleName}, prefab: {prefabName}");
                }
                bundle.Unload(true);
            }
            else
            {
                Debug.LogError($"Không thể load asset bundle: {bundlePath}");
            }
        }
        else
        {
            Debug.LogError($"File không tồn tại: {bundlePath}");
        }
        
        // Fallback về Resources nếu không load được từ asset bundle
      
      //  string pathLevel = StringHelper.PATH_CONFIG_LEVEL_TEST;
        // levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.LevelEggChest)));
        // levelData.transform.SetParent(postLevel, false);
        // levelData.Init(this);
        // boosterHint.Init();
    }

   


}
