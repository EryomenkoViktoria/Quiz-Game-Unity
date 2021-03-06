using System.Collections.Generic;
using UnityEngine;

namespace QuizGame.Data
{
    [CreateAssetMenu(fileName = "New Cells Data", menuName = "Cells Data", order = 10)] // для отображения в меню при создании даты данных
    public class CellsData : ScriptableObject
    {
        [SerializeField]
        private List<CardData> _cardData;

        internal List<CardData> CardData => _cardData;
    }
}