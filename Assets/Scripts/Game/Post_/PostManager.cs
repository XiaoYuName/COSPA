using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ARPG
{
    public class PostManager : MonoSingleton<PostManager>
    {
        private SceneVolumeConifg _config;
        private VolumeProfile profile;

        protected override void Awake()
        {
            base.Awake();
            _config = ConfigManager.LoadConfig<SceneVolumeConifg>("GameIni/SceneVolume");
        }

        /// <summary>
        /// 开启闪烁动画
        /// </summary>
        /// <param name="endTime">结束阶段时间</param>
        /// <param name="action">动画播放完毕回调</param>
        /// <param name="starTime">开始阶段时间</param>
        /// <param name="mode">回调的时机</param>
        public void StarTween(float starTime,float endTime,Action action,FuncMode mode)
        {
            profile = _config.GetActiveSceneVolumeProfile();
            ColorAdjustments component = profile.components[0] as ColorAdjustments;
            StartCoroutine(WaitTween(component,starTime,endTime,action,mode));
        }

        public IEnumerator WaitTween(ColorAdjustments component,float starTime,float EndTime,Action action,FuncMode mode)
        {
            if(mode == FuncMode.Star)
                action?.Invoke();
            component.postExposure.value = 0f;
            yield return DOTween.To(() => component.postExposure.value, x => component.postExposure.value = x, 5, starTime).WaitForCompletion();
            
            if(mode == FuncMode.Crent)
                action?.Invoke();

            component.postExposure.value = 5;
            yield return DOTween.To(() => component.postExposure.value, x => component.postExposure.value = x, 0, EndTime).WaitForCompletion();
            component.postExposure.value = 0f;
            if(mode == FuncMode.End)
                action?.Invoke();
        }
        
        
        
        
    }
}

