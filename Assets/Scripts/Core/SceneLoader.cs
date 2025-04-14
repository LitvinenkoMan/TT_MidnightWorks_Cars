using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("   Scene Data")]
        [SerializeField]
        private string SceneName;

        [Space(10)]
        [Header("Settings")]
        [SerializeField] private bool MakeItMain;
        [SerializeField] private bool UseMainIfDataIsNull;
        [SerializeField] private bool UnloadInsteadOfLoading;
        [SerializeField] private bool LoadSceneAsync = true;
        [SerializeField] private bool LoadOnStart;

        [Space(10)]
        [SerializeField] private UnityEvent<string> OnScenLoaded;
        [SerializeField] private UnityEvent<string> OnScenUnloaded;
        
        private string _activeUnloadedSceneName;
        
        private event Action<AsyncOperation> OnSceneLoadedAction;
        private event Action<AsyncOperation> OnSceneUnloadedAction;

        void Start()
        {
            if (LoadOnStart)
            {
                LoadScene();
            }
        }

        private void OnEnable()
        {
            OnSceneLoadedAction += SceneLoaded;
            OnSceneUnloadedAction += SceneUnloaded;
        }

        private void OnDisable()
        {
            OnSceneLoadedAction -= SceneLoaded;
            OnSceneUnloadedAction -= SceneUnloaded;
        }

        public void LoadScene()
        {
            if (SceneName != null)
            {
                if (SceneManager.GetSceneByName(SceneName).isLoaded && !UnloadInsteadOfLoading)
                {
                    return;
                }
            }
        
            if (UnloadInsteadOfLoading)
            {
                if (UseMainIfDataIsNull)
                {
                    _activeUnloadedSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()).completed += OnSceneUnloadedAction;
                }
                else if (SceneManager.GetSceneByName(SceneName) != null)
                    SceneManager.UnloadSceneAsync(SceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects).completed += OnSceneUnloadedAction;
            }
            else
            {
                if (LoadSceneAsync)
                {
                    SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive).completed += OnSceneLoadedAction;
                }
                else
                {
                    SceneManager.LoadScene(SceneName);
                }
            }
        }

        private void SceneUnloaded(AsyncOperation op)
        {
            OnScenUnloaded?.Invoke(UseMainIfDataIsNull ? _activeUnloadedSceneName : SceneName);
        }

        private void SceneLoaded(AsyncOperation op)
        {
            OnScenLoaded?.Invoke(SceneName);
            if (MakeItMain)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));
            }
        
        }
    }
}