using UnityEngine;

[CreateAssetMenu(fileName = "ObjectListData", menuName = "ContainerSO/ObjectListData", order = 1)]
public class ObjectListData : ScriptableObject
{
    [SerializeField] private Object[] objects;
    public Object[] Objects { get => objects; private set => objects = value; }
}
