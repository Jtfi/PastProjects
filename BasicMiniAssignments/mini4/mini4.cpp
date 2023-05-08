/*
 * Filename:    mini4.cpp
 * By:          Justin Fink
 * Date:		October 23, 2020
 * Description: This program utilizes 10 array elements
 *              in which you put any number you want into
 *              them, the program keeps track of what number 
 *              is the lowest and the index it is too
 *              it then outputs the number and what its index 
 *              is. 
 */




#include <stdio.h>
#pragma warning(disable: 4996)




int getNum(void);





int main() 
{
    int i = 0;
    int storedLowestValue = 0;
    int index = 0;
    const int TEN_NUMBERS = 10;

    int numbers[TEN_NUMBERS] = { 0 };

    for (i = 0; i <= 9; i++)
    {
        
        
        printf("Write number here: ");
        numbers[i] = getNum();

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

    printf("\nThe lowest value of the ten numbers is %d and the index would be %d.", storedLowestValue, index);

    return 0;
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
