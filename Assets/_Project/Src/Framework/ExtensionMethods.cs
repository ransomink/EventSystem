#region Namespaces

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion // Namespaces

namespace Ransomink.Extensions
{
    public enum Symbol
    {
        EQUAL = 0, LESS = 1, GREATER = 2, LESS_EQUAL = 3, GREATER_EQUAL = 4, NOT_EQUAL = 5
    }

    /// <summary>
    /// A collection of method extensions.
    /// </summary>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Check if an Component is null.
        /// </summary>
        /// <param name="comp">This component.</param>
        /// <returns>If the component is null or has a reference.</returns>
        public static bool IsNull(this Component comp)
        {
            return ReferenceEquals(comp, null) || (object)comp == null;
        }

        /// <summary>
        /// Check if an integer is zero.
        /// </summary>
        /// <param name="value">This integer value.</param>
        /// <returns>If the integer has a zero value.</returns>
        public static bool IsZero(this int val)
        {
            return val == 0;
        }

        /// <summary>
        /// Check if a float is zero.
        /// </summary>
        /// <param name="value">This float value.</param>
        /// <param name="tolerance">The marginal offset of the value.</param>
        /// <returns>If a float has a zero value.</returns>
        public static bool IsZero(this float val, float tol = default)
        {
            if (tol == default) tol = Mathf.Epsilon;
            return Mathf.Abs(val) <= tol;
        }

        /// <summary>
        /// Compare against two integer values.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <param name="valToCompare">Target value to compare.</param>
        /// <param name="sign">Operator used for comparison.</param>
        /// <param name="tol">Offset amount to target value.</param>
        /// <returns>The result of comparison.</returns>
        public static bool Compare(this int val, int valToCompare, Symbol sign = default, int tol = default)
        {
            switch (sign)
            {
                case Symbol.EQUAL:
                    var result = Mathf.Abs(val - valToCompare) <= tol;
                    val = result ? 0 : val;
                    return result;
                case Symbol.LESS:
                    return val <  valToCompare + tol;
                case Symbol.GREATER:
                    return val >  valToCompare - tol;
                case Symbol.LESS_EQUAL:
                    return val <= valToCompare + tol;
                case Symbol.GREATER_EQUAL:
                    return val >= valToCompare - tol;
                case Symbol.NOT_EQUAL:
                    return val != valToCompare;
                default:
                    Debug.LogError("[ERROR] Greater Than, Less Than, or Equal To operator not assigned!");
                    return false;
            }
        }

        /// <summary>
        /// Compare against two float values.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <param name="valToCompare">Target value to compare.</param>
        /// <param name="sign">Operator used for comparison.</param>
        /// <param name="tol">Offset amount to target value.</param>
        /// <returns>The result of comparison.</returns>
        public static bool Compare(this float val, float valToCompare, Symbol sign = 0, float tol = 0f)
        {
            switch (sign)
            {
                case Symbol.EQUAL:
                    if (tol == 0f) tol = Mathf.Epsilon;
                    var result = Mathf.Abs(val - valToCompare) <= tol;
                    val = result ? 0 : val;
                    return result;
                case Symbol.LESS:
                    return val <  valToCompare + tol;
                case Symbol.GREATER:
                    return val >  valToCompare - tol;
                case Symbol.LESS_EQUAL:
                    return val <= valToCompare + tol;
                case Symbol.GREATER_EQUAL:
                    return val >= valToCompare - tol;
                case Symbol.NOT_EQUAL:
                    return !Mathf.Approximately(val, valToCompare);
                default:
                    Debug.LogError("[ERROR] Greater Than, Less Than, or Equal To operator not assigned!");
                    return false;
            }
        }

        /// <summary>
        /// Return the square of the specified integer value.
        /// </summary>
        /// <param name="value">The value to square.</param>
        /// <returns>The squared value.</returns>
        public static int Squared(this int val)
        {
            return val * val;
        }

