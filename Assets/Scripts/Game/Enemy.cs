using System;
using Modules;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour, IDestroyable<GameObject>
    {
        public UnityEvent<Enemy> OnDeath;
        public event Action<GameObject> OnDestroyObj;
        public void Death()
        {
            OnDeath?.Invoke(this);
            Destroy();
        }

        public void Destroy(float time = 0)
        {
            OnDestroyObj?.Invoke(gameObject);
            Destroy(gameObject, time);
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Destroyer>())
                Destroy();
        }
    }
}
