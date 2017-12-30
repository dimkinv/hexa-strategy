using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Components
{
    public class HexGenerator
    {
        public enum MaterialType
        {
            Grass,
            Water,
            Rock
        }

        public enum PrefabType
        {
            Ocean,
            Plains,
            Mountain
        }

        public class HexGeneratorResponse
        {
            public Material Material { get; set; }
            public GameObject Prefab { get; set; }
        }

        private static readonly Dictionary<MaterialType, Material> MaterialsMap = new Dictionary<MaterialType, Material>();
        private static readonly Dictionary<PrefabType, GameObject> PrefabsMap = new Dictionary<PrefabType, GameObject>();
        private static bool _hasBeenInitialized = false;
        
        public HexGenerator()
        {
            if (HexGenerator._hasBeenInitialized)
            {
                return;
            }

            InitializeMaterials();
            InitializePrefabs();
            HexGenerator._hasBeenInitialized = true;
        }

        private void InitializeMaterials()
        {
            var materialsArr = Resources.LoadAll<Material>("Materials");
            foreach (var material in materialsArr)
            {
                var materialKey = (MaterialType) Enum.Parse(typeof(MaterialType), material.name);
                HexGenerator.MaterialsMap.Add(materialKey, material);
            }
        }

        private void InitializePrefabs()
        {
            var prefabArr = Resources.LoadAll<GameObject>("Prefabs");
            foreach (var prefab in prefabArr)
            {
                var prefabKey = (PrefabType) Enum.Parse(typeof(PrefabType), prefab.name);
                HexGenerator.PrefabsMap.Add(prefabKey, prefab);
            }
        }

        public HexGeneratorResponse GetMeshAndMaterialForTile(MaterialType material, PrefabType prefab)
        {
            return new HexGeneratorResponse()
            {
                Material = HexGenerator.MaterialsMap[material],
                Prefab = HexGenerator.PrefabsMap[prefab]
            };
        }
    }
}