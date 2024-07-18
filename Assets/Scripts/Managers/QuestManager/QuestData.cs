using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest")]
public class QuestData : ScriptableObject
{
    [SerializeField] public string Instruction;
    [SerializeField] public string TagEntityToCount;
    [SerializeField] public string QuestType;
}
