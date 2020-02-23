using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

class Solution {

    [Flags]
    private enum Direction
    {
        None = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8
    }

    private int _rowCount;
    private int _colCount;
    private bool[,] _countryMap;
    private int[,] _mapData;
    private Direction[,] _eltMatch;

    public int Soln(int[,] A) {
        // N & M in range 1..300,000
        // number of elements in range 1..300,000
        // each element is in range -1,000,000,000..1,000,000,000
        if (A.Length == 1 || A.Cast<int>().All(elt => elt == A[0,0])) return 1;

        int countryCount = 0;
        _mapData = A;
        _colCount = _mapData.GetLength(0);
        _rowCount = _mapData.GetLength(1);
        _countryMap = new bool[_colCount, _rowCount];
        _eltMatch = new Direction[_colCount, _rowCount];
        for (var y = 0; y < _rowCount; y++)
        {
            for (var x = 0; x < _colCount; x++)
            {
                var eltValue = _mapData[x,y];
                if (x < _colCount - 1 &&  _mapData[x + 1, y] == eltValue)
                {
                    _eltMatch[x, y] |= Direction.East;
                    _eltMatch[x + 1, y] |= Direction.West;
                }
                if (y < _rowCount - 1 && _mapData[x, y + 1] == eltValue)
                {
                    _eltMatch[x, y] |= Direction.South;
                    _eltMatch[x, y + 1] |= Direction.North;
                }
            }
        }

        for (int y = 0; y < _rowCount; y++)
        {
            for (int x = 0; x < _colCount; x++)
            {
                if (_countryMap[x,y]) continue;
                
                // new country, find all elements in country
                countryCount++;
                _countryMap[x, y] = true;
                NavigateEast(x, y, _mapData[x, y]);
                NavigateSouth(x, y, _mapData[x, y]);
                _eltMatch[x, y] = Direction.None;
            }

        }

        return countryCount;
    }

    private void SearchNorth(int x, int y, int countryCode)
    {
        _countryMap[x, y] = true;

        // burn bridges
        _eltMatch[x, y + 1] &= ~Direction.North;
        _eltMatch[x, y] &= ~Direction.South;

        NavigateWest(x, y, countryCode);
        NavigateNorth(x, y, countryCode);
        NavigateEast(x, y, countryCode);

    }

    private void SearchEast(int x, int y, int countryCode)
    {
        _countryMap[x, y] = true;

        // burn bridges
        _eltMatch[x - 1, y] &= ~Direction.East;
        _eltMatch[x, y] &= ~Direction.West;

        NavigateNorth(x, y, countryCode);
        NavigateEast(x, y, countryCode);
        NavigateSouth(x, y, countryCode);

    }

    private void SearchSouth(int x, int y, int countryCode)
    {
        _countryMap[x, y] = true;

        // burn bridges
        _eltMatch[x, y - 1] &= ~Direction.South;
        _eltMatch[x, y] &= ~Direction.North;

        NavigateEast(x, y, countryCode);
        NavigateSouth(x, y, countryCode);
        NavigateWest(x, y, countryCode);
    }

    private void SearchWest(int x, int y, int countryCode)
    {
        _countryMap[x, y] = true;

        // burn bridges
        _eltMatch[x + 1, y] &= ~Direction.West;
        _eltMatch[x, y] &= ~Direction.East;

        NavigateSouth(x, y, countryCode);
        NavigateWest(x, y, countryCode);
        NavigateNorth(x, y, countryCode);

    }

    private void NavigateNorth(int x, int y, int countryCode)
    {
        if (_eltMatch[x, y].HasFlag(Direction.North)) SearchNorth(x, y - 1, countryCode);
    }

    private void NavigateEast(int x, int y, int countryCode)
    {
        if (_eltMatch[x, y].HasFlag(Direction.East)) SearchEast(x + 1, y, countryCode);
    }

    private void NavigateWest(int x, int y, int countryCode)
    {
        if (_eltMatch[x, y].HasFlag(Direction.West)) SearchWest(x - 1, y, countryCode);
    }

    private void NavigateSouth(int x, int y, int countryCode)
    {
        if (_eltMatch[x, y].HasFlag(Direction.South)) SearchSouth(x, y + 1, countryCode);
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
        var colCount = 3000;
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
