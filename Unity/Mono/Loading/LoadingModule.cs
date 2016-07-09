using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingModule : MonoBehaviour {

    [SerializeField]
    Text process;

    //源场景，目标场景
    string oriScene, destScene;

    void Awake()
    {
        //按照预设框架，在此处获取跳转的 【源场景-目标场景】 键值对
        //并在协程中完成加载和跳转
        destScene = "Demo3";
    }

	// Use this for initialization
	void Start () 
    {
        StartCoroutine( ChangeScene() );
	}
	
    /// <summary>
    /// 异步加载场景，按每帧递增1显示加载进度值
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeScene()
    {
        //加上这么一句就可以先显示加载画面然后再进行加载
        yield return new WaitForEndOfFrame();
        //显示加载进度
        int displayProgress = 0;
        //每一帧目标加载进度（加载超过90%时，将直接修改为100%）
        int toProgress = 0;
        //异步加载目标场景
        AsyncOperation op = SceneManager.LoadSceneAsync( "Demo3" );
        //场景加载完毕不立刻执行脚本（目前只知道 true : op.progress 立刻为1，且即刻进入新场景，加载完成后再修改为false无效）
        //要完成加载过程需要在此处设为false，如果最后不设为true，将会将新场景加载完成但不跳转
        op.allowSceneActivation = false;
        //未知原因只能停留在0.9f，故先只能分段处理显示
        while( op.progress < 0.9f )
        {
            toProgress = (int)op.progress * 100;
            //按帧增加
            while( displayProgress < toProgress )
            {
                ++displayProgress;
                process.text = "加载进度：" + ( displayProgress ).ToString();
                yield return new WaitForEndOfFrame();
            }
        }
        //达到0.9后，手动调整目标加载进度到100
        toProgress = 100;
        while( displayProgress < toProgress )
        {
            //同样按帧增加
            ++displayProgress;
            process.text = "加载进度：" + ( displayProgress ).ToString();
            yield return new WaitForEndOfFrame();
        }
        //新加载场景激活进入
        op.allowSceneActivation = true;
    }
}
