using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A flexible handler for void events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
/// </summary>
public class VoidEventListener : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO channel = default;
    [SerializeField, Range(0, 30)] private float delayActivation = 0;

    public UnityEvent OnEventRaised;
    
    
    private void OnEnable()
    {
        if (channel != null)
            channel.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        if (channel != null)
            channel.OnEventRaised -= Respond;
    }

    private void Respond()
    {
        StartCoroutine(RespondCor());
    }
    
    private IEnumerator RespondCor()
    {
        yield return new WaitForSeconds(delayActivation);
        CallEvent();
        yield break;
    }
    private void CallEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
