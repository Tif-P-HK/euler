using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  partial class Calculator
  {
    private int resultInUnexpectedCondition = -999;

    public int Problem1()
    {
      int sum = 0;
      for (int i = 3; i < 1000; i++)
        if (i % 3 == 0 || i % 5 == 0)
          sum += i;
      return sum;
    }

    public Int32 Problem2()
    {
      int n1 = 1;
      int n2 = 2;
      int n3 = n1 + n2;

      Int32 sum = n2;

      while (n3 < 4000000)
      {
        n1 = n2;
        n2 = n3;
        n3 = n1 + n2;

        if (n3 % 2 == 0)
          sum += n3;
      }
      return sum;
    }

    public Int64 Problem3()
    {
      Int64 target = 600851475143;

      //determine largest factors then work backward
      Int64 factor = 1;

      //Loop through odd dividends from 1 to target value
      //so thay the largest factor is retured first
      for (int dividend = 1; dividend <= target; dividend += 2)
      {
        if (target % dividend == 0)
        {
          //Largest factor found
          factor = target / dividend;

          //Ddetermine if the largest factor is prime
          if (CalculatorUtil.IsPrime(factor))
            return factor;
        }
      }

      return resultInUnexpectedCondition;
    }

    public int Problem4()
    {
      int largestPalindromic = 997799;
      int smallestPalindromic = 10001;
      int factor2 = 0;

      for (int i = largestPalindromic; i >= smallestPalindromic; i--)
      {
        //Determine if i is a palindromic
        if (CalculatorUtil.IsPalindromic(i + ""))
        {
          //if so, determine if i is a product of two 3-digit numbers
          for (int factor1 = 100; factor1 <= 999; factor1++)
          {
            if (i % factor1 == 0)
            {
              factor2 = i / factor1;

              if (CalculatorUtil.IsBetweenStartAndEnd(factor2, 100, 999))
                return i;
            }
          }
        }
      }

      return resultInUnexpectedCondition;
    }

    public double Problem5()
    {
      //create an empty <int, int> dictionary 
      IDictionary<int, int> primeCountPairsFrom2to20 = new Dictionary<int, int>();
      
      //loop through 2 to 20 to get the prime numbers factors and their max. occurance 
      for (int i = 2; i <= 20; i++)
      { 
        int currentNum = i;

        //for each currentNum, find all its prime factors and the count of the prime factor
        IDictionary<int, int> primeCountPairs = new Dictionary<int, int>();
        CalculatorUtil.GetPrimesAndCounts(ref currentNum, primeCountPairs);

        //Compare the primeCountPairs of the current number with the entire set (from 2 to 20)
        foreach(KeyValuePair<int, int> pair in primeCountPairs)
        {
          //if prime factor never appears, add to dictionary
          if (!primeCountPairsFrom2to20.ContainsKey(pair.Key))
            primeCountPairsFrom2to20.Add(pair.Key, pair.Value);

          //if count of existing prime factor is larger than current largest, replace count
          else if (primeCountPairsFrom2to20.ContainsKey(pair.Key) && pair.Value > primeCountPairsFrom2to20[pair.Key])
            primeCountPairsFrom2to20[pair.Key] = pair.Value;
        }
      }

      //sumOf(prime^count)
      double product = 1;
      foreach (KeyValuePair<int, int> pair in primeCountPairsFrom2to20)
        product *= Math.Pow(pair.Key, pair.Value);
      
      return product;
    }

    public int Problem6()
    {
      int n = 100;

      return n * (n + 1) * (3 * n * n - n - 2) / 12; 
    }

    public int Problem7()
    {
      return CalculatorUtil.GetPrimeNumberAt(10001);
    }

    public Int64 Problem8()
    {
      string longString = "73167176531330624919225119674426574742355349194934" +
                         "96983520312774506326239578318016984801869478851843" +
                         "85861560789112949495459501737958331952853208805511" +
                         "12540698747158523863050715693290963295227443043557" +
                         "66896648950445244523161731856403098711121722383113" +
                         "62229893423380308135336276614282806444486645238749" +
                         "30358907296290491560440772390713810515859307960866" +
                         "70172427121883998797908792274921901699720888093776" +
                         "65727333001053367881220235421809751254540594752243" +
                         "52584907711670556013604839586446706324415722155397" +
                         "53697817977846174064955149290862569321978468622482" +
                         "83972241375657056057490261407972968652414535100474" +
                         "82166370484403199890008895243450658541227588666881" +
                         "16427171479924442928230863465674813919123162824586" +
                         "17866458359124566529476545682848912883142607690042" +
                         "24219022671055626321111109370544217506941658960408" +
                         "07198403850962455444362981230987879927244284909188" +
                         "84580156166097919133875499200524063689912560717606" +
                         "05886116467109405077541002256983155200055935729725" +
                         "71636269561882670428252483600823257530420752963450";

      int index = 0;
      Int64 currentProduct = 0;
      Int64 largestProduct = 0;
      List<int> numLst = new List<int>();
      while (index < longString.Length)
      {
        try
        {
          //When the substrig doesn't have 13 char, catch the exception and stop the calculation
          string subString = longString.Substring(index, 13);

          //If running into 0, skip to the digit after 0
          if (subString.Contains('0'))
            index += subString.IndexOf('0') + 1;
          else
          {
            //convert the string of digits into a list of numbers
            foreach (char c in subString)
              numLst.Add(CalculatorUtil.CharToInt(c));

            //multiply all digits in the list together
            currentProduct = CalculatorUtil.GetProductFromNums(numLst);

            //compare the product with the existing largest value
            if (currentProduct > largestProduct)
              largestProduct = currentProduct;
            
            numLst.Clear();
            index++;
          }
        }
        catch (ArgumentOutOfRangeException)
        {
          break;
        }
      }

      return largestProduct;
    }

    //From 
    //http://www.mathsisfun.com/numbers/pythagorean-triples.html
    //a = n^2 - m^2
    //b = 2nm
    //c = n^2 + m^2
    //if a + b + c = 1000
    //n(n+m) = 500
    //m = (500-n^2)/n
    //hence the largest possible n should be <= Math.Sqrt(500)
    public int Problem9()
    {
      int maxN = Convert.ToInt32(Math.Sqrt(500));
      int m = 0;
      int n = 0;
     
      for (int nGuess = 1; nGuess <= maxN; nGuess++)
      {
        //skip if (500-n^2)/n (i.e. m) is not an integer
        if ((500 - nGuess * nGuess) % nGuess != 0)
          continue;

        //m is an integer
        m = (500 - nGuess * nGuess) / nGuess;

        //from a = n^2 - m^2 ==> n > m
        if (nGuess > m)
        {
          //break once nGuess > m is matched (from the question, there is only one possible n) 
          n = nGuess;
          break;
        }
      }

      int a = n * n - m * m;
      int b = 2 * n * m;
      int c = n * n + m * m;

      return a * b * c;
    }

    public Int64 Problem10()
    {
      Int64 sum = 0;
      for (int i = 2; i < 2000000; i++)
        if (CalculatorUtil.IsPrime(i))
          sum += i;

      return sum;
    }

    public int Problem11()
    {
      string longString = "08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08 " + 
                          "49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00 " + 
                          "81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65 " + 
                          "52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91 " + 
                          "22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80 " + 
                          "24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50 " + 
                          "32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70 " + 
                          "67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21 " + 
                          "24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72 " + 
                          "21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95 " + 
                          "78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92 " + 
                          "16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57 " + 
                          "86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58 " + 
                          "19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40 " + 
                          "04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66 " + 
                          "88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69 " + 
                          "04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36 " +  
                          "20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16 " + 
                          "20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54 " + 
                          "01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48";

      //make it a 400 entries int array
      string shortString = "";
      int[] intArray = new int[400];
      int sizeCount = 0;
      int UPPERBOUND = 19;
      for (int i = 0; i < longString.Length; i++)
      {
        if (longString[i] == ' ')
        {
          CalculatorUtil.Problem11Helper.StringToInt(ref intArray, ref sizeCount, shortString);
          shortString = "";
        }
        else
          shortString += longString[i];
      }

      //find largest among RToL and LToR diagoals
      int RToLLargest = 0, LToRLargest = 0, diagonalLargest = 0;
      int currentRToLLargest = 0, currentLToRLargest = 0;
      //Left to Right
      for (int i = 0; i < 17; i++)
      {
        currentLToRLargest = CalculatorUtil.Problem11Helper.DiagonalLargest_LToR(intArray, 14, UPPERBOUND);
        if (currentLToRLargest > LToRLargest)
          LToRLargest = currentLToRLargest;
      }
      //Right to Left
      for (int i = 5; i < 20; i++)
      {
        currentRToLLargest = CalculatorUtil.Problem11Helper.DiagonalLargest_RtoL(intArray, i, UPPERBOUND);
        if (currentRToLLargest > RToLLargest)
          RToLLargest = currentRToLLargest;
      }
      Debug.WriteLine("diagonal largest: = " + RToLLargest);
      diagonalLargest = RToLLargest;
      if (diagonalLargest < LToRLargest)
        diagonalLargest = LToRLargest;

      //find largest among rows and columns
      int rowLargest = 0, colLargest = 0, linearLargest = 0;
      int currentRowLargest = 0, currentColLargest = 0;
      for (int i = 0; i < 20; i++)
      {
        //if rows, traverseIncrement = 1; if cols, traverseIncrement = 20; 
        //find largest for rows
        currentRowLargest = CalculatorUtil.Problem11Helper.LinearLargest(intArray, i * 20, UPPERBOUND, 1);
        if (currentRowLargest > rowLargest)
          rowLargest = currentRowLargest;
        
        //find largest for cols
        currentColLargest = CalculatorUtil.Problem11Helper.LinearLargest(intArray, i, UPPERBOUND, 20);
        if (currentColLargest > colLargest)
          colLargest = currentColLargest;

      }
      linearLargest = rowLargest;
      if (linearLargest < colLargest)
        linearLargest = colLargest;
      Debug.WriteLine("linear largest: = " + colLargest);

      return linearLargest > diagonalLargest ? linearLargest : diagonalLargest;
    }

    public Int64 Problem12()
    {
      //the 45th triangle number is 1035, should be safe to start guessing from this number
      int startingNaturalNumber = 44;
      Int64 triangleNumber = CalculatorUtil.GetTriangleNumber(startingNaturalNumber);
      int divCount = 0;

      List<int> primeNums = CalculatorUtil.PrimeNums1to1M;
      while (divCount <= 500)
      {
        startingNaturalNumber++;
        triangleNumber += startingNaturalNumber;

        divCount = CalculatorUtil.GetDivisorsCount(triangleNumber, ref primeNums);
      }

      return triangleNumber;
    }

    public void Problem13()
    {
      //read the file line by line into an array
      int numberOfRows = 100, numberOfCols = 50;
      int[,] largeArray = new int[numberOfRows, numberOfCols];
      FileStream fs = new FileStream(@"..\..\Data\Problem13Data.txt", FileMode.Open, FileAccess.Read);
      StreamReader sr = new StreamReader(fs);
      sr.BaseStream.Seek(0, SeekOrigin.Begin);
      string aLine = sr.ReadLine();
      int rowCount = 0;
      while(aLine != null)
      {
        CalculatorUtil.ToIntArray(ref largeArray, rowCount, aLine);
        rowCount++;
        aLine = sr.ReadLine();
      }

      int toGive = 0;
      int oneColSum = 0;
      int resultSize = numberOfCols + 1;
      int[] result = new int[resultSize];
      //add up all numbers from the last columns, remember what to give to next col
      for (int j = 0; j < numberOfCols; j++)
      {
        //add up what's left from previous col
        oneColSum += toGive;

        //add up one column
        for (int i = 0; i < numberOfRows; i++)
          oneColSum += largeArray[i, j];  //replace 0 with y

        //value of that column
        result[j] = oneColSum % 10;

        //what to give to next column
        toGive = (oneColSum - result[j]) / 10; //replace 0 with y

        //reset column
        oneColSum = 0;
      }
      result[numberOfCols] = toGive;

      //print the result array
      Debug.Write("===>");
      int numOfDigit = 1;
      for (int i = --resultSize; i >= 0; i--)
      {
        Debug.Write(result[i]);
        numOfDigit++;

        if (numOfDigit == 10)
          Debug.WriteLine("");
      }
    }

    public int Problem14()
    {
      int itemCount = 0;
      int largerCount = 0;
      int longestItem = 0;
      for (int i = 1000000; i > 0; i--)
      {
        itemCount = CalculatorUtil.GetICollatzSequenceItemCount(i);
        if (itemCount > largerCount)
        {
          largerCount = itemCount;
          longestItem = i;
        }
      }
      return longestItem;
    }

    public double Problem15()
    {
      //denominator part
      double denom = 0;
      for (int i = 1; i <= 20; i++)
        denom += Math.Log10(i);

      //numerator
      double num = 0;
      for (int i = 21; i <= 40; i++)
        num += Math.Log10(i);

      double result = Math.Pow(10, (num - denom));

      //need to manually round the num to the nearest integer
      return result;
    }

    public double Problem16()
    {
      int powerOf2 = 1000;
      int[] largeArray = null;
      int sum = 0;
      List<int> result = CalculatorUtil.Problem16Helper.Calculate2PowNmin1(powerOf2, ref largeArray);
      for (int i = 0; i < result.Count; i++)
        sum += result[i];
      return sum;
    }

    public int Problem17()
    {
      FileStream fs = new FileStream(@"..\..\Data\Problem17Data.txt", FileMode.Open, FileAccess.Read);
      StreamReader sr = new StreamReader(fs);
      sr.BaseStream.Seek(0, SeekOrigin.Begin);
      string aLine = sr.ReadLine();

      int charCount = 0;
      while (aLine != null)
      {
        charCount += aLine.Length;
        aLine = sr.ReadLine();
      }

      /*
        charCount  		 	//*10
        {one, two...nine}	//(99+1)*each
        hundredand		    //*99*9
        hundred			    //1*9
        onethousand		    //*1
       */

      return charCount * 10 + 36 * 100 + 10 * 99 * 9 + 11 + 63;
    }

    public int Problem18()
    {
      int[][] dataArray = CalculatorUtil.Problem18Helper.ReadFileToIntArray(@"..\..\Data\Problem18Data.txt");

      CalculatorUtil.Problem18Helper.BinaryTree<int> bt = new CalculatorUtil.Problem18Helper.BinaryTree<int>();
      for(int i=0; i<dataArray.Length; i++)
        for (int j = 0; j < dataArray[i].Length; j++)
        {
          bt.AddNode(dataArray[i][j]);
        }

      return 0;
    }

    public int Problem18a()
    {
      int[][] dataArray = CalculatorUtil.Problem18Helper.ReadFileToIntArray(@"..\..\Data\Problem18Data.txt");

      int lastI = dataArray.Length - 1;
      int tempSum = 0;

      //row
      for (int i = 0; i < lastI; i++)
      {
        int oriNextRowj = 0;
        int oriNextRowNextCol = 0;

        //column
        for (int j = 0; j < dataArray[i].Length; j++)
        {
          if (dataArray[i][j] == 0)
            break;

          int nextRow = i; nextRow++;
          int nextCol = j; nextCol++;

          //if (oriNextRowj == 0)
          //  oriNextRowj = dataArray[nextRow][j];

          Debug.WriteLine(dataArray[i][j]);

          if (oriNextRowNextCol == 0)
            tempSum = dataArray[i][j] + dataArray[nextRow][j];
          else
            tempSum = dataArray[i][j] + oriNextRowNextCol;
          //tempSum = dataArray[i][j] + oriNextRowj;
          if (oriNextRowNextCol == 0)
            Debug.Write("+" + dataArray[nextRow][j] + " ==> ");
          else
            Debug.WriteLine("+" + oriNextRowNextCol + " ==> ");
          if (tempSum > dataArray[nextRow][j])
            dataArray[nextRow][j] = tempSum;
          Debug.WriteLine(dataArray[nextRow][j]);

          if (oriNextRowNextCol == 0)
            oriNextRowNextCol = dataArray[nextRow][nextCol];
          tempSum = dataArray[i][j] + dataArray[nextRow][nextCol];
          //tempSum = dataArray[i][j] + oriNextRowNextCol;
          Debug.Write("+" + dataArray[nextRow][nextCol] + " ==> ");
          if (tempSum > dataArray[nextRow][nextCol])
            dataArray[nextRow][nextCol] = tempSum;
          Debug.WriteLine(dataArray[nextRow][nextCol]);
        }
      }

      return 0;
    }

    public int Problem20(Int64 N)
    {
      Dictionary<Int64, BigInteger> memoKVPs = new Dictionary<Int64, BigInteger>();
      BigInteger bigInt = CalculatorUtil.GetFactorialBigInteger(N);
      char [] bigIntArr = bigInt.ToString().ToCharArray();
      int sumOfInt = 0;
      foreach (char c in bigIntArr)
        sumOfInt += Convert.ToInt16(c - '0');
      return sumOfInt;
    }

    public int Problem19()
    {      
      int NumOfSun = 0;

      DateTime dtStart = new DateTime(1901, 1, 1);
      DateTime dtEnd = new DateTime(2000, 12, 31);
      while (dtStart.CompareTo(dtEnd) != 0)
      {
        if (dtStart.DayOfWeek == DayOfWeek.Sunday && dtStart.Day == 1)
          NumOfSun++;
        dtStart = dtStart.Add(new TimeSpan(1, 0, 0, 0));
      }
      return NumOfSun;
    }

    public int Problem21()
    {
      Dictionary<int, int> amicablePairs = new Dictionary<int, int>();
      List<int> dnList = new List<int>();

      //(220,284) is the first pair so starting from 220
      for (int n = 220; n < 10000; n++)
      {
        //For cases like 220 vs 284; Once n = 200 is through, 284 was added to dnList 
        //so when n = 284, the number will be skipped directly
        if (dnList.Contains(n)) continue;

        //Get d(n) from n
        int dn = CalculatorUtil.GetNumbersSum(CalculatorUtil.GetDivisors(n)); 

        if (dn == n) continue;

        //Get d(dn) from dn
        int m = CalculatorUtil.GetNumbersSum(CalculatorUtil.GetDivisors(dn));

        //if(d(dn) == n), add (n, dn) to amicablePairs and add dn to dnList 
        if (m == n)
        {
          amicablePairs.Add(n, dn);
          dnList.Add(dn);
        }
      }

      //Sum the key and valus 
      int keySum = amicablePairs.Select(kvp => kvp.Key).Sum();
      int valueSum = amicablePairs.Select(kvp => kvp.Value).Sum();

      return keySum + valueSum;
    }

    public Int64 Problem22()
    {
      StringBuilder sb = new StringBuilder();
      using (StreamReader sr = new StreamReader(@"..\..\Data\Problem22Data.txt"))
      {
        sb.Append(sr.ReadLine());
      }

      List<string> names = sb.ToString().Split(',').ToList<string>();
      names.Sort();

      Int64 nameScore = 0;
      int position = 1;
      for (int i = 0; i < names.Count(); i++)
      {
        nameScore += position * CalculatorUtil.GetSumOfAlphabetOrder(names[i]);
        position++;
      }
      return nameScore;
    }

    public int Problem23()
    {
      int upper = 28123;
      int upperHalf = (upper + 1) / 2;

      //List of all nums from 1 to upper bound
      List<int> num1to28123 = new List<int>();
      for (int i = 1; i <= upper; i++)
        num1to28123.Add(i);

      //List of all abundant nums from 1 to upper bound
      List<int> abundantNumsLeft = new List<int>();
      abundantNumsLeft = num1to28123.Where(n => CalculatorUtil.IsNumberDeficientPerfectAbundunt(n) == CalculatorUtil.DeficientPerfectAbundunt.Abundant).ToList<int>();

      //Make a copy of the abundant nums from 1 to upper bound
      List<int> abundantNumsRight = new List<int>(abundantNumsLeft);

      //Get the nums from 1 to 28123 which = as sum of 2 abundant nums
      List<int> sumOfAbundantNums = new List<int>();
      foreach (int left in abundantNumsLeft)
      {
        foreach (int right in abundantNumsRight)
        {
          //if both are >= half of 28124 then skip
          if (left >= upperHalf && right >= upperHalf)
            break;

          int leftPlusRight = left + right;
          if (leftPlusRight > upper)
            continue;

          sumOfAbundantNums.Add(leftPlusRight);
        }
      }

      //filter out the repeated entries
      List<int> sumOfAbundantNumsFiltered = sumOfAbundantNums.Distinct().ToList();

      //Get the rest of the nums (can't be expressed as sum of 2 abundant nums)
      List<int> sumOfNonAbundantNums = num1to28123.Where(n => !sumOfAbundantNumsFiltered.Contains(n)).ToList<int>();

      return sumOfNonAbundantNums.Sum();
    }
   
    public int Problem24()
    {
      int guessNum = 1;
      int guessNumPlus1 = 2;
      int MILLION = 25;
      Dictionary<int, long> guessNumMemoKVPs = new Dictionary<int, long>();
      guessNumMemoKVPs.Add(1, 1);
      Dictionary<int, long> guessNumPlus1MemoKVPs = new Dictionary<int, long>();
      guessNumPlus1MemoKVPs.Add(1, 1);

      int digit = 1;
      while (guessNum < MILLION)
      {
        //long factGUessNum = CalculatorUtil.GetFactorial(guessNumMemoKVPs, guessNum);
        //long factGUessNumPlus1 = CalculatorUtil.GetFactorial(guessNumPlus1MemoKVPs, guessNumPlus1);

        long factGUessNum = CalculatorUtil.GetFactorial(guessNum);
        long factGUessNumPlus1 = CalculatorUtil.GetFactorial(guessNumPlus1);
        if (factGUessNum < MILLION && factGUessNumPlus1 > MILLION)
        {
          digit = guessNum;
          break;
        }
        else if (factGUessNum == MILLION)
        {
          digit = guessNum;
          break;
        }
        else if (factGUessNumPlus1 == MILLION)
        {
          digit = guessNumPlus1;
          break;
        }
        else
        {
          guessNum = guessNumPlus1;
          ++guessNumPlus1;
        }
      }

      return guessNum;
    }

    public int Problem25()
    {
      int n = 1;
      int NumOfDigits = 1000;
      while (true)
      {
        CalculatorUtil.PopulateFibonacciList(n);
        if (CalculatorUtil.GetDigitsCount(CalculatorUtil.BigInts.Last()) == NumOfDigits)
        {
          Console.WriteLine(n);
          break;
        }
        else
          n++;
      }
      return n;
    }

    public int Problem26()
    {
      //Find all recurring fractions from 1/1 to 1/1000
      List<int> RecurringFractions = new List<int>();
      for (int i = 1; i < 1000; i++)
        if (CalculatorUtil.IsFractionRecurring(i))
          RecurringFractions.Add(i);

      List<int> Quotients = new List<int>();
      List<int> recurringCycles = new List<int>();
      Dictionary<int, int> denom_recurringCycles = new Dictionary<int, int>();
      foreach (int denominator in RecurringFractions)
      {
        Quotients = CalculatorUtil.GetDigitsAfterDecimals(denominator);
        denom_recurringCycles.Add(denominator, (Quotients.LastIndexOf(Quotients.Last()) - Quotients.IndexOf(Quotients.Last())));
      }

      int maxCycle = denom_recurringCycles.Values.Max();

      return denom_recurringCycles.First(kvp => kvp.Value == maxCycle).Key;
    }

    public int Problem27()
    {
      int n = 0;
      int result = 0;
      int primeCount = 0;
      int largestPrimeCount = primeCount;
      int productOfCoeffs = 0;

      for (int a = -999; a <= 999; a++)
      {
        for (int b = -999; b <= 999; b++)
        {
          while (true)
          {
            result = n * n + a * n + b;

            if (result < 0 || result % 2 == 0)
              break;
            if (!CalculatorUtil.IsPrime(result))
            {
              if (primeCount > largestPrimeCount)
              {
                largestPrimeCount = primeCount;
                productOfCoeffs = a * b;
              }
              break;
            }
            else
              primeCount++;

            n++;
          }
          n = 0;
          primeCount = 0;
        }
      }

      return productOfCoeffs;
    }

    public int Problem28()
    {
      List<int> diagonals = new List<int>();

      int current = 1;
      int flagOf4 = 1;
      int maxRows = 1001; 
      int halfTheRows = (maxRows - 1) / 2 + 1;  

      for (int numOfLayers = 1; numOfLayers < halfTheRows; numOfLayers++)
      {
        while (flagOf4 <= 4)
        {
          diagonals.Add(current);
          current = diagonals.Last() + (2 * numOfLayers);
          ++flagOf4;
        }
        flagOf4 = 1;
      }
      diagonals.Add(current);

      return CalculatorUtil.GetNumbersSum(diagonals);
    }

    public int Problem29()
    {
      int max = 100;
      List<BigInteger> nums = new List<BigInteger>();

      for (int _base = 2; _base <= max; _base++)
        for (int power = 2; power <= max; power++)
          nums.Add(CalculatorUtil.GetPower(_base, power));

      int distinctCount = nums.Distinct().Count();

      return distinctCount;
    }

    public int Problem30()
    {
      List<int> targets = new List<int>();
      int max = Convert.ToInt32(Math.Pow(9, 5) * 6);
      int sum;
      for (int i = 2; i <= max; i++)
      {
        sum = 0;
        char [] maxString = i.ToString().ToCharArray();
        foreach (char c in maxString)
        {
          int x = CalculatorUtil.CharToInt(c);
          sum += Convert.ToInt32(Math.Pow(x,5));
        }
        if (sum == i)
          targets.Add(i);
      }

      return CalculatorUtil.GetNumbersSum(targets);
    }

    public int Problem31()
    {
      int p100 = 100;
      int p50 = 50;
      int p20 = 20;
      int p10 = 10;
      int p5 = 5;
      int p2 = 2;
      int p1 = 1;
      int combinationCount = 0;

      int p2Sum = 0;
      int p1Sum = 0;
      int goal = 200;

      int sum = 0;
      while (true)
      {
        while (sum < goal)
        {
          while (sum < goal)
          {
            sum += p2;
          }
          int diff = goal - p2Sum;

          sum += p1;
        }
      }

      return 0;
    }

    public int Problem32()
    {
      List<Int32> PanDigitProducts = new List<Int32>();

      //Part 1, x = 1-8 and y = 1234-9876
      CalculatorUtil.PanDigits_P32Helper.CalculatePanDigits(1, 8, 1234, 9876, ref PanDigitProducts);

      //Part 2, x = 12-82 and y = 123-983
      CalculatorUtil.PanDigits_P32Helper.CalculatePanDigits(12, 82, 123, 983, ref PanDigitProducts);

      Int32 sum = CalculatorUtil.GetNumbersSum(PanDigitProducts.Distinct().ToList());
      return sum;
    }

    public int Problem33()
    {
      for (int num = 11; num <= 99; num++)
      {
        for (int den = 11; den <= 99; den++)
        {
          //number > 1, skip
          if (num >= den)
            continue;
          //trivial example, skip
          if (num % 10 == 0 && den % 10 == 0)
            continue;
          //For cases like 11/22, cancelling digit and the remaining value will be undefined, skip
          if (num % 11 == 0 && den % 11 == 0)
            continue;
          if (num == den)
            continue;
          //no char in num is the same as den, skip
          Func<int, int, char> GetNumDenSameChar = delegate(int n, int d)
          {
            foreach (char numChar in n.ToString())
              if (d.ToString().Any(denC => denC == numChar))
                return numChar;
            return '0';
          };

          char sameChar = GetNumDenSameChar(num,den);
          if (sameChar == '0')
            continue;

          string numString = num.ToString();
          string denString = den.ToString();

          int newNum = Convert.ToInt16(numString.Substring(1 - numString.IndexOf(sameChar), 1));
          int newDen = Convert.ToInt16(denString.Substring(1 - denString.IndexOf(sameChar), 1));

          string fractionOri = num.ToString() + ',' + den.ToString();
          string fractionNew = newNum.ToString() + ',' + newDen.ToString();
        
          //The rest of the steps will be done by hand
          if ((double)num / (double)den == (double)newNum / (double)newDen)
            Debug.WriteLine(fractionOri + " ----> " + fractionNew);
        }
      }
      return 0;
    }

    public int Problem34()
    {
      //List of numbers that match the requirement
      List<int> matchedList = new List<int>();

      //upper limit will be 9! * 7 
      //since 9! * 8 is also a 7-digit number
      //http://www.mathblog.dk/project-euler-34-factorial-digits/
      int upperLimit = Convert.ToInt32(CalculatorUtil.GetFactorial(9)) * 7;

      //Get the factorials of digits 1 to 9 to avoid repeated calculations
      List<long> factorials0To9 = GetFactorials0To9();

      for (int number = 3; number <= upperLimit; number++)
      {
        int numberCopy = number; //a copy of the number for manipulation
        long sumOfDigitFactorial = 0; //sum of the factorials of the digits
        int digit = 0;  //a digit of the given number

        while (numberCopy >= 1)
        {
          digit = Convert.ToInt16(numberCopy % 10);
          sumOfDigitFactorial += factorials0To9[digit]; 
          numberCopy = numberCopy / 10;
        }
        if (sumOfDigitFactorial == number)
          matchedList.Add(number);  //add to the list
      }

      return CalculatorUtil.GetNumbersSum(matchedList);
    }

    private List<long> GetFactorials0To9() 
    {
      List<long> factorials0To9 = new List<long>();

      for(int i=0; i<=9; i++)
        factorials0To9.Add(CalculatorUtil.GetFactorial(i));
      return factorials0To9;
    }

    public int Problem35()
    {
      int max = 1000000;
      List<Int32> primeNums = new List<int>();
      List<Int32> circularPrimeNums = new List<int>();
      List<Int32> circular = new List<Int32>();

      //prime numbers < 10 are not circular 
      //11 is not circular neither
      //hence starts from 13
      for (Int32 i = 2; i < max; i++)
        if (CalculatorUtil.IsPrime(i))
          primeNums.Add(i);

      foreach (Int32 primeNum in primeNums)
      {
        //If the prime number contians 2,4,5,6,8,0, there must be at least one non-prime #
        //except for 2 and 5 
        string primeNumS = primeNum.ToString();
        if (primeNum != 2 && primeNum != 5 && (primeNumS.Contains('0') ||
          primeNumS.Contains('2') ||
          primeNumS.Contains('4') ||
          primeNumS.Contains('5') ||
          primeNumS.Contains('6') ||
          primeNumS.Contains('8')))
          continue;

        //Get the circular numbers out of the prime number
        circular = CalculatorUtil.GetCircularNumbers(primeNum);

        //are all of them prime numbers?
        bool areAllPrimeNums = circular.Count(p => CalculatorUtil.IsPrime(p)) == circular.Count ? true : false;
        if (areAllPrimeNums)
        {
          //Add only if it does not exist in the list 
          foreach (Int32 permutationI in circular)
            if (!circularPrimeNums.Contains(permutationI))
              circularPrimeNums.Add(permutationI);
        }
      }

      return circularPrimeNums.Count;
    }
    
    public int Problem36()
    {
      List<int> matchedList = new List<int>();
      string binary = "";
      bool isDecPalindromic = false;
      bool isBinPalindromic = false;
      for (int i = 0; i <= 999999; i++)
      {
        //Check if the number is Palindromic
        isDecPalindromic = CalculatorUtil.IsPalindromic(i + "");

        //Check if the binary form of the number is Palindromic
        binary = CalculatorUtil.GetBinaryNumber(i);
        isBinPalindromic = CalculatorUtil.IsPalindromic(binary);

        //Add to the result list if both are true
        if (isBinPalindromic && isDecPalindromic)
          matchedList.Add(i);
      }
      return CalculatorUtil.GetNumbersSum(matchedList);
    }


    public long Problem37()
    {
      //Assume the evelen numbers are all within 1 to 1M
      List<int> primes = new List<int>();
      primes.AddRange(CalculatorUtil.PrimeNums1to1M);

      //Set the starting prime as 23 as anything below contains non-prime number
      int primeIndex = 0;
      for (int i = 0; primes[i] <= 23; i++)
        primeIndex = i;

      int prime = 0;  //current prime number
      string numString = "";  //current prime number in string
      string subString = "";  //sub-string of the numString
      int subStringLength = 1;  //determines the length of the sub-string to check
      bool leftToRightAllPrimes = true;
      bool rightToLeftAllPrimes = true;
      List<int> matchedNumbers = new List<int>(); //list of truncatable prime numbers

      while(matchedNumbers.Count != 11)
      {
        prime = primes[primeIndex++];

        //resetting values for the next prime
        numString = prime + "";
        subString = "";
        subStringLength = 1;
        leftToRightAllPrimes = true;
        rightToLeftAllPrimes = true;

        //basic checking - a number with one of the following will not be a truncatable prime
        if (numString.StartsWith("1") ||
          numString.EndsWith("1") || 
          numString.Contains('0') || 
          numString.Contains('4') || 
          numString.Contains('6') || 
          numString.Contains('8'))
          continue;

        //check truncatable: left to right
        for(int index = 0; index < numString.Length; index++)
        {
          subString = numString.Substring(0, subStringLength++);
          if (!CalculatorUtil.IsPrime(Convert.ToInt32(subString)))
          {
            leftToRightAllPrimes = false;
            break;
          }
        }
        if (!leftToRightAllPrimes) continue;  //don't proceed if one of the sub-numbers is not prime

        //check truncatable: right to left
        subStringLength = 1;
        numString = prime + "";
        for (int index = numString.Length - 1; index >= 0; index--)
        {
          subString = numString.Substring(index, subStringLength++);
          if (!CalculatorUtil.IsPrime(Convert.ToInt32(subString)))
          {
            rightToLeftAllPrimes = false;
            break;
          }  
        }
        if (!rightToLeftAllPrimes) continue;  //don't add to the result list if one of the sub-numbers is not prime

        matchedNumbers.Add(prime);
      }

      return CalculatorUtil.GetNumbersSum(matchedNumbers);
    }

    public string Problem38()
    {
      List<string> PanDigits = new List<string>();
      StringBuilder sb = new StringBuilder();
      int oneProduct = 0;
      string oneProductStr = "";
      string appendedStr = "";
      int len = 0;

      for (int i = 1; i <= 9999; i++)
      {
        if (CalculatorUtil.PanDigits_P32Helper.Has0(i.ToString()) ||
          CalculatorUtil.PanDigits_P32Helper.RepeatedChar(i.ToString()) != null)
          continue;

        for (int n = 1; n <= 9; n++)
        {
          oneProduct = i * n;
          oneProductStr = oneProduct.ToString();
          if (CalculatorUtil.PanDigits_P32Helper.Has0(oneProductStr) ||
            CalculatorUtil.PanDigits_P32Helper.RepeatedChar(oneProductStr) != null)
            break;

          sb.Append(oneProduct);
          appendedStr = sb.ToString();
          len = appendedStr.Length;

          if (CalculatorUtil.PanDigits_P32Helper.RepeatedChar(appendedStr) != null)
            break;

          if (len > 9)
            break;

          if (len == 9)
          {
            Debug.WriteLine(i + " * " + n + " = " + appendedStr);
            PanDigits.Add(appendedStr);
            break;
          }
        }
        sb = new StringBuilder();
      }
      return "The last number is the answer";
    }

    public int Problem39b()
    {
      int pMax = 1000;
      Dictionary<int, int> numOfSlns = new Dictionary<int, int>();
      SortedSet<int> abc;
      SortedSet<int> abcOld = new SortedSet<int>();

      for (int p = 12; p <= pMax; p++)
      {
        numOfSlns.Add(p, 0);
        for (int a = 1; a < p; a++)
        {
          for (int b = 1; b < p; b++)
          {
            if (a + b >= p)
              break;

            int c = p - a - b;

            Int32 aSq_Plus_bSq = (Int32)(Math.Pow(a, 2) + Math.Pow(b, 2));
            Int32 aSq_Mins_bSq = Math.Abs((Int32)(Math.Pow(a, 2) - Math.Pow(b, 2)));

            //not a Right angled triangle
            if ((Int32)Math.Pow(c, 2) != aSq_Plus_bSq && (Int32)Math.Pow(c, 2) != aSq_Mins_bSq)
              continue;

            if (a + b + c == p)
            {
              abc = new SortedSet<int>(new int[] { a, b, c });
              if (!abc.SetEquals(abcOld))
                ++numOfSlns[p];
              abcOld = abc;
            }
          }
        }
        if (numOfSlns[p] == 0 || numOfSlns[p] == 1)
          numOfSlns.Remove(p);
      }
      return 0;
    }

    public int Problem39a()
    {
      int a, b, c;
      int sum = 0;
      int curNumOfSlns = 0;
      int maxNumOfSlns = 0;

      for(int m=3; m<22; m++)
      {
        for (int n = 3; n < 22; n++)
        {
          a = m * m - n * n;
          b = 2 * m * n;
          c = m * m + n * n;
          sum = a + b + c;

          if (a <= 0 || b <= 0)
            break;

          if (a + b + c > 1000)
          {
            if (curNumOfSlns > maxNumOfSlns)
            {
              maxNumOfSlns = curNumOfSlns;
              Debug.WriteLine(string.Format("maxNumOfSlns {0}", maxNumOfSlns));
            }
            break;
          }
          else
          {
            Debug.WriteLine(string.Format("{0} + {1} + {2} = {3}", a, b, c, sum));
            curNumOfSlns++;
          }
        }

        curNumOfSlns = 0;
      }

      return 0;
    }

    public int Problem39()
    {
      Dictionary<int, int> sumCountPairs = new Dictionary<int, int>();
      List<Tuple<int, int, int>> pythTriples = this.GetPythTriples();

      int sum = 0;
      bool is120 = false;
      foreach (Tuple<int, int, int> pythTriple in pythTriples)
      {
        sum = pythTriple.Item1 + pythTriple.Item2 + pythTriple.Item3;
        if (sum == 120) is120 = true;
        if (sum > 1000)
          continue;

        if (!sumCountPairs.ContainsKey(sum))
          sumCountPairs.Add(sum, 1);
        else
          ++sumCountPairs[sum];
      }
      //Find the key from the sumCountPairs with the highest value
      //code adopted from http://stackoverflow.com/questions/2805703/good-way-to-get-the-key-of-the-highest-value-of-a-dictionary-in-c-sharp
      int maxValueKey = sumCountPairs.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

      return maxValueKey;
    }

    //All the pyth. triples whose sums are below 1000
    private List<Tuple<int, int, int>> GetPythTriples()
    {
      //http://en.wikipedia.org/wiki/Formulas_for_generating_Pythagorean_triples
      List<Tuple<int, int, int>> PythTriples = new List<Tuple<int, int, int>>();

      int a = 0, b = 4, c = 0;
      int m = 0, n = 0;

      bool is120 = false;

      while (b < 1000)
      {
        if (b == 48) is120 = true;
        List<int> factors = CalculatorUtil.GetFactors(b / 2);  
        List<Tuple<int, int>> factorsPairs = GetFactorsPairs(factors);

        foreach (Tuple<int, int> factorsPair in factorsPairs)
        {
          m = factorsPair.Item1; n = factorsPair.Item2;
          a = m * m - n * n;
          c = m * m + n * n;

          if (a + b + c == 120) is120 = true;

          if (c > 1000)
            continue;

          if (a + b + c <= 1000)
            PythTriples.Add(new Tuple<int, int, int>(a, b, c));
        }

        b += 2; 
      }

      return PythTriples;
    }

    private List<Tuple<int,int>> GetFactorsPairs(List<int> factors)
    {
      List<Tuple<int, int>> factorsPairs = new List<Tuple<int, int>>();
      int lastIndex = factors.Count;
      --lastIndex;
      for (int i = 0; i <= (factors.Count / 2) - 1; i++)
        factorsPairs.Add(new Tuple<int, int>(factors[lastIndex - i], factors[i]));
      return factorsPairs;
    }

    public int Problem40()
    {
      StringBuilder sb = new StringBuilder('0');
      int i = 1;

      List<int> positions = new List<int>(new int[] { 0, 9, 99, 999, 9999, 99999, 999999 });
      int catcha = 1;

      while (true)
      {
        sb.Append(i);
        i++;

        if(sb.Length>positions[0])
        {
          catcha *= CalculatorUtil.CharToInt(sb[positions[0]]);
          positions.RemoveAt(0);
        }

        if (positions.Count == 0)
          break;
      }

      return catcha;
    }

    public Int32 Problem41a()
    {
      Int32 max = 987654321;
      while (max >= 2)
      {
        while (CalculatorUtil.HasDigit(max, 0))
        {
          int log10 = max.ToString().Length - 1 - max.ToString().IndexOf('0');
          max = max - Convert.ToInt32(Math.Pow(10, log10));

          while (!CalculatorUtil.HasDigit(max, 1) || CalculatorUtil.HasRepeatedChar(max.ToString()) != null)
            --max;
        }

        while (!CalculatorUtil.HasDigit(max, 1) || CalculatorUtil.HasRepeatedChar(max.ToString()) != null)
        {
          --max;
          while (CalculatorUtil.HasDigit(max, 0))
          {
            int log10 = max.ToString().Length - 1 - max.ToString().IndexOf('0');
            max = max - Convert.ToInt32(Math.Pow(10, log10));
          }
        }

        int highestDigit = max.ToString().ToCharArray().Max() - 48;
        if (highestDigit == max.ToString().Length)
        {
          if (CalculatorUtil.IsPrime(max))
            return max;
          else
            --max;
        }
        else
          --max;
      }
      return 0;
    }

    /// <summary>
    /// Approach adopted from http://www.mathblog.dk/project-euler-41-pandigital-prime/)
    /// </summary>
    /// <returns></returns>
    public int Problem41()
    {
      // Max possible pandigital prime number would be 7654321 (see the explanaton on
      // http://www.mathblog.dk/project-euler-41-pandigital-prime/)    
      int upperLimiit = 7654321;

      //Get all prime numbers up to the upper limit using the ESieve method
      var primeNums = CalculatorUtil.GetPrimeNumsUpTo(upperLimiit);
      //Reverse the numbers so we start from highest number 
      primeNums.Reverse();
      
      //Return when the first pandigital prime is found
      foreach (int primeNum in primeNums)
        if (CalculatorUtil.IsPandigital(primeNum))
          return primeNum;
      
      return 0;
    }
        
    public int Problem42()
    {
      StringBuilder sb = new StringBuilder();
      using (StreamReader sr = new StreamReader(@"..\..\Data\Problem42Data.txt"))
      {
        sb.Append(sr.ReadLine());
      }

      string[] vocabs = sb.ToString().Split(',');
      int longestLen = 0;
      foreach (string vocab in vocabs)
      {
        if (vocab.Length > longestLen)
          longestLen = vocab.Length;
      }

      int max = longestLen * 26;
      List<int> AllTriAngleNums = CalculatorUtil.GetTriangleNumbers(1, max, true);

      int tWord = 0;
      foreach (string vocab in vocabs)
      {
        int number = CalculatorUtil.GetSumOfAlphabetOrder(vocab);
        if (AllTriAngleNums.Contains(number))
        {
          Debug.WriteLine(vocab + ", " + number);
          tWord++;
        }
      }
      return tWord;
    }

    public long FixRepeatedDigit(long number, bool increment)
    {
      List<char> repeatedChars = CalculatorUtil.HasRepeatedChars(number.ToString());
      string numberStr = number.ToString();
      int powerToRaise = numberStr.Count() - 1 - numberStr.LastIndexOf(repeatedChars[repeatedChars.Count - 1] + "");

      if (increment)
        return (number + (Int32)Math.Pow(10, powerToRaise));
      else
        return (number - (Int32)Math.Pow(10, powerToRaise));
    }

    public long Problem43()
    {
      long panSum = 0;
      long i = 1023456789;
      long max = 9876543210;
      while(i <= 9876543210)
      {
        while (CalculatorUtil.HasRepeatedInt(i) != -1 && i <= max)
          i = FixRepeatedDigit(i, true);

        string numStr = i.ToString();

        if (Convert.ToInt16(numStr.Substring(7, 3)) % 17 != 0)
        { i++; continue; }

        if (Convert.ToInt16(numStr.Substring(6, 3)) % 13 != 0)
        { i++; continue; }

        if (Convert.ToInt16(numStr.Substring(5, 3)) % 11 != 0)
        { i++; continue; }

        if (Convert.ToInt16(numStr.Substring(4, 3)) % 7 != 0)
        { i++; continue; }

        if (Convert.ToInt16(numStr.Substring(3, 3)) % 5 != 0)
        { i++; continue; }

        if (Convert.ToInt16(numStr.Substring(2, 3)) % 3 != 0)
        { i++; continue; }

        if (Convert.ToInt16(numStr.Substring(1, 3)) % 2 == 0)
        {
          panSum += i;
          i++; continue; 
        }

        ++i;
      }
      return panSum;
    }

    public int Problem44()
    {
      int currentPn, nextPn;
      int D = 0;
      int from = 1, to = 5000;

      //first get an initial set of Pn (1 to 5000)
      List<int> PnList = new List<int>();
      for (int i = from; i <= to; i++)
        PnList.Add(CalculatorUtil.GetNthPentagonalNumber(i));
      int[] PnSet = PnList.ToArray();

      int PnSetLenth = PnSet.Length;

      #region Method One. Add x to { 2, 3..... lastIndex }, then x++ and repeat the steps 
      //From 0 up until the last one in the set
      for (int i = 0; i < PnSetLenth - 1; i++)
      {
        //From the one after x up until the last one in the set
        for (int j = i + 1; j < PnSetLenth; j++)
        {
          currentPn = PnSet[i];
          nextPn = PnSet[j];
          D = nextPn - currentPn;
          if (CalculatorUtil.IsPentagonal(D))
            if (CalculatorUtil.IsPentagonal(currentPn + nextPn))
              return D;
        }
      }
      #endregion

      #region Method Two. Add 1 to 2, 2 to 3 .... nGuess-1 to nGuess, then 1 to 3 ... nGuess-2 to nGuess, and so on
      int jumpIndex = 1;
      //From 0 up until the last one in the set
      for (int i = 0; i < PnSetLenth - 1; i++)
      {
        if (jumpIndex == PnSetLenth)
          return 0;

        currentPn = PnSet[i];

        nextPn = PnSet[i + jumpIndex];

        D = nextPn - currentPn;
        if (CalculatorUtil.IsPentagonal(D))
          if (CalculatorUtil.IsPentagonal(currentPn + nextPn))
            return D;

        //No more Pn[nextIndex] after the jump, increment jumpIndex & reset x 
        if (i + jumpIndex + 1 >= PnSetLenth)
        {
          jumpIndex++;
          i = -1;
          continue;
        }
      }
      #endregion

      return 0;
    }

    public void Problem45() 
    { 
    }


    #region problem 454
    // reorder the given formula, we have (x+y)/(xy) = 1/nGuess
    // Put log on both sides when dealing with division of large number
    // And re-order a bit more so that we avoid multiplying x by y, we have: 
    // log(x) + log(y) - log(x+y) =  log(nGuess) 
    // The question becomes: given x < y <= L, find #solutions for which log(x) + log(y) - log(x+y) =  log(nGuess) 
    // We loop through all possible x and y to obtain the value on the left hand side
    // x.e. LHS = log(nGuess)
    // ==>  10^LHS = nGuess
    // x.e if(10^LHS) is an integer, then we have found a solution
    //
    public Int64 Problem454_A1()
    {
      Int64 L = Convert.ToInt64(Math.Pow(10, 12));
      Int64 slnCount = 0;
      Stopwatch sw = new Stopwatch();
      sw.Start();

      for (Int64 x = 3; x <= L - 1; x++)
        for (Int64 y = x + 1; y <= L; y++)
        {
          double d = Math.Log10(x) + Math.Log10(y) - Math.Log10(x + y);

          if (Math.Round(Math.Pow(10, d), 3) % 1 == 0)
          {
            //Debug.WriteLine(string.Format("1/{0} + 1/{1}", x, y));
            slnCount++;
          }

          //BigInteger m = new BigInteger(x * y);
          //BigInteger a = new BigInteger(x + y);
          //if (((double)m / (double)a) % 1 == 0)
          //{
          //  //Debug.WriteLine(string.Format("1/{0} + 1/{1}", x, y));
          //  slnCount++;
          //}
        }
      sw.Stop();
      TimeSpan ts = new TimeSpan(sw.ElapsedTicks);
      return slnCount;
    }

    //public string Problem454()
    //{
    //  Int64 LargestN = Convert.ToInt64(Math.Pow(10, 12)) - 2;

    //  Int64 divisorCount = CalculatorUtil.GetDivisorsCount(LargestN ^ 2);

    //  return ((divisorCount + 1) / 2).ToString();
    //}

    #endregion
  }
}

















