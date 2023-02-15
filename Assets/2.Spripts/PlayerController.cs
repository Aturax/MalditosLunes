using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    [SerializeField] private TMP_Text _livesText = null;
    [SerializeField] private TMP_Text _stepText = null;
    private CharacterController _characterController = null;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 _spawnPosition = Vector3.zero;    
    private float _gravity = -9.8f;
    private int _lives = 3;
    private int _currentStep = 0;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _spawnPosition = transform.position;
        UpdateStep();
    }

    private void Update()
    {
        CaptureInput();
        Move();
    }

    private void CaptureInput()
    {
        float horizontal = Input.GetAxisRaw("Vertical");
        float vertical = Input.GetAxisRaw("Horizontal");

        Vector2 input = new Vector2(-vertical, -horizontal).normalized;
        moveDirection = new Vector3(input.x, _gravity, input.y) * _speed * Time.deltaTime;        
    }

    private void Move()
    {
        _characterController.Move(moveDirection);
    }

    private void Spawn()
    {
        transform.position = _spawnPosition;
        Physics.SyncTransforms();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            OnDeath();
        }
        else if (hit.gameObject.CompareTag("Objective"))
        {
            if (_currentStep == (int)hit.gameObject.GetComponent<Objectives>().GetStep())
                _currentStep++;
            UpdateStep();
            Destroy(hit.gameObject);
        }
    }

    private void OnDeath()
    {
        _lives--;
        _livesText.text = _lives.ToString();

        if (_lives > -1)
        {
            Spawn();
        }
        else
        {
            _livesText.text = "0";
            gameObject.gameObject.SetActive(false);
        }
    }

    private void UpdateStep()
    {
        if (_currentStep < 4)
            _stepText.text = ((Step)_currentStep).ToString();
        else
            gameObject.gameObject.SetActive(false);
    }
}
