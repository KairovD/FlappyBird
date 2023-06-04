using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IAppsFlyerConversionData
{
    [Tooltip("IDoubleSideTransition, for toggling different difficulties")]public Component Easy, Medium, Hard;
    public TextMeshProUGUI[] scoreVisualisations;

    public Slider volumeSlider;

    public TextMeshProUGUI volumeText;
    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty(PlayerPrefs.GetInt("Difficulty", 0));
        volumeSlider.value = PlayerPrefs.GetInt("Audio", 100);
        volumeText.text = PlayerPrefs.GetInt("Audio", 100).ToString();
        audiodata.instance.ReloadVolume();
        AppsFlyer.initSDK("ZLigGqGzDdxGMT7QBPjsMG", "com.testtask.flappybird", this);
        AppsFlyer.startSDK();
    }
    public void onConversionDataSuccess(string conversionData)
    {
        AppsFlyer.AFLog("onConversionDataSuccess", conversionData);
        Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
        // add deferred deeplink logic here
    }

    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("onConversionDataFail", error);
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
        // add direct deeplink logic here
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
    }

    public void Play()
    {
        SceneLoader.instance.LoadScene("Game");
    }
    public void SetDifficulty(int tier)
    {
        PlayerPrefs.SetInt("Difficulty", tier);
        if (tier == 0)
        {
            (Easy as IDoubleSideTransition).StartForwardTransition();
            (Medium as IDoubleSideTransition).StartReverseTransition();
            (Hard as IDoubleSideTransition).StartReverseTransition();
        }
        else if (tier == 1)
        {
            (Easy as IDoubleSideTransition).StartReverseTransition();
            (Medium as IDoubleSideTransition).StartForwardTransition();
            (Hard as IDoubleSideTransition).StartReverseTransition();
        }
        else if (tier == 2)
        {
            (Easy as IDoubleSideTransition).StartReverseTransition();
            (Medium as IDoubleSideTransition).StartReverseTransition();
            (Hard as IDoubleSideTransition).StartForwardTransition();
        }
        for (int i = 0; i < scoreVisualisations.Length; i++)
        {
            scoreVisualisations[i].text = PlayerPrefs.GetInt("Record" + PlayerPrefs.GetInt("Difficulty"), 0).ToString();
        }
    }

    public void setVolume()
    {
        PlayerPrefs.SetInt("Audio", Mathf.RoundToInt(volumeSlider.value));
        volumeSlider.value = PlayerPrefs.GetInt("Audio", 100);
        volumeText.text = PlayerPrefs.GetInt("Audio", 100).ToString();
        audiodata.instance.ReloadVolume();
    }
}
