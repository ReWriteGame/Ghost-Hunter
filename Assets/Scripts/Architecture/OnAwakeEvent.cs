using UnityEngine;
using UnityEngine.Events;

public class OnAwakeEvent : MonoBehaviour
{
    [SerializeField] private bool showConsoleMessage = true;
    public UnityEvent awakeEvent;

    private void Awake()
    {
        if (showConsoleMessage) Debug.Log($"OnAwakeEvent \"{gameObject.name}\" called.");
        awakeEvent?.Invoke();
    }
}