        /// <summary>
        /// Return the square of the specified float value.
        /// </summary>
        /// <param name="value">The value to square.</param>
        /// <returns>The squared value.</returns>
        public static float Squared(this float val)
        {
            return val * val;
        }

        public static float RescaleValue(float val, float min, float max, float percent)
        {
            return min + (max - min) * percent;
        }

        public static float RescaleValue(float val, float newMin, float newMax, float curVal, float curMin, float curMax)
        {
            return newMin + (newMax - newMin) * (curVal - curMin) / (curMax - curMin);
        }

        /// <summary>
        /// Get the squared distance between two positions.
        /// </summary>
        /// <param name="a">This position.</param>
        /// <param name="b">The target position.</param>
        /// <returns>The squared distance between both positions.</returns>
        public static float GetDistance(Vector3 a, Vector3 b)
        {
            return ((b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y) + (b.z - a.z) * (b.z - a.z));
        }

        public static float Distance(this Vector3 a, Vector3 b)
        {
            return GetDistance(a, b);
        }
        
        /// <summary>
        /// Get the squared distance between two transforms.
        /// </summary>
        /// <param name="a">This transform.</param>
        /// <param name="b">The target transform.</param>
        /// <returns>The squared distance between both transforms.</returns>
        public static float GetDistance(Transform a, Transform b)
        {
            Vector3 posA = a.position;
            Vector3 posB = b.position;
            return GetDistance(posA, posB);
        }

        /// <summary>
        /// Get the squared distance vector between two positions.
        /// </summary>
        /// <param name="a">This position.</param>
        /// <param name="b">The target position.</param>
        /// <returns>The squared distance vector between both positions.</returns>
        public static Vector3 GetDistanceVector(Vector3 a, Vector3 b)
        {
            return new Vector3((b.x - a.x) * (b.x - a.x), (b.y - a.y) * (b.y - a.y), (b.z - a.z) * (b.z - a.z));
        }

        /// <summary>
        /// Get the squared distance vector between two positions.
        /// </summary>
        /// <param name="a">This position.</param>
        /// <param name="b">The target position.</param>
        /// <returns>The squared distance vector between both positions.</returns>
        public static Vector3 DistanceVector(this Vector3 a, Vector3 b)
        {
            return GetDistanceVector(a, b);
        }

        /// <summary>
        /// Get the squared distance vector between two positions.
        /// </summary>
        /// <param name="a">This transform.</param>
        /// <param name="b">The target transform.</param>
        /// <returns>The squared distance vector between both transforms.</returns>
        public static Vector3 GetDistanceVector(Transform a, Transform b)
        {
            Vector3 posA = a.position;
            Vector3 posB = b.position;
            return GetDistanceVector(posA, posB);
        }

        /// <summary>
        /// Calculates the Chebyshev distance between the two points.
        /// </summary>
        /// <param name="x1">The first x coordinate.</param>
        /// <param name="x2">The second x coordinate.</param>
        /// <param name="y1">The first y coordinate.</param>
        /// <param name="y2">The second y coordinate.</param>
        /// <returns>The Chebyshev distance between (x1, x2) and (y1, y2).</returns>
        public static int CalculateChebyshevDistance(int x1, int x2, int y1, int y2)
        {
            var dx = Math.Abs(x2 - x1);
            var dy = Math.Abs(y2 - y1);
            return (dx + dy) - Math.Min(dx, dy);
        }

        /// <summary>
        /// Calculates the Manhattan distance between the two points.
        /// </summary>
        /// <param name="x1">The first x coordinate.</param>
        /// <param name="y1">The first y coordinate.</param>
        /// <param name="x2">The second x coordinate.</param>
        /// <param name="y2">The second y coordinate.</param>
        /// <returns>The Manhattan distance between (x1, y1) and (x2, y2).</returns>
        public static int CalculateManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public static Vector3 GetDirection(Vector3 a, Vector3 b)
        {
            return new Vector3((b.x - a.x), (b.y - a.y), (b.z - a.z));
        }

