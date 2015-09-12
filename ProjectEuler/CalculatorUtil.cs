using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  class CalculatorUtil
  {
    #region Cleaned up and Improved comments
    //Get all prime numbers from 1 to 1 million
    public static List<int> PrimeNums1to1M
    {
      get { return GetAllPrimeNums1to1M(); }
    }

    ////Get all prime numbers from 1 millon to 2 million
    //public static List<Int64> PrimeNums1Mto2M
    //{
    //  get { return GetAllPrimeNums1Mto2M(); }
    //}

    ////Get all prime numbers from 2 millon to 3 million
    //public static List<Int64> PrimeNums2Mto3M
    //{
    //  get { return GetAllPrimeNums2Mto3M(); }
    //}

    ////Get all prime numbers from 3 millon to 4 million
    //public static List<Int64> PrimeNums3Mto4M
    //{
    //  get { return GetAllPrimeNums3Mto4M(); }
    //}


    /// <summary>
    /// Abundant - Sum of factors of a given number > the number 
    /// Perfect - Sum of factors of a given number = the number 
    /// Deficient - Sum of factors of a given number < the number 
    /// </summary>
    public enum DeficientPerfectAbundunt
    {
      Abundant, Perfect, Deficient
    }

    #region Helper classes
    public class Problem11Helper
    {
      public static int LinearLargest(int[] intArray, int startIndex, int UPPERBOUND, int traverseIncrement)
      {
        int tempStartIndex;
        int currentProduct = 1;
        int largerProduct = 0;
        for (int j = startIndex; j <= startIndex + (UPPERBOUND - 3) * traverseIncrement; j += traverseIncrement)
        {
          tempStartIndex = j;
          currentProduct = intArray[tempStartIndex];

          for (int i = 0; i < 3; i++)
          {
            if (intArray[tempStartIndex] != 0)
            {
              tempStartIndex += traverseIncrement;
              currentProduct *= intArray[tempStartIndex];
            }
            else
            {
              currentProduct = 0;
              break;
            }
          }

          if (largerProduct < currentProduct)
            largerProduct = currentProduct;
        }
        return largerProduct;
      }

      public static int DiagonalLargest_RtoL(int[] intArray, int startIndex, int UPPERBOUND)
      {
        int tempStartIndex;
        int currentProduct = 1;
        int largerProduct = 0;

        //calculate largest for one diagonal
        for (int j = startIndex; j <= 20 * startIndex - 19 * 3; j += 19)
        {
          tempStartIndex = j;

          //find product of 4 
          currentProduct = intArray[tempStartIndex];
          for (int i = 0; i < 3; i++)
          {
            if (intArray[tempStartIndex] != 0)
            {
              tempStartIndex = tempStartIndex + 19;
              currentProduct *= intArray[tempStartIndex];
            }
            else
            {
              currentProduct = 0;
              break;
            }
          }

          if (largerProduct < currentProduct)
            largerProduct = currentProduct;
        }
        return largerProduct;
      }

      public static int DiagonalLargest_LToR(int[] intArray, int startIndex, int UPPERBOUND)
      {
        int tempStartIndex;
        int currentProduct = 1;
        int largerProduct = 0;

        //calculate largest for one diagonal
        for (int j = startIndex; j <= startIndex + (UPPERBOUND - startIndex - 3) * 21; j += 21)  //20 * startIndex - 21 * 3
        {
          tempStartIndex = j;

          //find product of 4 
          currentProduct = intArray[tempStartIndex];
          for (int i = 0; i < 3; i++)
          {
            if (intArray[tempStartIndex] != 0)
            {
              tempStartIndex = tempStartIndex + 21;
              currentProduct *= intArray[tempStartIndex];
            }
            else
            {
              currentProduct = 0;
              break;
            }
          }
          if (largerProduct < currentProduct)
            largerProduct = currentProduct;
        }
        return largerProduct;
      }

      public static void StringToInt(ref int[] intArraySingleRow, ref int sizeCount, string shortString)
      {
        if (shortString[0] == '0')
          shortString = shortString[1] + "";

        intArraySingleRow[sizeCount] = Convert.ToInt16(shortString);

        sizeCount++;
      }
    }

    public class Problem16Helper
    {
      public static List<int> Calculate2PowNmin1(int N, ref int[] largeArray)
      {
        if (N == 1)
          return new List<int>() { 2 };

        return ArrayItemsMultipleBy2(Calculate2PowNmin1(N - 1, ref largeArray));
      }

      private static List<int> ArrayItemsMultipleBy2(List<int> currentNumberArray)
      {
        int toGive = 0, oneColSum = 0;
        int numberOfCols = currentNumberArray.Count;
        List<int> result = new List<int>();

        //add up all numbers from the last columns, remember what to give to next col
        for (int j = 0; j < numberOfCols; j++)
        {
          //add up what's left from previous col
          oneColSum += toGive;

          //add up one column
          oneColSum += currentNumberArray[j] * 2;

          //value of that column
          result.Add(oneColSum % 10);

          //what to give to next column
          toGive = (oneColSum - result[j]) / 10; //replace 0 with y

          //reset column
          oneColSum = 0;
        }
        if (toGive > 0)
          result.Add(toGive);

        return result;
      }

      public static double Calculate2PowNmin1(int N)
      {
        double current = 0;
        if (N == 1)
          return 2;

        current = Calculate2PowNmin1(N - 1) + Calculate2PowNmin1(N - 1);

        return current;
      }
    }

    public class Problem18Helper
    {
      public static int[][] ReadFileToIntArray(string filePath)
      {
        #region Trial 1
        //FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //StreamReader sr = new StreamReader(fs);

        //IList<int>[] arrayOfList = new IList<int>[15];
        //string shortString = "";
        //int lineNumber = 0, digit0 = 0, digit1 = 1;
        //string longString = sr.ReadLine();
        //while (longString != null)
        //{
        //  arrayOfList[lineNumber] = new List<int>();

        //  for (int x = 0; x < longString.Length; x++)
        //  {
        //    if (digit0 > longString.Length)
        //      break;

        //    shortString = longString[digit0] == '0' ? longString[digit1] + "" : longString[digit0] + "" + longString[digit1];
        //    arrayOfList[lineNumber].Add(int.Parse(shortString));

        //    digit0 += 3;
        //    digit1 += 3;
        //  }

        //  digit0 = 0;
        //  digit1 = 1;
        //  lineNumber++;
        //  longString = sr.ReadLine();       
        //}

        //IList<int> temp;
        //for (int y = 0; y < arrayOfList.Length; y++)
        //{
        //  temp = arrayOfList[y] as List<int>;
        //  foreach (int k in temp)
        //    Debug.Write(k + " ");

        //  Debug.WriteLine("");
        //}

        #endregion

        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);

        int[][] dataArray = new int[20][];
        int row = 0, col = 0;

        string longString = sr.ReadLine();
        while (longString != null)
        {
          dataArray[row] = new int[30];

          //Decompose the string into a string array
          string[] shortString = longString.Split(' ');

          foreach (string value in shortString)
          {
            dataArray[row][col] = Convert.ToInt32(value);
            Console.Write(dataArray[row][col] + " ");
            col++;
          }
          row++;  //row ++ when finish reading a line
          col = 0;  //resetting col to 0 for next row
          longString = sr.ReadLine(); //read the next line
          Console.WriteLine();
        }
        return dataArray;
      }

      public class BinaryTree<T>
      {
        public class Node<T>
        {
          public Node<T> LeftNode { get; set; }
          public Node<T> RightNode { get; set; }
          public T Data { get; set; }

          public Node(T Data, Node<T> Parent, Node<T> LeftNode, Node<T> RightNode)
            : this(Data)
          {
            this.RightNode = RightNode;
            this.LeftNode = LeftNode;
          }

          public Node(T Data)
          {
            this.Data = Data;
          }
        }

        public Node<T> Root { get; set; }
        public BinaryTree<T> LeftTree { get; set; }
        public BinaryTree<T> RightTree { get; set; }
        public T Data { get; set; }

        protected BinaryTree(T data)
        {
          this.Root = new Node<T>(data);
        }

        protected BinaryTree(Node<T> root)
        {
          this.Root = root;
        }

        public BinaryTree()
        {
          this.Root = null;
        }

        public BinaryTree(T Data, BinaryTree<T> leftTree, BinaryTree<T> rightTree)
          : this(Data)
        {
          if (leftTree != null)
            Root.LeftNode = leftTree.Root;
          else
            Root.LeftNode = null;

          if (rightTree != null)
            Root.RightNode = rightTree.Root;
          else
            Root.RightNode = null;
        }

        public BinaryTree<T> GetLeftSubtree()
        {
          if (Root != null && Root.LeftNode != null)
            return new BinaryTree<T>(Root.LeftNode);
          else
            return null;
        }

        public BinaryTree<T> GetRightNodeSubtree()
        {
          if (Root != null && Root.RightNode != null)
            return new BinaryTree<T>(Root.RightNode);
          else
            return null;
        }

        public bool isLeaf()
        {
          return (Root == null || (Root.LeftNode == null && Root.RightNode == null));
        }

        private void Add(BinaryTree<T> subTree)
        {
          if (LeftTree == null)
            LeftTree = subTree;
          else if (RightTree == null)
            RightTree = subTree;
        }

        public void AddNode(T data)
        {
          if (LeftTree == null)
            LeftTree.Root = new Node<T>(data);
          else if (RightTree == null)
            RightTree.Root = new Node<T>(data);
        }
      }
    }

    public class PanDigits_P32Helper
    {
      /// <summary>
      /// Calculate the list of number whose multiplicand, multiplier, and product is 1 through 9 pandigital e.g. 39 (x) * 186 (y) = 7254
      /// </summary>
      public static void CalculatePanDigits(int x, int xMax, int y, int yMax, ref List<int> PanDigitProducts)
      {
        string LHS_string = "";
        string RHS_string = "";
        int product = 0;
        while (x <= xMax)
        {
          //Check if x contains 0 or repeated character. If so, increment x by 1
          if (Has0(x) || RepeatedChar(x.ToString()) != null)
            x++;

          while (y <= yMax)
          {
            //Check if 0 appears, increment the number by 10^N (N = position of the 0) so it goes away
            if (Has0(y))
              y += Convert.ToInt16(Math.Pow(10, PositionOf0(y)));

            //At this point x and y won't contain 0
            //Check if any repeated char is found 
            LHS_string = AppendInts(new[] { x, y });
            while ((RepeatedChar(LHS_string) != null || Has0(LHS_string) == true) && y <= yMax)
            {
              ++y;
              LHS_string = AppendInts(new[] { x, y });
            }

            //now LHS has no repeated char and no 0 , multiply the numbers
            product = x * y;
            //If the product > 9999, the total #digits will be >10, hence skip and reset y 
            if (product > 9999)
              break;

            //check if the result has repeated chars or has 0 
            //Add to list if not
            RHS_string = AppendInts(new[] { x, y, product });
            if (RepeatedChar(RHS_string) == null && Has0(RHS_string) == false)
              PanDigitProducts.Add(product);
            ++y;
          }

          ++x;
          y = ResetY(x, y);
        }
      }

      public static int ResetY(int x, int y)
      {
        if (y > 1000)
        {
          if (x == 2)
            return 1345;
          else if (x == 3)
            return 1245;
          else if (x == 4)
            return 1235;
          else
            return 1234;
        }
        else
          return 123;
      }

      public static string AppendInts(int[] Ints)
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Ints.Count(); i++)
          sb.Append(Ints[i]);
        return sb.ToString();
      }

      public static char? RepeatedChar(string IntString)
      {
        char[] chars = IntString.ToCharArray();
        foreach (char c in chars)
        {
          if (chars.Count(aChar => aChar == c) > 1)
            return c;
          else
            continue;
        }
        return null;
      }

      public static bool Has0(string IntString)
      {
        bool has0 = IntString.Where(c => c == '0').Count() > 0;
        return has0;
      }

      public static bool Has0(int integer)
      {
        return (Has0(integer.ToString()));
      }

      public static int PositionOf0(int integer)
      {
        string IntString = integer.ToString();
        int position = IntString.Length - IntString.IndexOf('0') - 1;
        return position;
      }
    }

    public class Problem37Helper
    {
      public static bool AllTruncatedNumArePrime(int number, char Operator)
      {
        if (CalculatorUtil.IsPrime(number) == false)
          return false;

        int div10counter = 1;
        int shadow = number;
        while (number > 1)
        {
          //If /: 3797 --> 379 --> 37 --> 3
          //If %: 3797 --> 7 --> 97 --> 979
          if (Operator == '/')
            number = number / 10;
          else if (Operator == '%')
          {
            div10counter *= 10;
            number = shadow % (div10counter);
          }

          if (number == shadow)   //for %
            break;

          if (CalculatorUtil.IsPrime(number) == false)
            return false;
        }
        return true;
      }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Reverse the digits of a given number 
    /// </summary>
    public static int ReverseDigits(int number)
    {
      int reverse = 0;
      int mod = 0;
      int originalNumber = number;
      int numOfDigits = 0;

      while (number >= 1)
      {
        numOfDigits = number.ToString().Length;
        originalNumber = number;
        number = number / 10;
        mod = originalNumber % 10;
        reverse += mod * Convert.ToInt32(Math.Pow(10, --numOfDigits));
      }
      return reverse;
    }

    /// <summary>
    /// Gets the last index of a char array (i.e. length -1)
    /// </summary>
    public static int GetLastIndexInArray(char[] arr)
    {
      int length = arr.Length;
      return --length;
    }

    /// <summary>
    /// Take the input string, swap the char at index1 with that at index2, then output the processed string
    /// </summary>
    public static string SwapChars(string String, int index1, int index2)
    {
      StringBuilder stringBuilder = new StringBuilder(String);
      char placeHolder = stringBuilder[index1];
      stringBuilder[index1] = stringBuilder[index2];
      stringBuilder[index2] = placeHolder;
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Get the substring of the input string, then reverse from startIndx to 0
    /// For example, SubStringReverse("string", 3) will output "irts"
    /// </summary>
    public static string ReverseSubstringCharsFromStartTo0(string String, int startIndex)
    {
      StringBuilder sb = new StringBuilder();
      for (int i = startIndex; i >= 0; i--)
        sb.Append(String[i]);
      return sb.ToString();
    }

    /// <summary>
    /// Get the substring from startIndex to endIndex 
    /// </summary>
    public static string SubString(string String, int startIndex, int endIndex)
    {
      if (startIndex < 0 || endIndex < 0 || startIndex > endIndex)
        return "";

      StringBuilder sb = new StringBuilder();
      for (int i = startIndex; i <= endIndex; i++)
        sb.Append(String[i]);
      return sb.ToString();
    }

    /// <summary>
    /// Get the n-th pentagonal number (#44)
    /// </summary>
    public static int GetNthPentagonalNumber(int n)
    {
      double Pn = (n * (3 * n - 1) / 2);
      return Convert.ToInt32(Pn);
    } 
    #endregion

    ////Get all permutations of input char set in random orders
    //public static List<string> GetPermutation(string charSet)
    //{
    //  List<string> perms = new List<string>();
    //  int lastIndex = GetLastIndexInArray(charSet.ToCharArray());
    //  int lastIndexMin1 = lastIndex - 1;

    //  if (charSet.Length == 1)
    //    perms.Add(charSet[0].ToString());
    //  else
    //  {
    //    for (int i = 0; i < charSet.Length; i++)
    //    {
    //      //Get the set of perms from 0 to lastIndexMin1 (x.e. without the char at lastIndex)
    //      List<string> lastPerms = GetPermutation(ReverseSubstringCharsFromStartTo0(charSet, lastIndexMin1));

    //      //for each permutation from the last set, add back the char at lastIndex
    //      foreach (string perm in lastPerms)
    //        perms.Add(charSet[lastIndex].ToString() + perm);

    //      //swap the characters one at a time x.e. 0,lastIndex; 1,lastIndex etc
    //      charSet = SwapChars(charSet, lastIndex, i);
    //    }
    //  }
    //  return perms;
    //}
    #endregion

    //Get all triangle number (i.e. nGuess*(nGuess+1)) between the lower and the upper bounds
    public static List<Int32> GetTriangleNumbers(Int32 lowerBound, Int32 upperBound, bool Inclusive)
    {
      int n = Convert.ToInt16(Math.Sqrt(lowerBound * 2));
      List<Int32> triangleNums = new List<int>();

      Int32 triNum = 0;

      triNum = n * (n + 1) / 2;
      triangleNums.Add(triNum);
      ++n;

      if (Inclusive)
      {
        while (triangleNums.Last() >= lowerBound && triangleNums.Last() <= upperBound)
        {
          triNum = n * (n + 1) / 2;
          triangleNums.Add(triNum);
          ++n;
        }

        if (triangleNums.First() < lowerBound)
          triangleNums.Remove(triangleNums.First());

        if (triangleNums.Last() > upperBound)
          triangleNums.Remove(triNum);
      }
      else
      {
        while (triangleNums.Last() > lowerBound && triangleNums.Last() < upperBound)
        {
          triNum = n * (n + 1) / 2;
          triangleNums.Add(triNum);
          ++n;
        }

        if (triangleNums.First() <= lowerBound)
          triangleNums.Remove(triangleNums.First());

        if (triangleNums.Last() >= upperBound)
          triangleNums.Remove(triNum);
      }

      return triangleNums;
    }
 
    /// <summary>
    /// Get the list of number that is generated by circulating the given number. E.g. 1234,2341,3412,4123
    /// </summary>
    public static List<Int32> GetCircularNumbers(Int32 number)
    {
      List<Int32> circular = new List<Int32>();
      string String = number.ToString();
      string newString = "";
      int lastIndex = GetLastIndexInArray(String.ToCharArray());
      int beforeI = -1;
      for (int i = 0; i < String.Length; i++)
      {
        beforeI = i;
        newString += SubString(String, i, lastIndex);
        newString += SubString(String, 0, --beforeI);
        circular.Add(Convert.ToInt32(newString));
        newString = "";
      }
      return circular;
    }

    /// <summary>
    /// Returns the prime number of the given "whichPrimeNumber" position (starting from 1). Upper bound of position is 1,000,000
    /// </summary>
    public static Int32 GetPrimeNumberAt(int position)
    {
      return PrimeNums1to1M[--position];
    }

    public static List<Int32> GetAllPrimeNums1to1M()
    {
      List<Int32> primeNums = new List<Int32>();
      int lineNum = 0;
      using (StreamReader sr = new StreamReader(@"..\..\Data\PrimeNums1to1000000.txt"))
      {
        string line = sr.ReadLine();
        while (line != null)
        {
          lineNum++;
          line = line.TrimEnd(',');
          try
          {
            primeNums.AddRange(line.Split(',').ToList().ConvertAll<Int32>(s => Convert.ToInt32(s)));
          }
          catch (Exception)
          {
            int a = lineNum;
          }
          line = sr.ReadLine();
        }
      }
      return primeNums;
    }

    /// <summary>
    /// Get all the prime numbers up to the given upper limit
    /// using the Sieve of Eratosthenes method
    /// 
    /// The method was adopted from http://www.mathblog.dk/sum-of-all-primes-below-2000000-problem-10/
    /// </summary>
    public static List<int> GetPrimeNumsUpTo(int upperLimit)
    {
      int sieveBound = upperLimit % 2 == 0 ? upperLimit % 2 : (upperLimit - 1) / 2;
      int upperSqrt = ((int)Math.Sqrt(upperLimit) - 1) / 2;

      BitArray PrimeBits = new BitArray(sieveBound + 1, true);

      for (int i = 1; i <= upperSqrt; i++)
      {
        if (PrimeBits.Get(i))
        {
          for (int j = i * 2 * (i + 1); j <= sieveBound; j += 2 * i + 1)
            PrimeBits.Set(j, false);
        }
      }

      List<int> numbers = new List<int>((int)(upperLimit / (Math.Log(upperLimit) - 1.08366)));
      numbers.Add(2);

      for (int i = 1; i <= sieveBound; i++)
      {
        if (PrimeBits.Get(i))
          numbers.Add(2 * i + 1);
      }

      return numbers;
    }

    //public static List<Int64> GetAllPrimeNums1Mto2M()
    //{
    //  List<Int64> primeNums = new List<Int64>();
    //  int lineNum = 0;
    //  using (StreamReader sr = new StreamReader(@"..\..\Data\PrimeNums1000000to2000000.txt"))
    //  {
    //    string line = sr.ReadLine();
    //    while (line != null)
    //    {
    //      lineNum++;
    //      line = line.TrimEnd(',');
    //      try
    //      {
    //        primeNums.AddRange(line.Split(',').ToList().ConvertAll<Int64>(s => Convert.ToInt64(s)));
    //      }
    //      catch (Exception)
    //      {
    //        int a = lineNum;
    //      }
    //      line = sr.ReadLine();
    //    }
    //  }
    //  return primeNums;
    //}

    //public static List<Int64> GetAllPrimeNums2Mto3M()
    //{
    //  List<Int64> primeNums = new List<Int64>();
    //  int lineNum = 0;
    //  using (StreamReader sr = new StreamReader(@"..\..\Data\PrimeNums2000000to3000000.txt"))
    //  {
    //    string line = sr.ReadLine();
    //    while (line != null)
    //    {
    //      lineNum++;
    //      line = line.TrimEnd(',');
    //      try
    //      {
    //        primeNums.AddRange(line.Split(',').ToList().ConvertAll<Int64>(s => Convert.ToInt64(s)));
    //      }
    //      catch (Exception)
    //      {
    //        int a = lineNum;
    //      }
    //      line = sr.ReadLine();
    //    }
    //  }
    //  return primeNums;
    //}

    //public static List<Int64> GetAllPrimeNums3Mto4M()
    //{
    //  List<Int64> primeNums = new List<Int64>();
    //  int lineNum = 0;
    //  using (StreamReader sr = new StreamReader(@"..\..\Data\PrimeNums3000000to4000000.txt"))
    //  {
    //    string line = sr.ReadLine();
    //    while (line != null)
    //    {
    //      lineNum++;
    //      line = line.TrimEnd(',');
    //      try
    //      {
    //        primeNums.AddRange(line.Split(',').ToList().ConvertAll<Int64>(s => Convert.ToInt64(s)));
    //      }
    //      catch (Exception)
    //      {
    //        int a = lineNum;
    //      }
    //      line = sr.ReadLine();
    //    }
    //  }
    //  return primeNums;
    //}

    public static string GetBinaryNumber(int decimalNumber)
    {
      return Convert.ToString(decimalNumber, 2);
    }

    /// <summary>
    /// Gets the chain length of a Collatz sequence (starting from StartNum and finally becomes 1)
    /// </summary>
    public static int GetICollatzSequenceItemCount(Int64 StartNum)
    {
      int itemCount = 1;
      while (StartNum > 1)
      {
        if (StartNum % 2 == 0)
          StartNum /= 2;
        else
          StartNum = 3 * StartNum + 1;

        itemCount++;
      }
      return itemCount;
    }

    /// <summary>
    /// Gets the sum of consecutive numbers from 1 to position
    /// </summary>
    /// <param name="position">Last number to sum</param>
    public static Int64 GetTriangleNumber(int position)
    {
      return position * (position + 1) / 2;
    }
    
    /// <summary>
    /// Gets the divisors of the Int64 number
    /// 
    /// http://mathschallenge.net/index.php?section=faq&ref=number/number_of_divisors
    /// Given nGuess is the input parameter
    /// nGuess = p^a * q^b * r^c...
    /// where p, q, r are the prime factors
    /// Given d(nGuess) = num. of divisors
    /// Then d(nGuess) = (a + 1)(b + 1)(c + 1)
    /// </summary>
    public static int GetDivisorsCount(Int64 number, ref List<int> primeNums)
    {
      int numOfDivisor = 1;
      Int64 originalNumber = number;

      foreach (int primeNum in primeNums)
      {
        if (primeNum > originalNumber / 2)
          break;

        int primeDivisorCount = 0;
        while (number % primeNum == 0)
        {
          primeDivisorCount++;
          number /= primeNum;
        }
        if (primeDivisorCount > 0)
          numOfDivisor *= (primeDivisorCount + 1);
      }

      //The last one is for the number itself. By the formula, we multiple by (1+1)
      //numOfDivisor *= (1 + 1);

      return numOfDivisor;
    }

    /// <summary>
    /// Gets the divisors of the number
    /// </summary>
    public static List<int> GetDivisors(int number)
    {
      List<int> factors = new List<int>();
      if (number == 1)
        factors.Add(number);
      else
      {
        for (int i = 1; i <= number/2; i++) //factor won't be < number/2
          if (number % i == 0)
            factors.Add(i);
      }
      return factors;
    }

    /// <summary>
    /// Gets the factors of the number (i.e. divisors plus the number itself)
    /// </summary>
    public static List<int> GetFactors(int number)
    {
      List<int> factors = GetDivisors(number);
      factors.Add(number);
      return factors;
    }

    /// <summary>
    /// Get the sum of divisors of a given number
    /// </summary>
    public static int GetDivisorsSum(int number)
    {
      return GetFactors(number).Sum();
    }

    /// <summary>
    /// Returns the sum of a list of numbers
    /// </summary>
    public static int GetNumbersSum(List<int> numbers)
    {
      if (numbers.Count == 0)
        return 0;
      return numbers.Aggregate((acc, curr) => acc + curr);
    }

    /// <summary>
    /// Returns the sum of a list of Int64 numbers
    /// </summary>
    public static long GetNumbersSum(List<long> numbers)
    {
      if (numbers.Count == 0)
        return 0;
      return numbers.Aggregate((acc, curr) => acc + curr);
    }

    /// <summary>
    /// Check if the digit appears in number
    /// </summary>
    public static bool HasDigit(Int32 number, int digit)
    {
      return HasChar(number.ToString(), Convert.ToChar(digit + 48));
    }

    /// <summary>
    /// Check if char appears in s
    /// </summary>
    public static bool HasChar(string s, char c)
    {
      return s.Contains(c);
    }

    /// <summary>
    /// Check if the number has any repeated digits. Returns -1 if no repeated digit, otherwise return the first repeated digit
    /// </summary>
    public static int HasRepeatedInt(long number)
    {
      char? noRepeated = HasRepeatedChar(number.ToString());
      return noRepeated == null ? -1 : CharToInt((char)noRepeated);
    }

    /// <summary>
    /// Check if the string has any repeated character
    /// </summary>
    public static char? HasRepeatedChar(string s)
    {
      char[] sChars = s.ToCharArray();
      foreach (char c in sChars)
      {
        if (sChars.Where(_c => _c == c).Count() > 1)
          return c;
      }
      return null;
    }

    /// <summary>
    /// Check if the string has any characters
    /// </summary>
    public static List<char> HasRepeatedChars(string s)
    {
      List<char> repeatedChars = new List<char>();
      char[] sChars = s.ToCharArray();
      foreach (char c in sChars)
        if (sChars.Where(_c => _c == c).Count() > 1)
          if (!repeatedChars.Contains(c))
            repeatedChars.Add(c); 

      return repeatedChars;
    }

    /// <summary>
    /// Works together with the BigInts list. Populate the list with Fibonacci nums starting from 1
    /// </summary>
    public static List<BigInteger> BigInts = new List<BigInteger>();
    public static void PopulateFibonacciList(int nthTerm)
    {
      if (BigInts.Count == 0)
        BigInts.Add(0);
      if (nthTerm == 1 || nthTerm == 2)
        BigInts.Add(1);
      else
      {
        BigInteger temp = BigInts[nthTerm - 1] + BigInts[nthTerm - 2];
        BigInts.Add(temp);
      }
    }

    /// <summary>
    /// Gets the number of digits in the input number 
    /// </summary>
    public static int GetDigitsCount(BigInteger number)
    {
      return number.ToString().Length;
    }

    /// <summary>
    /// Determines if the fraction in the form of 1/denominator is a recurring fraction
    /// </summary>
    public static bool IsFractionRecurring(int denominator)
    {
      while (denominator >= 2)
      {
        if (denominator % 2 > 0)
          break;
        else 
          denominator /= 2;
      }

      while (denominator >= 5)
      {
        if (denominator % 5 > 0)
          break;
        else
          denominator /= 5;
      }

      return denominator != 1;
    }

    /// <summary>
    /// Returns all digits after the decimals until the recurring one is hit (inclusively)
    /// </summary>
    /// <remarks>
    /// Use this method to e.g. determine how many digits there are between the recurring digits, 
    /// or to get the digits after the decimal point
    /// </remarks>
    public static List<int> GetDigitsAfterDecimals(int denominator)
    {
      int origNumerator = 1;
      int mod = 0, divide = 0;
      bool IsFirstQuitient = true;

      List<int> Quotients = new List<int>();
      List<int> Numerators = new List<int>();

      int currNumerator = origNumerator;

      while (true)
      {
        IsFirstQuitient = true;
        divide = currNumerator / denominator;

        //keep *10 till division is possible
        while (divide < 1)
        {
          currNumerator *= 10;

          if (IsFirstQuitient == false)
            Quotients.Add(divide);
          divide = currNumerator / denominator;

          IsFirstQuitient = false;
        }

        Quotients.Add(divide);

        if (Numerators.Contains(currNumerator))
          break;

        mod = currNumerator % denominator;
        Numerators.Add(currNumerator);
        currNumerator = mod;
      }

      return Quotients;
    }

    /// <summary>
    /// Splits the input shortString into char array and read them into intArray. Increments a row each time the method is called
    /// </summary>
    public static void ToIntArray(ref int[,] intArray, int rowCount, string shortString)
    {
      int a = 0;
      int charAt = 0;
      for (int colCount = (shortString.Length - 1); colCount >= 0; colCount--)
      {
        a = Convert.ToInt16(shortString[charAt]) - 48;
        intArray[rowCount, colCount] = a;
        charAt++;
      }
    }

    /// <summary>
    /// Gets the dictionary of prime number and its #occurance from a given dividend
    //http://www.dotnetperls.com/dictionary
    /// </summary>
    public static  void GetPrimesAndCounts(ref int dividend, IDictionary<int, int> localPrimeCountDictionary)
    {
      for (int prime = 2; prime <= dividend; prime++)
      {
        if (IsPrime(prime) && dividend % prime == 0)
        {
          if (!localPrimeCountDictionary.ContainsKey(prime))
            localPrimeCountDictionary.Add(prime, 1);
          else
            localPrimeCountDictionary[prime]++;

          dividend /= prime;

          GetPrimesAndCounts(ref dividend, localPrimeCountDictionary);
        }
      }
    }

    /// <summary>
    /// Take the input array and return the product calculated using all items
    /// </summary>
    public static Int64 GetProductFromNums(IEnumerable<int> numEnum) 
    {
      Int64 product = 1;
      foreach (int num in numEnum)
        product *= num;
      return product;
    }
    
    /// <summary>
    /// Covert a 0-9 digit to char
    /// If input is not within 0 to 9, return null
    /// </summary>
    public static char IntToChar(int digit)
    {
      return (char)(digit + 48);
    }

    /// <summary>
    /// Returns true if num is a square number
    /// </summary>
    public static bool IsSquareNumber(Int64 num)
    {
      Int64 sqrt = Convert.ToInt64(Math.Sqrt((double)num));
      return sqrt * sqrt == num ? true : false;
    }

    /// <summary>
    /// Checks if the number is between start and end
    /// </summary>
    public static bool IsBetweenStartAndEnd(int number, int start, int end)
    {
      if (number >= start && number <= end)
        return true;
      else
        return false;
    }

    /// <summary>
    /// Checks if a number is Palindromic x.e. reads the same both ways e.g. 9009
    /// This approach is done based on number comparison
    /// </summary>
    public static bool IsPalindromicNumComparison(int num)
    {
      int n = num;  //n is a copy of the original number
      int rev = 0, dig = 0; //rev is the reversed number, dig is the module
      while (num > 0)
      {
        dig = num % 10;
        rev = rev * 10 + dig;
        num = num / 10;
      }

      if (rev == n)
        return true;
      else
        return false;
    }

    /// <summary>
    /// Check if the number is pandigital 
    /// (i.e. for a n-digit number is pandigital if it makes use of all the digits 1 to n exactly once)
    /// </summary>
    public static bool IsPandigital(long number)
    {
      // Brute force way to check if the input is a pandigital number
      // by converting the number into a string then check recurcively for repeated characters
      // Pros: With this approach, input number can be Int32 or Int64
      string numberString = number + "";
      int stringLength = numberString.Length;
      if (stringLength == 1)
        return numberString == "1";
      else
        return numberString.Contains(stringLength + "") &&
          HasRepeatedChar(numberString) == null &&
          IsPandigital(Convert.ToInt32(numberString.Replace(stringLength + "", "")));

      // Mathematical way. Algorithm adopted from: http://stackoverflow.com/questions/2484892/fastest-algorithm-to-check-if-a-number-is-pandigital
      // This approach only works with Int32
      //int digits = 0; int count = 0; int tmp;

      //for (; number > 0; number /= 10, ++count)
      //{
      //  if ((tmp = digits) == (digits |= 1 << (number - ((number  / 10) * 10) - 1)))
      //    return false;
      //}

      //return digits == (1 << count) - 1;
    }

    /// <summary>
    /// Checks if a number is Palindromic x.e. reads the same both ways e.g. 9009
    /// This approach is done based on string comparison
    /// </summary>
    public static bool IsPalindromic(string num)
    {
      //Adjust the stopping index based on the number of digits
      int endAtIndex = num.Length % 2 == 0 ? num.Length / 2 : (num.Length - 1) / 2;
     
      for (int index = 0; index < endAtIndex; index++)
        if (num[index] != num[num.Length - index - 1])
          return false;

      return true;
    }

    /// <summary>
    /// Checks if a number is Pentagonal (see #44)
    /// </summary>
    public static bool IsPentagonal(long number)
    {
      ////Use quadratic formula to try to find the N corresponding to the number
      ////(nGuess * (3 * nGuess - 1) / 2) = number
      ////nGuess * (3 * nGuess - 1) = number * 2
      ////3n^2 - nGuess - number * 2 = 0
      //number = number * 2;

      //Int64 delta = 1 + 4 * 3 * number;
      //if (!CalculatorUtil.IsSquareNumber(delta))
      //  return false;

      //if ((1 + Math.Sqrt(delta)) % 6 == 0)
      //  return true;
      //else
      //  return false;

      // Formula that determines if a number is pentagonal
      // http://www.divye.in/2012/07/how-do-you-determine-if-number-n-is.html
      double mod = (1 + Math.Sqrt(24 * number + 1)) % 6;

      return  mod == 0 ? true : false;        
    }

    /// <summary>
    /// Checks if a number is a prime number
    /// </summary>
    public static bool IsPrime(Int64 factor)
    {
      if (factor == 1)
        return false;

      bool isPrime = true;
      for (Int64 i = 2; (i * i) <= factor; i++)
        if (factor % i == 0)
          return !isPrime;

      return isPrime;
    }

    /// <summary>
    /// Use permutation to compute a list of pandigital numbers using the given first and last digits
    /// 
    /// firstDigit and lastDigit must be digits from 0 to 9. firstDigit must be less than or equal to lastDigit. 
    /// Otherwise null will be returned
    /// </summary>
    public static List<string> GetPandigitalNumbers(int firstDigit, int lastDigit)
    {
      if ((firstDigit < 0 || firstDigit > 9) || (lastDigit < 0 || lastDigit > 9))
        return null;
      if (firstDigit > lastDigit)
        return null;

      List<char> cs = new List<char>();
      int index = firstDigit;
      while(index <= lastDigit)
      {
        cs.Add(IntToChar(index));
        ++index;
      }

      List<List<char>> permutations = GetPermutations(cs);
      List<string> pandigitalNumbers = new List<string>();
      foreach (List<char> permutation in permutations)
        pandigitalNumbers.Add(string.Concat(permutation));

      return pandigitalNumbers;
    }

    public static List<List<char>> GetPermutations(List<char> items)
    {
      List<List<char>> permutations = new List<List<char>>(); 

      List<char> copiedItems = items;

      if (items.Count == 1)
        permutations.Add(items);
      else
      {
        for(int index = 0; index < items.Count; index++)
        {
          //char removedItem = items[index];
          //copiedItems.Remove(removedItem);

          //swap items[index] with items[0]
          if (index >=1){
            char item = copiedItems[index];
            copiedItems.RemoveAt(index);
            copiedItems.Insert(0, item);
          }
          List<char> copiedItemsSubSet = copiedItems.GetRange(1, (copiedItems.Count - 1));
          List<List<char>> partialPermutations = GetPermutations(copiedItemsSubSet);
          foreach(List<char> partialPermutation in partialPermutations)
          {
            //partialPermutation.Insert(0, removedItem);
            permutations.Add(partialPermutation);
          }
        }
      }

      return permutations;
    }

    /// <summary>
    /// Get the number of permutations based on the number of items
    /// Number of permutations = Factorial(itemCount);
    /// </summary>
    private long GetPermutationCount(int itemCount)
    {
      return GetFactorial(itemCount);
    }

    /// <summary>
    /// Returns Base ^ Power. Used it if the result might be very large
    /// </summary>
    public static BigInteger GetPower(int Base, int Power)
    {
      BigInteger product = Base;
      for (int i = 1; i < Power; i++)
        product *= Base;

      return product;
    }

    [Obsolete]
    private static void AddLargeNumbersSumOutputIntoArray(int numberOfCols, int numberOfRows = 999)
    {
      int[][] largeArray = new int[numberOfRows][];

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
          oneColSum += largeArray[i][j];

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
      for (int i = --resultSize; i >= 0; i--)
      {
        Debug.Write(result[i]);
      }
    }

    /// <summary>
    /// Gets the factorial of N. This method was optimized using the memoization technique
    /// </summary>
    public static long GetFactorial(int N)
    {
      if (N == 0)
        return 1;

      Dictionary<int, long> memoKVPs = new Dictionary<int, long>();
      memoKVPs.Add(1, 1);

      return getFactorial(memoKVPs, N);
    }

    private static long getFactorial(Dictionary<int, long> memoKVPs, int N)
    {
      long factorial = 0;
      if (memoKVPs.ContainsKey(N))
      {
        //if the number was previously calculated and saved to memoKVPs, 
        //set it as the value to be returned
        factorial = memoKVPs[N];
      }
      else
      {
        //if the number was never calculated 
        //do the calculation and save to memoKVPs
        factorial = getFactorial(memoKVPs, (N - 1)) * N;
        memoKVPs.Add(N, factorial);
      }
      return factorial;
    }
    
    /// <summary>
    /// Gets the factorial of a big N. This method was optimized using the memoization technique
    /// </summary>
    public static BigInteger GetFactorialBigInteger(Int64 N)
    {
      if (N == 0)
        return 1;

      Dictionary<Int64, BigInteger> memoKVPs = new Dictionary<Int64, BigInteger>();
      memoKVPs.Add(1, 1);

      return getFactorialBigInteger(memoKVPs, N);
    }

    private static BigInteger getFactorialBigInteger(Dictionary<Int64, BigInteger> memoKVPs, Int64 N)
    {
      BigInteger factorial = 0;
      if (memoKVPs.ContainsKey(N))
      {
        //if the number was previously calculated and saved to memoKVPs, 
        //set it as the value to be returned
        factorial = memoKVPs[N];
      }
      else
      {
        //if the number was never calculated 
        //do the calculation and save to memoKVPs
        factorial = BigInteger.Multiply(getFactorialBigInteger(memoKVPs, (N - 1)), N);
        memoKVPs.Add(N, factorial);
      }
      return factorial;
    }

    /// <summary>
    /// split inputString into char array and add each up using their alphabetical orders x.e. a=1 etc
    /// </summary>
    public static int GetSumOfAlphabetOrder(string name)
    {
      char[] nameChars = name.ToCharArray();
      int sum=0;
      foreach (char c in nameChars)
        sum += c - '@';
      return sum;
    }
    
    /// <summary>
    /// Checks if a number is deficient, perfect of abundunt
    /// </summary>
    public static DeficientPerfectAbundunt IsNumberDeficientPerfectAbundunt(int number)
    {
      int sum = GetDivisorsSum(number);

      if (sum > number) return DeficientPerfectAbundunt.Abundant;
      else if (sum == number) return DeficientPerfectAbundunt.Perfect;
      else return DeficientPerfectAbundunt.Deficient;
    }

    /// <summary>
    /// Converts a given char to an int
    /// </summary>
    public static int CharToInt(char c)
    {
      return c - '0';
    }
  }
}