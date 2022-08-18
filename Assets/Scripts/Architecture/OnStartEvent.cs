using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    [Range(0, 600), SerializeField] private float delayActivation = 0;
    [SerializeField] private bool showConsoleMessage = true;
    public UnityEvent startEvent;


    private IEnumerator Start()
    {
        if (showConsoleMessage) Debug.Log($"OnStartEvent \"{gameObject.name}\" started. Delay time activation: {delayActivation}s");
        yield return new WaitForSeconds(delayActivation);
        startEvent?.Invoke();
        if (showConsoleMessage) Debug.Log($"OnStartEvent \"{gameObject.name}\" called.");
        yield break;
    }
}
