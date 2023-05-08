/*
 * Filename:    mini3.cpp
 * By:          Justin Fink
 * Date:		October 09, 2020
 * Description: This program gets a number from the user 
 *              and determines whether or not the number is even or odd
 *              it prints a statement on if it was even or odd
 */
#include <stdio.h>
#pragma warning(disable: 4996)

int getNum(void);
int isOdd(int userNumber);

int main()
{
    int usersNumber = 0;
    int result = 0;
    
    printf("Enter number here: ");
    usersNumber = getNum();
    result = isOdd(usersNumber);
    
    if (result == 0)
    {
        printf("The Number is Even!");
    }
    else
    {
        printf("The Number is Odd!");
    }

    return 0;
}

/*
 * Function: isOdd()
 * Description: This function will determine whether or not a number 
 *              is odd or even.
 * Parameters:  int userNumber: this is the value of the users 
 *              number that we got from the user in the main() function
 * Return Values: This function returns 0 if the number is even, and
 *                1 if the number is odd.
 */
int isOdd(int userNumber)
{
    userNumber %= 2;

    if (userNumber == 0)
    {
        return 0;
    }
    else
    {
        return 1;
    }
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