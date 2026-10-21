using Cysharp.Threading.Tasks;

public interface ISceneSwitchManager
{
    UniTask SwitchSceneAsync(string sceneName);
}
