using JetBrains.Annotations;
using UnityEngine;

public class PlayerCharacterProfile : Entity
{
    public CharacterData CharacterData;

    public override void SetData<T>(T data)
    {
        if (data is CharacterData characterData)
        {
            base.SetData(characterData.EntityData);
            CharacterData = characterData;
        }
    }

    public override T GetData<T>()
    { 
        if (typeof(T) == typeof(CharacterData))
        {
            CharacterData.EntityData = base.GetData<EntityData>(); 
            return (T)(object)CharacterData;
        }
        return default;
    }
}
