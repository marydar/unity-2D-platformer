using UnityEngine.Events;
using UnityEngine;
public class CharacterEvents {
    public static UnityAction<GameObject, int> characterDamaged;
    public static UnityAction<GameObject, int> characterHealed;
}