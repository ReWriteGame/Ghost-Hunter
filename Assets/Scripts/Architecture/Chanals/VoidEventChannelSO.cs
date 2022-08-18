using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    [SerializeField] private bool showConsoleMessage = true;
    public UnityAction OnEventRaised;

	public void RaiseEvent()
	{
        if(showConsoleMessage) Debug.Log($"Event \"{this.name}\" called.");
		if (OnEventRaised != null)
			OnEventRaised.Invoke();
	}
}


