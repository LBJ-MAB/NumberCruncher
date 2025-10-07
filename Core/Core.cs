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
    public static T Max<T>(T[] nums) where T: IComparable<T>
    {
        /*
         * INPUTS:
         * nums : an array of numbers of type T
         * OUTPUTS:
         * maxValue : the highest number in the nums array
        */
        
        // initialise maxValue as first element
        T maxValue = nums[0];
        
        // check the rest of the numbers in the array
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i].CompareTo(maxValue) > 0) // current number > maxValue
            {
                maxValue = nums[i]; // set maxValue to current number
            }
        }
        return maxValue;    // return maxValue
    }
    public static double Mean<T>(T[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers of type T
         * OUTPUTS:
         * sum / nums.Length : the mean of the nums array
        */
        
        // initalise sum with 0
        double sum = 0;
        
        // loop through all numbers in array
        for (int i = 0; i < nums.Length; i++)
        {
            sum += Convert.ToDouble(nums[i]);         // add current number to the sum
        }
        // return sum divided by amount of numbers in array
        return sum / nums.Length;
    }
    public static double Median<T>(T[] nums)
    {
        /*
         * INPUTS:
         * nums : an array of numbers of type T
         * OUTPUTS:
         * med : the median of the nums array
        */
        
        Array.Sort(nums);       // sort the array from smallest to largest
        double med;             // define variable for storing median value
        
        if (nums.Length % 2 != 0)    // length of array is odd  
        {
            int medianIndex = nums.Length / 2;      // find middle index
            med = Convert.ToDouble(nums[medianIndex]);                // median is middle number in sorted array
        }
        else    // length of array is even
        {
            int upperIndex = nums.Length / 2;       // find first index above middle of array
            int lowerIndex = upperIndex - 1;        // find first index below middle of array
            med = (Convert.ToDouble(nums[upperIndex]) + Convert.ToDouble(nums[lowerIndex])) / 2;    // median is sum two numbers at these indeces and divide by 2
        }
        return med;     // return med
    }
    public static T Min<T>(T[] nums) where T: IComparable<T>
    {
        /*
         * INPUTS:
         * nums : an array of numbers
         * OUTPUTS:
         * minValue : the smallest number in nums array
        */
        
        T minValue = nums[0];      // initialise minValue as first number in array
        
        // loop through rest of numbers in the array
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i].CompareTo(minValue) < 0)     // current number < minValue
            {
                minValue = nums[i];     // set minValue to current number
            }
        }
        return minValue;    // return minValue
    }
    public static T[]? Mode<T>(T[] nums) where T : notnull
    {
        /*
         * INPUTS:
         * nums : an array of numbers of type T
         * OUTPUTS:
         * modeValue : array of mode values || null if no mode value
        */
        
        // if nums is empty, return null
        if (nums.Length == 0)
        {
            return null;
        }

        List<T> modeList = new List<T>();     // empty list for storing mode values n
        int maxOccurences = 0;                          // number of times the mode number appears
        Dictionary<T, int> dict = new Dictionary<T, int>();       // define dictionary for number, total pairs

        // loop through each number of nums
        foreach (T num in nums)
        {
            if (dict.ContainsKey(num))      // current number has already appeared in nums array
            {
                dict[num] = dict[num] + 1;      // increment number of times this number has appeared by 1
            }
            else        // current number has not appeared before
            {
                dict.Add(num, 1);   // set number of times this number has appeared to 1
            }
            
            // update max occurences if applicable
            if (dict[num] > maxOccurences)
            {
                maxOccurences = dict[num];
            }
        }
        
        // if all values only appear once, return null
        if (maxOccurences == 1 && nums.Length > 1)
        {
            return null;
        }

        // loop through each <number, total> in dictionary
        foreach (KeyValuePair<T, int> ele in dict)
        {
            if (ele.Value == maxOccurences)      // if it is a mode value
            {
                modeList.Add(ele.Key);           // add mode value to list
            }
        }

        return modeList.ToArray();    // return array of mode values
    }
    public static double Stdev<T>(T[] nums)
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
            double squareDiff = Math.Pow(Convert.ToDouble(nums[i]) - mean, 2);    // compute square of difference between current number and mean
            sumSquareDiff += squareDiff;        // increment sumSquareDiff by squareDiff
        }
        return Math.Sqrt(sumSquareDiff / nums.Length);      // return square root of sumSquareDiff / array length
    }
    public static T Sum<T>(T[] nums)
    {
        dynamic sum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
        }
        return sum;
    }
}