using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private string _name;
    private float _health;
    private int _team;
    private bool _win;

    private Vector3 _position;
    private GameObject mesh;

    public string SetName { get => _name; set => _name = value;}
    public float SetHealth  { get => _health; set => _health = value;}
    public int SetTeam { get => _team; set => _team = value;}
    public bool SetWin { get => _win; set => _win = value;}

    public Player(string Name, int Team, Vector3 pos) {
      _name = Name;
      _health = 100f;
      _team = Team;
      _win = false;
      _position = pos;
    }

    public Player() {
      _name = "Joe Doe";
      _health = 100;
      _team = 0;
      _win = false;
      _position = Vector3.zero;
    }

    public void AddMesh(GameObject m) //adds mesh to player
    {
        mesh = m;
    }

    public Vector3 Movement(float x, float z){ //set the player movement
        return Vector3.zero;
    }

    public void Shoot(Vector3 pos){
        // shoot the bullet
    }

    public Vector3 Spawn(float x, float z){
        return Vector3.zero;
    }

    public bool CheckWin(){
        return _win;
    }

}
