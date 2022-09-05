using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]    //��� ����� �������. ���� ������ ������, ����� ���������� �� ��������� ����� ��������� ������ ������, ������������ ������
public class Interaction_Manager : MonoBehaviour
{
    //[HideInInspector] ��� ����� �������� ������
    [SerializeField] private GameObject _spawnedObjectPrefab;   // ��������� ��������� ���������� ������������ � ����� � �������������� ���������

    [HideInInspector]public int hello;

    private ARRaycastManager _aRRaycastManager;
    private List<ARRaycastHit> _raycastHits;

    //Awake ����������� ��� ������������� �����. ������ �������� ����� ���� �� ����. 
    //Start ����������� ����� �����. ��� ������� �� ����� ���������

    // Start is called before the first frame updateb
    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();   // [RequireComponent(typeof(ARRaycastManager))] - ��� ����� ������ ���, ����� ��� ������ ������
        if (_aRRaycastManager == null)
            throw new MissingComponentException("boba biba boba");
        _raycastHits = new List<ARRaycastHit>();
    }

    void Start()
    {

    }
        

    // Update is called once per frame
    private void Update()   // ������� ���������� ������ ����. ��� ������ ���� �����������.
    {
        if(Input.touchCount >0) // ������� ������ �����. ������������ ��� �����. ����, ��� ���������. ������� ���� ������������� �� ������?
        {
            ProcessFirstTouch(Input.GetTouch(0));   // �������� ������� ���������. 
        }
    }

    private void ProcessFirstTouch(Touch touch)
    {
        if(touch.phase == TouchPhase.Began)     // ��� ���� - ���� �����, (����� -������ �� ������������ � ��� �� ���������. ��������� - ����� �� ���������, ��� - ���������, ������- �������� �����?)
        {
            SpawnObject(touch);                 // ������� ������ ��� �����
        }
    }

    void SpawnObject (Touch touch)
    {
        _aRRaycastManager.Raycast(touch.position, _raycastHits, TrackableType.Planes);      // �������� ����� � ����� ������ � �������, ���������� ���������� � ��� ������ ����. 3 �������� - �� , � ��� ����������� ��� ���������. ���������� � ����
        Instantiate(_spawnedObjectPrefab, _raycastHits[0].pose.position, _spawnedObjectPrefab.transform.rotation); // ������� �������� �������. ������� ��������� , ���� ������.  ���� �������� �� ����� 100.
        //������� ����� �������, � ����� 1�� ����������� ���� � ����������. �� ����� � ���� ����, � ����� ������� � ������ ������� �������.
        // ��� � ��������� �����������, �������� ������� ���
    }
}
