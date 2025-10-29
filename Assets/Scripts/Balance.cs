using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Survivor
{
    [Serializable]
    public class Balance
    {
        public int NumEnemies;
        public float EnemyVelocity;
        public float EnemyRadius;
        public float SpawnRadius;
        public float PlayerVelocity;
        public float MinCollisionDistance;
        public float SpawnTime;
    }
}