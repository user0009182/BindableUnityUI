# BindableUnityUI
A simple implementation of two-way binding between unity UI controls and data objects. If you are familiar with binding in WPF or Angular, it's the same idea.

If you are not, then binding allows automatic synchronization between UI control content and underlying data object properties. Basically almost all the "glue" code you typically write between the game and UI, eg to handle a multitide of control events from one direction, and to set UI control values (eg set textfield text) from the other, is no longer needed. The binding system handles all the glue for you, hopefully saving time, reducing the chance of bugs and meaning cleaner/less code. Your game code can focus instead on reading and updating property values without knowing about how the UI itself works. 

This is not production code, it is not even alpha, it is experimental containing known bugs, non-ideal API, lacking documentation, missing features, etc. Take it as a rough example of how binding could work if you wanted to implement such a system yourself.

Usage:
First you define a data object containing game settings that use ObservableProperty<T> instead of regular C# types:

    class GameSettings
    {
        internal ObservableProperty<int> MusicVolume = new ObservableProperty<int>(0, 0, 50); //int in range 0-50 with start value 0
        internal ObservableProperty<int> EffectVolume = new ObservableProperty<int>(0, 0, 50);
        internal ObservableProperty<int> VoiceVolume = new ObservableProperty<int>(0, 0, 50);
        internal ObservableProperty<string> AudioDevice = new ObservableProperty<string>("Option2", new string[] { "Option1", "Option2", "Option3", "Option4" });
        internal ObservableProperty<bool> UseVsync = new ObservableProperty<bool>(false);
        internal ObservableProperty<string> Text = new ObservableProperty<string>("aaa");
    }

Then you can bind UI elements to any of these properties:

    uiMusicVolumeSlider.BindTo(gameSettings.MusicVolume);
    uiMusicVolumeLabel.BindTo(gameSettings.MusicVolume);

Synchronization is then automatic. The result of the above is that:
* The music volume slider control will be initialized to the correct min/max range on startup
* When the user moves the slider thumb:
  * the gameSettings.MusicVolume property value will be automatically updated
  * the uiMusicVolumeLabel text will be automatically updated to the new value of the property
* If the gameSettings.MusicVolume property value is changed directly in code:
  * the slider is automatically updated
  * the uiMusicVolumeLabel text is automatically updated

Another example, given the earlier definition of gameSettings.AudioDevice:
    uiDropdown.BindTo(gameSettings.AudioDevice);
    
The result is that:
* The drop down control is initialized to have "Option1", "Option2", "Option3", "Option4" as the available options
* The starting value of the dropdown control is "Option2"
* If the user selects an option in the drop down control, the value of gameSettings.AudioDevice is automatically updated

The only other thing that is needed is for each Unity UI component you want to be bindable, you must add a corresponding UInnnn component. This is best done through the editor, but here is an example done in code:
    
    Unity.UI.Slider slider = transform.Find("MySlider").GetComponent<Unity.UI.Slider>();
    var uiSlider = slider.GameObject.AddComponent<UISlider>;
    
These UInnnn components, UISlider, UILabel, UITextField, etc define the BindTo method and handle observing the bound property for changes.
  
