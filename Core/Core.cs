namespace Core;

public static class Statistics
{
    public static double[]? ConvertStringToArray(string numberString)
    {
        /*
         * INPUTS:
         * numberString : a string of numbers inputted by the user, each number separated by a comma
         * OUTPUTS:
         * numbersList.ToArray() : an array of the numbers that the user inputted into the console || null if input
         *  string is not formatted correctly
        */
        
        // define local variable for numberString
        string numberStringTmp = numberString;
        
        // define list for storing numbers
        List<double> numbersList = new List<double>();

        while (numberStringTmp.Length > 0)
        {
            // find a comma in the string
            int commaIndex = numberStringTmp.IndexOf(',');

            try
            {
                if (commaIndex >= 0)
                {
                    // Comma exists - get the number preceding the next comma in the string
                    double nextNum = Convert.ToDouble(numberStringTmp.Substring(0, commaIndex));
                    // add the next number to the list
                    numbersList.Add(nextNum);
                    // get rid of that number and comma from the string
                    numberStringTmp = numberStringTmp.Substring(commaIndex+1);
                }
                else
                {
                    // Comma doesn't exist - we have reached the final number
                    // turn the rest of the string into a number
                    double nextNum = Convert.ToDouble(numberStringTmp);
                    // add next number to the list
                    numbersList.Add(nextNum);
                    // we have reached final number, so set string length to 0
                    numberStringTmp = "";
                }
            }
            catch
            {
                return null;        // can't make array - return null
            }
        }
        // return the array of numbers
        return numbersList.ToArray();
    }
    public static double Max(double[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers
         * OUTPUTS:
         * maxValue : the highest number in the nums array
        */
        
        // initialise maxValue as first element
        double maxValue = nums[0];
        
        // check the rest of the numbers in the array
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > maxValue) // current number > maxValue
            {
                maxValue = nums[i]; // set maxValue to current number
            }
        }
        return maxValue;    // return maxValue
    }
    public static double Mean(double[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers
         * OUTPUTS:
         * sum / nums.Length : the mean of the nums array
        */
        
        // initalise sum with 0
        double sum = 0;
        
        // loop through all numbers in array
        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];         // add current number to the sum
        }
        // return sum divided by amount of numbers in array
        return sum / nums.Length;
    }
    public static double Median(double[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers
         * OUTPUTS:
         * med : the median of the nums array
        */
        
        Array.Sort(nums);       // sort the array from smallest to largest
        double med;             // define variable for storing median value
        
        if (nums.Length % 2 != 0)    // length of array is odd  
        {
            int medianIndex = nums.Length / 2;      // find middle index
            med = nums[medianIndex];                // median is middle number in sorted array
        }
        else    // length of array is even
        {
            int upperIndex = nums.Length / 2;       // find first index above middle of array
            int lowerIndex = upperIndex - 1;        // find first index below middle of array
            med = (nums[upperIndex] + nums[lowerIndex]) / 2;    // median is sum two numbers at these indeces and divide by 2
        }
        return med;     // return med
    }
    public static double Min(double[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers
         * OUTPUTS:
         * minValue : the smallest number in nums array
        */
        
        double minValue = nums[0];      // initialise minValue as first number in array
        
        // loop through rest of numbers in the array
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] < minValue)     // current number < minValue
            {
                minValue = nums[i];     // set minValue to current number
            }
        }
        return minValue;    // return minValue
    }
    public static double Mode(double[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers
         * OUTPUTS:
         * modeValue : the mode of the nums array
        */
        
        double modeValue = 0;       // mode value to return
        int maxOccurences = 0;      // number of times the mode number appears
        Dictionary<double, int> dict = new Dictionary<double, int>();       // define dictionary for key, value pairs

        // loop through each number of nums
        foreach (double num in nums)
        {
            if (dict.ContainsKey(num))      // current number has already appeared in nums array
            {
                dict[num] = dict[num] + 1;      // increment number of times this number has appeared by 1
            }
            else        // current number has not appeared before
            {
                dict.Add(num, 1);   // set number of times this number has appeared to 1
            }
        }

        // loop through each <number, total> in dictionary
        foreach (KeyValuePair<double, int> ele in dict)
        {
            if (ele.Value > maxOccurences)      // total > maxOccurences
            {
                modeValue = ele.Key;        // set new mode value
                maxOccurences = ele.Value;  // set new mode threshold
            }
        }

        return modeValue;    // return mode number
    }
    public static void PrintAverages(double mean, double mode, double median,
        double min, double max, double stdev)
    {
        /*
         * INPUTS:
         * mean : mean value of an array
         * mode : mode value of an array
         * median : median value of an array
         * min : min value of an array
         * max : max value of an array
         * stdev : standard deviation of an array
         * OUTPUTS:
         * None : Prints to console each of the averages given as arguments
        */
        
        Console.WriteLine("--- Averages ---");
        Console.WriteLine("Mean   : {0:F2}", mean);
        Console.WriteLine("Mode   : {0:F2}", mode);
        Console.WriteLine("Median : {0:F2}", median);
        Console.WriteLine("Min    : {0:F2}", min);
        Console.WriteLine("Max    : {0:F2}", max);
        Console.WriteLine("Stdev  : {0:F2}", stdev);
    }
    public static double Stdev(double[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers
         * OUTPUTS:
         * Math.Sqrt(sumSquareDiff / nums.Length) : the standard deviation of the nums array
        */
        
        double mean = Mean(nums);       // compute mean of nums array
        double sumSquareDiff = 0;       // variable for sum of the squares of (num - mean)
        
        // loop through each value in nums
        for (int i = 0; i < nums.Length; i++)
        {
            double squareDiff = Math.Pow(nums[i] - mean, 2);    // compute square of difference between current number and mean
            sumSquareDiff += squareDiff;        // increment sumSquareDiff by squareDiff
        }
        return Math.Sqrt(sumSquareDiff / nums.Length);      // return square root of sumSquareDiff / array length
    }
}