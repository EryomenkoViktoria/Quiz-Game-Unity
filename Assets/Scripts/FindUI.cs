using QuizGame.Core;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuizGame.UI
{
    internal class FindUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI task;
        [SerializeField]
        private GameObject panelRestart, loadPanel;
        [SerializeField]
        private Button restartButton;
        GeneratorTaskGame generatorTaskGame;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
            generatorTaskGame = FindObjectOfType<GeneratorTaskGame>();
            generatorTaskGame.OnSetNameFind += GenerationTask;
            generatorTaskGame.OnGameOver += RestartPanelActivation;
        }

        private void OnDisable()
        {
            generatorTaskGame.OnSetNameFind -= GenerationTask;
            generatorTaskGame.OnGameOver -= RestartPanelActivation;
        }

        private void GenerationTask(string nameFind)
        {
            task.text = "Find " + nameFind;
        }

        private void RestartPanelActivation()
        {
            panelRestart.SetActive(true);
        }

        private async void RestartGame()
        {
            loadPanel.SetActive(true);
            await Task.Delay(1000);
            SceneManager.LoadScene("Game");
        }
    }
}