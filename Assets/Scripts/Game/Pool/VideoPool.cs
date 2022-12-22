namespace ARPG.BasePool
{
    public class VideoPool : BasePool<UIVideoItem>
    {
        protected override void Awake()
        {
            base.Awake();
            Init();
        }
    }
}

