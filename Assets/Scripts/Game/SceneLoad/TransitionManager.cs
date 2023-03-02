using System;
using System.Collections;
using ARPG;
using ARPG.Config;
using ARPG.UI.Config;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Transition
{
    public class TransitionManager : MonoSingleton<TransitionManager>
    {
        [Header("游戏开始默认场景"),Scene]
        public string startSceneName = String.Empty;
        private bool isFade;
        private CanvasGroup fadeGroup;
        private const float fadeDuration = 1.5f;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(LoadScnen("UI"));
        }
        
        private void Start()
        {
            fadeGroup = GameObject.FindWithTag("fadeGroup").GetComponent<CanvasGroup>();
            StartCoroutine(LoadAsynScnen(startSceneName));
        }
        
        private void OnEnable()
        {
            MessageAction.TransitionEvent += TransitionScnen;
            MessageAction.StartGameScene += StarGameScen;
        }
        
        private void OnDisable()
        {
            MessageAction.TransitionEvent -= TransitionScnen;
            MessageAction.StartGameScene -= StarGameScen;
        }

        private void StarGameScen(string SceneName,Vector3 pos,CharacterBag data,RegionItem regionItem)
        {
            if (!isFade)
                StartCoroutine(StartGameScene(SceneName,pos,data,regionItem));
        }

        private IEnumerator StartGameScene(string ScnenName, Vector3 pos,CharacterBag data,RegionItem regionItem)
        {
            yield return Transition(ScnenName, pos, GameManager.Instance.StarSceneGame(data, pos,regionItem));
            EnemyManager.Instance.PlayEnemy();
        }


        private void TransitionScnen(string ScnenName, Vector3 pos)
        {
            if (!isFade)
                StartCoroutine(Transition(ScnenName, pos));
        }

        /// <summary>
        /// 加载新场景(叠加): 注意
        /// </summary>
        /// <param name="ScnenName">场景名称</param>
        public void TransitionScnen(string ScnenName)
        {
            if(!isFade)
                StartCoroutine(Transition(ScnenName));
        }

        private IEnumerator LoadScnen(string scnemName)
        {
            yield return SceneManager.LoadSceneAsync(scnemName,LoadSceneMode.Additive);
        }

        private IEnumerator LoadAsynScnen(string scnenName)
        {
            yield return SceneManager.LoadSceneAsync(scnenName,LoadSceneMode.Additive);
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
            MessageAction.OnAfterScenenLoadEvent();
        }
        
        /// <summary>
        ///  场景切换
        /// </summary>
        /// <param name="sceneName">切换的场景名称</param>
        /// <param name="targetpos">目标场景</param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName,Vector3 targetpos)
        {
            //卸载当前场景
            MessageAction.OnBeforScenenUnloadEvent();
            yield return Fade(1);
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return LoadAsynScnen(sceneName);
            MessageAction.OnMovToPosint(targetpos);
            yield return Fade(0);
            MessageAction.OnAfterScenenLoadEvent();
            
        }
        
        
        /// <summary>
        /// 场景切换，并且等待func事件执行完毕
        /// </summary>
        /// <param name="sceneName">切换的场景名称</param>
        /// <param name="targetpos">目标场景</param>
        /// <param name="func"></param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName,Vector3 targetpos,IEnumerator func)
        {
            //卸载当前场景
            MessageAction.OnBeforScenenUnloadEvent();
            yield return Fade(1);
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return LoadAsynScnen(sceneName);
            yield return func;
            yield return Fade(0);
            MessageAction.OnAfterScenenLoadEvent();
            
        }
        
        private IEnumerator Transition(string sceneName)
        {
            //卸载当前场景
            yield return Fade(1);
            yield return LoadAsynScnen(sceneName);
            yield return Fade(0);
            MessageAction.OnAfterScenenLoadEvent();
            
        }

        /// <summary>
        /// 淡入淡出场景
        /// </summary>
        /// <param name="targetAlpha">0是淡出,1是淡入</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            if (fadeGroup == null)
                fadeGroup = FindObjectOfType<CanvasGroup>();
            fadeGroup.blocksRaycasts = true;
            float speed = Mathf.Abs(fadeGroup.alpha - targetAlpha)/ fadeDuration;
            while (!Mathf.Approximately(fadeGroup.alpha,targetAlpha))
            {
                fadeGroup.alpha = Mathf.MoveTowards(fadeGroup.alpha, targetAlpha, speed * UnityEngine.Time.deltaTime);
                yield return null;
            }
            fadeGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}

