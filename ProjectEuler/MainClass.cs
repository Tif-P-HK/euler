using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProjectEuler
{
  class MainClass
  {
    static Calculator calculator = new Calculator();

    private static void PrintResult(int problemNum, object result)
    {
      Debug.WriteLine("Problem " + problemNum + ": " + result.ToString());
    }

    public static void Main()
    {
      #region Solved Problems
      #region Cleaned up + Imrpoved commets
      //PrintResult(1, calculator.Problem1());

      //PrintResult(2, calculator.Problem2());

      //PrintResult(3, calculator.Problem3());

      //PrintResult(4, calculator.Problem4());

      //PrintResult(5, calculator.Problem5());

      //PrintResult(6, calculator.Problem6());

      //PrintResult(7, calculator.Problem7());

      //PrintResult(8, calculator.Problem8());

      //PrintResult(9, calculator.Problem9()); 

      //PrintResult(10, calculator.Problem10()); 
      #endregion

      //Debug.WriteLine(calculator.Problem11());

      //PrintResult(12, calculator.Problem12());

      //calculator.Problem13();

      //Debug.WriteLine(calculator.Problem14());

      ////problem 15 http://joaoff.com/2008/01/20/a-square-grid-path-problem/
      //Debug.WriteLine(calculator.Problem15());

      //Debug.WriteLine(calculator.Problem16());

      //Debug.WriteLine(calculator.Problem17());

      ////http://www.dijksterhuis.org/implementing-a-generic-binary-tree-in-c/
      ////http://stackoverflow.com/questions/2942517/how-do-x-iterate-over-binary-tree
      ////http://stackoverflow.com/questions/1104644/how-would-you-print-out-the-data-in-a-binary-tree-level-by-level-starting-at-t
      //Debug.WriteLine(calculator.Problem18a());

      //Debug.WriteLine(calculator.Problem19());

      //Debug.WriteLine(calculator.Problem20(100));

      //Debug.WriteLine(calculator.Problem21());

      //Debug.WriteLine(calculator.Problem22());

      //Debug.WriteLine(calculator.Problem23());

      //Debug.WriteLine(calculator.Problem24());

      //Debug.WriteLine(calculator.Problem25());

      //Debug.WriteLine(calculator.Problem26());

      //Debug.WriteLine(calculator.Problem27());

      //Debug.WriteLine(calculator.Problem28());

      //Debug.WriteLine(calculator.Problem29());

      ////http://duncan99.wordpress.com/2009/01/31/project-euler-problem-30/
      //Debug.WriteLine(calculator.Problem30());

      //Debug.WriteLine(calculator.Problem32());

      //Debug.WriteLine(calculator.Problem33());

      //Cleaned up + improved comments
      //PrintResult(calculator.Problem34());

      //PrintResult(35, calculator.Problem35());

      //Cleaned up + improved comments
      //PrintResult(36, calculator.Problem36());

      //Cleaned up + improved comments
      //PrintResult(37, calculator.Problem37());

      //Debug.WriteLine(calculator.Problem38());

      //Debug.WriteLine(calculator.Problem39a());

      //Debug.WriteLine(calculator.Problem40());

      //Cleaned up + improved comments
      //PrintResult(41, calculator.Problem41());

      //Debug.WriteLine(calculator.Problem42());

      //Debug.WriteLine(calculator.Problem43());

      //MessageBox.Show(calculator.Problem44().ToString());

      //MessageBox.Show(Calculator.Problem454().ToString());
      
      //CalculatorUtil.SubString() -- make it a string extension method

      //PrintResult(999, CalculatorUtil.ReverseSubstringCharsFromStartTo0("string", 3));

      //whats the limit of short, long, BigNum and so on?
      #endregion

    }
  }
}
