using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceColorDictionary : MonoBehaviour
{
    [Serializable, SerializeField]
    private struct ColorSprite
    {
        public ColorType color;
        public Sprite sprite;
    }

    [SerializeField] private ColorSprite[] colorSprites;

    private Dictionary<ColorType, Sprite> colorSpriteDict;

    public int NumberColors => colorSprites.Length;

    private void Awake()
    {
        colorSpriteDict = new Dictionary<ColorType, Sprite>();

        for (int i = 0; i < colorSprites.Length; i++)
        {
            if (!colorSpriteDict.ContainsKey(colorSprites[i].color))
            {
                colorSpriteDict.Add(colorSprites[i].color, colorSprites[i].sprite);
            }
        }
    }

    public Sprite GetSpriteByColor(ColorType color)
    {
        if (colorSpriteDict.ContainsKey(color))
            return colorSpriteDict[color];
        
        return null;
    }
}
