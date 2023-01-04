using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTool
{
    /// <summary>
    /// 固定数组中的不重复随机
    /// </summary>
    /// <param name="nums">数组</param>
    /// <param name="count">要随机的个数</param>
    /// <returns></returns>
    private static List<T> GetRandom<T>( List<T> nums, int count)
    {
        if (count > nums.Count)
        {
            Debug.LogError("要取的个数大于数组长度！");
            return null;
        }
 
        List<T> result = new List<T>();
        List<int> id = new List<int>();
 
        for (int i = 0; i < nums.Count; i++)
        {
            id.Add(i);
        }
 
        int r;
        while (id.Count > nums.Count - count)
        {
            r = Random.Range(0, id.Count);
            result.Add(nums[id[r]]);
            id.Remove(id[r]);
        }
        return (result);
    }


    /// <summary>
    /// 获取一个点随机范围数量的点
    /// </summary>
    /// <param name="Point">原点</param>
    /// <param name="minRadius">最小范围x,y</param>
    /// <param name="maxRadius">最大范围x,y</param>
    /// <param name="count">生成随机的数量</param>
    /// <returns>返回基于原点内随机范围的点</returns>
    public static List<Vector3> GetRandomPoint(Vector3 Point,Vector2 minRadius,Vector2 maxRadius,int count)
    {
        List<Vector3> RandomPoint = new List<Vector3>(count);
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(minRadius.x, maxRadius.x);
            float y = Random.Range(minRadius.y, maxRadius.y);
            Vector3 newPoint = new Vector3(Point.x + x, Point.y + y, Point.z);
            RandomPoint.Add(newPoint);
        }
        return RandomPoint;
    }

}
