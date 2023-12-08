using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class CollectiblesInteractor : MonoBehaviour, Interactable
{
    public enum CollectiblesType
    {
        Gold,
        GreenHerb,
        RedHerb,
        NormalGunpowder,
        HighGradeGunpowder,
        HandGrenade,
        FlashGrenade,
        Revolver,
        GoldBarTreasure,
        RubyTreasure,
        EmeraldTreasure,
        EmblemKey,
        KeyCardKey,
        SpadeKey,
        HeartKey

    }
    public CollectiblesType collectibleType;
    public void Interact()
    {
        // check if inventory has place (depending on the type) ---- revolver has an extra check
        // Add if possible 
        // error message if not possible
        // destroy after finishing

        Debug.Log("Interacted with " + formatCollectibleTypeName(collectibleType));
        Destroy(gameObject);
    }

    public string GetInteractMessage()
    {
        return "Pick up " + formatCollectibleTypeName (collectibleType);
    }

    string formatCollectibleTypeName(Enum collectibleType)
    {
        string input = collectibleType.ToString();
        StringBuilder formattedName = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];

            if (char.IsUpper(currentChar) && i > 0)
            {
                formattedName.Append(' ').Append(currentChar);
            }
            else
            {
                formattedName.Append(currentChar);
            }
        }

        return formattedName.ToString();
    }


}
