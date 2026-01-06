using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class EquipScriptables : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<BaseWeapons> _weapons;
    [SerializeField] private List<PlayerCharactersStats> _charactersStats;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private List<Mesh> _mesh;

    private void OnEnable()
    {
        EquipClass();
    }

    private void EquipClass()
    {
        if ( PlayerPrefs.GetInt("Alice") == 1)
        {
            _player.characterStats = _charactersStats.Find(x => x.name == "AliceStats");
            _player.weapons.Add(_weapons.Find(x => x.name == "Knife"));
            _player.GetComponent<MeshRenderer>().material = _materials.Find(x => x.name == "Alice");
            _player.GetComponent<MeshFilter>().mesh = _mesh.Find(x => x.name == "Cube.002");
        }

        if (PlayerPrefs.GetInt("MadHatter") == 1)
        {
            _player.characterStats = _charactersStats.Find(x => x.name == "MadHatterStats");
            _player.weapons.Add(_weapons.Find(x => x.name == "Staff"));
            _player.GetComponent<MeshRenderer>().material = _materials.Find(x => x.name == "Cappellaio");
            _player.GetComponent<MeshFilter>().mesh = _mesh.Find(x => x.name == "Cylinder.006");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Alice", 0);
        PlayerPrefs.SetInt("MadHatter", 0);
    }
}