        public static Vector3 Direction(this Vector3 a, Vector3 b)
        {
            return GetDirection(a, b);
        }

        public static Vector3 GetDirectionNormalized(Vector3 a, Vector3 b)
        {
            return GetDirection(a, b).normalized;
        }

        public static Vector3 DirectionNormalized(this Vector3 a, Vector3 b)
        {
            return GetDirectionNormalized(a, b);
        }

        public static T GetClosest<T>(T comp, IList<T> list) where T : Component
        {
            var pos = comp.transform.position;
            return GetClosest(pos, list);
        }

        public static T GetClosest<T>(Vector3 pointA, IList<T> list) where T : Component
        {
            Vector3 pointB;
            int   index = -1;
            float dist  = float.MaxValue;
            float diff  = float.MaxValue;

            for (int i = list.Count - 1; i >= 0; --i)
            {
                pointB = list[i].transform.position;
                diff   = GetDistance(pointA, pointB);
                
                if (diff <= dist)
                {
                    dist  = diff;
                    index = i;
                }
            }

            return index > -1 ? list[index] : null;
        }

        public static Vector3 GetClosestPoint(Vector3 pointA, IList<Vector3> list)
        {
            Vector3 pointB;
            int   index = -1;
            float dist  = float.MaxValue;
            float diff  = float.MaxValue;

            for (int i = list.Count - 1; i >= 0; --i)
            {
                pointB = list[i];
                diff   = GetDistance(pointA, pointB);
                
                if (diff <= dist)
                {
                    dist  = diff;
                    index = i;
                }
            }

            return index > -1 ? list[index] : Vector3.negativeInfinity;
        }

        public static Vector3 GetClosestPoint<T>(Vector3 pointA, IList<T> list) where T : Component
        {
            Vector3 pointB;
            int   index = -1;
            float dist  = float.MaxValue;
            float diff  = float.MaxValue;

            for (int i = list.Count - 1; i >= 0; --i)
            {
                pointB = list[i].transform.position;
                diff   = GetDistance(pointA, pointB);
                
                if (diff <= dist)
                {
                    dist  = diff;
                    index = i;
                }
            }

            return index > -1 ? list[index].transform.position : Vector3.negativeInfinity;
        }

        /// <summary>
        /// Check if an index is valid within an array.
        /// </summary>
        /// <typeparam name="T">The type (generic).</typeparam>
        /// <param name="arr">This array (generic).</param>
        /// <param name="i">The index to check.</param>
        /// <returns>If index is valid.</returns>
        public static bool IsIndexValid<T>(this T[] arr, int i)
        {
            return (!ReferenceEquals(arr, null)) && (i >= 0) && (i < arr.Length);
        }

        /// <summary>
        /// Check if an index is valid within a list.
        /// </summary>
        /// <typeparam name="T">The type (generic).</typeparam>
        /// <param name="arr">This list (generic).</param>
        /// <param name="i">The index to check.</param>
        /// <returns>If index is valid.</returns>
        public static bool IsIndexValid<T>(this IList<T> list, int i)
        {
            return (!ReferenceEquals(list, null)) && (i >= 0) && (i < list.Count);
        }

        /// <summary>
        /// Reverse the order of an array while avoiding heap allocation.
        /// </summary>
        /// <param name="arr">This array collection.</param>
        public static T[] ReverseNonAlloc<T>(this T[] arr, IList<T> list = null)
        {
            if (!ReferenceEquals(list, null)) list = new List<T>();
            
            int n = arr.Length;

            for (var i = 0; i < n; i++)
            {
                if (!ReferenceEquals(arr[i], null))
                {
                    list.Add (arr[i]);
                }
            }

            //arr = new T[list.Count];
            //list.CopyTo(arr);

            n = list.Count;

            for (var i = n - 1; i >= 0; --i)
            {
                //Debug.Log("ARRAY["+ i +"] = LIST["+ (n - i - 1) +"]");
                arr[i] = list[n - i - 1];
            }

            return arr;
        }

