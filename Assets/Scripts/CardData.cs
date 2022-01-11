using System;
using UnityEngine;

namespace QuizGame.Data
{
    [Serializable]
    public class CardData
    {
        [SerializeField]
        private string identifier;

        [SerializeField]
        private Sprite sprite;

        internal string Identifier => identifier;

        internal Sprite Sprite => sprite;
    }
}
