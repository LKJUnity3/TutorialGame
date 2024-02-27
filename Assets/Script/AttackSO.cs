using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "AttackSO", order = 1)]
public class AttackSO : ScriptableObject
{
    public Transform targetTransform;
    public GameObject AttackPrefab;

    public float speed = 10; 
    public int minProjectiles = 4;
    public int maxProjectiles = 6;
    public float angleBetweenProjectiles = 15f;
}