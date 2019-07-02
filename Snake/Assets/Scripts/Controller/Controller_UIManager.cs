// Nota:
// Sempre que posso utilizo um ou mais UI Managers como este para centralizar
// chamadas da UI (Eventualmente até Um por tela/Painel) 
// e para ativar eventos em entidades da UI.
// Isso me permite separar melhor o "VIEW" do "CONTROLLER"

// Costumo evitar o uso de Listeners e Delegates e verificações constantes no Update 
// para atualizar componentes da UI como vida/score/pontuação etc..
// preferindo a atualização por eventos quando eles ocorrem para 
// diminuir esse tipo de verificação ou chamadas.


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

        // Singleton
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
        UpdateCurrentScore(0);
    }

    // Eventos da UI

    public void StartGame_Click() {
        GoToInGame();
        Controller_GameManager.main.StartGame();
        
        // Outros eventos e animações vão aqui
    }

    // Elementos da UI

    // Atualiza o Best Score
    public void UpdateBestScore(int _score) {
        // Atualiza o Placar
        score.text = _score.ToString();
    }

    // Atualiza o Score Corrente
    public void UpdateCurrentScore(int _score) {
        inGameScore.text = _score.ToString();
    }

}
