using QuizGame.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace QuizGame.Core
{
    internal class GeneratorTaskGame : MonoBehaviour
    {
        [SerializeField]
        private CellsData cellsData;

        [SerializeField]
        private FieldData fieldData;

        private List<CardData> cardData;
        private List<LevelData> levelData;

        internal List<CardData> elementsPlayingField = new List<CardData>();

        private List<CardData> numberRepeatCheck = new List<CardData>();

        #region Events
        internal delegate void TaskFind(string name);
        internal delegate void SpawnCells(bool subLevel);
        internal delegate void ClearGameField();
        internal delegate void GameOver();
        internal delegate void IncorrectChoice(Sprite sprite);
        internal delegate void CorrectChoice(Sprite sprite);

        internal event TaskFind OnSetNameFind;
        internal event SpawnCells OnSpawn;
        internal event ClearGameField OnClearGameField;
        internal event GameOver OnGameOver;
        internal event IncorrectChoice OnIncorrectChoice;
        internal event CorrectChoice OnCorrectChoice;
        #endregion

        [SerializeField]
        private UnityEvent OnEvent;

        private int levelNow { get; set; }

        private int subLevelNow { get; set; }

        private int checkSubLevel { get; set; }

        private int creatGameCellsLevelOne { get; set; }
        private int creatGameCellsLevelTwo { get; set; }
        private int creatGameCellsLevelThree { get; set; }

        private bool elementRepeat { get; set; }

        private string checkerFind { get; set; }

        private void Start()
        {
            AssignmentOfData();
            CheckLevelsInData();
            GeneratorCells(levelNow);
        }
        
        private void AssignmentOfData()
        {
            cardData = cellsData.CardData;
            levelData = fieldData.LevelData;
            checkSubLevel = fieldData.SubLevel;
        }

        private void CheckLevelsInData()
        {
            creatGameCellsLevelOne = levelData[0].LevelNumber * levelData[0].Cells;
            creatGameCellsLevelTwo = levelData[1].LevelNumber * levelData[1].Cells;
            creatGameCellsLevelThree = levelData[2].LevelNumber * levelData[2].Cells;
        }

        private void GeneratorCells(int level)
        {
            switch (level)
            {
                case 0:
                    CreatePlayingField(creatGameCellsLevelOne);
                    break;
                case 1:
                    CreatePlayingField(creatGameCellsLevelTwo);
                    break;
                case 2:
                    CreatePlayingField(creatGameCellsLevelThree);
                    break;
                case 3:
                    OnGameOver?.Invoke();
                    break;
                default:
                    Debug.Log("LEVEL ERROR");
                    GeneratorCells(0);
                    break;
            }
        }

        private void CreatePlayingField(int sizeField)
        {
            int cellRecording = 0;
            do
            {
                var symbolConvert = cardData[Random.Range(0, cardData.Count)];
                for (int a = 0; a < elementsPlayingField.Count; a++)
                {
                    if (symbolConvert == elementsPlayingField[a])
                        elementRepeat = true;
                }
                if(!elementRepeat)
                {
                    elementsPlayingField.Add(symbolConvert);
                    cellRecording++;
                }
                elementRepeat = false;
            }
            while (cellRecording < sizeField);

            if (subLevelNow == 0)
            {
                subLevelNow++;
                OnSpawn?.Invoke(true);
            }
            else
                OnSpawn?.Invoke(false);

            FindesCells();
        }

        
        private void FindesCells()
        {
            var FindTaskIndex = elementsPlayingField[Random.Range(0, elementsPlayingField.Count)];

            if(numberRepeatCheck.Count > 1) 
            {
                for (int i = 0; i < numberRepeatCheck.Count; i++)
                {
                    if (FindTaskIndex == numberRepeatCheck[i])
                    {
                        elementsPlayingField.RemoveAt(i);
                        FindTaskIndex = elementsPlayingField[Random.Range(0, elementsPlayingField.Count)];
                    }
                }
            }

            OnSetNameFind?.Invoke(FindTaskIndex.Identifier.ToString());
            numberRepeatCheck.Add(FindTaskIndex);

            if(numberRepeatCheck.Count > 1)
                numberRepeatCheck.RemoveAt(0);

            checkerFind = FindTaskIndex.Identifier;
        }

        internal async void CharacterSelectionCheck(Sprite indexSprite)
        {
            if (indexSprite.name == checkerFind)
            {
                OnEvent?.Invoke();
                OnCorrectChoice?.Invoke(indexSprite);
                await Task.Delay(2000);

                OnClearGameField?.Invoke();
                elementsPlayingField.Clear();

                if (subLevelNow >= checkSubLevel)
                {
                    GeneratorCells(levelNow++);
                    subLevelNow = 0;
                }
                else
                {
                    GeneratorCells(levelNow);
                    subLevelNow++;
                }
            }
            else
            {
                OnIncorrectChoice?.Invoke(indexSprite);
            }
        }
    }
}