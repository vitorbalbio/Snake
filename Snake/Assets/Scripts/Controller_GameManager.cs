// Nota:
// Costumo estruturar meus projetos com prefixos que identifiquem a função do script.
// Utilizo "Controller_" para Mono Behaviors que exercem função de modelar e 
// controlar outras entidades (Managers) ou si próprio seguindo o pattern
// "Entidade-Componente" da Unity porém tendo em vista a manter o MVC quando possível.

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller_GameManager : MonoBehaviour
{

    // Prefabs
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private GameObject itemPrefab;

    // Cache de componentes críticos (Evita o overhead por GetComponent)
    public Controller_Item item;
    public Controller_Snake snake;

    // PlaceHolder para manter outros elementos da cena
    public Transform Scene;

    // Game States
    public enum GameState {Menu,Play};
    public GameState gameState;

    // Dados de Persistência
    public int bestScore;
    public int BestScore { get => bestScore; set => bestScore = value; }

    private int currentScore;

    // Controle de Temporização
    [SerializeField] private float gameSpeed;
    private int frameCount;

    // Singleton
    public static Controller_GameManager main;

    // Constantes
    public const int BOARDSIZE = 10;

    private void Awake() {
        // Nota:
        // Uso Singletons nos Gerenciadores de hierarquia mais alta no game
        // por exemplo nos "Level Managers", "Game Managers", "UI Managers" etc
        // Isso facilita o acesso e a verificação de estados globais do game por entidades
        // de menor hierarquia e diminui o uso de delegates/actions melhorando 
        // a legibilidade do código.

        // Nesses casos a Unicidade da classe é garantida pela estrutura do projeto 
        // então a declaração é direta.
        main = this;

        LoadPersitence(); 
    }

        
    private void Start() {
        
        // Inicializa Outros Managers
        Controller_UIManager.uiManager.ShowMainMenu(BestScore);
    }


    // Carrega Dados de Persistência
    private void LoadPersitence() {
        BestScore = PlayerPrefs.GetInt("BestScore");
    }

    // Salva Score
    private void SaveScore() {
        if(currentScore > BestScore) {
            BestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
    }

    private void FixedUpdate() {

        if(gameState == GameState.Menu) {
            return;
            // Aqui entraria algum código global que precisasse rodar no Menu
        }

        if(gameState == GameState.Play) {
            // Nota:
            // FixedUpdate garante (em condições normais) a execução do código em 1/50 segundos. 
            // Logo gameSpeed determina a quantidade de atualizações por segundo do game.

            frameCount++;
            if ((frameCount * gameSpeed) < 50) {
                return;
            }

            frameCount = 0;
            UpdateGameLoop();
        }       

    }

    // Inicializa uma nova sessão do game
    public void StartGame() {

        gameState = GameState.Play;

        // Instancia e Inicializa o Snake
        GameObject snakeTmp = GameObject.Instantiate(snakePrefab);
        snake = snakeTmp.GetComponent<Controller_Snake>(); // Cache do componente principal
        snakeTmp.transform.position = new Vector3(0, 0, 0);
        snakeTmp.transform.parent = Scene;

        // Instancia e Inicializa o Item
        GameObject itemTmp = GameObject.Instantiate(itemPrefab);
        item = itemTmp.GetComponent<Controller_Item>(); // Cache do componente principal
        itemTmp.transform.parent = Scene;

        //  Reposiciona o item
        item.SetPosition(GetUniquePosition());

    }

    // Finaliza uma sessão do game
    private void EndGame() {
        gameState = GameState.Menu;

        // Destroy objetos dinâmicos
        if (snake != null) { Destroy(snake.gameObject); }
        if (item != null) { Destroy(item.gameObject); }

        // Verifica e salva o ultimo score se necessário
        SaveScore();

        // Volta a mostrar o menu principal
        Controller_UIManager.uiManager.ShowMainMenu(BestScore);
    }

    // Substitui o "Update()" como loop principal do game.
    private void UpdateGameLoop() {
        snake.UpdateSnake();
    }

    // Eventos Globais
    
    // Nota:
    // Geralmente faço a comunicação de eventos que afetam várias entidades de forma
    // centralizada em managers (de Cena, ou de Game). Isso facilita a leitura do código
    // já que com algumas dezenas de entidades comunicando entre si fica mais difícil
    // entender a cadeia de mensagens.

    // Eventos globais que ocorrem quando um item é consumido
    public void EventItemCollided() {
        // Aumenta a velocidade
        gameSpeed += 0.1f;

        //  Reposiciona o item
        item.SetPosition(GetUniquePosition());

        // Adiciona Pontuação
        currentScore++;

        // Aqui poderiam entrar outros eventos como efeitos de particulas, animações etc...

    }

    // Eventos globais que ocorrem quando ocorre colisão (self ou borda)
    public void EventSnakeCollided() {
        EndGame();
        // Aqui poderiam entrar outros eventos como efeitos de particulas, animações etc...
    }

    // Outras Funções e Utilities

    // Retorna uma posição ainda não ocupada do board
    public Vector2 GetUniquePosition() {

        // Pega uma lista de todas as posições usadas pelo Snake + a do próprio item
        List<Vector2> listPositions = snake.GetAllPositions();
        listPositions.Add(item.Position);

        // Gera uma lista com cada posição possível da Board
        List<Vector2> BoardModel = new List<Vector2>();
        for (int i = 0; i < BOARDSIZE; i++) {
            for (int j = 0; j < BOARDSIZE; j++) {
                BoardModel.Add(new Vector2(i, j));
            }
        }

        // Seleciona apenas as posições Disponíveis
        listPositions = BoardModel.Except(listPositions).ToList();

        // Pega alguma posição aleatória Disponivel;
        return listPositions[Random.Range(0, listPositions.Count)];
    }



}
