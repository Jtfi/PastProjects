/*
 * Filename: mini7
 * By: Justin Fink
 * Date: December 04, 2020
 * Description: This program takes a command line argument
 *				of previous used flight times and the program
 *				parses the information into different sections 
 *				and it prints the 'city: hh hours and mm minutes'
 *				in that format rather than the one line command
 *				line argument.
 */





#include <stdio.h>
#include <string.h> 
#include <stdlib.h>
#pragma warning(disable: 4996)




int main(int argc, char *argv[])
{
	
	const char comma = ',';
	const int kStringsMaxValue = 40;
	char flightInfoString[kStringsMaxValue] = "";
	char cityName[kStringsMaxValue] = "";
	char hours[kStringsMaxValue] = "";
	char minutes[kStringsMaxValue] = "";
	char* commaPosition = NULL;
	int length = 0;
	int i = 0;
	int commaChecker = 0;
	
	//More than 1 command line argument ERROR
	if (argc != 2)
	{
		printf("Error: Must have exactly one command-line argument");
		return 0;
	}


	//getting the city name
	strcpy(flightInfoString,argv[1]);
	length = strlen(flightInfoString);

	//Comma Error
	commaPosition = strchr(flightInfoString, comma);
	if (commaPosition == NULL)
	{
		printf("Error: Comma missing");
		return 0;
	}

	flightInfoString[length - 5] = '\0';
	strcpy_s(cityName, flightInfoString);


	//getting the hours
	strcpy(flightInfoString, argv[1]);
	memmove(flightInfoString + 0, flightInfoString + (length - 4), 2);
	flightInfoString[2] = '\0';
	strcpy(hours, flightInfoString);


	//getting the minutes
	strcpy(flightInfoString, argv[1]);
	memmove(flightInfoString + 0, flightInfoString + (length - 2), 2);
	flightInfoString[2] = '\0';
	strcpy(minutes, flightInfoString);


	printf("%s: %s hours and %s minutes.", cityName, hours, minutes);

	return 0;
}