using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]    //Тег перед классом. Этот скрипт теперь, когда накидываем на компонент будет требовать данный тайпоф, самопроверка короче
public class Interaction_Manager : MonoBehaviour
{
    //[HideInInspector] эта штука наоборот прячет
    [SerializeField] private GameObject _spawnedObjectPrefab;   // Позволяет приватной переменной отображаться в юнити в преднастройках редактора

    [HideInInspector]public int hello;

    private ARRaycastManager _aRRaycastManager;
    private List<ARRaycastHit> _raycastHits;

    //Awake запускается при инициализации сцена. Многих объектов может быть не быть. 
    //Start запускается много позже. Все объекты на сцене загружены

    // Start is called before the first frame updateb
    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();   // [RequireComponent(typeof(ARRaycastManager))] - эта штука делает так, чтобы тут ошибки небыло
        if (_aRRaycastManager == null)
            throw new MissingComponentException("boba biba boba");
        _raycastHits = new List<ARRaycastHit>();
    }

    void Start()
    {

    }
        

    // Update is called once per frame
    private void Update()   // Функция вызывается каждый кадр. Все должно быть легковесным.
    {
        if(Input.touchCount >0) // Система инпута юнити. Обрабатывает все вводы. Тачи, под мобилками. Сколько есть прикосновений на экране?
        {
            ProcessFirstTouch(Input.GetTouch(0));   // Вызываем функцию обработки. 
        }
    }

    private void ProcessFirstTouch(Touch touch)
    {
        if(touch.phase == TouchPhase.Began)     // Тач фейс - если беган, (беган -ничего не прикоснулось и вот он коснулсяю. Сташинари - палец не двигается, мув - двигается, кансел- оторвали палец?)
        {
            SpawnObject(touch);                 // Создаем объект под тачем
        }
    }

    void SpawnObject (Touch touch)
    {
        _aRRaycastManager.Raycast(touch.position, _raycastHits, TrackableType.Planes);      // стреляем лучем с нашей камеры в позицию, записываем результаты в наш массив Хитс. 3 аргумент - то , с чем пересечется наш аргуемент. Записываем в хиты
        Instantiate(_spawnedObjectPrefab, _raycastHits[0].pose.position, _spawnedObjectPrefab.transform.rotation); // Создает дубликат объекта. функция медленная , пока сойдет.  Пока объектов не более 100.
        //Создаем копию префаба, в точке 1го пересечения луча с плоскостью. Мы берем у него позу, и берем позицию и делаем поворот объекта.
        // Луч с плоскотью пересечения, создания объекта итд
    }
}
