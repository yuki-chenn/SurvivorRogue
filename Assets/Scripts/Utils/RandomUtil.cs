using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Survivor.Utils
{
    class RandomUtil
    {
        private const int MAX_ATTEMPTS = 100;

        public static bool IsProbabilityMet(float probability)
        {
            return Random.Range(0.0f, 1.0f) < probability;
        }

        public static float RandomFloat(float range,bool neg = true)
        {
            if(neg) return Random.Range(-range, range);
            else return Random.Range(0, range);
        }

        public static int RandomInt(int min,int max)
        {
            return Random.Range(min, max + 1);
        }

        public static Vector2 RandomVector2(float xMin, float xMax, float yMin, float yMax)
        {
            float randomX = Random.Range(xMin, xMax);
            float randomY = Random.Range(yMin, yMax);
            return new Vector2(randomX, randomY);
        }

        public static int RandomIndexWithProbablity(float[] probability)
        {
            // Convert float[] to List<float>
            List<float> probabilityList = probability.ToList();

            // Call the previously defined method
            return RandomIndexWithProbablity(probabilityList);
        }

        public static int RandomIndexWithProbablity(List<float> probability)
        {
            if (probability == null || probability.Count == 0)
            {
                Debug.LogError("Probability list cannot be null or empty.");
            }

            // Calculate the sum of all probabilities
            float totalProbability = probability.Sum();
            if (totalProbability <= 0)
            {
                Debug.LogError("Sum of probabilities must be greater than zero.");
            }

            // Normalize the probabilities to ensure they sum to 1
            List<float> normalizedProbabilities = probability.Select(p => p / totalProbability).ToList();

            // Calculate the cumulative probabilities
            List<float> cumulativeProbabilities = new List<float>(normalizedProbabilities.Count);
            float cumulative = 0f;
            for (int i = 0; i < normalizedProbabilities.Count; i++)
            {
                cumulative += normalizedProbabilities[i];
                cumulativeProbabilities.Add(cumulative);
            }

            // Generate a random number between 0 and 1
            float randomValue = Random.Range(0.0f, 1.0f);

            // Find the index corresponding to the random value
            for (int i = 0; i < cumulativeProbabilities.Count; i++)
            {
                if (randomValue <= cumulativeProbabilities[i])
                {
                    return i;
                }
            }

            // In case of numerical issues, return -1
            return -1;
        }

        public static Vector2 RandomPosInRect(float x,float y)
        {
            var posx = Random.Range(-x, x);
            var posy = Random.Range(-y, y);
            return new Vector2(posx, posy);
        }

        public static Vector2 RandomPosInRect(float x, float y,Vector2 o)
        {
            return RandomPosInRect(x, y) + o;
        }

        public static Vector2 RandomDirection(float len = 1f)
        {
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);
            return new Vector2(len * Mathf.Cos(randomAngle),len * Mathf.Sin(randomAngle));
        }

        public static T GetRandomValueInList<T>(T[] array)
        {
            // Convert T[] to List<float>
            List<T> list = array.ToList();
            return GetRandomValueInList(list);
        }

        public static T GetRandomValueInList<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new System.ArgumentException("The list is null or empty.");
            }

            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

        public static T GetRandomValueInList<T>(List<T> list, T diffValue)
        {
            int attempts = 0;
            T res = GetRandomValueInList(list);
            while (EqualityComparer<T>.Default.Equals(diffValue, res) && attempts < MAX_ATTEMPTS)
            {
                attempts++;
                res = GetRandomValueInList(list);
            }
            return res;
        }

    }
}
