using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;

public class MixerGroupController : MonoBehaviour
{
    [SerializeField]private string exposedParameters;
    [SerializeField]private AudioMixerGroup mixerGroup;
    [SerializeField]private Slider slider;

    public UnityEvent onVolumeEvent;
    public UnityEvent offVolumeEvent;
    public UnityEvent changeVolumeEvent;


    public const float minValue = -80;
    private float currentVolume;
    private bool lastStateVolumeOn;
    
    
    public void Off()
    {
        ToggleMusic(false);
    }
    public void On()
    {
        ToggleMusic(true);
    }
    public void ChangeVolume(float volume)
    {
        mixerGroup.audioMixer.SetFloat(exposedParameters, Mathf.Lerp(minValue,0,volume));
    }
    public void ToggleMusic(bool enabled)
    { 
        mixerGroup.audioMixer.SetFloat(exposedParameters, enabled ? 0 : minValue);
    }
    public void UpdateSliderPosition()
    {
        float tmp = currentVolume;
        mixerGroup.audioMixer.GetFloat(exposedParameters,out currentVolume);
        if(tmp != currentVolume)changeVolumeEvent?.Invoke();
        
        slider.value = 1 - currentVolume / minValue;
    }
    public bool VolumeIsOn()
    {
        UpdateSliderPosition();
        return currentVolume == minValue;
    }
    private void OnEnable()
    {
        lastStateVolumeOn = VolumeIsOn();
    }
    private void FixedUpdate()
    {
        UpdateSliderPosition();
        if (VolumeIsOn() && lastStateVolumeOn)
        {
            lastStateVolumeOn = false;
            offVolumeEvent?.Invoke();
        }
        else if (!VolumeIsOn() &&!lastStateVolumeOn)
        {
            lastStateVolumeOn = true;
            onVolumeEvent?.Invoke();
        }
    }
}

//can be errors if volume in mixerGroup > 0