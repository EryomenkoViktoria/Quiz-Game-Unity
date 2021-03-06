using System.Collections.Generic;
using UnityEngine;

namespace QuizGame.Data
{
    [CreateAssetMenu(fileName = "New Field Data", menuName = "Field Data", order = 10)]
    public class FieldData : ScriptableObject
    {
        [SerializeField]
        private int subLevels;

        //[SerializeField]
        //private int summLevels;

        [SerializeField]
        private List<LevelData> levelData;

        internal int SubLevel => subLevels;

        //internal int SummLevels => summLevels;

        internal List<LevelData> LevelData => levelData;

    }
}
