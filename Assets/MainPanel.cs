using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SettingColor
{
    Red,
    Blue,
    Brown
}


class AudioSettings
{
    internal ObservableProperty<int> MusicVolume = new ObservableProperty<int>(0, 0, 50);
    internal ObservableProperty<int> EffectVolume = new ObservableProperty<int>(0, 0, 50);
    internal ObservableProperty<int> VoiceVolume = new ObservableProperty<int>(0, 0, 50);
    internal ObservableProperty<string> AudioDevice = new ObservableProperty<string>("Option2", new string[] { "Option1", "Option2", "Option3", "Option4" });
    internal ObservableProperty<bool> UseVsync = new ObservableProperty<bool>(false);
    internal ObservableProperty<string> Text = new ObservableProperty<string>("aaa");
}

public class MainPanel : MonoBehaviour
{
    AudioSettings audioSettings = new AudioSettings();

    public UISlider uiMusicVolumeSlider;
    public UILabel uiMusicVolumeText;
    public UISlider uiEffectVolumeSlider;

    void Start()
    {


        uiMusicVolumeSlider.BindTo(audioSettings.MusicVolume);
        uiMusicVolumeText.BindTo(audioSettings.Text);
        uiEffectVolumeSlider.BindTo(audioSettings.EffectVolume);
        GetComponentInChildren<UIDropdown>().BindTo(audioSettings.AudioDevice);

        transform.Find("txtText").GetComponent<UILabel>().BindTo(audioSettings.AudioDevice);

        transform.Find("RightPanel").Find("Toggle").GetComponent<UIToggle>().BindTo(audioSettings.UseVsync);


        transform.Find("RightPanel").Find("InputField").GetComponent<UIInputField>().BindTo(audioSettings.Text);


        //uiMusicVolumeText.TextFormatter = (value) =>
        //{
        //    return string.Format("{0:0.##}", value);
        //};

        //musicVolume.Set(4);
    }

    private void UseVsync_OnChanged(ObservableProperty arg1, object arg2)
    {
        Debug.Log(arg1.AsBool());
    }

    private void AudioCard_OnChanged(ObservableProperty arg1, object source)
    {
        var s = arg1.AsString();
    }

    private void MusicVolume_OnChanged(ObservableProperty p, object source)
    {
        float v= p.AsFloat();
    }

    private void Update()
    {
        if (Random.value < 0.01f)
        {
            //musicVolume.Set(Random.Range(0, 100));
        }
    }
}
