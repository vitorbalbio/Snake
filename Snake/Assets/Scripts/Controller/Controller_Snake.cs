using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Snake : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;

    // Inicializado com o Head no Prefab
    [SerializeField] private List<Model_Element> elements; 

    private enum DirectionType {Up,Down,Left,Right};
    private DirectionType currentDirection = DirectionType.Right;

    private enum CollisionType {None,Self,Board,Item}
    private Vector2 lastEndPosition;

    private bool canChangeDirection = true;

    // Atualiza Snake. 
    // Chamado pelo GameManager Main Loop.
    public void UpdateSnake() {

        // Atualiza posições
        lastEndPosition = elements[elements.Count - 1].Position;
        MoveBody();
        MoveHead();

        // Verifica colisões e Eventos
        CheckCollisions();
        canChangeDirection = true;
    }

    // Verifica Colisões
    private void CheckCollisions() {
        Model_Element head = elements[0];

        // Colisão com Item
        if(head.Position == Controller_GameManager.main.item.Position) {
            Controller_GameManager.main.EventItemCollided();
            IncreaseSnake();
        }

        // Colisão consigo mesmo
        for (int i = 1; i < elements.Count; i++) {
            if (head.Position == elements[i].Position) {
                Controller_GameManager.main.EventSnakeCollided();
            }
        }

        // Colisão com borda
        if (head.Position[1] > Controller_GameManager.BOARDSIZE-1 ||
           head.Position[0] > Controller_GameManager.BOARDSIZE-1 ||
           head.Position[1] < 0 ||
           head.Position[0] < 0) {
            Controller_GameManager.main.EventSnakeCollided();
        }

        return;
    }

    // Aumenta o Snake
    public void IncreaseSnake() {
        // Cria e instancia um novo segmento
        Model_Element tempElement = new Model_Element();
        tempElement.Model = GameObject.Instantiate(bodyPrefab);
        tempElement.Position = lastEndPosition;
        tempElement.Model.transform.position = new Vector3(tempElement.Position[0],
                                                            0,
                                                            tempElement.Position[1]);
        tempElement.Model.transform.parent = this.transform;
        tempElement.Model.transform.name = "Body";

        elements.Add(tempElement);
    }

    // Atualiza posições do corpo
    private void MoveBody() {
        // Atualiza outros Elementos, precisa percorrer de baixo para cima
        for (int i = elements.Count-1; i >= 1; i--) {
            elements[i].Position = elements[i - 1].Position;
            // Aplica no modelo3D
            elements[i].Model.transform.position = new Vector3(elements[i].Position[0],
                                                                0,
                                                                elements[i].Position[1]);
        }
    }

    // Atualiza posição do Head
    private void MoveHead() {

        // Atualiza a posição conforme a direção
        switch (currentDirection){
            case DirectionType.Up:
                elements[0].Position += new Vector2(0,1);
                break;
            case DirectionType.Down:
                elements[0].Position += new Vector2(0,-1);
                break;
            case DirectionType.Left:
                elements[0].Position += new Vector2(-1, 0);
                break;
            case DirectionType.Right:
                elements[0].Position += new Vector2(1, 0);
                break;
        }

        // Aplica no modelo3D
        elements[0].Model.transform.position = new Vector3( elements[0].Position[0],
                                                            0,
                                                            elements[0].Position[1]);

    }

    public void Update() {

        // ## Nota:
        // Por conta da simplicidade do projeto seria pedante separar um Manager de Inputs
        // apenas para realizar essas verificações, mas dependendo da complexidade pode ser
        // interessante separar melhor essa camada de "VIEW" do "CONTROLLER" e se aproximar mais
        // de um MVC tradicional principalmente quando um mesmo input ativa multiplos eventos
        // e precisa ser selecionado por contexto.

        // Previne que mais de uma mudança de direção seja feita por GameLoop
        // e Impede rotações maiores que 90º
        if (!canChangeDirection) { return; }

        if (Input.GetKeyDown(KeyCode.UpArrow) && currentDirection != DirectionType.Down) {
            currentDirection = DirectionType.Up;
            canChangeDirection = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentDirection != DirectionType.Up) {
            currentDirection = DirectionType.Down;
            canChangeDirection = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection != DirectionType.Right) {
            currentDirection = DirectionType.Left;
            canChangeDirection = false;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow) && currentDirection != DirectionType.Left) {
            currentDirection = DirectionType.Right;
            canChangeDirection = false;
            return;
        }
    }

    // Outras Funções e Utilities

    // Retorna todas as posições ocupadas pelo Snake
    public List<Vector2> GetAllPositions() {
        List<Vector2> listPositions = new List<Vector2>();
        for (int i = 0; i < elements.Count; i++) {
            listPositions.Add(elements[i].Position);
        }
        return listPositions;
    }

}
