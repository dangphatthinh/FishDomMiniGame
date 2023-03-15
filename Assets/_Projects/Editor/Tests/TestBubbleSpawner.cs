using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBubbleSpawner
{
    // Data.LevelData _levelData;

    // [SetUp]
    // public void SetUp()
    // {
    //     //load level data
    //     var levelId = 1;
    //     var dataPath = string.Format(C.ResourcePaths.LevelDataPath, levelId);
    //     var dataText = Resources.Load<TextAsset>(dataPath);
    //     var jsonObj = GenericsJSONParser.JsonDecode(dataText.text);
    //     _levelData = new Data.LevelData(jsonObj);
    // }

    // [Test]
    // public void TestSpawnBubbleCountIsCorrect()
    // {
    //     var bubbleController = new Gameplay.Controllers.Logic.BubbleHelper();
    //     var spawner = new Gameplay.Controllers.Logic.BubbleSpawner();
    //     spawner.CreateBubbleDelegate = createBubble;
    //     spawner.Init(bubbleController);
    //     spawner.LoadLevel(_levelData);

    //     Assert.AreEqual(bubbleController.Bubbles.Count, 9 * 11 + 9 * 10);
    // }

    // [Test]
    // public void TestSpawnBulletIsCorrect()
    // {
    //     var bubbleController = new Gameplay.Controllers.Logic.BubbleHelper();
    //     var spawner = new Gameplay.Controllers.Logic.BubbleSpawner();
    //     spawner.CreateBubbleDelegate = createBubble;
    //     spawner.Init(bubbleController);
    //     var bullet = spawner.CreateBubbleForShooting(1);

    //     Assert.AreEqual(bullet.ColorId.Value, 1);
    //     Assert.NotNull(bullet.Bullet);
    // }

    // [Test]
    // public void TestCreateSystem()
    // {
    //     // Contexts contexts = new Contexts();
    //     // Entitas.Systems systems = new Entitas.Systems();
    //     // CreateEntitySystem createEntitySystem = new CreateEntitySystem(contexts);
    //     // systems.Add(createEntitySystem);
    //     // systems.Initialize();

    //     // Assert.True(contexts.game.count == 1);
    // }

    // private Gameplay.MyObjects.IBubble createBubble()
    // {
    //     return new TestBubble();
    // }

    // public class TestBubble : Gameplay.MyObjects.IBubble
    // {
    //     public Gameplay.Components.HexCell HexCell { get; set; }
    //     public Gameplay.Components.ColorId ColorId { get; set; }
    //     public Gameplay.Components.Destroyer Destroyer { get; set; }
    //     public Gameplay.Components.Bullet Bullet { get; set; }
    //     public Gameplay.Components.Hitable Hitable { get; set; }

    //     public Vector3 Position
    //     {
    //         get { return Vector3.zero; }
    //         set { }
    //     }

    //     public void Init()
    //     {
    //     }

    //     public void Shake(Vector2 shakePosition)
    //     {
    //     }

    //     public void Fall(Vector2 position, float duration)
    //     {

    //     }
    // }
}
