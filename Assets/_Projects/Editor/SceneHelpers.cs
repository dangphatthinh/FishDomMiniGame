using System;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneHelpers
{
    [MenuItem("ZScenes/Open Entrance Scene %#e")]
    public static void OpenEntranceScene()
    {
        EditorSceneManager.OpenScene("Assets/_Projects/Scenes/bootstrap.unity");
    }

    [MenuItem("ZScenes/Open Game Scene %#g")]
    public static void OpenGameScene()
    {
        EditorSceneManager.OpenScene("Assets/_Projects/Scenes/game.unity");
    }
}
