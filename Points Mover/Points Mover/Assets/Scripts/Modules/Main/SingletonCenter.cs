using UnityEngine;

namespace Modules
{
    public class SingletonCenter : MonoBehaviour
    {
        private static SingletonCenter _instance;

        public static SingletonCenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("_instances");
                    go.AddComponent<SingletonCenter>();
                }

                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                if (this != _instance)
                    Destroy(this.gameObject);
            }
        }

    }
}