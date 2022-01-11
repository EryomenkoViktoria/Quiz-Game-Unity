using QuizGame.Cells;
using QuizGame.Core;
using UnityEngine;

namespace QuizGame.Spawner
{
    internal class Spawner : MonoBehaviour
    {
        [SerializeField]
        GameObject prefabGameCell;

        [SerializeField]
        Transform contentList;

        GeneratorTaskGame generatorTaskGame;

        private void Start()
        {
            generatorTaskGame = FindObjectOfType<GeneratorTaskGame>();
            generatorTaskGame.OnSpawn += SpawnPlayningField;
        }

        private void OnDisable()
        {
            generatorTaskGame.OnSpawn -= SpawnPlayningField;
        }

        private void SpawnPlayningField(bool subLevel)
        {
            for (int i = 0; i < generatorTaskGame.elementsPlayingField.Count; i++)
                Instantiate(prefabGameCell, contentList).GetComponent<Cell>().SetSprite(generatorTaskGame.elementsPlayingField[i], subLevel);
        }
    }
}