using QuizGame.Core;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using QuizGame.Data;

namespace QuizGame.Cells
{
    internal class Cell : MonoBehaviour
    {
        [SerializeField]
        private Button cellButton;

        [SerializeField]
        private Image image;

        GeneratorTaskGame generatorTaskGame;

        private void Start()
        {
            cellButton.onClick.AddListener(ClicCellButton);
            generatorTaskGame = FindObjectOfType<GeneratorTaskGame>();
            generatorTaskGame.OnClearGameField += DestroyCellButton;
            generatorTaskGame.OnIncorrectChoice += IncorrectChoice;
            generatorTaskGame.OnCorrectChoice += CorrectChoice;
        }

        private void OnDisable()
        {
            generatorTaskGame.OnClearGameField -= DestroyCellButton;
            generatorTaskGame.OnIncorrectChoice -= IncorrectChoice;
            generatorTaskGame.OnCorrectChoice -= CorrectChoice;
        }

        internal void SetSprite(CardData cardData, bool subLevel)
        {
            image.sprite = cardData.Sprite;

            if (cardData.Identifier == "7" || cardData.Identifier == "8")
                transform.Rotate(0, 0, -90);

            if(subLevel)
            {
                image.transform.DOPunchScale(Vector3.up, 20, 3);
                image.transform.DOPunchScale(Vector3.right, 20, 3);
            }
        }

        private void ClicCellButton()
        {
            generatorTaskGame.CharacterSelectionCheck(image.sprite);
        }

        private void DestroyCellButton()
        {
            gameObject.SetActive(false);
        }

        private void CorrectChoice(Sprite sprite)
        {
            if (image.sprite == sprite)
            {
                cellButton.interactable = false;
                image.transform.DOPunchScale(Vector3.up, 20, 3);
                image.transform.DOPunchScale(Vector3.right, 20, 3);
            }
        }

        private void IncorrectChoice(Sprite sprite)
        {
            if (image.sprite == sprite)
                image.transform.DOShakePosition(2, 30);
        }
    }
}
