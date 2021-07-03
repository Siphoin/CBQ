using System.Collections;
using UnityEngine;

namespace UI.LoadingSystem
{
    public class LoadingWaitManager : MonoBehaviour, IInitObject, IRemoveObject
    {
        #region Constants
        private const string PATH_PREFAB_LOADING_WAIT = "Prefabs/UI/LoadingWait";
        #endregion

        #region Fields
        private static LoadingWaitManager manager;

        private LoadingWait loadingWaitActive;
        private LoadingWait loadingWaitPrefab;
        #endregion


        #region Properties
        public static LoadingWaitManager Manager { get => manager; }
        #endregion


        #region Init manager

        void Awake()
        {
            Init();
        }
        public void Init()
        {
            if (manager == null)
            {
                loadingWaitPrefab = Resources.Load<LoadingWait>(PATH_PREFAB_LOADING_WAIT);

                if (!loadingWaitPrefab)
                {
                    throw new LoadingWaitManagerException("prefab loading wait not found");
                }


                manager = this;

                DontDestroyOnLoad(gameObject);
            }
        }
        #endregion

        public void Remove()
        {
            Destroy(gameObject);
        }

        public void Remove(float time)
        {
            Destroy(gameObject, time);
        }

        // Use this for initialization

        public LoadingWait NewLoadingWait ()
        {
            if (loadingWaitActive)
            {
                return loadingWaitActive;
            }

            loadingWaitActive = Instantiate(loadingWaitPrefab);
            return loadingWaitActive;
        }

    }
}