using System;
using UnityEngine;

public class Border : MonoBehaviour
{
    
    [Serializable]
    public struct BorderData
    {
        public Size size;
        public Position position;
    }

    [Serializable]
    public struct Size
    {
        public Sizes x;
        public Sizes y;
    }
    
    [Serializable]
    public struct Position
    {
        public Positions x;
        public Positions y;
    }

    [Serializable]
    public enum Positions
    {
        Default,
        TopY,
        BottomY,
        LeftX,
        RightX
    }
    
    [Serializable]
    public enum Sizes
    {
        Default,
        Width,
        Height
    }

    private class BorderObject
    {
        private Vector2 _size;
        private Vector2 _position;

        public Vector2 Size
        {
            get => _size;
            set => _size = value;
        }
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
    }

    [SerializeField] private BorderData[] _borders;
    [SerializeField] private float _defaultSize = 0.1f;
    [SerializeField] private GameObject[] _spritesPrefab;

    private void Start()
    {
        foreach (var border in _borders)
        {
            var obj = GetObject(border);
            GenerateBorder(obj.Size, obj.Position);
        }
        
        GenerateSprite(_spritesPrefab[0]);
        GenerateSprite(_spritesPrefab[1], true);
    }

    private BorderObject GetObject(BorderData data)
    {
        var size = new Vector2(GetSize(data.size.x), GetSize(data.size.y));
        var position = new Vector2(GetPosition(data.position.x), GetPosition(data.position.y));
        var border = new BorderObject();
        border.Position = position;
        border.Size = size;
        return border;
    }

    private float GetSize(Sizes size)
    {
        return size switch
        {
            Sizes.Height => DisplayManager.Height,
            Sizes.Default => _defaultSize,
            Sizes.Width => DisplayManager.Width,
            _ => _defaultSize
        };
    }

    private float GetPosition(Positions position)
    {
        return position switch
        {
            Positions.Default => 0,
            Positions.TopY => DisplayManager.TopY + _defaultSize/2,
            Positions.BottomY => DisplayManager.BottomY - _defaultSize/2,
            Positions.LeftX => DisplayManager.LeftX - _defaultSize/2,
            Positions.RightX => DisplayManager.RightX + _defaultSize/2,
            _ => 0
        };
    }
    
    private void GenerateBorder(Vector2 size, Vector2 position) 
    {
        var collider2D = gameObject.AddComponent<BoxCollider2D>();
        collider2D.size = size;
        collider2D.offset = position;
    }

    private void GenerateSprite(GameObject prefab, bool collider = false)
    {
        var obj = Instantiate(prefab, transform);
        var sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) return;
     
        transform.localScale = new Vector3(1,1,1);
     
        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;
     
        var worldScreenHeight = DisplayManager.Height;
        var worldScreenWidth = DisplayManager.Width;

        obj.transform.localScale = new Vector2(worldScreenWidth / width, worldScreenHeight / height);
        if(collider) obj.AddComponent<PolygonCollider2D>();
    }
}
