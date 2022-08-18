using UnityEngine;

[CreateAssetMenu(fileName = "PrefabListData", menuName = "ContainerSO/PrefabListData", order = 1)]
public class PrefabListData : ScriptableObject
{
    [SerializeField] private GameObject[] prefabs;
    public GameObject[] Prefabs { get => prefabs; private set => prefabs = value; }
}
