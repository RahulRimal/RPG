using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned;

        void Awake()
        {
            if(hasSpawned)
                return;

            spawnPersistentObjects();

            hasSpawned = true;
        }

        void spawnPersistentObjects()
        {
            GameObject persistentObject =  Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }




    }
}