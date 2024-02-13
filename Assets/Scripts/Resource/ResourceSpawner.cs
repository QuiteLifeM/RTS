using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource resource;
    [SerializeField] private float _delay;
    [SerializeField] private List<ResourceCell> _cells = new List<ResourceCell>();

    private Random _random = new Random();
    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer > _delay)
        {
            Spawn();
            _timer = 0f;
        }
    }

    private void Spawn()
    {
        ResourceCell cell = GetRandomSpawnPoint();

        if (cell == null)
        {
            return;
        }

        Resource resource = Instantiate(this.resource, cell.transform.position, Quaternion.identity);
        cell.SetResource(resource);
    }

    private ResourceCell GetRandomSpawnPoint()
    {
        List<ResourceCell> cells = new List<ResourceCell>();
        
        foreach (var cell in _cells)
        {
            if (cell.IsEmpty)
            {
                cells.Add(cell);
            }
        }

        if (cells.Count == 0)
        {
            return null;
        }

        var randomIndex = _random.Next(0, cells.Count);

        return cells[randomIndex];
    }
}