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
        if ( PlayerPrefs.GetInt("Freddy") == 1)
        {
            _player.characterStats = _charactersStats.Find(x => x.name == "FreddyStats");
            _player.weapons.Add(_weapons.Find(x => x.name == "Knife"));
            _player.GetComponent<MeshRenderer>().material = _materials.Find(x => x.name == "Freddy");
            _player.GetComponent<MeshFilter>().mesh = _mesh.Find(x => x.name == "Cube");
        }

        if (PlayerPrefs.GetInt("Chica") == 1)
        {
            _player.characterStats = _charactersStats.Find(x => x.name == "ChicaStats");
            _player.weapons.Add(_weapons.Find(x => x.name == "Staff"));
            _player.GetComponent<MeshRenderer>().material = _materials.Find(x => x.name == "Chica");
            _player.GetComponent<MeshFilter>().mesh = _mesh.Find(x => x.name == "Cube.004");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Freddy", 0);
        PlayerPrefs.SetInt("Chica", 0);
    }
}