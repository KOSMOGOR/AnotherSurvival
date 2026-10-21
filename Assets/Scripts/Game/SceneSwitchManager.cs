using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : ISceneSwitchManager
{
    public async UniTask SwitchSceneAsync(string sceneName)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);
        await loadScene;
    }
}
