using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

class Solution {

    private int _rowCount;
    private int _colCount;
    private int[,] _mapData;
    private int[] _parent;
    private int[] _rank;

    public int Soln(int[,] A) {
        // N & M in range 1..300,000
        // number of elements in range 1..300,000
        // each element is in range -1,000,000,000..1,000,000,000
        if (A.Length == 1 || A.Cast<int>().All(elt => elt == A[0,0])) return 1;

        _mapData = A;
        _colCount = _mapData.GetLength(0);
        _rowCount = _mapData.GetLength(1);
        _parent = new int[ _colCount * _rowCount];
        _rank = new int[ _colCount * _rowCount];

        // initialise parents to self
        for (var i = 0; i < _colCount * _rowCount; i++)
        {
            _parent[i] = i;
        }

        // check neighbours and join if adjacent
        for (var y = 0; y < _rowCount; y++)
        {
            for (var x = 0; x < _colCount; x++)
            {
                if (x < _colCount - 1 && _mapData[x, y] == _mapData[x + 1, y]) Union(y * _colCount + x, y * _colCount + x + 1);
                if (y < _rowCount - 1 && _mapData[x, y] == _mapData[x, y + 1]) Union(y * _colCount + x, (y + 1) * _colCount + x);
            }
        }

        return _parent.ToHashSet().Count();
    }

    private void Union(int setOne, int setTwo)
    {
        var setOneRoot = FindParentOf(setOne);
        var setTwoRoot = FindParentOf(setTwo);
        if (setOneRoot == setTwoRoot) return;

        if (_rank[setOneRoot] < _rank[setTwoRoot])
        {
            _parent[setOneRoot] = setTwoRoot;
        }
        else if (_rank[setTwoRoot] < _rank[setOneRoot])
        {
            _parent[setTwoRoot] = setOneRoot;
        }
        else
        {
            _parent[setTwoRoot] = setOneRoot;
            _rank[setOneRoot]++;
        }

    }

    private int FindParentOf(int s)
    {
        if (_parent[s] != s)
        {
            _parent[s] = FindParentOf(_parent[s]); // compress path
        }

        return _parent[s];
    }

}
 
public class A_codility_solution_should
{

    public static IEnumerable<object[]> MatrixData
    {
        get
        {
            return new []
            {
                new object[] { new int[,] { { 1, 2 },
                                            { 2, 2 } }, 2 },
                new object[] { new int[,] { { 5, 4, 4 },
                                            { 4, 3, 4 } }, 4 },
                new object[] { new int[,] { { 0 } }, 1 },
                new object[] { new int[,] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } }, 1 },
                new object[] { new int[,] { { 1, 2, 3 } }, 3 },
                new object[] { new int[,] { { 1 }, { 2 }, { 3 } }, 3 },
                new object[] { new int[,] { { 1, 2 }, { 3, 4 } }, 4 },
                new object[] { new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } }, 6 },
                new object[] { new int[,] { { 1, 2, 3 }, { 4, 5, 6 } }, 6}, 

                new object[] { new int[,] { { 1, 1, 1 } }, 1 },
                new object[] { new int[,] { { 1 }, { 1 }, { 1 } }, 1 },
                new object[] { new int[,] { { 1, 1 }, { 1, 1 } }, 1 },
                new object[] { new int[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } }, 1 },
                new object[] { new int[,] { { 1, 1, 1 }, { 1, 1, 1 } }, 1}, 

                new object[] { new int[,] { { 1, 2, 1 } }, 3 },
                new object[] { new int[,] { { 1 }, { 2 }, { 1 } }, 3 },
                new object[] { new int[,] { { 1, 2 },
                                            { 1, 2 } }, 2 },
                new object[] { new int[,] { { 1, 2 }, { 1, 2 }, { 1, 2 } }, 2 },
                new object[] { new int[,] { { 1, 2, 1 }, { 1, 2, 1 } }, 3}, 
                
                new object[] { new int[,] { { 5, 4, 4 }, 
                                            { 4, 3, 4 },
                                            { 3, 2, 4 } }, 6 },
                new object[] { new int[,] { { 5, 4, 4 }, 
                                            { 4, 3, 4 },
                                            { 3, 2, 4 },
                                            { 2, 2, 2 } }, 6 },
                new object[] { new int[,] { { 5, 4, 4 }, 
                                            { 4, 3, 4 },
                                            { 3, 2, 4 },
                                            { 2, 2, 2 }, 
                                            { 3, 3, 4 }, 
                                            { 1, 4, 4 }, 
                                            { 4, 1, 1 } }, 11 },
            };
        }
    }

    [Theory]
    [MemberData(nameof(MatrixData))]
    public void pass_the_example_test(int[,] testData, int expected)
    {
        // arrange
        var soln = new Solution();

        // act
        var output = soln.Soln(testData);

        // assert
        Assert.Equal(expected, output);
    }

    [Fact]
    public void pass_large_array_test()
    {
        // arrange
        var soln = new Solution();
        var rand = new Random();
        var colCount = 20000;
        var rowCount = 20000;
        var numbers = Enumerable.Range(0, 45).ToList();
        var testData = new int[colCount,rowCount];
        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < colCount; x++)
            {
                var idx = rand.Next(0, numbers.Count);
                testData[x, y] = numbers[idx];
            }
        }

        // act
        var output = soln.Soln(testData);

        // assert
        Assert.True(true);
    }
}
