using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Managers
{
    /// <summary>
    /// Simple prefab-based object pool. Pools by prefab reference.
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance { get; private set; }

        [System.Serializable]
        public class PoolConfig
        {
            public GameObject prefab;
            public int prewarmCount = 0;
        }

        [SerializeField]
        private List<PoolConfig> prewarmPools = new List<PoolConfig>();

        private readonly Dictionary<GameObject, Queue<GameObject>> pools = new();
        private readonly Dictionary<GameObject, GameObject> instanceToPrefab = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Prewarm configured pools
            foreach (var cfg in prewarmPools)
            {
                if (cfg.prefab == null || cfg.prewarmCount <= 0) continue;
                EnsurePool(cfg.prefab);
                for (int i = 0; i < cfg.prewarmCount; i++)
                {
                    var obj = CreateNew(cfg.prefab);
                    Return(obj);
                }
            }
        }

        private void EnsurePool(GameObject prefab)
        {
            if (prefab == null) return;
            if (!pools.ContainsKey(prefab))
                pools[prefab] = new Queue<GameObject>();
        }

        private GameObject CreateNew(GameObject prefab)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            instanceToPrefab[obj] = prefab;
            // Add helper component if missing
            if (obj.GetComponent<PooledObject>() == null)
                obj.AddComponent<PooledObject>();
            return obj;
        }

        public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (prefab == null)
            {
                Debug.LogError("PoolManager.Get called with null prefab");
                return null;
            }

            EnsurePool(prefab);
            GameObject obj = null;
            var q = pools[prefab];
            while (q.Count > 0 && obj == null)
            {
                obj = q.Dequeue();
            }
            if (obj == null)
            {
                obj = CreateNew(prefab);
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);
            return obj;
        }

        public void Return(GameObject instance)
        {
            if (instance == null) return;
            if (!instanceToPrefab.TryGetValue(instance, out var prefab))
            {
                // If instance wasn't created by pool, try to infer via PooledObject
                var po = instance.GetComponent<PooledObject>();
                if (po != null && po.SourcePrefab != null)
                {
                    prefab = po.SourcePrefab;
                    instanceToPrefab[instance] = prefab;
                }
                else
                {
                    // As a fallback, disable the instance and destroy to avoid leaks
                    instance.SetActive(false);
                    Destroy(instance);
                    return;
                }
            }

            instance.SetActive(false);
            EnsurePool(prefab);
            pools[prefab].Enqueue(instance);
        }

        public bool TryGetPrefab(GameObject instance, out GameObject prefab)
        {
            return instanceToPrefab.TryGetValue(instance, out prefab);
        }
    }

    /// <summary>
    /// Helper component to allow pooled instances to know their source prefab.
    /// </summary>
    public class PooledObject : MonoBehaviour
    {
        public GameObject SourcePrefab { get; private set; }

        private void Awake()
        {
            // If this object is a prefab instance created by PoolManager, it will be registered there.
            // For editor-placed objects, allow manual assignment via inspector by storing the prefab reference when first returned.
        }

        // Optional: allow manual assignment for editor-spawned objects
        public void SetSourcePrefab(GameObject prefab)
        {
            SourcePrefab = prefab;
        }

        private void OnDisable()
        {
            // No-op: returning to pool is controlled by gameplay scripts
        }
    }
}

