using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Block", menuName = "Block")]
public class Block : ScriptableObject
{
    public Transform Block_transform;
    public int ID;
}
