/*
 * Filename: mini6
 * By: Justin Fink
 * Date: November 20, 2020
 * Description: This program gets user input for 4 doubles, 
 *              it then prints the average and sum of those 
 *              4 doubles, then the program gets an array of
 *              doubles from the user, the program then
 *              calculates the average and sum of all the numbers in
 *              the array. After the program fills in every element of
 *              the array in a number I have picked.
 */

#include <stdio.h>
#pragma warning(disable: 4996) 


int getDouble(double *pNumber);
void generateStats4(double d1, double d2, double d3, double d4, double* pAverage, double* pSum);
void generateStatsFromArray(double values[], int numArray, double *pSum, double *pAverage);
void fillArray(double values[], int numArray, double fillValue);




int main()
{
    int i = 0;
    double d1 = 0.0;
    double d2 = 0.0;
    double d3 = 0.0;
    double d4 = 0.0;
    int invalidCheck = 0;

    printf("Enter Frist double: ");
    invalidCheck = getDouble(&d1);

    printf("Enter Second double: ");
    invalidCheck += getDouble(&d2);

    printf("Enter Third double: ");
    invalidCheck += getDouble(&d3);
    
    printf("Enter Fourth double: ");
    invalidCheck += getDouble(&d4);
   
    if (invalidCheck < 4)
    {
        return 0;
    }
    
    double dAverage = 0;
    double dSum = 0;

    generateStats4(d1, d2, d3, d4, &dAverage, &dSum);
    printf("The average and sum of the variables: Average = %lf, Sum = %lf\n", dAverage, dSum);

    const int SIZE_OF_ARRAY = 7;
    int lengthOfArray = SIZE_OF_ARRAY - 1;
    double arrayOfDoubles[SIZE_OF_ARRAY] = { 0 };

    for (i = 0; i <= lengthOfArray; i++)
    {
        printf("Enter a number for the array: ");
        getDouble(&arrayOfDoubles[i]);

        if (arrayOfDoubles[i] == 0)
        {
            return 0;
        }
    }

    generateStatsFromArray(arrayOfDoubles, lengthOfArray, &dSum, &dAverage);
    printf("The average and sum of the array elements: Average: %lf, Sum = %lf\n", dAverage, dSum);

    double fillValue = 40.0;

    fillArray(arrayOfDoubles, lengthOfArray, fillValue);

    for (i = 0; i <= lengthOfArray; i++)
    {
        printf("Elements of the Array: ");
        if (i != lengthOfArray)
        {
            printf("%lf, ", arrayOfDoubles[i]);
        }
        else
        {
            printf("%lf.", arrayOfDoubles[i]);
        }
    }

    return 0;
}




/* 
 * Function: getDouble()
 * Parameter: double *pNumber: pointer to a variable that is filled in by the user input, if valid
 * Return Value: int: 1 if the user entered a valid floating-point number, 0 otherwise 
 * Description: This function gets a floating-point number from the user. 
 *              If the user enters a valid floating-point number, the value is put into
 *              the variable pointed to by the parameter and 1 is returned.  
 *              If the user-entered value is not valid, 0 is returned.
 */

int getDouble(double *pNumber) 
{
    char record[121] = { 0 };
    double number = 0.0;

    fgets(record, 121, stdin);

    if (sscanf(record, "%lf", &number) != 1)
    {
        return 0;
    }

    *pNumber = number;
    return 1;
}




/* Function: generateStats4()
 * Parameters:  double d1, d2, d3, d4: fourfloating-point numbers
 *              double *pAverage: pointer to a variable that is filled 
 *                  in by this function with the average of d1, d2, d3, and d4
 *              double *pSum: pointer to a variable that is filled in by this
 *                  function with the sum of d1, d2, d3, and d4
 * Return Value: none
 * Description: This function takes fourfloating-point 
 *              numbers passed as doubles and calculates the average 
 *              and sum of the numbers.  Once calculated, the average 
 *              gets put into the variable pointed to by the pAverage 
 *              parameter and the sum gets put into the variable pointed 
 *              to by the pSum parameter.
 */

void generateStats4(double d1, double d2, double d3, double d4, double *pAverage, double *pSum)
{
    *pSum = d1 + d2 + d3 + d4;
    *pAverage = *pSum / 4;
}




/*
 * Function: generateStatsFromArray()
 * Parameters:  double values[]:floating-point numbers
 *              int numArray: number of array elements
 *              double *pSum: pointer to a variable that is filled in by this function with
 *                            the sum of all elements in the array
 *              double *pAverage: pointer to a variable that is filled 
 *                                in by this function with the average of all elements in the array
 * Return Value: none
 * Description: This function takes an array offloating-point(double) numbers, given the 
 *              number of elements in the array as a parameter,and calculates the average 
 *              and sum of the numbers. Once calculated, the average gets put into the variable 
 *              pointed to by the pAverage parameter and the sum gets put into the variable pointed 
 *              to by the pSum parameter.
 */

void generateStatsFromArray(double values[], int numArray, double *pSum, double *pAverage)
{
    int i = 0;
    *pSum = 0;
    *pAverage = 0;
    int divisor = numArray + 1;

    for (i = 0; i <= numArray; i++)
    {
        *pSum = *pSum + values[i];
    }

    *pAverage = *pSum / divisor;
}



/*
 * Function: fillArray()
 * Parameters:  double values[]:floating-point numbers
 *              int numArray: number of array elements
 *              double fillValue: value to put into array elements
 * Return Value: none
 * Description: This function takes an array offloating-point(double)numbers,
 *              given the number of elements in the array as a parameter,and 
 *              puts the fillValue into each element of the array.
 */

void fillArray(double values[], int numArray, double fillValue)
{
    int i = 0;

    for (i = 0; i <= numArray; i++)
    {
        values[i] = fillValue;
    }
}