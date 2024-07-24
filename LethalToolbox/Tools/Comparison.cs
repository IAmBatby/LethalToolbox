using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox
{
    public enum ComparisonType { Tag, Name, IsPlayer }

    [System.Serializable]
    public class Comparison
    {
        public ComparisonType comparisonType;
        public string comparisonFilter = string.Empty;

        public Comparison(ComparisonType newComparisonType, string newComparisonFilter)
        {
            comparisonType = newComparisonType;
            comparisonFilter = newComparisonFilter;
        }

        public bool ValidComparison(GameObject comparableGameObject)
        {
            return (ValidComparison(comparableGameObject, comparisonType));
        }

        public bool ValidComparison(GameObject comparableGameObject, ComparisonType comparisonType)
        {
            if (comparisonType == ComparisonType.Tag && comparableGameObject.CompareTag(comparisonFilter))
                return (true);
            else if (comparisonType == ComparisonType.Name && comparableGameObject.name == comparisonFilter)
                return (true);
            else if (comparisonType == ComparisonType.IsPlayer)
                foreach (GameObject player in Data.playerAllObjectsList)
                    if (player == comparableGameObject)
                        return (true);

            return (false);
        }
    }
}
