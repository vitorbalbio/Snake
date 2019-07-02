// Nota:
// Por conta da simplicidade do projeto essa classe ficou subutilizada
// Mas em projetos reais sempre que posso utilizo um ou mais UI Managers como este para centralizar
// chamadas da UI (Eventualmente um por tela) e ativar eventos em outras entidades da UI.
// Isso me permite separar melhor o VIEW do CONTROLLER


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_UIManager : MonoBehaviour
{
    // Painéis
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject inGamePanel;

    // Cache de Elementos
    [SerializeField] private Text score;
    [SerializeField] private Text inGameScore;

    public static Controller_UIManager uiManager;

    public void Awake() {
        uiManager = this;
    }

    // Transições
    public void GoToMainMenu(int _score) {
        mainMenuPanel.SetActive(true);
        inGamePanel.SetActive(false);
        UpdateBestScore(_score);
    }

    public void GoToInGame() {
        mainMenuPanel.SetActive(false);
        inGamePanel.SetActive(true);
    }

    // Eventos da UI

    public void StartGame_Click() {
        GoToInGame();
        Controller_GameManager.main.StartGame();
        // Outros eventos e animações vão aqui
    }

    // Elementos da UI
    public void UpdateBestScore(int _score) {
        // Atualiza o Placar
        score.text = _score.ToString();
    }

    public void UpdateCurrentScore(int _score) {
        inGameScore.text = _score.ToString();
    }

}
