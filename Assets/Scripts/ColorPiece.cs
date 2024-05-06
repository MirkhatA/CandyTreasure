using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPiece : MonoBehaviour
{
    public enum TypeOfColor {
        RED, GREEN, BLUE
    }

    [System.Serializable]
    public struct ColorSprite {
        public TypeOfColor color;
        public Sprite sprite;
    };

    public TypeOfColor color;
    public ColorSprite[] colorSprites;

    private SpriteRenderer _spriteName;
    private Dictionary<TypeOfColor, Sprite> colorSpriteDict;

    public TypeOfColor Color {
        get { return color; }
        set { SetColor(value); }
    }
    public int NumColors => colorSprites.Length; 

    private void Awake() {
        _spriteName = transform.Find("piece").GetComponent<SpriteRenderer>();

        colorSpriteDict = new Dictionary<TypeOfColor, Sprite>();

        for (int i = 0; i < colorSprites.Length; i++) if (!colorSpriteDict.ContainsKey(colorSprites[i].color)) colorSpriteDict.Add(colorSprites[i].color, colorSprites[i].sprite);
    }

    public void SetColor(TypeOfColor nColor) {
        color = nColor;

        if (colorSpriteDict.ContainsKey(nColor)) _spriteName.sprite = colorSpriteDict[nColor];
    }
}
