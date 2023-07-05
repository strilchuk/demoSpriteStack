using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stack object", menuName = "StackObject")]
public class SpriteStackSO : ScriptableObject
{
    public List<Sprite> stack;
}
