using System;
using UnityEngine;

namespace QuizGame.Data
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private int levelNumber;

        [SerializeField] private int cells;

        internal int LevelNumber => levelNumber;

        internal int Cells => cells;
    }
}
