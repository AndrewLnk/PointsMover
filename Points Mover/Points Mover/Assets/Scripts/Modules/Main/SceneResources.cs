using UnityEngine;

namespace Modules
{
    public static class SceneResources
    {
        private static Transform resourcesObject;

        private static Transform ResourcesObject
        {
            get
            {
                if (resourcesObject == null)
                    TryFindResourcesObject();

                return resourcesObject;
            }
        }

        public static GameObject Get(string objectName) => ResourcesObject.Find(objectName).gameObject;

        public static void Set(GameObject gameObject) => gameObject.transform.SetParent(ResourcesObject);

        public static GameObject GetPreparedCopy(string objectName)
        {
            var origin = Get(objectName);
            var copy = Object.Instantiate(origin).gameObject;
            copy.transform.SetParent(null);
            copy.name = origin.name;

            return copy;
        }

        private static void TryFindResourcesObject()
        {
            resourcesObject = GameObject.Find("Resources").transform;
            
            if (resourcesObject == null)
                DebugLog.LogRed("Resources Object Not Found");
            
            resourcesObject.gameObject.SetActive(false);
        }
    }
}