        /// <summary>
        /// Reverse the order of a list while avoiding heap allocation.
        /// </summary>
        /// <param name="list">This list collection.</param>
        public static void ReverseNonAlloc<T>(this IList<T> list)
        {
            int n = list.Count;

            for (int i = 0; i < n; i++)
            {
                T tmp			= list[i];
                list[i]		    = list[n - i - 1];
                list[n - i - 1] = tmp;
            }
        }

        /// <summary>
        /// Randomly shuffle a list.
        /// </summary>
        /// <typeparam name="T">The type of list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;

            while (n > 1)
            {
                n--;
                var i   = UnityEngine.Random.Range(0, n + 1);
                T val   = list[i];
                list[i] = list[n];
                list[n] = val;
            }
        }

        /*public static void FindMinMaxValue<T> (IEnumerable<T> col, T value)
        {
            T min = (typeof)T.MaxValue;
            T max = int.MinValue;

            Vector3? minDist;
            Vector3? maxDist;

            foreach (var p in vectors)
            {
                var dist = Vector3.SqrMagnitude(p - point);

                if (dist < min)
                {
                    minDist = p;
                    max = dist;
                }

                if (dist > max)
                {
                    maxDist = p;
                    max = dist;
                }
            }
        }*/

        public static void FindMinMaxValue(Vector3 point, IList<Vector3> vectors)
        {
            var min = float.MaxValue;
            var max = float.MinValue;

            Vector3? minDist;
            Vector3? maxDist;

            foreach (var p in vectors)
            {
                var dist = Vector3.SqrMagnitude(p - point);

                if (dist < min)
                {
                    minDist = p;
                    max =  dist;
                }

                if (dist > max)
                {
                    maxDist = p;
                    max =  dist;
                }
            }
        }

        /// <summary>
        /// Sort a list based on distance of a transform or position.
        /// </summary>
        /// <typeparam name="GameObject"></typeparam>
        /// <param name="list">This list collection.</param>
        /// <param name="rootGo">The root GameObject to compare distance from.</param>
        /// <param name="root">The root position to compare distance from. If rootGo is declared, used as an offset position.</param>
        /// <returns>The sorted list.</returns>
        /* public static List<GameObject> SortGameObjectByDistance<GameObject>(this IList<GameObject> list, GameObject rootGo = null, Vector3 root = Vector3.zero)
        {
            if (!ReferenceEquals(rootGo, null)) root += rootGo.transform.position;

            list.Sort(delegate(GameObject a, GameObject b)
            {
                float posA  = a.transform.position;
                float posB  = b.transform.position;
                float distA = GetDistance(root, posA);
                float distB = GetDistance(root, posB);
                return distA.CompareTo(distB);

                //float sqrMagA = (posA - root).sqrMagnitude;
                //float sqrMagB = (posB - root).sqrMagnitude;
                //return sqrMagA.CompareTo(sqrMagB);
            });

            return list;
        } */

        public static Vector3 RandomPointInCircle(Vector3 center, float radius, bool useEdge = false)
        {
            var point = UnityEngine.Random.insideUnitCircle;
            var pos   = useEdge ? point.normalized * radius : point * radius;
            return center + new Vector3(point.x, 0f, point.y);
        }
        
        public static Vector3 RandomPointAroundCircle(Vector3 center, float min, float max)
        {
            var range = UnityEngine.Random.Range(min, max);
            var point = UnityEngine.Random.insideUnitCircle.normalized * range;
            return center + new Vector3(point.x, 0f, point.y);
        }

        public static void RemoveComponent<T>(this T comp, float time = 0f) where T : Component
        {
            UnityEngine.Object.Destroy(comp, time);
        }

        public static void RemoveComponentImmediate<T>(this T comp) where T : Component
        {
            UnityEngine.Object.DestroyImmediate(comp);
        }

        public static void Destroy(GameObject go)
        {
            try
            {
                if (go != null) GameObject.Destroy(go);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
