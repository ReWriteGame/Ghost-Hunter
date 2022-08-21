using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tools
{
    public class EnableObject : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objects;

        private void Awake()
        {
            SetStateAllObjects(false);
            EnableRandomObject();
        }

        private void EnableRandomObject()
        {
            objects[Random.Range(0,objects.Count)].SetActive(true);
        }

        private void SetStateAllObjects(bool state)
        {
            objects.ForEach(x=> x.SetActive(state));
        }
    }
}
