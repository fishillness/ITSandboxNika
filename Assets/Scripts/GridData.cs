using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridData
{
    //private List<List<PlacementData>> placedObjects = new List<List<PlacementData>>();

    [Serializable]
    public class PlacementData
    {
        public bool IsActive => m_IsActive;

        [SerializeField] private bool m_IsActive;

        private int m_ID;
        private List<Vector3Int> m_OccupiedPositions;
        private int m_PlacedObjectIndex;

    }
    

}
