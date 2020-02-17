using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

class Solution {

    private int _rowCount;
    private int _colCount;
    private bool[,] _countryMap;
    private int[,] _mapData;

    public int solution(int[,] A) {
        // N & M in range 1..300,000
        // number of elements in range 1..300,000
        // each element is in range -1,000,000,000..1,000,000,000
        if (A.Length == 1 || A.Cast<int>().All(elt => elt == A[0,0])) return 1;

        int countryCount = 0;
        _mapData = A;
        _colCount = _mapData.GetLength(0);
        _rowCount = _mapData.GetLength(1);
        _countryMap = new bool[_colCount, _rowCount];
        for (int y = 0; y < _rowCount; y++)
        {
            for (int x = 0; x < _colCount; x++)
            {
                if (_countryMap[x,y]) continue;
                // new country, find all elements in country
                countryCount++;
                _countryMap[x, y] = true;
                SearchEast(x, y, _mapData[x, y]);
                SearchSouth(x, y, _mapData[x, y]);
            }

        }

        return countryCount;
    }

    private void findAllAdjacentNorth(int x, int y, int countryCode)
    {
        _countryMap[x,y] = true;
        SearchWest(x, y, countryCode);
        SearchNorth(x, y, countryCode);
        SearchEast(x, y, countryCode);
    }

    private void findAllAdjacentWest(int x, int y, int countryCode)
    {
        _countryMap[x,y] = true;
        SearchNorth(x, y, countryCode);
        SearchWest(x, y, countryCode);
        SearchSouth(x, y, countryCode);
    }

    private void findAllAdjacentEast(int x, int y, int countryCode)
    {
        _countryMap[x,y] = true;
        SearchNorth(x, y, countryCode);
        SearchEast(x, y, countryCode);
        SearchSouth(x, y, countryCode);
    }

    private void findAllAdjacentSouth(int x, int y, int countryCode)
    {
        _countryMap[x,y] = true;
        SearchEast(x, y, countryCode);
        SearchSouth(x, y, countryCode);
        SearchWest(x, y, countryCode);
    }

    private void SearchSouth(int x, int y, int countryCode)
    {
        if (y < _rowCount - 1 && _mapData[x, y + 1] == countryCode && !_countryMap[x, y + 1]) findAllAdjacentSouth(x, y + 1, countryCode);
    }

    private void SearchEast(int x, int y, int countryCode)
    {
        if (x < _colCount - 1 && _mapData[x + 1, y] == countryCode && !_countryMap[x + 1, y]) findAllAdjacentEast(x + 1, y, countryCode);
    }

    private void SearchNorth(int x, int y, int countryCode)
    {
        if (y > 0 && _mapData[x, y - 1] == countryCode && !_countryMap[x, y - 1]) findAllAdjacentNorth(x, y - 1, countryCode);
    }

    private void SearchWest(int x, int y, int countryCode)
    {
        if (x > 0 && _mapData[x - 1, y] == countryCode && !_countryMap[x - 1, y]) findAllAdjacentWest(x - 1, y, countryCode);
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
        var output = soln.solution(testData);

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
        var output = soln.solution(testData);

        // assert
        Assert.True(true);
    }
}
