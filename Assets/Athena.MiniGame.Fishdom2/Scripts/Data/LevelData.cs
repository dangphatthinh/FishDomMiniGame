using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2.Data
{
    [System.Serializable]
    public class LevelData 
    {
        public readonly int totalTile;
        public readonly int firstIndex;
        public readonly IList<GameObjectTile> tile;
        public readonly IList<CameraPosition> cameraPos;
        public readonly IList<TextObject> textPos;
        public readonly IList<CharacterObject> charPos;

        public int TotalTile => totalTile;
        public int FirstIndex => firstIndex;
        public IList<GameObjectTile> Tile => tile;
        public IList<CameraPosition> CameraPos => cameraPos;
        public IList<TextObject> TextPos => textPos;
        public IList<CharacterObject> CharPos => charPos;

        public LevelData(object jsonObj)
        {
            totalTile = jsonObj.parseJsonInt("totalTile");
            firstIndex = jsonObj.parseJsonInt("firstIndex");
            tile = jsonObj.parseJsonObjList("ObjectTile", t => convertObj(t));
            cameraPos = jsonObj.parseJsonObjList("cameraPos", t => convertCamPos(t));
            textPos = jsonObj.parseJsonObjList("TextPosition", t => convertTextPos(t));
            charPos = jsonObj.parseJsonObjList("CharacterPosition", t => convertCharPos(t));
        }

        private GameObjectTile convertObj (object jsonObj)
        {
            return new GameObjectTile(jsonObj);
        }
        private CameraPosition convertCamPos(object jsonObj)
        {
            return new CameraPosition(jsonObj);
        }
        private TextObject convertTextPos(object jsonObj)
        {
            return new TextObject(jsonObj);
        }
        private CharacterObject convertCharPos(object jsonObj)
        {
            return new CharacterObject(jsonObj);
        }
    }

    [System.Serializable]
    public class GameObjectTile : MonoBehaviour
    {
        public readonly float xPos;
        public readonly float yPos;
        public readonly int tileType;
        public int value;
        public readonly string calculation;
        public readonly int state;
        public readonly int statechangeflag;

        public float XPos => xPos;
        public float YPos => yPos;
        public int TileType => tileType;
        public int Value
        {
            get => value;
            set => this.value = value;
        }
        public string Calculation => calculation;
        public int State => state;
        public int StateChangeFlag => statechangeflag;

        public GameObjectTile(object jsonObj)
        {
            xPos = jsonObj.parseJsonFloat("xPos");
            yPos = jsonObj.parseJsonFloat("yPos");
            tileType = jsonObj.parseJsonInt("tileType");
            value = jsonObj.parseJsonInt("value");
            calculation = jsonObj.parseJsonString("calculation");
            state = jsonObj.parseJsonInt("state");
            statechangeflag = jsonObj.parseJsonInt("statechangeflag");
        }
    }
    public class CameraPosition
    {
        public readonly float x;
        public readonly float y;
        public readonly float z;
        public readonly float size;

        public float X => x;
        public float Y => y;
        public float Z => z;
        public float Size => size;

        public CameraPosition(object jsonObj)
        {
            x = jsonObj.parseJsonFloat("x");
            y = jsonObj.parseJsonFloat("y");
            z = jsonObj.parseJsonFloat("z");
            size = jsonObj.parseJsonFloat("size");

        }
    }
           public class TextObject
    {
        public readonly float xTextPos;
        public readonly float yTextPos;

        public float XTextPos => xTextPos;
        public float YTextPos => yTextPos;

        public TextObject(object jsonObj)
        {
            xTextPos = jsonObj.parseJsonFloat("xTextPos");
            yTextPos = jsonObj.parseJsonFloat("yTextPos");
        }
    }
    public class CharacterObject
    {
        public readonly float xCharPos;
        public readonly float yCharPos;

        public float XCharPos => xCharPos;
        public float YCharPos => yCharPos;

        public CharacterObject(object jsonObj)
        {
            xCharPos = jsonObj.parseJsonFloat("xCharPos");
            yCharPos = jsonObj.parseJsonFloat("yCharPos");
        }
    }
}

