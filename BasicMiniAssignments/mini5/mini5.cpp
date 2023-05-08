/*
 * Filename:    mini5.cpp
 * By:          Justin Fink
 * Date:		November 11, 2020
 * Description: This program utilizes 10 array elements
 *              in which you put any number you want into
 *              them, the program keeps track of what number 
 *              is the lowest and the index it is too
 *              it then outputs the number and what its index 
 *              is. It uses two functions for getting the users input
 *              and another for getting the minimum value and index.
 */




#include <stdio.h>
#pragma warning(disable: 4996)




int getNum(void);
int changeArray(int numbers[]);
int minArray(int numbers[]);




int main()
{
    int index = 0;
    int storedLowestValue = 0;
    const int TEN_NUMBERS = 10;

    int numbers[TEN_NUMBERS] = { 0 };

    changeArray(numbers);
    index = minArray(numbers);
    
    storedLowestValue = numbers[index];

    printf("\nThe lowest value of the ten numbers is %d and the index would be %d.", storedLowestValue, index);

    return 0;
}




/*
 * Function: changeArray()
 * Description: This function gets the users input and put that number they input into
 *              every spot of the array
 * Parameters: There is only one parameter and that is the array numbers.
 * Return Values: This function returns 0
 */
int changeArray(int numbers[])
{
    int i = 0;

    for (i = 0; i <= 9; i++)
    {
        printf("Write number here: ");
        numbers[i] = getNum();
    }

    return 0;
}




/*
 * Function: minArray()
 * Description: This function finds what the minimum number of the array is. 
 *              And then it also get the index and returns it to main.
 * Parameters: There is only one parameter and that is the array numbers.
 * Return Values: This function returns the index number of the lowest number
 */
int minArray(int numbers[])
{
    int i = 0;
    int index = 0;
    int storedLowestValue = 0;

    for (i = 0; i <= 9; i++)
    {
        numbers[i];

        if (i == 0)
        {
            storedLowestValue = numbers[0];
        }

        if (numbers[i] < storedLowestValue)
        {
            storedLowestValue = numbers[i];
            index = i;
        }

    }
    
    return index;
}




/*
 * Function: getNum()
 * Description: This function gets a number from the user.
 * Parameters: Void / Nothing
 * Return Values: It returns the value that has been input by the user,
 *                the variable 'number'.
 */
int getNum(void)
{
    char record[121] = { 0 };
    int number = 0;

    fgets(record, 121, stdin);

    if (sscanf(record, "%d", &number) != 1)
    {
        number = -1;
    }
    return number;
}
