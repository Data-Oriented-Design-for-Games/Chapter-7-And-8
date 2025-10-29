using System;
using System.IO;
using UnityEngine;

namespace Survivor
{
    public static class GameDataIO
    {
        public static void Save(GameData gameData, Balance balance)
        {
            Debug.LogFormat("SaveGame()");

            if (!Directory.Exists(Application.persistentDataPath + "/DODSurvivor"))
                Directory.CreateDirectory(Application.persistentDataPath + "/DODSurvivor");

            string fileName = Application.persistentDataPath + "/DODSurvivor/gamedata.dat";
            using (FileStream fs = File.Create(fileName))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                int version = 2;
                bw.Write(version);

                bw.Write(gameData.InGame);

                bw.Write(balance.NumEnemies);
                for (int i = 0; i < balance.NumEnemies; i++)
                {
                    bw.Write(gameData.AliveEnemyIndices[i]);
                    bw.Write(gameData.DeadEnemyIndices[i]);
                }
                bw.Write(gameData.AliveEnemyCount);
                bw.Write(gameData.DeadEnemyCount);

                bw.Write(gameData.SpawnTime);

                for (int i = 0; i < balance.NumEnemies; i++)
                {
                    bw.Write(gameData.EnemyPosition[i].x);
                    bw.Write(gameData.EnemyPosition[i].y);
                }

                bw.Write(gameData.PlayerDirection.x);
                bw.Write(gameData.PlayerDirection.y);

                bw.Write(gameData.GameTime);
            }
        }

        public static void Load(GameData gameData)
        {
            string fileName = Application.persistentDataPath + "/DODSurvivor/gamedata.dat";
            if (File.Exists(fileName))
            {
                using (FileStream stream = File.Open(fileName, FileMode.Open))
                using (BinaryReader br = new BinaryReader(stream))
                {
                    int version = br.ReadInt32();

                    if (version >= 2)
                        gameData.InGame = br.ReadBoolean();

                    int numEnemies = br.ReadInt32();

                    for (int i = 0; i < numEnemies; i++)
                    {
                        gameData.AliveEnemyIndices[i] = br.ReadInt32();
                        gameData.DeadEnemyIndices[i] = br.ReadInt32();
                    }
                    gameData.AliveEnemyCount = br.ReadInt32();
                    gameData.DeadEnemyCount = br.ReadInt32();

                    gameData.SpawnTime = br.ReadSingle();

                    for (int i = 0; i < numEnemies; i++)
                    {
                        gameData.EnemyPosition[i].x = br.ReadSingle();
                        gameData.EnemyPosition[i].y = br.ReadSingle();
                    }

                    gameData.PlayerDirection.x = br.ReadSingle();
                    gameData.PlayerDirection.y = br.ReadSingle();

                    gameData.GameTime = br.ReadSingle();
                }
            }
        }

        public static bool SaveGameExists()
        {
            bool inGame = false;
            string fileName = Application.persistentDataPath + "/DODSurvivor/gamedata.dat";
            if (File.Exists(fileName))
            {
                using (FileStream stream = File.Open(fileName, FileMode.Open))
                using (BinaryReader br = new BinaryReader(stream))
                {
                    int version = br.ReadInt32();

                    inGame = br.ReadBoolean();
                }
            }
            return inGame;
        }
    }
}