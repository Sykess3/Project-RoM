using System;
using Cysharp.Threading.Tasks;

namespace Code.Utils
{
    public class Timer
    {
        public bool IsReached { get; private set; }
        public async UniTask AwaitToEnd(float time)
        {
            IsReached = false;
            
            await UniTask.Delay(TimeSpan.FromSeconds(time), ignoreTimeScale: false);

            IsReached = true;
        }
    }
}