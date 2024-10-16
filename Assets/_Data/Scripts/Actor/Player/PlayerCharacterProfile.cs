using JetBrains.Annotations;
using UnityEngine;

public class PlayerCharacterProfile : Entity
{
    public CharacterData CharacterData;

    public override void SetData<T>(T data)
    {
        if (data is CharacterData characterData)
        {
            base.SetData<EntityData>(characterData.EntityData);
            CharacterData = characterData;
        }
    }

    public override T GetData<T>()
    {
        CharacterData.EntityData = base.GetData<EntityData>();

        if (CharacterData is T data)
        {
            return data;
        }
        else
        {
            return default;
        }
    }
}
