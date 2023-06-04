using UnityEngine;

public class sprite_container : MonoBehaviour
{
    public GraphicObject[] Objects;
    public static sprite_container instance;
    public sprite_container()
    {
        instance = this;
    }
    public AnimationSet[] request(string name)
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i].index == name)
            {
                return Objects[i].SpriteSet.animations;
            }
        }

        return new AnimationSet[0];
    }

    public Sprite GetDefaultSprite(string name)
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i].index == name)
                return Objects[i].SpriteSet.defaultSprite;
        }
        return null;
    }
}
[System.Serializable]
public struct GraphicObject
{
    public string index;
    public spriteset SpriteSet;
}